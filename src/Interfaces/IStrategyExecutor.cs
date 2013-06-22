using MarketSimulator.Core;
using MarketSimulator.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// IStrategyExecutor
    /// </summary>
    public interface IStrategyExecutor
    {
        System.Collections.Generic.List<MarketSimulator.Core.StrategyExecutionSandbox> Sandboxes { get; set; }
        MarketSimulator.Core.GlobalSecuritiesData SecurityMaster { get; set; }
        int Tick { get; }
    }
}
