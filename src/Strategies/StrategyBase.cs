using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// StrategyBase
    /// </summary>
    public abstract class StrategyBase : IStrategy
    {
        

        /// <summary>
        /// StrategyBase
        /// </summary>
        /// <param name="dataManipulator"></param>
        protected StrategyBase()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void MarketTick(object sender, MarketTickEventArgs e);

        /// <summary>
        /// Name of the strategy
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// BuyEvent
        /// </summary>
        public event EventHandler<MarketTickEventArgs> BuyEvent;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected void OnBuyEvent(MarketTickEventArgs e)
        {
            if (BuyEvent != null)
                BuyEvent(this, e);
        }

        public event EventHandler<MarketTickEventArgs> sellEvent;

        public void OnSellEvent(MarketTickEventArgs e)
        {
            EventHandler<MarketTickEventArgs> handler = sellEvent;
            if (handler != null) handler(this, e);
        }
    }
}
