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

    /// <summary>
    /// Signals
    /// </summary>
    public static class Signals
    {
        /// <summary>
        /// SellWhenDoubled
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static SellEventArgs SellWhenNAVDoubledForSecurity(MarketTickEventArgs e)
        {
            var startingCash = e.StrategyInfo.ParentSandbox.StartingCash;
            var securityNAV = e.StrategyInfo.PositionData.SecurityValue(e.MarketData.Date, e.Symbol);
            var securityShares = e.StrategyInfo.PositionData.SecurityShares(e.MarketData.Date, e.Symbol);

            if (securityNAV > startingCash * 2)
            {
                return new SellEventArgs(e, securityShares);
            }

            return null;
        }
    }
}
