using MarketSimulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Events
{
    /// <summary>
    /// TradeEventArgs
    /// </summary>
    public class TradeEventArgs : MarketEventArgs
    {
        /// <summary>
        /// Creates a new TradeEventArgs
        /// </summary>
        /// <param name="tradeType">the trade type</param>
        /// <param name="marketData">market data</param>
        /// <param name="shares">shares quantity</param>
        public TradeEventArgs(TradeType tradeType, MarketData marketData, int shares, 
            double stop = 0d, double limit = 0d, TradeFlags flags = Core.TradeFlags.Market)
            : base(marketData)
        {
            TradeType = tradeType;
            Shares = shares;
            Cancel = default(bool);
            MarketData = marketData;
        }

        /// <summary>
        /// TradeType
        /// </summary>
        public TradeType TradeType { get; private set; }

        /// <summary>
        /// TradeFlags
        /// </summary>
        public TradeFlags TradeFlags { get; private set; }

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares { get; set; }

        /// <summary>
        /// Stop
        /// </summary>
        public double Stop { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        public double Limit { get; set; }

        /// <summary>
        /// Cancel
        /// </summary>
        public bool Cancel { get; set; }
    }
}
