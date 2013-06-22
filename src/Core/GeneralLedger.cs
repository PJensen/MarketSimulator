using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// Portfolio; initially this was a more custom object but there was very 
    /// little benefit to doing it that way -- so a standard implementation is better; for now.
    /// </summary>
    public class GeneralLedger
    {
        /// <summary>
        /// backingStore
        /// </summary>
        private readonly Dictionary<string, Double> backingStore = new Dictionary<string, double>();

        /// <summary>
        /// Portfolio
        /// </summary>
        public GeneralLedger()
            : base() { }

        /// <summary>
        /// GetWeight
        /// </summary>
        /// <param name="entry">the entry to get the weight for</param>
        /// <returns>the weight of the entry</returns>
        public double GetWeight(string entry)
        {
            return backingStore[entry] / Value;
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        public double this[string entry]
        {
            get
            {
                if (backingStore.ContainsKey(entry))
                    return backingStore[entry];
                return 0.0d;
            }
            set
            {
                if (!backingStore.ContainsKey(entry))
                    backingStore.Add(entry, value);
                else
                    backingStore[entry] = value;
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="currentPositions"></param>
        public void Add(List<IPosition> currentPositions)
        {
            currentPositions.ForEach(Add);
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="position">the position to update the general ledger with</param>
        public void Add(IPosition position)
        {
            var @key = position.Symbol;
            var @value = position.Shares * position.Price;

            this[@key] = @value;
        }

        /// <summary>
        /// The total value of this portfolio
        /// </summary>
        public double Value
        {
            get
            {
                return backingStore.Sum(kvp => kvp.Value);
            }
        }
    }
}
