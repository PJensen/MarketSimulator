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
            if (strategyExecutor == null)
            {
                throw new ArgumentNullException("strategyExecutor");
            }
            else if (strategy == null)
            {
                throw new ArgumentNullException("strategy");
            }

            Initialize();
            StrategyExecutor = strategyExecutor;
            Strategy = strategy;

            // outer event wiring
            strategy.SellEvent += OnSellEvent;
            strategy.BuyEvent += OnBuyEvent;
            strategy.MarketTickEvent += OnMarketTickEvent;
        }

        /// <summary>
        /// Clears the execution sandbox for a possible re-run
        /// </summary>
        public void Initialize()
        {
            MarketTicks = new List<MarketTickEventArgs>();
            ActiveTradeStrings = new TradeStringCollection();
            CashHistory = new List<double>();
            //GeneralLedger = new PositionSnap();
            Cash = GlobalExecutionSettings.Instance.StartingBalance;
            Tick = 0;
        }

        #region Constants

        /// <summary>
        /// CashSymbol
        /// </summary>
        private const string CashSymbol = "Cash";

        #endregion

        #region Public Facing Methods

        /// <summary>
        /// MarketTicks
        /// </summary>
        public List<MarketTickEventArgs> MarketTicks { get; set; }

        /// <summary>
        /// OnMarketTickEvent
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="eventArgs">event args</param>
        public void OnMarketTickEvent(object sender, MarketTickEventArgs eventArgs)
        {
            MarketTicks.Add(eventArgs);
            CashHistory.Add(Cash);
            Tick++;
        }

        /// <summary>
        /// OnBuyEvent
        /// </summary>
        /// <param name="eventArgs">eventArgs</param>
        public void OnBuyEvent(object sender, BuyEventArgs eventArgs)
        {
            var totalValue = eventArgs.Shares * eventArgs.MarketData.Close;

            if (totalValue >= Cash)
            {
                eventArgs.Cancel = true;
                return;
            }

            Cash -= totalValue;

           // GeneralLedger.AddPosition(eventArgs);
            Shares += eventArgs.Shares;

            NumberOfTrades++;
            ActiveTradeStrings[eventArgs.Symbol].BuyLine.Add(eventArgs);
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
            {
                // re-set shares to keep strategy from selling more shares than it has
                eventArgs.Shares = Shares;
            }

            Cash += eventArgs.Price * eventArgs.Shares;

            // GeneralLedger.Add(eventArgs);
            Shares -= eventArgs.Shares;

            NumberOfTrades++;
            ActiveTradeStrings[eventArgs.Symbol].SellLine.Add(eventArgs);
        }

        /// <summary>
        /// Determines if this execution strategy has made money -- yet.
        /// </summary>
        /// <returns><value>true</value> if the strategy has made money</returns>
        public bool MadeMoney(string symbol)
        {
            var activeTradeString = ActiveTradeStrings[symbol];

            var mktTotal = (activeTradeString.TotalSoldMV - activeTradeString.TotalPurchasedMV)
                - (activeTradeString.LastPurchasedPrice * activeTradeString.TotalSharesAfterExecution);

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
        public double Cash { get; set; }

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
        public TradeStringCollection ActiveTradeStrings { get; set; }

        /// <summary>
        /// GetMarketData returns the market data for the security at the specified tick
        /// </summary>
        /// <param name="security">the security</param>
        /// <param name="tick">the tick</param>
        /// <returns>the market data associated with the tick & security</returns>
        public MarketData GetMarketData(string security, int tick)
        {
            return StrategyExecutor.SecurityMaster[security, tick];
        }

        /// <summary>
        /// return the entire securities snap at the specified interval
        /// </summary>
        /// <param name="security">the security to get current market data for</param>
        /// <returns></returns>
        public MarketData GetCurrentMarketData(string security)
        {
            // NOTE: Here were referring to the Tick property
            return GetMarketData(security, Tick);
        }

        /// <summary>
        /// Reference to the StrategyExecutor's MarketTicks
        /// </summary>
        // public List<SecuritiesSnap> MarketTicks { get { StrategyExecutor.Tick; } }

        /// <summary>
        /// Portfolio
        /// </summary>
        //public PositionSnap GeneralLedger { get; private set; }

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
