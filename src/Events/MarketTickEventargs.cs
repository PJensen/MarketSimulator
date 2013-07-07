using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Events;

namespace MarketSimulator
{
    /// <summary>
    /// MarketTickEventArgs
    /// </summary>
    public class MarketTickEventArgs : MarketEventArgs
    {
        /// <summary>
        /// Create a new instance of <see cref="MarketTickEventArgs"/>
        /// </summary>
        /// <param name="marketData">The market data</param>
        /// <param name="sandbox"> </param>
        public MarketTickEventArgs(StrategySnapshot sandbox, string symbol, MarketData marketData, SecuritiesSnap securitiesData)
            : base(securitiesData)
        {
            MarketData = marketData; 
            StrategyInfo = sandbox;
        }

        /// <summary>
        /// Sandbox
        /// </summary>
        public StrategySnapshot StrategyInfo { get; set; }

        /// <summary>
        /// The Symbol this market tick referrs to
        /// </summary>
        public string Symbol { get { return MarketData.Symbol; } }

        /// <summary>
        /// MarketTicks
        /// </summary>
        public MarketData MarketData { get; private set; }
    }
}
