using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// GlobalExecutionSettings
    /// </summary>
    public class GlobalExecutionSettings
    {
        /// <summary>
        /// GlobalExecutionSettings
        /// Use the <see cref="GetUserDefaults"/> static method
        /// </summary>
        private GlobalExecutionSettings() { }

        /// <summary>
        /// GetDefaults
        /// </summary>
        /// <returns></returns>
        public static GlobalExecutionSettings GetUserDefaults()
        {
            return new GlobalExecutionSettings()
            {
                SecurityMaster = Properties.Settings.Default.PreviousSecurities,
                StartingBalance = Properties.Settings.Default.StartingBalance,
                StartDate = DateTime.MinValue,
                EndDate = DateTime.MaxValue,
            };
        }

        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// StartingCash
        /// </summary>
        public double StartingBalance { get; set; }

        /// <summary>
        /// EquityPool
        /// </summary>
        public StringCollection SecurityMaster { get; set; }
    }
}
