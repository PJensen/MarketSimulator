using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// RandomStrategy
    /// </summary>
    public class RandomStrategy : StrategyBase
    {
        /// <summary>
        /// RandomStrategy
        /// </summary>
        public RandomStrategy()
            : base("Random Strategy") { }

        /// <summary>
        /// BuySignal
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            if (R.Random.Next(0, 100) <= 1)
            {
                return Buy(1);
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
                return Sell(1);
            }
            return null;
        }
    }

    /// <summary>
    /// RandomStrategy
    /// </summary>
    public class RandomStrategy2 : StrategyBase
    {
        /// <summary>
        /// RandomStrategy
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
                return Buy(1);
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
                return Sell(1);
            }
            return null;
        }
    }
}
