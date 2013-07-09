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
    /// SMA50Strategy
    /// </summary>
    public class SMA50Strategy : StrategyBase
    {
        /// <summary>
        /// SMA50Strategy
        /// </summary>
        public SMA50Strategy()
            : base("SMA50Strategy Strategy")
        {
            AddTechnical<SMA50>();
        }

        /// <summary>
        /// BuySignal
        /// </summary>
        /// <param name="eventArgs"></param>
        /// <returns></returns>
        public override Events.BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            var sma50 = GetTechnicalValue<SMA50, double>();

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
            var sma50 = GetTechnicalValue<SMA50, double>();

            if (Math.Abs(sma50 - 0) > 0.001 && eventArgs.MarketData.Close < sma50)
            {
                return new SellEventArgs(eventArgs, eventArgs.StrategyInfo.PositionData.SecurityShares(eventArgs.MarketData.Date, eventArgs.Symbol));
            }

            return null;
        }
    }
}
