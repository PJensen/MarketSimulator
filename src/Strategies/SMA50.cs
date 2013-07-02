using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Core.Indicators;
using MarketSimulator.Events;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// SMA50
    /// </summary>
    public class SMA50 : StrategyBase
    {
        /// <summary>
        /// SMA50
        /// </summary>
        public SMA50()
            : base("SMA50 Strategy")
        {
            AddTechnical("SMA50", new SMA(50));
        }

        /// <summary>
        /// BuySignal
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            double sma50 = ((SMA)GetTechnical("SMA50")).Value;
            if (Math.Abs(sma50 - 0) > 0.001)
            {
                if (eventArgs.MarketData.Close > sma50)
                {
                    var cashPerSecurity = eventArgs.StrategyInfo.Cash / eventArgs.SecuritiesData.Count;
                    var sharesToBuy = (int)(cashPerSecurity / eventArgs.MarketData.Close);
                    return new BuyEventArgs(eventArgs, sharesToBuy);
                }
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
            var sma50 = ((SMA)GetTechnical("SMA50")).Value;
            if (Math.Abs(sma50 - 0) > 0.001 && eventArgs.MarketData.Close < sma50)
            {
                return new SellEventArgs(eventArgs, eventArgs.StrategyInfo.PositionData.SecurityShares(eventArgs.Symbol));
            }
            return null;
        }
    }

    /// <summary>
    /// SMA50
    /// </summary>
    public class RandomStrategy2 : StrategyBase
    {
        /// <summary>
        /// SMA50
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
            if (R.Random.Next(0, 1000) <= 1)
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
            if (R.Random.Next(0, 1000) <= 1)
            {
                return Sell(1);
            }
            return null;
        }
    }

    /// <summary>
    /// SMA50
    /// </summary>
    public class BuyOnly : StrategyBase
    {
        /// <summary>
        /// SMA50
        /// </summary>
        public BuyOnly()
            : base("Buy Only") { }

        /// <summary>
        /// BuySignal
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            if (R.Random.Next(0, 10) <= 1)
            {
                return Buy(R.Random.Next(1, 10));
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
            return null;
        }
    }
}
