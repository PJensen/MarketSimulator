using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;

namespace MarketSimulator.Events
{
    /// <summary>
    /// MarketEventArgs
    /// </summary>
    public class MarketEventArgs : MarketSimulatorEventArgs
    {
        /// <summary>
        /// MarketEventArgs
        /// </summary>
        public MarketEventArgs(SecuritiesSnap securitiesData)
        {
            SecuritiesData = securitiesData;
        }

        /// <summary>
        /// MarketData
        /// </summary>
        public SecuritiesSnap SecuritiesData { get; protected set; }
    }
}
