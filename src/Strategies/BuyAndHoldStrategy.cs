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
            var numSecurities = eventArgs.SecuritiesData.Keys.Count;

            if (!inMarket.ContainsKey(eventArgs.Symbol))
                inMarket.Add(eventArgs.Symbol, new BuyHoldTracking() { InMarket = false, Shares = 0, Symbol = eventArgs.Symbol });

            if (inMarket[eventArgs.Symbol].InMarket)
                return null;

            // buy as much as possible; distributing across the index of securities.
            var buyShares = (int)Math.Floor((eventArgs.StrategyInfo.Cash / numSecurities) / eventArgs.MarketData.Close);

            // if the first tick has already happened; 
            var retVal = (!inMarket[eventArgs.Symbol].InMarket) ? Buy(buyShares) : null;

            // we are in the market for this security
            inMarket[eventArgs.Symbol].InMarket = true;
            inMarket[eventArgs.Symbol].Shares = buyShares;

            return retVal;
        }

        /// <summary>
        /// SellSignal; the details of the sell signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public override SellEventArgs SellSignal(MarketTickEventArgs eventArgs)
        {
            if (!inMarket.ContainsKey(eventArgs.Symbol))
                return null;
            var shares = inMarket[eventArgs.Symbol].Shares;
            if (inMarket.Remove(eventArgs.Symbol))
                return Sell(shares);
            return null;
        }

        #endregion
    }
}
