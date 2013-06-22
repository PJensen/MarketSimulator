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
    public class GeneralLedger : Dictionary<string, Double>
    {
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
            return this[entry] / Value;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="currentPositions"></param>
        public void Update(List<IPosition> currentPositions)
        {
            currentPositions.ForEach(Update);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="position">the position to update the general ledger with</param>
        private void Update(IPosition position)
        {
            var @key = position.Symbol;
            var @value = position.Shares * position.Price;

            if (!Keys.Contains(@key))
            {
                this.Add(@key, @value);
            }
            else 
            {
                this[@key] = @value;
            }
        }

        /// <summary>
        /// The total value of this portfolio
        /// </summary>
        public double Value 
        {
            get 
            {
                return this.Sum(kvp => kvp.Value);
            }
        }
    }
}
