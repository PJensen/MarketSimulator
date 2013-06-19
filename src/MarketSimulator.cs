using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MarketSimulator.Core;
using MarketSimulator.Events;
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
        private MarketSimulator() {  }

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


    }
}
