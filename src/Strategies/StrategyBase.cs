using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Strategies
{
    /// <summary>
    /// StrategyBase
    /// </summary>
    public class StrategyBase : IStrategy
    {
        /// <summary>
        /// Name of the strategy
        /// </summary>
        public string Name { get; set; }
    }
}
