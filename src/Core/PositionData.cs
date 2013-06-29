using MarketSimulator.Exceptions;
using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// PositionData
    /// </summary>
    public class PositionData : IEnumerable<IPosition>
    {
        /// <summary>
        /// Initialize
        /// </summary>
        public PositionData()
        {
            Positions = new List<IPosition>();
        }

        /// <summary>
        /// PositionData Copy Constructor
        /// </summary>
        /// <param name="positionData">Position Data</param>
        public PositionData(PositionData positionData)
            : this()
        {
            foreach (var position in positionData)
            {
                this.AddPosition(new Position(position));
            }
        }

        /// <summary>
        /// RemovePosition Empty Positions
        /// </summary>
        public void RemoveEmptyPositions()
        {
            Positions.RemoveAll(p => p.Shares == 0);
        }

        /// <summary>
        /// Position indexer
        /// </summary>
        /// <param name="index">the security to find position for</param>
        /// <returns>The position tied to the security</returns>
        public IPosition this[IPosition index]
        {
            get { return this[index.Symbol]; }
        }

        /// <summary>
        /// Position indexer
        /// </summary>
        /// <param name="index">the security to find position for</param>
        /// <returns>The position tied to the security</returns>
        public IPosition this[string index]
        {
            get
            {
                return Positions.FirstOrDefault(p => p.Symbol.Equals(index));
            }
        }

        /// <summary>
        /// RemovePosition the shares amount from the position
        /// </summary>
        /// <param name="position"></param>
        public bool RemovePosition(IPosition position)
        {
            bool retVal = false;

            if (this[position] != null)
            {
                if (this[position].Shares >= position.Shares)
                {
                    this[position].Shares -= position.Shares;

                    retVal = true;
                }
            }

            return retVal;
        }

        /// <summary>
        /// AddPosition position data
        /// </summary>
        /// <param name="position"></param>
        public void AddPosition(IPosition position)
        {
            if (this[position] == null)
            {
                Positions.Add(position);
            }
            else
            {
                this[position].Shares += position.Shares;
            }
        }

        /// <summary>
        /// Securities that live under this Positions tick
        /// </summary>
        public IEnumerable<string> Securities
        {
            get { return Positions.Select(f => f.Symbol).Distinct(); }
        }

        /// <summary>
        /// Clears all position data
        /// </summary>
        public void Clear()
        {
            Positions.Clear();
        }

        /// <summary>
        /// TotalShares
        /// </summary>
        public int SecurityShares(string security)
        {
            return Positions.Where(p => p.Symbol.Equals(security)).Sum(p => p.Shares);
        }

        /// <summary>
        /// TotalShares
        /// </summary>
        public double SecurityValue(string security)
        {
            return Positions.Where(p => p.Symbol.Equals(security)).Sum(p => p.Shares * p.Price);
        }

        /// <summary>
        /// TotalMarketValue
        /// </summary>
        public double TotalMarketValue
        {
            get
            {
                double retVal = 0.0d;

                foreach (var p in Positions.Select(f => f.Symbol).Distinct())
                {
                    retVal += SecurityValue(p);
                }

                return 0;
            }
        }

        /// <summary>
        /// Positions
        /// </summary>
        private List<IPosition> Positions { get; set; }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator<IPosition> GetEnumerator()
        {
            return Positions.GetEnumerator();
        }

        /// <summary>
        /// GetEnumerator
        /// </summary>
        /// <returns></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Positions.GetEnumerator();
        }
    }

    /// <summary>
    /// Ledger
    /// </summary>
    public class PositionHistory : Dictionary<DateTime, PositionData>
    {
        /// <summary>
        /// Ledger
        /// </summary>
        public PositionHistory()
            : base() { }
    }
}
