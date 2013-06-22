using System;
namespace MarketSimulator.Events
{
    /// <summary>
    /// ITechnicalSnap
    /// TODO: (TH) Fill in details.
    /// </summary>
    interface ITechnicalSnap
    {
        double EMA13 { get; set; }
        double MACDHistogram { get; set; }
        double RSI { get; set; }
    }
}
