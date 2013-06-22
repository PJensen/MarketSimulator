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
    }
}
