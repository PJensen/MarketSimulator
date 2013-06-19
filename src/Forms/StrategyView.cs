using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarketSimulator.Forms
{
    /// <summary>
    /// StrategyView
    /// </summary>
    public partial class StrategyView : Form
    {
        #region Public Facing Properties

        /// <summary>
        /// StrategyExecutionSandbox
        /// </summary>
        StrategyExecutionSandbox StrategyExecutionSandbox { get; set; }

        #endregion

        /// <summary>
        /// StrategyView
        /// </summary>
        public StrategyView(StrategyExecutionSandbox strategyExecutor)
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrategyView_Load(object sender, EventArgs e)
        {

        }
    }
}
