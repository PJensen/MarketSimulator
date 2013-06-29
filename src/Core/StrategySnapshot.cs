using MarketSimulator.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// StrategySnapshot
    /// </summary>
    public class StrategySnapshot
    {
        /// <summary>
        /// Create a new StrategySnapshot
        /// </summary>
        /// <param name="cash">given a specific amount of cash</param>
        public StrategySnapshot(StrategyExecutionSandbox sandbox)
        {
            ParentSandbox = sandbox;

            Positions = new List<IPosition>();

            Cash = sandbox.Cash;
            NumberOfTrades = sandbox.NumberOfTrades;
            Tick = sandbox.Tick;
            Date = sandbox.Date;
        }

        /// <summary>
        /// AddPosition
        /// </summary>
        /// <param name="position"></param>
        public void AddPosition(IPosition position)
        {
            Positions.Add(position);
        }

        /// <summary>
        /// The number of trades
        /// </summary>
        public int NumberOfTrades { get; private set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Cash amount
        /// </summary>
        public double Cash { get; private set; }

        /// <summary>
        /// The market tick this snapshot represents
        /// </summary>
        public int Tick { get; private set; }

        /// <summary>
        /// Positions
        /// </summary>
        public List<IPosition> Positions { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public StrategyExecutionSandbox ParentSandbox{get; private set;}
    }
}
