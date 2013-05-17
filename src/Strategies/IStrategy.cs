using MarketSimulator.Core;
namespace MarketSimulator.Strategies
{
    /// <summary>
    /// IStrategy
    /// </summary>
    interface IStrategy
    {
        /// <summary>
        /// The name of the strategy
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        BuySignal BuySignal { get; set; }

        /// <summary>
        /// 
        /// </summary>
        SellSignal SellSignal { get; set; }
    }
}
