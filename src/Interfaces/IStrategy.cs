using MarketSimulator.Core;
using MarketSimulator.Events;

namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// IStrategy
    /// </summary>
    interface IStrategy : ITradeEventListener, IMarketSignalEventSource
    {
        /// <summary>
        /// The name of the strategy
        /// </summary>
        string Name { get; set; }      
    }
}
