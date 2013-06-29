using MarketSimulator.Components;
using MarketSimulator.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketSimulator.Forms
{
    /// <summary>
    /// MultiStrategyView
    /// </summary>
    public partial class MultiStrategyView : Form
    {
        /// <summary>
        /// simulator
        /// </summary>
        private readonly MarketSimulatorComponent simulator;

        /// <summary>
        /// MultiStrategyView
        /// </summary>
        public MultiStrategyView(MarketSimulatorComponent simulator)
        {
            InitializeComponent();

            this.simulator = simulator;
        }

        /// <summary>
        /// MultiStrategyView_Load
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void MultiStrategyView_Load(object sender, EventArgs e)
        {
            foreach (var sandbox in simulator.Sandboxes)
            {
                var sandboxControl = new StrategyExecutionSandboxControl(sandbox);

                var tmpSandboxPage = new TabPage(sandbox.Name);
                tmpSandboxPage.Controls.Add(sandboxControl);
                tabControl.TabPages.Add(tmpSandboxPage);

                chartView.Series.Add(sandboxControl.CashSeries);
            }

            foreach (var security in simulator.SecurityMaster)
            {
                var tmpSecuritySeries = new Series(security.Key) 
                {
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.DateTime,
                    YValueType = ChartValueType.Double,
                    XAxisType = AxisType.Primary,
                    YAxisType = AxisType.Secondary,
                    Enabled = true,
                };

                foreach (var marketTick in security.Value)
                {
                    tmpSecuritySeries.Points.AddXY(marketTick.Date, marketTick.Close);
                }

                chartView.Series.Add(tmpSecuritySeries);
            }
        }
    }
}
