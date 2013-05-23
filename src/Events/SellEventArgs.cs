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
    public class SellEventArgs : TradeEventArgs
    {
        /// <summary>
        /// Create a new SellEventArgs with the specified quantity
        /// </summary>
        /// <param name="marketData">The market data</param>
        /// <param name="shares">The number of shares to purchase</param>
        public SellEventArgs(MarketData marketData, int shares, double stop = 0d, double limit = 0d, TradeFlags flags = Core.TradeFlags.Market)
            : base(TradeType.Sell, marketData, shares, stop, limit, flags) { }
    }
}
