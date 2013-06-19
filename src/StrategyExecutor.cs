using MarketSimulator.Core;
using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator
{
    /// <summary>
    /// A strategy executor is responsible for the execution of a trading strategy.
    /// </summary>
    public class StrategyExecutor
    {
        /// <summary>
        /// StrategyExecutor
        /// </summary>
        public StrategyExecutor()
        {
            MarketData = new List<MarketData>();
        }

        /// <summary>
        /// Add a strategy to the strategy executor
        /// </summary>
        /// <param name="strategy">the strategy to add</param>
        public void Add(StrategyBase strategy)
        {
            // sanity check to make sure another strategy with the same name is not already there.
            if (Sandboxes.FirstOrDefault(s => s.Strategy.Name == strategy.Name) != null)
                return;

            Sandboxes.Add(new StrategyExecutionSandbox(this, strategy));
        }

        /// <summary>
        /// The last price that ticked in the simulator
        /// </summary>
        public double PXLast { get; set; }

        /// <summary>
        /// The common backing store for all market data.
        /// </summary>
        public List<MarketData> MarketData { get; set; }

        /// <summary>
        /// Sandboxes
        /// </summary>
        public List<StrategyExecutionSandbox> Sandboxes { get; set; }
    }
}
