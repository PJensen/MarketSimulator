using MarketSimulator.Forms;
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
    [System.ComponentModel.ToolboxItem(true)]
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

        #region Constants
        const string CategoryExecutionSandbox = "Execution Sandbox";
        const string CategoryGlobal = "Global";
        #endregion

        /// <summary>
        /// StartDate
        /// </summary>
        [System.ComponentModel.Category(CategoryGlobal)]
        [System.ComponentModel.Description("The start date that for all securities")]
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
        [System.ComponentModel.Category(CategoryGlobal)]
        [System.ComponentModel.Description("The end date all securities data")]
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
        /// EndDate
        /// </summary>
        [System.ComponentModel.Category(CategoryGlobal)]
        [System.ComponentModel.Description("The periodicity of market ticks")]
        public TimeSpan Periodicity
        {
            get { return Properties.Settings.Default.Periodicity; }
            set
            {
                Properties.Settings.Default.Periodicity = value;
                Save();
            }
        }

        /// <summary>
        /// StartingBalance
        /// </summary>
        [System.ComponentModel.Category(CategoryExecutionSandbox)]
        [System.ComponentModel.Description("The balance to initialize execution sandboxes with")]
        public double StartingBalance
        {
            get { return Properties.Settings.Default.StartingBalance; }
            set { Properties.Settings.Default.StartingBalance = value; Save(); }
        }

        /// <summary>
        /// EquityPool
        /// </summary>
        [System.ComponentModel.Category(CategoryGlobal)]
        [System.ComponentModel.Description("The securities the sandboxes have to work with")]
        [System.ComponentModel.SettingsBindable(true)]
        [System.ComponentModel.ReadOnly(false)]
        [System.ComponentModel.ListBindable(true)]
        public StringCollection SecurityMaster
        {
            get
            {
                return Properties.Settings.Default.PreviousSecurities;
            }
            set
            {
                Properties.Settings.Default.PreviousSecurities = value;
                Save();
            }
        }

        /// <summary>
        /// AddTicker
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public bool AddTicker(string symbol)
        {
            symbol = symbol.Trim().ToUpperInvariant();
            if (Properties.Settings.Default.PreviousSecurities.Contains(symbol))
                return false;
            Properties.Settings.Default.PreviousSecurities.Add(symbol);
            Save();
            return true;
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
