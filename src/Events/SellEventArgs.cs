using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Interfaces;

namespace MarketSimulator.Events
{
    /// <summary>
    /// BuyEventArgs
    /// </summary>
    public class SellEventArgs : TradeEventArgs, IPosition
    {
        /// <summary>
        /// Create a new SellEventArgs with the specified quantity
        /// </summary>
        /// <param name="marketData">The market data</param>
        /// <param name="Shares">The number of Shares to purchase</param>
        public SellEventArgs(IPosition position, MarketData marketData)
            : base(TradeType.Sell, position, marketData)
        { }

        /// <summary>
        /// BuyEventArgs
        /// </summary>
        /// <param name="marketTickEventArgs">the market tick event args</param>
        /// <param name="Shares">the number of Shares</param>
        public SellEventArgs(MarketTickEventArgs marketTickEventArgs, int shares)
            : base(TradeType.Sell, marketTickEventArgs, shares)
        { }
    }
}
