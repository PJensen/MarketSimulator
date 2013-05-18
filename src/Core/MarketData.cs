﻿using System;
using System.Diagnostics;

namespace MarketSimulator.Core
{
    /// <summary>
    /// MarketData
    /// </summary>
    [DebuggerDisplay("{Date} [{Open}, {High}, {Low}, {Close}]")]
    public class MarketData
    {
        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>string representation of this market data element</returns>
        public override string ToString()
        {
            return string.Format("{0} [{1}, {2}, {3}]", Date, Open, High, Low, Close);
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

        public double AsLine
        {
            get
            {
                return Close;
            }
        }

    }
}