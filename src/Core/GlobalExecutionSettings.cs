using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace MarketSimulator.Core
{
    /// <summary>
    /// GlobalExecutionSettings
    /// <remarks>
    /// Singletons are the bane of existence, except here.
    /// </remarks>
    /// </summary>
    public class GlobalExecutionSettings
    {
        #region Singleton
        private GlobalExecutionSettings() { }
        private static readonly object syncRoot = new object();
        private static GlobalExecutionSettings instance = null;
        public static GlobalExecutionSettings Instance
        {
            get
            {
                lock (syncRoot)
                    if (instance == null) { instance = new GlobalExecutionSettings(); }
                return instance;
            }
        }
        #endregion

        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return Properties.Settings.Default.StartDate;
            }
            set
            {
                Properties.Settings.Default.StartDate = value;
                Save();
            }
        }

        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate
        {
            get { return Properties.Settings.Default.EndDate; }
            set
            {
                Properties.Settings.Default.EndDate = value;
                Save();
            }
        }

        /// <summary>
        /// StartingBalance
        /// </summary>
        public double StartingBalance
        {
            get { return Properties.Settings.Default.StartingBalance; }
            set { Properties.Settings.Default.StartingBalance = value; Save(); }
        }

        /// <summary>
        /// EquityPool
        /// </summary>
        public StringCollection SecurityMaster
        {
            get { return Properties.Settings.Default.PreviousSecurities; }
            set
            {
                Properties.Settings.Default.PreviousSecurities = value;
                Save();
            }
        }

        /// <summary>
        /// Save
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
