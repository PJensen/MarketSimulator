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
    public class BuyEventArgs : TradeEventArgs
    {
        /// <summary>
        /// BuyEventArgs
        /// </summary>
        /// <param name="position">the position to take</param>
        /// <param name="marketData">the market data snap</param>
        public BuyEventArgs(IPosition position, MarketData marketData)
            : base(TradeType.Buy, position, marketData)
        { }

        /// <summary>
        /// BuyEventArgs
        /// </summary>
        /// <param name="marketTickEventArgs">the market tick event args</param>
        /// <param name="shares">the number of shares</param>
        public BuyEventArgs(MarketTickEventArgs marketTickEventArgs, int shares)
            : base(TradeType.Buy, marketTickEventArgs, shares)
        { }
    }
}
