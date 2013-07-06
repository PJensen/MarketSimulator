using MarketSimulator.Core;
using MarketSimulator.Events;
using MarketSimulator.Exceptions;
using MarketSimulator.Interfaces;
using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace MarketSimulator.Components
{
    /// <summary>
    /// MarketSimulatorComponent
    /// </summary>
    public partial class MarketSimulatorComponent : Component, IStrategyExecutor
    {
        /// <summary>
        /// Creates a new MarketSimulatorComponent
        /// </summary>
        public MarketSimulatorComponent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates a new MarketSimulatorComponent given a container
        /// </summary>
        /// <param name="container">the container to wire up to</param>
        public MarketSimulatorComponent(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        /// <summary>
        /// AddPosition a strategy to the strategy executor
        /// </summary>
        /// <param name="strategy">the strategy to add</param>
        public bool AddStrategy(StrategyBase strategy)
        {
            if (Sandboxes == null)
            {
                Sandboxes = new List<StrategyExecutionSandbox>();
            }

            // sanity check to make sure another strategy with the same name is not already there.
            if (Sandboxes.FirstOrDefault(s => s.Strategy.Name == strategy.Name) != null)
                return false;

            Sandboxes.Add(new StrategyExecutionSandbox(this, strategy));

            return true;
        }

        /// <summary>
        /// ClearStrategies
        /// </summary>
        public void ClearStrategies()
        {
            if (Sandboxes == null)
                return;

            Sandboxes.Clear();
        }

        /// <summary>
        /// LoadMarketData
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        public bool LoadMarketData(Action<string> failureAction)
        {
            bool methodFail = false;
            bool fail = false;
            string message;

            if (SecurityMaster == null)
            {
                SecurityMaster = new GlobalSecuritiesData(R.EstimatedTicks);
            }

            SecurityMaster.Clear();

            // iterate over each security and populate the individual ticks within the security master; 
            // WARNING: individual failures will trigger the failure callback; and, return FALSE.
            foreach (var security in GlobalExecutionSettings.Instance.SecurityMaster)
            {
                var tmpMarketData = R.Convert(security, new YahooDataRetriever().Retrieve(security, out message, out fail));

                if (fail)
                {
                    if (failureAction == null)
                    {
                        throw new ArgumentException("failureAction should be registered");
                    }

                    failureAction(message);

                    methodFail = true;
                }
                else
                {
                    SecurityMaster.Add(security, tmpMarketData);
                }
            }

            return methodFail;
        }

        /// <summary>
        /// Tick; the same for all strategies being executed
        /// </summary>
        public int Tick { get; private set; }

        /// <summary>
        /// CurrentProgress
        /// </summary>
        public int CurrentProgress { get; private set; }

        /// <summary>
        /// The common backing store for all market data; for all securities; for all ticks.
        /// Refer to SecuritiesData for individual ticks across all constituents; and, 
        /// MarketTicks for individual ticks on a per security basis.
        /// </summary>
        public GlobalSecuritiesData SecurityMaster { get; set; }

        /// <summary>
        /// Sandboxes
        /// </summary>
        public List<StrategyExecutionSandbox> Sandboxes { get; set; }

        /// <summary>
        /// Has the market simulator component been initialized with a ticker
        /// </summary>
        public bool Initialized { get; private set; }

        /// <summary>
        /// WorkComplete
        /// </summary>
        public bool WorkComplete { get; private set; }

        /// <summary>
        /// Initializes the market simulator with a given ticker
        /// </summary>
        /// <param name="ticker">the ticker to initialize mkt sim with</param>
        /// <param name="message">the result (string) of the initialization</param>
        /// <returns></returns>
        public bool Initialize(Action<string> failureAction = null)
        {
            // TODO: Fix this crap; not a huge fan of this 2-tier, out-paramed method;
            // really a result of the re-write; it was cool before; that's changed since
            // we moved to multiple securities.

            Initialized = !LoadMarketData(failureAction);

            //message = Initialized ? "OK" : "Failed loading market data.";

            return Initialized;
        }

        /// <summary>
        /// TickDates
        /// </summary>
        public List<DateTime> TickDates { get; set; }

        #region Worker

        /// <summary>
        /// marketSimulatorWorker_DoWork
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void marketSimulatorWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            WorkComplete = false;

            #region Init and Sanity Checking

            if (!Initialized)
            {
                throw new MarketSimulatorException("The market simulator component has not been initialized.");
            }
            if (Sandboxes == null || Sandboxes.Count <= 0)
            {
                throw new MarketSimulatorException("Market simulator sandboxes was null or empty.");
            }
            if (SecurityMaster == null || SecurityMaster.Count <= 0)
            {
                throw new MarketSimulatorException("MarketTicks was null or empty!");
            }

            TickDates = new List<DateTime>();

            #endregion

            // This is important because not all securities have the same amount of data.
            // AAAAA   X
            // BB      X
            // CCCCCCCCX
            // Thus we use an accumulator to get to (X); but; obviously events simply 
            // stop firing if there is no data - to save time we just continue.
            var maximumPossibleTicks = SecurityMaster.Values.Max(l => l.Count - 1);
            var currentMarketDate = GlobalExecutionSettings.Instance.StartDate;
            var currentMarketTick = 0;

            foreach (var sandbox in Sandboxes)
            {
                var startDate = SecurityMaster.MinimumDate;
                var endDate = SecurityMaster.MaximumDate;
                var currentDate = startDate;

                sandbox.Initialize();

                while (currentDate < endDate)
                {
                    var securitySnap = SecurityMaster[currentDate];
                    var nextDate = currentDate;

                    foreach (var security in SecurityMaster.Keys)
                    {
                        var tmpMarketData = SecurityMaster[security, currentDate];

                        if (tmpMarketData == null)
                        {
                            continue;
                        }

                        // TODO: find a more natural place for StrategyTickHistory.Add(..)
                        sandbox.StrategyTickHistory.Add(
                            sandbox.Strategy.MarketTick(this,
                                new MarketTickEventArgs(new StrategySnapshot(sandbox),
                                    security, tmpMarketData, securitySnap)));

                        if (nextDate == currentDate && tmpMarketData.HasNext)
                        {
                            nextDate = tmpMarketData.Next.Date;
                        }

                        sandbox.PositionData.UpdatePrice(security, tmpMarketData.Close);
                    }

                    ReportProgress((currentMarketTick / maximumPossibleTicks) * 100, currentDate);

                    if (CheckCancelled())
                    {
                        return;
                    }

                    if (currentDate != nextDate && nextDate > currentDate)
                    {
                        currentDate = nextDate;
                    }
                    else
                    {
                        currentDate = currentDate.AddDays(1);
                    }

                    TickDates.Add(currentDate);
                    sandbox.SnapshotSandbox(currentDate);
                    currentMarketTick++;
                }

                if (StrategyFinishedRunningEvent != null)
                {
                    StrategyFinishedRunningEvent(this, new StrategyEventArgs(sandbox.Strategy));
                }
            }
        }

        /// <summary>
        /// CheckCancelled
        /// </summary>
        /// <returns>true if cancelled</returns>
        private bool CheckCancelled()
        {
            if (marketSimulatorWorker.WorkerSupportsCancellation && marketSimulatorWorker.CancellationPending)
            {
                marketSimulatorWorker.CancelAsync();

                return true;
            }

            return false;
        }

        /// <summary>
        /// StrategyFinishedRunningEvent
        /// </summary>
        public event EventHandler<StrategyEventArgs> StrategyFinishedRunningEvent;

        /// <summary>
        /// ReportProgress
        /// </summary>
        /// <param name="percent">percent</param>
        /// <param name="state">state</param>
        private void ReportProgress(double percent, object state)
        {
            percent = Math.Round(percent, 0);

            if (percent > 100)
            {
                throw new MarketSimulatorException("percent exceeded 100");
            }

            if (marketSimulatorWorker.WorkerReportsProgress)
            {
                marketSimulatorWorker.ReportProgress((int)percent, state);
            }
        }

        /// <summary>
        /// marketSimulatorWorker_ProgressChanged
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void marketSimulatorWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        /// <summary>
        /// marketSimulatorWorker_RunWorkerCompleted
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void marketSimulatorWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WorkComplete = true;
        }

        #endregion
    }
}
