using System;

namespace MarketSimulator.Core
{
    /// <summary>
    /// MarketData
    /// </summary>
    public struct MarketData
    {
        public DateTime Date { get; set; }
        public double Close { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public long Volume { get; set; }

        public double[] AsCandleStick
        {
            get
            {
                return new[]
                       {
                           Low,
                           High,
                           Close,
                           Open
                       };
            }
        }

        public double AsLine
        {
            get
            {
                return Close;
            }
        }

    }
}
