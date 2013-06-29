using MarketSimulator.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// GlobalSecuritiesData
    /// </summary>
    public class GlobalSecuritiesData : IEnumerable<KeyValuePair<string, List<MarketData>>>
    {
        /// <summary>
        /// The backing store for all securities data
        /// </summary>
        Dictionary<string, List<MarketData>> BackingStore { get; set; }

        /// <summary>
        /// GlobalSecuritiesData
        /// </summary>
        public GlobalSecuritiesData(int estimatedTicks)
            : base()
        {
            BackingStore = new Dictionary<string, List<MarketData>>(estimatedTicks);
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="security">the security</param>
        /// <param name="index">the index dimension</param>
        /// <returns>market data for that date and index</returns>
        public MarketData this[string security, DateTime index]
        {
            get
            {
                if (!BackingStore.ContainsKey(security))
                {
                    throw new MarketSimulatorException(string.Format("The security \"{0}\" could not be found!", security));
                }

                return BackingStore[security].FirstOrDefault(f => f.Date.Equals(index));
            }
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="security">the security</param>
        /// <param name="index">the index dimension</param>
        /// <returns>market data for that date and index</returns>
        public MarketData this[string security, int index]
        {
            get 
            {
                return BackingStore[security][index];
            }
        }

        /// <summary>
        /// SecuritiesSnap Indexer
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns>SecuritiesSnap</returns>
        public SecuritiesSnap this[DateTime index]
        {
            get 
            {
                var retVal = new SecuritiesSnap();

                foreach (var k in Keys.Distinct())
                {
                    retVal.Add(k, this[k, index]);
                }

                return retVal;
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="security">the security</param>
        /// <param name="marketData">the market data</param>
        public void Add(string security, List<MarketData> marketData)
        {
            BackingStore.Add(security, marketData);
        }

        /// <summary>
        /// Clear
        /// </summary>
        public void Clear()
        {
            BackingStore.Clear();
        }

        /// <summary>
        /// The number of entries
        /// </summary>
        public int Count { get { return BackingStore.Count; } }

        /// <summary>
        /// Keys
        /// </summary>
        public IEnumerable<string> Keys
        {
            get { return BackingStore.Keys; }
        }

        /// <summary>
        /// Keys
        /// </summary>
        public IEnumerable<List<MarketData>> Values
        {
            get { return BackingStore.Values; }
        }

        /// <summary>
        /// MaximumDate
        /// </summary>
        public DateTime MaximumDate
        {
            get
            {
                var tmpMaxDateLookup = new Dictionary<string, DateTime>();
                foreach (var securitySymbol in BackingStore.Keys.Distinct())
                {
                    tmpMaxDateLookup.Add(securitySymbol, BackingStore[securitySymbol].Max(f => f.Date));
                }
                return tmpMaxDateLookup.Values.Max();
            }
        }

        /// <summary>
        /// MinimumDate
        /// </summary>
        public DateTime MinimumDate
        {
            get
            {
                var tmpMixDateLookup = new Dictionary<string, DateTime>();
                foreach (var securitySymbol in BackingStore.Keys.Distinct())
                {
                    tmpMixDateLookup.Add(securitySymbol, BackingStore[securitySymbol].Min(f => f.Date));
                }
                return tmpMixDateLookup.Values.Min();
            }
        }

        /// <summary>
        /// CanSecurityTickPastDate
        /// </summary>
        /// <param name="security">the security</param>
        /// <param name="n">the date</param>
        /// <returns>true if there is another day</returns>
        public bool CanSecurityTickIndex(string security, int n)
        {
            return this[security, n] != null;
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, List<MarketData>>> GetEnumerator()
        {
            return BackingStore.GetEnumerator();
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return BackingStore.GetEnumerator();
        }
    }
}
