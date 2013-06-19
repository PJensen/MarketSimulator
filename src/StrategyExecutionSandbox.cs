using MarketSimulator.Core;
using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    /// <summary>
    /// StrategyExecutionSandbox
    /// </summary>
    public class StrategyExecutionSandbox
    {
        /// <summary>
        /// Creates a new StrategyExecutionSandbox
        /// </summary>
        public StrategyExecutionSandbox(StrategyExecutor strategyExecutor, StrategyBase strategy)
        {
            Strategy = strategy;
            Balance = Cash = Properties.Settings.Default.StartingBalance;
        }

        /// <summary>
        /// The number of shares currently owned by this strategy
        /// </summary>
        public int Shares { get; set; }

        /// <summary>
        /// The remaining balance this strategy has in the bank
        /// </summary>
        public double Balance { get; set; }

        /// <summary>
        /// The market execution tick that this strategy is on
        /// </summary>
        public int Tick { get; set; }

        /// <summary>
        /// The number of trades that this strategy has made.
        /// </summary>
        public int NumberOfTrades { get; set; }

        /// <summary>
        /// The paper value for this execution sandbox.
        /// </summary>
        public double PaperValue
        {
            get { return StrategyExecutor.PXLast * Shares; }
        }

        /// <summary>
        /// The current cash position
        /// </summary>
        public double Cash { get; set; }

        /// <summary>
        /// The strategy executor for easy access to data at that seggrated level
        /// </summary>
        public StrategyExecutor StrategyExecutor { get; private set; }

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
        public List<MarketData> MarketData { get { return StrategyExecutor.MarketData; } }

        /// <summary>
        /// Determines if this execution strategy has made money -- yet.
        /// </summary>
        /// <returns><value>true</value> if the strategy has made money</returns>
        public bool MadeMoney()
        {
            // sum all purchases
            var marketValue = 0d;
            foreach (var e in ActiveTradeString.BuyLine)
                marketValue += e.MarketData.Close * e.Shares;

            // sum all sales.
            var saleValue = 0d;
            foreach (var e in ActiveTradeString.SellLine)
                saleValue += e.MarketData.Close * e.Shares;

            // compute remainder
            var remainder = Shares * StrategyExecutor.PXLast;

            // subtract remainder
            var mktTotal = (saleValue - marketValue) - remainder;

            return mktTotal > 0;
        }
    }
}
