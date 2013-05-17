using MarketSimulator.Events;

namespace MarketSimulator.Core
{
    /// <summary>
    /// BuySignal
    /// </summary>
    /// <param name="eventArgs">event arguments</param>
    /// <returns></returns>
    public delegate BuyEventArgs BuySignal(MarketTickEventArgs eventArgs);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventArgs">event arguments</param>
    /// <returns></returns>
    public delegate SellEventArgs SellSignal(MarketTickEventArgs eventArgs);
}
