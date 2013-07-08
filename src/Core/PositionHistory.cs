using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// Ledger
    /// </summary>
    public class PositionHistory
    {
        /// <summary>
        /// PositionHistory
        /// </summary>
        readonly Dictionary<DateTime, IEnumerable<IPosition>> positionHistory;

        /// <summary>
        /// PositionHistory
        /// </summary>
        public PositionHistory()
            : base()
        {
            // init position history
            positionHistory = new Dictionary<DateTime, IEnumerable<IPosition>>();
        }

        /// <summary>
        /// AddPosition
        /// </summary>
        /// <param name="date">the date</param>
        /// <param name="position">the position to add</param>
        /// <returns></returns>
        public void AddPositions(DateTime date, IEnumerable<IPosition> positions)
        {
            positionHistory.Add(date, new List<IPosition>(positions));
        }

        /// <summary>
        /// Indexer into position history
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IEnumerable<IPosition> this[DateTime index]
        {
            get
            {
                return positionHistory[index];
            }
        }
    }
}
