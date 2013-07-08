using MarketSimulator.Exceptions;
using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// PositionData2
    /// </summary>
    public class PositionData2
    {
        /// <summary>
        /// PositionData2
        /// </summary>
        public PositionData2()
        {
            positions = new Dictionary<DateTime, List<IPosition>>();
        }

        /// <summary>
        /// internal positions backing store
        /// </summary>
        private readonly Dictionary<DateTime, List<IPosition>> positions;

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="date"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        private IPosition this[DateTime date, string security]
        {
            get
            {
                // Note: Doesn't matter that this returns null.
                if (positions.ContainsKey(date))
                {
                    return positions[date].FirstOrDefault(p => p.Symbol.Equals(security));
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Add(IPosition position)
        {
            var tmpPosition = this[position.Date, position.Symbol];

            if (tmpPosition != null)
            {
                this[position.Date, position.Symbol].Shares += position.Shares;

                return true;
            }
            else
            {
                if (!positions.ContainsKey(position.Date))
                {
                    positions[position.Date] = new List<IPosition>();
                }

                positions[position.Date].Add(position);

                return true;
            }
        }

        /// <summary>
        /// REFACTOR THIS
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public bool Remove(IPosition position)
        {
            var tmpPosition = this[position.Date, position.Symbol];

            if (tmpPosition == null)
            {
                return false;
            }

            if (position.Shares <= this[position.Date, position.Symbol].Shares)
            {
                this[position.Date, position.Symbol].Shares -= position.Shares;

                return true;
            }

            return false;
        }

        /// <summary>
        /// indexer
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<IPosition> this[DateTime date]
        {
            get
            {
                if (!positions.ContainsKey(date))
                    positions[date] = new List<IPosition>();
                return positions[date];
            }
        }

        /// <summary>
        /// TotalMarketValue
        /// </summary>
        /// <param name="date">total market value as of date</param>
        /// <returns>the total market value</returns>
        public double TotalMarketValue(DateTime date)
        {
            return this[date].Sum(p => p.Shares * p.Price);
        }

        /// <summary>
        /// Clears all position data
        /// </summary>
        public void Clear()
        {
            positions.Clear();
        }

        /// <summary>
        /// SecurityShares
        /// </summary>
        public int SecurityShares(DateTime date, string security)
        {
            if (this[date, security] == null)
                return 0;

            return this[date, security].Shares;
        }

        /// <summary>
        /// SecurityValue
        /// </summary>
        public double SecurityValue(DateTime date, string security)
        {
            if (this[date, security] == null)
                return 0.0d;

            return this[date, security].Shares *
                this[date, security].Price;
        }

        /// <summary>
        /// UpdatePrice
        /// </summary>
        /// <param name="date">the date to update the position for</param>
        /// <param name="security">the security to update</param>
        /// <param name="price">the potentially new price for that date and security</param>
        public bool UpdatePrice(DateTime date, string security, double price)
        {
            if (this[date, security] == null)
            {
                return false;
            }

            this[date, security].Price = price;

            return true;
        }

        /// <summary>
        /// CarryForward
        /// </summary>
        public bool CarryForward(DateTime previousDate, DateTime nextDate)
        {
            if (positions[previousDate] == null)
            {
                return false;
            }

            foreach (var position in positions[previousDate].Where(p => p.Shares > 0))
            {
                if (!positions.ContainsKey(nextDate))
                {
                    positions[nextDate] = new List<IPosition>();
                }

                Add(new Position(position) { Date = nextDate });
            }

            return true;
        }
    }

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
        /// RemovePosition the Shares amount from the position
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
        /// UpdatePrice
        /// </summary>
        /// <param name="security"></param>
        /// <param name="price"></param>
        public void UpdatePrice(string security, double price)
        {
            if (this[security] == null)
                return;

            this[security].Price = price;
        }

        /// <summary>
        /// AddPosition position data
        /// </summary>
        /// <param name="position"></param>
        public bool AddPosition(IPosition position)
        {
            if (this[position] == null)
            {
                Positions.Add(position);
            }
            else
            {
                this[position].Shares += position.Shares;
            }

            return true;
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
        /// TotalMarketValueAsOf
        /// </summary>
        /// <returns></returns>
        public double TotalMarketValue
        {
            get
            {
                return Positions.Sum(s => s.Price * s.Shares);
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
}
