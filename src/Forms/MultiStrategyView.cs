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
            foreach (var security in simulator.SecurityMaster.Keys)
            {
                chartView.Series.Add(new Series(security)
                                         {
                                             ChartType = SeriesChartType.Line,
                                             XAxisType = AxisType.Primary,
                                             YAxisType = AxisType.Primary,
                                             XValueType = ChartValueType.Date,
                                             YValueType = ChartValueType.Double,
                                         });
            }

            foreach (var sandbox in simulator.Sandboxes)
            {
                chartView.Series.Add(new Series(sandbox.Name)
                {
                    ChartType = SeriesChartType.Line,
                    XAxisType = AxisType.Primary,
                    YAxisType = AxisType.Secondary,
                    XValueType = ChartValueType.Date,
                    YValueType = ChartValueType.Double,
                });
            }

            foreach (var tickDate in simulator.TickDates)
            {
                foreach (var marketData in simulator.SecurityMaster[tickDate])
                {
                    if (marketData != null)
                    {
                        chartView.Series[marketData.Symbol].Points.AddXY(tickDate, marketData.Close);
                    }
                }
            }

            foreach (var sandbox in simulator.Sandboxes)
            {
                foreach (var snap in sandbox.StrategySnapshots)
                {
                    var tmpDataPoint = new DataPoint()
                    {

                    };

                    chartView.Series[sandbox.Name].Points.AddXY(snap.Date, snap.NAV);
                    
                    if (snap.Cash <= 0)
                    {
                        var tmpPoint = chartView.Series[sandbox.Name].Points.LastOrDefault();
                        if (tmpPoint == null)
                            continue;

                        tmpPoint.IsEmpty = true;
                        tmpPoint.SetValueXY(snap.Date, DBNull.Value);

                        chartView.Annotations.Add(new ArrowAnnotation()
                        {
                            AnchorDataPoint = tmpPoint,
                            ArrowSize = 20,
                            ArrowStyle = ArrowStyle.Simple,
                        });
                    }
                }
            }
        }
    }
}
