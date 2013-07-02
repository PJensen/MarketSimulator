using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MarketSimulator.Core
{


    /// <summary>
    /// MarketTicks
    /// TODO: Think about renaming this to <c>SecurityData</c>
    /// </summary>
    [DebuggerDisplay("{Date} [{Open}, {High}, {Low}, {Close}]")]
    public class MarketData : IMarketData
    {
        /// <summary>
        /// Create a new MarketData
        /// </summary>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        public MarketData(MarketData prev = null, MarketData next = null)
        {
            Next = next;
            Prev = prev;
        }

        /// <summary>
        /// IsValid
        /// </summary>
        public bool IsValid 
        {
            get 
            {
                return Date != null;
            } 
        }

        /// <summary>
        /// HasNext
        /// </summary>
        public bool HasNext { get { return Next != null; } }

        /// <summary>
        /// HasPrev
        /// </summary>
        public bool HasPrev { get { return Prev != null; } }

        /// <summary>
        /// The next market tick
        /// </summary>
        public MarketData Next { get; set; }

        /// <summary>
        /// The next market tick
        /// </summary>
        public MarketData Prev { get; set; }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>string representation of this market data element</returns>
        public override string ToString()
        {
            return string.Format("{0} [{1}, {2}, {3}, {4}]", Date, Open, High, Low, Close);
        }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Close
        /// </summary>
        public double Close { get; set; }

        /// <summary>
        /// Open
        /// </summary>
        public double Open { get; set; }

        /// <summary>
        /// High
        /// </summary>
        public double High { get; set; }

        /// <summary>
        /// Low
        /// </summary>
        public double Low { get; set; }

        /// <summary>
        /// Volume
        /// </summary>
        public long Volume { get; set; }

        /// <summary>
        /// Technicals
        /// </summary>
        public Dictionary<string, double> Technicals { get; set; }

        /// <summary>
        /// AsCandleStick
        /// </summary>
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

        /// <summary>
        /// AsLine is just the closing price
        /// </summary>
        public double AsLine
        {
            get
            {
                return Close;
            }
        }
    }
}
