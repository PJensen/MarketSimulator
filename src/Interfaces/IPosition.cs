using System;
namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// IPosition
    /// </summary>
    public interface IPosition
    {
        double Price { get; set; }
        int Shares { get; set; }
        string Symbol { get; set; }
        DateTime Date { get; }
    }
}
