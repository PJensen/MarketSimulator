using MarketSimulator.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Events
{
    /// <summary>
    /// AggregateStrategyTickData
    /// </summary>
    public class StrategyMarketTickResult : MarketSimulatorEventArgs
    {
        /// <summary>
        /// StrategyMarketTickResult
        /// </summary>
        /// <param name="e">market tick</param>
        /// <param name="b">buy result</param>
        /// <param name="s">sell result</param>
        public StrategyMarketTickResult(MarketTickEventArgs e, BuyEventArgs b, SellEventArgs s)
        {
            MarketTickEventArgs = e;
            BuyEventArgs = b;
            SellEventArgs = s;
        }

        /// <summary>
        /// StrategySnapshot
        /// </summary>
        public StrategySnapshot StrategySnapshot { get; set; }

        /// <summary>
        /// MarketTickEventArgs
        /// </summary>
        public MarketTickEventArgs MarketTickEventArgs { get; private set; }

        /// <summary>
        /// BuyEventArgs
        /// </summary>
        public BuyEventArgs BuyEventArgs { get; private set; }

        /// <summary>
        /// SellEventArgs
        /// </summary>
        public SellEventArgs SellEventArgs { get; private set; }
    }
}
