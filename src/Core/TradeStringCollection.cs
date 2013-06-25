using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// TradeStringCollection
    /// </summary>
    public class TradeStringCollection
    {
        /// <summary>
        /// TradeStringCollection
        /// </summary>
        public TradeStringCollection()
            : base() { }

        /// <summary>
        /// indexer into the trade string; this is critical; 
        ///     call *clear* if you want to clear the active string.
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public TradeString this[string security]
        {
            get
            {
                if (!ActiveTradeStrings.ContainsKey(security))
                    ActiveTradeStrings.Add(security, new TradeString(security));
                return ActiveTradeStrings[security];
            }
        }

        /// <summary>
        /// Initialize
        /// </summary>
        public void Clear()
        {
            ActiveTradeStrings.Clear();
        }

        /// <summary>
        /// ActiveTradeStrings
        /// </summary>
        private Dictionary<string, TradeString> ActiveTradeStrings = new Dictionary<string, TradeString>();
    }
}
