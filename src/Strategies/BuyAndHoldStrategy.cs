using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Events;
using MarketSimulator.Core;

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
        /// AAPL:false
        /// EMC:true
        /// etc ...
        /// </summary>
        private Dictionary<string, BuyHoldTracking> inMarket = new Dictionary<string, BuyHoldTracking>();

        /// <summary>
        /// BuyHoldTracking
        /// </summary>
        class BuyHoldTracking
        {
            public string Symbol { get; set; }
            public bool InMarket { get; set; }
            public int Shares { get; set; }
        }

        /// <summary>
        /// BuySignal; the details of the buy signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public override BuyEventArgs BuySignal(MarketTickEventArgs eventArgs)
        {
            if (skipFirst)
            {
                skipFirst = false;
                return null;
            }

            var numSecurities = eventArgs.SecuritiesData.Keys.Count;
            var security = eventArgs.Symbol;

            if (numSecurities <= 0)
                return null;

            var numShares = (int)Math.Floor((eventArgs.StrategyInfo.Cash / numSecurities) / eventArgs.MarketData.Close);
            var retVal = numShares > 0 ? new BuyEventArgs(eventArgs, numShares) : null;

            if (!inMarket.ContainsKey(security))
            {
                inMarket.Add(security, new BuyHoldTracking()
                {
                    Shares = numShares,
                    Symbol = security,
                    InMarket = true,
                });
            }
            else { retVal = null; }

            return retVal;
        }

        /// <summary>
        /// skipFirst
        /// </summary>
        bool skipFirst = true;

        /// <summary>
        /// SellSignal; the details of the sell signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public override SellEventArgs SellSignal(MarketTickEventArgs eventArgs)
        {
            if (eventArgs.MarketData.HasNext && eventArgs.MarketData.Next.HasNext)
            {
                return null;
            }
            else
            {
                if (inMarket.ContainsKey(eventArgs.Symbol) && inMarket[eventArgs.Symbol].InMarket)
                {
                    return Sell(inMarket[eventArgs.Symbol].Shares);
                }
            }
            return null;

        }

        #endregion
    }
}
