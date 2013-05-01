using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Strategies;

namespace MarketSimulator
{
    /// <summary>
    /// MarketSimulator
    /// </summary>
    public class MarketSimulator
    {
        #region Singleton

        /// <summary>
        /// _instance
        /// </summary>
        private static volatile MarketSimulator _instance;

        /// <summary>
        /// syncRoot
        /// </summary>
        private static readonly object syncRoot = new Object();

        /// <summary>
        /// MarketSimulator
        /// </summary>
        private MarketSimulator()
        {
            Balance = cash = Properties.Settings.Default.StartingBalance;
            MarketData = new List<MarketData>();
            CurrentStrategy = default(StrategyBase);
        }

        /// <summary>
        /// Instance
        /// </summary>
        public static MarketSimulator Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (_instance == null)
                            _instance = new MarketSimulator();
                    }
                }

                return _instance;
            }
        }

        #endregion

        public int Shares { get; set; }
        public double Balance { get; set; }
        public int Tick { get; set; }
        public int numberOfTrade { get; set; }
        public int numberOfWinningTrade { get; set; }
        public double cash { get; set; }
        /// <summary>
        /// CurrentStrategy
        /// </summary>
        public StrategyBase CurrentStrategy { get; set; }

        /// <summary>
        /// MarketData
        /// </summary>
        public List<MarketData> MarketData { get; set; }
    }
}
