using MarketSimulator.Events;
using System;
namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// ITradeEventSource
    /// </summary>
    interface IMarketSignalEventSource
    {
        /// <summary>
        /// BuySignal; the details of the buy signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        BuyEventArgs BuySignal(MarketTickEventArgs eventArgs);

        /// <summary>
        /// SellSignal; the details of the sell signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        SellEventArgs SellSignal(MarketTickEventArgs eventArgs);       
    }
}
