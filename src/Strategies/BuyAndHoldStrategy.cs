using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Events;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// BuyAndHoldStrategy
    /// </summary>
    public class BuyAndHoldStrategy : StrategyBase
    {
        /// <summary>
        /// BuyAndHoldStrategy
        /// </summary>
        public BuyAndHoldStrategy()
            : base("Buy And Hold")

        {
            Description =
                "Buy and hold is a long-term investment strategy based on the " +
                "view that in the long run financial markets give a good rate of " +
                "return despite periods of volatility or decline. This viewpoint " +
                "also holds that short-term market timing, i.e. the concept that " +
                "one can enter the market on the lows and sell on the highs, does " +
                "not work; attempting timing gives negative results, at least for " +
                "small or unsophisticated investors, so it is better for them to " +
                "simply buy and hold.";
        }

        #region Overrides of StrategyBase

        /// <summary>
        /// BuySignal; the details of the buy signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public override BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// SellSignal; the details of the sell signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public override SellEventArgs SellSignal(MarketTickEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
