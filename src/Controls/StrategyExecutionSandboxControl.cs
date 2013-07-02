using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MarketSimulator.Core;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketSimulator.Controls
{
    /// <summary>
    /// StrategyExecutionSandboxControl
    /// </summary>
    public partial class StrategyExecutionSandboxControl : UserControl
    {
        /// <summary>
        /// StrategyExecutionSandboxControl
        /// </summary>
        /// <param name="sandbox">the execution sandbox tied to this control</param>
        public StrategyExecutionSandboxControl(StrategyExecutionSandbox sandbox)
        {
            InitializeComponent();
            StrategyExecutionSandbox = sandbox;
            propertyGridSandbox.SelectedObject = StrategyExecutionSandbox;

            CashSeries = new Series(StrategyExecutionSandbox.Name)
            {
                ChartType = SeriesChartType.Line,
                XValueType = ChartValueType.DateTime,
                YValueType = ChartValueType.Double,
                XAxisType = AxisType.Primary,
                YAxisType = AxisType.Primary,
                Enabled = true,
            };
        }

        /// <summary>
        /// StrategyExecutionSandbox
        /// </summary>
        public StrategyExecutionSandbox StrategyExecutionSandbox { get; set; }

        /// <summary>
        /// StrategyExecutionSandboxControl_Load
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void StrategyExecutionSandboxControl_Load(object sender, EventArgs e)
        {
            lock (StrategyExecutionSandbox)
            {
                lock (chartSandbox)
                {
                    foreach (var snapshot in StrategyExecutionSandbox.StrategySnapshots)
                    {
                        CashSeries.Points.AddXY(snapshot.Date, snapshot.NAV);
                    }

                    chartSandbox.Series.Add(CashSeries);
                }
            }
        }

        /// <summary>
        /// CashSeries
        /// </summary>
        public readonly Series CashSeries;

        /// <summary>
        /// removeToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
