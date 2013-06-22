using MarketSimulator.Core;
using MarketSimulator.Events;
using MarketSimulator.Interfaces;
using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// StrategyExecutionSandbox
    /// </summary>
    public class StrategyExecutionSandbox : IStrategyEventReciever, IComparable<StrategyExecutionSandbox>
    {
        /// <summary>
        /// Creates a new StrategyExecutionSandbox
        /// </summary>
        public StrategyExecutionSandbox(IStrategyExecutor strategyExecutor, StrategyBase strategy)
        {
            Strategy = strategy;
            Cash = Properties.Settings.Default.StartingBalance;
            CashHistory = new List<double>();
            ActiveTradeString = new TradeString();
            GeneralLedger = new GeneralLedger();

            CashHistory.Add(Cash);
            GeneralLedger.Add(CashSymbol, Cash);

            // outer event wiring
            strategy.SellEvent += OnSellEvent;
            strategy.BuyEvent += OnBuyEvent;
        }

        #region Constants

        /// <summary>
        /// CashSymbol
        /// </summary>
        private const string CashSymbol = "Cash";

        #endregion

        #region Public Facing Methods

        /// <summary>
        /// OnBuyEvent
        /// </summary>
        /// <param name="eventArgs">eventArgs</param>
        public void OnBuyEvent(object sender, BuyEventArgs eventArgs)
        {
            var totalValue = eventArgs.Shares * eventArgs.SecuritiesData.Close;

            if (totalValue >= Cash)
            {
                eventArgs.Cancel = true;

                return;
            }

            Cash -= totalValue;
            Shares += eventArgs.Shares;
            NumberOfTrades++;

            CashHistory.Add(Cash);
            ActiveTradeString.BuyLine.Add(eventArgs);
        }

        /// <summary>
        /// OnSellEvent
        /// </summary>
        /// <param name="eventArgs">eventArgs</param>
        public void OnSellEvent(object sender, SellEventArgs eventArgs)
        {
            if (eventArgs.Shares <= 0)
            {
                eventArgs.Cancel = true;
                return;
            }

            if (eventArgs.Shares > Shares)
                eventArgs.Shares = Shares;

            Shares -= eventArgs.Shares;
            Cash += eventArgs.Shares * eventArgs.SecuritiesData.Close;
            NumberOfTrades++;

            CashHistory.Add(Cash);
            ActiveTradeString.SellLine.Add(eventArgs);
        }

        /// <summary>
        /// Determines if this execution strategy has made money -- yet.
        /// </summary>
        /// <returns><value>true</value> if the strategy has made money</returns>
        public bool MadeMoney()
        {
            // sum all purchases
            var marketValue = 0d;
            foreach (var e in ActiveTradeString.BuyLine)
                marketValue += e.SecuritiesData.Close * e.Shares;

            // sum all sales.
            var saleValue = 0d;
            foreach (var e in ActiveTradeString.SellLine)
                saleValue += e.SecuritiesData.Close * e.Shares;

            // compute remainder
            // TODO: Fix this shit
            //var remainder = Shares * StrategyExecutor.PXLast;

            // subtract remainder
            var mktTotal = (saleValue - marketValue); // -remainder;

            return mktTotal > 0;
        }

        #endregion

        #region Public Facing Properties

        /// <summary>
        /// The number of shares currently owned by this strategy
        /// </summary>
        public int Shares { get; set; }

        /// <summary>
        /// The market execution tick that this strategy is on
        /// </summary>
        public int Tick { get; set; }

        /// <summary>
        /// The number of trades that this strategy has made.
        /// </summary>
        public int NumberOfTrades { get; set; }

        /// <summary>
        /// The current cash position
        /// </summary>
        public double Cash
        {
            get { return GeneralLedger[CashSymbol]; }
            set { GeneralLedger[CashSymbol] = value; }
        }

        /// <summary>
        /// The strategy executor for easy access to data at that seggrated level
        /// </summary>
        public IStrategyExecutor StrategyExecutor { get; private set; }

        /// <summary>
        /// The trading strategy that is being executed in this sandbox.
        /// </summary>
        public StrategyBase Strategy { get; private set; }

        /// <summary>
        /// ActiveTradeString
        /// </summary>
        public TradeString ActiveTradeString { get; set; }

        /// <summary>
        /// Reference to the StrategyExecutor's MarketData
        /// </summary>
        //public List<SecuritiesData> MarketData { get { StrategyExecutor.SecurityMaster; } }

        /// <summary>
        /// Portfolio
        /// </summary>
        public GeneralLedger GeneralLedger { get; set; }

        /// <summary>
        /// BuyTally
        /// </summary>
        public List<double> CashHistory { get; set; }

        #endregion

        /// <summary>
        /// The name of the strategy
        /// </summary>
        public string Name 
        {
            get { return Strategy.Name; }
            set { Strategy.Name = value; }
        }

        /// <summary>
        /// CompareTo
        /// </summary>
        /// <param name="other">The other execution sandbox to compare this one to</param>
        /// <returns>less than 1, greater than 1, or 0</returns>
        public int CompareTo(StrategyExecutionSandbox other)
        {
            return Cash.CompareTo(other.Cash);
        }
    }
}
