using System;
namespace MarketSimulator.Components
{
    interface IMarketSimulatorComponent
    {
        bool AddStrategy(MarketSimulator.Strategies.StrategyBase strategy);
        bool Initialize(string ticker, out string message);
        bool Initialized { get; }
        bool LoadMarketData(out string message);
        System.Collections.Generic.List<MarketSimulator.Core.MarketData> MarketData { get; set; }
        void OnTickEvent(MarketSimulator.MarketTickEventArgs eventArgs);
        double PXLast { get; set; }
        System.Collections.Generic.List<MarketSimulator.StrategyExecutionSandbox> Sandboxes { get; set; }
        int Tick { get; }
        string Ticker { get; }
    }
}
