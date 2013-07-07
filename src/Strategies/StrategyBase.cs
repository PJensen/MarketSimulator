using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Core;
using MarketSimulator.Core.Indicators;
using MarketSimulator.Events;
using MarketSimulator.Interfaces;
using System.Diagnostics;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// StrategyBase
    /// </summary>
    [DebuggerDisplay("{Name},{Description}")]
    public abstract class StrategyBase : IStrategy
    {
        /// <summary>
        /// StrategyBase
        /// </summary>
        protected StrategyBase()
        {
            TechnicalIndicators = new Dictionary<string, Technical>();
            StrategyTickHistory = new List<StrategyMarketTickResult>();
        }

        /// <summary>
        /// Creates a new StrategyBase
        /// </summary>
        /// <param name="name">The name of the strategy</param>
        protected StrategyBase(string name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Creates a new StrategyBase
        /// </summary>
        /// <param name="name">the name of the strategy</param>
        /// <param name="desciption">the description of the strategy</param>
        protected StrategyBase(string name, string desciption)
            : this(name)
        {
            Description = desciption;
        }

        /// <summary>
        /// TechnicalIndicators
        /// </summary>
        Dictionary<string, Technical> TechnicalIndicators { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Technical GetTechnical(string key)
        {
            return TechnicalIndicators[key];
        }

        /// <summary>
        /// AddTechnical
        /// </summary>
        /// <param name="key"></param>
        /// <param name="technical"></param>
        public void AddTechnical(string key, Technical technical)
        {
            TechnicalIndicators.Add(key, technical);
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
        public virtual StrategyMarketTickResult MarketTick(object sender, MarketTickEventArgs e)
        {
            // TODO: Determine if strategies should be prevented from buying AND selling in the same tick.
            // if not; determine precedence; for now it's sell first (for liquidity) and purchase 2nd.
            currentMarketTick = e;

            foreach (var technicalIndicator in TechnicalIndicators)
                technicalIndicator.Value.MarketTick(e);
            
            var s = SellSignal(e);
            var b = BuySignal(e);

            var thisHistory = new StrategyMarketTickResult(OnMarketTickEvent(e), OnBuyEvent(b), OnSellEvent(s));

            StrategyTickHistory.Add(thisHistory);

            return thisHistory;
        }

        /// <summary>
        /// StrategyTickHistory
        /// </summary>
        public List<StrategyMarketTickResult> StrategyTickHistory { get; set; }

        /// <summary>
        /// Buy some Shares based on the last tick
        /// </summary>
        /// <param name="Shares"></param>
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
        /// Sell some Shares based on the last tick
        /// </summary>
        /// <param name="Shares"></param>
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
        protected BuyEventArgs OnBuyEvent(BuyEventArgs e)
        {
            if (BuyEvent != null && e != null)
            {
                BuyEvent(this, e);
            }

            return e;
        }

        /// <summary>
        /// Sell Event Invocator
        /// </summary>
        /// <param name="e">The sell event argument</param>
        protected SellEventArgs OnSellEvent(SellEventArgs e)
        {
            if (SellEvent != null && e != null)
            {
                SellEvent(this, e);
            }

            return e;
        }

        /// <summary>
        /// Market tick event invocator
        /// </summary>
        /// <param name="e">market tick event args</param>
        public MarketTickEventArgs OnMarketTickEvent(MarketTickEventArgs e)
        {
            if (MarketTickEvent != null && e != null)
            {
                MarketTickEvent(this, e);
            }

            return e;
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
        /// MarketTickEvent
        /// </summary>
        public event EventHandler<MarketTickEventArgs> MarketTickEvent;

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
