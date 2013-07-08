using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// Any market simulator object that has a date
    /// </summary>
    public interface IHasDate
    {
        /// <summary>
        /// The date
        /// </summary>
        DateTime Date { get; }
    }
}
