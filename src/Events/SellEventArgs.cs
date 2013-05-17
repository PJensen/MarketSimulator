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
    public class SellEventArgs : MarketEventArgs
    {
        /// <summary>
        /// Createa  new BuyEventArgs with the specified quantity
        /// </summary>
        /// <param name="marketData">The market data</param>
        /// <param name="shares">The number of shares to purchase</param>
        public SellEventArgs(MarketData marketData, int shares)
            : base(marketData)
        {
            Shares = shares;
        }

        /// <summary>
        /// Shares
        /// </summary>
        public int Shares { get; set; }
    }
}
