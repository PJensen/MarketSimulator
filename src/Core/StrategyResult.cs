using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// StrategyResult
    /// </summary>
    public class StrategyResult
    {
        /// <summary>
        /// StrategyResult
        /// </summary>
        public StrategyResult(StrategyBase strategy)
        {
        }

        /// <summary>
        /// Cash
        /// </summary>
        public double Cash { get; private set; }
    }
}
