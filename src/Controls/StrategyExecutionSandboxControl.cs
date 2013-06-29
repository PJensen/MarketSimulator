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
                    foreach (var security in StrategyExecutionSandbox.StrategyExecutor.SecurityMaster.Keys)
                    {
                        var tmpSeriesSecurity = new Series(security)
                        {
                            ChartType = SeriesChartType.Line,
                            XValueType = ChartValueType.DateTime,
                            YValueType = ChartValueType.Double,
                            XAxisType = AxisType.Primary,
                            YAxisType = AxisType.Primary,
                            Enabled = true,
                        };

                        foreach (var hist in StrategyExecutionSandbox.StrategyExecutor.StrategyTickHistory[security])
                        {
                            tmpSeriesSecurity.Points.AddXY(hist.MarketTickEventArgs.MarketData.Date,
                                hist.MarketTickEventArgs.MarketData.Close);
                            
                        }

                        chartSandbox.Series.Add(tmpSeriesSecurity);
                    }

                    #region Sandbox Info Series

                    var tmpSeriesCash = new Series(StrategyExecutionSandbox.Name)
                    {
                        ChartType = SeriesChartType.Line,
                        XValueType = ChartValueType.DateTime,
                        YValueType = ChartValueType.Double,
                        XAxisType = AxisType.Primary,
                        YAxisType = AxisType.Primary,
                        Enabled = true,
                    };

                    foreach (var h in StrategyExecutionSandbox.StrategyExecutor.StrategySnapshots)
                    {
                        tmpSeriesCash.Points.AddXY(h.Date, h.Cash);
                    }

                    chartSandbox.Series.Add(tmpSeriesCash);

                    #endregion
                }
            }
        }

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
