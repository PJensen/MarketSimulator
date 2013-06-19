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
        bool AddStrategy(StrategyBase strategy);
        bool Initialize(string ticker, out string message);
        bool Initialized { get; }
        bool LoadMarketData(out string message);
        System.Collections.Generic.List<MarketData> MarketData { get; set; }
        void OnTickEvent(MarketTickEventArgs eventArgs);
        double PXLast { get; set; }
        System.Collections.Generic.List<StrategyExecutionSandbox> Sandboxes { get; set; }
        int Tick { get; }
        string Ticker { get; }
    }
}
