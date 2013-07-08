using System;
using System.Collections.Generic;
namespace MarketSimulator.Core.Indicators
{
    /// <summary>
    /// ITechnicalValue
    /// </summary>
    /// <typeparam name="T">The type of technical value</typeparam>
    public interface ITechnicalValue<T>
    {
        /// <summary>
        /// The value of this technical
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Historical Values for this technical value over time.
        /// </summary>
        Dictionary<DateTime, T> Historical { get; }
    }
}