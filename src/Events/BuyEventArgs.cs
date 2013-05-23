using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;

namespace MarketSimulator.Events
{
    /// <summary>
    /// BuyEventArgs
    /// </summary>
    public class BuyEventArgs : TradeEventArgs
    {
        /// <summary>
        /// Create a new <see cref="BuyEventArgs"/> with the specified quantity
        /// </summary>
        /// <param name="marketData">The market data</param>
        /// <param name="shares">The number of shares to purchase</param>
        /// <param name="limit">The limit level</param>
        /// <param name="stop">The stop level</param>
        public BuyEventArgs(MarketData marketData, int shares, double stop = 0d, double limit = 0d, TradeFlags flags = Core.TradeFlags.Market)
            : base(TradeType.Buy, marketData, shares, stop, limit, flags) { }
    }
}
