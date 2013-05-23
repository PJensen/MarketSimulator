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
        public MarketTickEventArgs(MarketData marketData)
            : base(marketData) { }

        /// <summary>
        /// Relative strength index
        /// </summary>
        public double RSI { get; set; }

        /// <summary>
        /// EMA13
        /// </summary>
        public double EMA13 { get; set; }

        /// <summary>
        /// MACDHistogram
        /// </summary>
        public double MACDHistogram { get; set; }
    }
}
