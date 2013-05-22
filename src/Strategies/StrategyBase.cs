using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Core;
using MarketSimulator.Events;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// StrategyBase
    /// </summary>
    public abstract class StrategyBase : IStrategy
    {
        /// <summary>
        /// FinancialFormulae
        /// </summary>
        public FinancialFormula FinancialFormulae { get; protected set; }

        /// <summary>
        /// Creates a new StrategyBase
        /// </summary>
        /// <param name="name">The name of the strategy</param>
        /// <param name="buySignal">The buy signal</param>
        /// <param name="sellSignal">The sell signal</param>
        protected StrategyBase(string name, BuySignal buySignal, SellSignal sellSignal, FinancialFormula financialFormulae)
        {
            Name = name;
            SellSignal = sellSignal;
            BuySignal = buySignal;
            FinancialFormulae = financialFormulae;
        }

        /// <summary>
        /// MarketTick
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">market Tick event args</param>
        public void MarketTick(object sender, MarketTickEventArgs e)
        {
            var sellEventArgs = SellSignal(e);
            var buyEventArgs = BuySignal(e);

            if (sellEventArgs != null && SellEvent != null)
                SellEvent(this, sellEventArgs);
            else if (buyEventArgs != null && BuyEvent != null)
                BuyEvent(this, buyEventArgs);
        }

        /// <summary>
        /// Name of the strategy
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// BuySignal
        /// </summary>
        public BuySignal BuySignal { get; set; }

        /// <summary>
        /// SellSignal
        /// </summary>
        public SellSignal SellSignal { get; set; }

        /// <summary>
        /// BuyEvent
        /// </summary>
        public event EventHandler<BuyEventArgs> BuyEvent;

        /// <summary>
        /// SellEvent
        /// </summary>
        public event EventHandler<SellEventArgs> SellEvent;
    }
}
