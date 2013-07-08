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
    public struct StrategySnapshot
    {
        /// <summary>
        /// Create a new StrategySnapshot
        /// </summary>
        /// <param name="cash">given a specific amount of cash</param>
        public StrategySnapshot(StrategyExecutionSandbox sandbox) 
            : this()
        {
            ParentSandbox = sandbox;
            PositionData = sandbox.PositionData;
            Cash = sandbox.Cash;
            NumberOfTrades = sandbox.NumberOfTrades;
            Tick = sandbox.Tick;
            Date = sandbox.Date;
            NAV = sandbox.PositionData.TotalMarketValue(Date) + Cash;
            TotalMarketValue = sandbox.PositionData.TotalMarketValue(Date);
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
        /// NAV
        /// </summary>
        public double NAV { get; set; }

        /// <summary>
        /// TotalMarketValue
        /// </summary>
        public double TotalMarketValue { get; private set; }

        /// <summary>
        /// The market tick this snapshot represents
        /// </summary>
        public int Tick { get; private set; }

        /// <summary>
        /// Positions
        /// </summary>
        public PositionData2 PositionData { get; private set; }

        /// <summary>
        /// ParentSandbox
        /// </summary>
        public StrategyExecutionSandbox ParentSandbox { get; private set; }
    }
}
