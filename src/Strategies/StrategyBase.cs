using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Core;
using MarketSimulator.Events;
using MarketSimulator.Interfaces;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// StrategyBase
    /// </summary>
    public abstract class StrategyBase : IStrategy
    {
        /// <summary>
        /// Creates a new StrategyBase
        /// </summary>
        /// <param name="name">The name of the strategy</param>
        /// <param name="buySignal">The buy signal</param>
        /// <param name="sellSignal">The sell signal</param>
        public StrategyBase(string name)
        {
            Name = name;
        }

        /// <summary>
        /// BuySignal; the details of the buy signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public abstract BuyEventArgs BuySignal(MarketTickEventArgs eventArgs);

        /// <summary>
        /// SellSignal; the details of the sell signal are filled in by the concrete
        /// implementations.
        /// </summary>
        /// <param name="eventArgs">incoming market tick event arguments</param>
        /// <returns><c>possibly</c> a buy event; may be null to do hold or do nothing</returns>
        public abstract SellEventArgs SellSignal(MarketTickEventArgs eventArgs);

        /// <summary>
        /// MarketTick
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">market Tick event arguments</param>
        public virtual void MarketTick(object sender, MarketTickEventArgs e)
        {
            // TODO: Determine if strategies should be prevented from buying AND selling in the same tick.
            // if not; determine precedence; for now it's sell first (for liquidity) and purchase 2nd.
            currentMarketTick = e;
            OnSellEvent(SellSignal(e));
            OnBuyEvent(BuySignal(e));
        }

        /// <summary>
        /// Buy some shares based on the last tick
        /// </summary>
        /// <param name="shares"></param>
        /// <returns></returns>
        protected BuyEventArgs Buy(int shares)
        {
            if (currentMarketTick == null)
            {
                throw new NullReferenceException("Expected internal state to exist after MarketTick");
            }

            return new BuyEventArgs(currentMarketTick, shares);
        }

        /// <summary>
        /// Sell some shares based on the last tick
        /// </summary>
        /// <param name="shares"></param>
        /// <returns></returns>
        protected SellEventArgs Sell(int shares)
        {
            if (currentMarketTick == null)
            {
                throw new NullReferenceException("Expected internal state to exist after MarketTick");
            }

            return new SellEventArgs(currentMarketTick, shares);
        }

        /// <summary>
        /// currentMarketTickState
        /// </summary>
        private MarketTickEventArgs currentMarketTick;

        /// <summary>
        /// Name of the trading strategy
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Buy Event Invocator
        /// </summary>
        /// <param name="e">The buy event arguments</param>
        protected void OnBuyEvent(BuyEventArgs e)
        {
            if (BuyEvent != null && e != null)
            {
                BuyEvent(this, e);
            }
        }

        /// <summary>
        /// Sell Event Invocator
        /// </summary>
        /// <param name="e">The sell event argument</param>
        protected void OnSellEvent(SellEventArgs e)
        {
            if (SellEvent != null && e != null)
            {
                SellEvent(this, e);
            }
        }

        /// <summary>
        /// BuyEvent
        /// </summary>
        public event EventHandler<BuyEventArgs> BuyEvent;

        /// <summary>
        /// SellEvent
        /// </summary>
        public event EventHandler<SellEventArgs> SellEvent;

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>string representation of this strategy</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
