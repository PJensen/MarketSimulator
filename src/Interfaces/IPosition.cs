using System;
namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// IPosition
    /// </summary>
    public interface IPosition
    {
        double Price { get; }
        int Shares { get; }
        string Symbol { get; }
        DateTime Date { get; }
    }
}
