using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// TradeStringCollection
    /// </summary>
    public class TradeStringCollection : IEnumerable<TradeString>
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
        /// <param name="Symbol"></param>
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

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>TradeStrings enumerator</returns>
        public IEnumerator<TradeString> GetEnumerator()
        {
            foreach (var s in ActiveTradeStrings)
            {
                yield return s.Value;
            }
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns>TradeStrings enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var s in ActiveTradeStrings)
            {
                yield return s.Value;
            }
        }
    }
}
