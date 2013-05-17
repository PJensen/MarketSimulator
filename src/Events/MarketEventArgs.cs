using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;

namespace MarketSimulator.Events
{
    /// <summary>
    /// MarketEventArgs
    /// </summary>
    public class MarketEventArgs : EventArgs
    {
        /// <summary>
        /// MarketEventArgs
        /// </summary>
        public MarketEventArgs(MarketData marketData)
        {
            MarketData = marketData;
        }

        /// <summary>
        /// MarketData
        /// </summary>
        public MarketData MarketData { get; protected set; }

        /// <summary>
        /// Cancel
        /// </summary>
        public bool Cancel { get; set; }
    }
}
