using MarketSimulator.Core;
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
        /// Add a strategy to the strategy executor
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
            foreach (var security in GlobalExecutionSettings.GetUserDefaults().SecurityMaster)
            {
                var tmpMarketData = R.Convert(new YahooDataRetriever().Retrieve(security, out message, out fail));
                tmpMarketData.Reverse();

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
        /// MarketData for individual ticks on a per security basis.
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
            else if (Sandboxes == null || Sandboxes.Count <= 0)
            {
                throw new MarketSimulatorException("Market simulator sandboxes was null or empty.");
            }
            else if (SecurityMaster == null || SecurityMaster.Count <= 0)
            {
                throw new MarketSimulatorException("MarketData was null or empty!");
            }

            // This is important because not all securities have the same amount of data.
            // AAAAA   X
            // BB      X
            // CCCCCCCCX
            // Thus we use an accumulator to get to (X); but; obviously events simply 
            // stop firing if there is no data - to save time we just continue.
            var maximumPossibleTicks = SecurityMaster.Values.Max(l => l.Count);
            var currentMarketTick = 0;

            while (currentMarketTick < maximumPossibleTicks)
            {
                foreach (var strategySandbox in Sandboxes)
                {
                    SecuritiesSnap securitiesSnap = new SecuritiesSnap();

                    foreach (var securitySymbol in SecurityMaster.Keys)
                    {
                        if (!SecurityMaster.CanSecurityTickPast(securitySymbol, currentMarketTick))
                        {
                            continue;
                        }

                        var marketData = SecurityMaster[securitySymbol][currentMarketTick];

                        strategySandbox.Strategy.MarketTick(this, new MarketTickEventArgs(securitySymbol,
                            marketData, securitiesSnap));

                        if (marketSimulatorWorker.WorkerReportsProgress)
                        {
                            marketSimulatorWorker.ReportProgress((int)((currentMarketTick * 1.0 / maximumPossibleTicks) * 100.00));
                        }

                        if (marketSimulatorWorker.WorkerSupportsCancellation && marketSimulatorWorker.CancellationPending)
                        {
                            marketSimulatorWorker.CancelAsync();
                        }

                        currentMarketTick++;
                    }
                }
            }

            #endregion
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
