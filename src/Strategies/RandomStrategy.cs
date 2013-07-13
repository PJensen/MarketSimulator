using MarketSimulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// SMA50Strategy
    /// </summary>
    public class RandomStrategy2 : StrategyBase
    {
        /// <summary>
        /// SMA50Strategy
        /// </summary>
        public RandomStrategy2()
            : base("Random Strategy 2") { }

        /// <summary>
        /// BuySignal
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            if (R.Random.Next(0, 100) <= 1)
            {
                return Buy(10);
            }
            return null;
        }

        /// <summary>
        /// SellSignal
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public override Events.SellEventArgs SellSignal(MarketTickEventArgs eventArgs)
        {
            if (R.Random.Next(0, 100) <= 1)
            {
                return Sell(10);
            }
            return null;
        }
    }
}
