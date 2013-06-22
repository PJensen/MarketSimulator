using MarketSimulator.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// ITradeEventListener
    /// </summary>
    interface ITradeEventListener
    {
        /// <summary>
        /// BuyEvent
        /// </summary>
        event EventHandler<BuyEventArgs> BuyEvent;

        /// <summary>
        /// SellEvent
        /// </summary>
        event EventHandler<SellEventArgs> SellEvent;
    }
}
