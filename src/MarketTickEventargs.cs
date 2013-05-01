using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;

namespace MarketSimulator
{
    /// <summary>
    /// EventArgs
    /// </summary>
    public class MarketTickEventArgs : EventArgs
    {
        public double RSI { get; set; }
        public MarketData marketData { get; set; }
    }
}
