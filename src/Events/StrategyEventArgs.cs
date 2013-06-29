using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Events
{
    /// <summary>
    /// StrategyEventArgs
    /// </summary>
    public class StrategyEventArgs : MarketSimulatorEventArgs
    {
        /// <summary>
        /// StrategyEventArgs
        /// </summary>
        /// <param name="strategy">event args</param>
        public StrategyEventArgs(StrategyBase strategy)
        {
            Strategy = strategy;
        }

        /// <summary>
        /// Strategy
        /// </summary>
        public StrategyBase Strategy { get; set; }
    }
}
