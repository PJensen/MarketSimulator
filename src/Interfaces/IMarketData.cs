using System;
namespace MarketSimulator.Interfaces
{
    public interface IMarketData
    {
        double Close { get; set; }
        DateTime Date { get; set; }
        double High { get; set; }
        double Low { get; set; }
        double Open { get; set; }
        long Volume { get; set; }
        string Symbol { get; set; }
    }
}
