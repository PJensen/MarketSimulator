using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Interfaces
{
    /// <summary>
    /// ISnappable
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISnappable<in T>
    {
        /// <summary>
        /// Snapshot T in time
        /// </summary>
        /// <param name="snapIn"></param>
        /// <returns></returns>
        void Snap(DateTime dt, T snapIn);
    }
}
