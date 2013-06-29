using MarketSimulator.Components;
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
            var generalSeries = new Series("Market")
            {
                ChartType = SeriesChartType.Line,
                YAxisType = AxisType.Secondary,
                XAxisType = AxisType.Primary,
                // TODO: fill in details for sandbox' securitySeries
            };

           // foreach (var s in simulator.SecuritiesSnaps)
         //   {
         //       generalSeries.Points.AddXY(s.Date, s.PriceTotal);
//
          //  }

            chartView.Series.Add(generalSeries);
            
            foreach (var sandbox in simulator.Sandboxes)
            {
                
                var sandboxSeries = new Series(sandbox.Name)
                {
                    ChartType = SeriesChartType.Line,
                    YAxisType = AxisType.Secondary,
                    XAxisType = AxisType.Primary,
                    // TODO: fill in details for sandbox' securitySeries
                };

                foreach (var cash in sandbox.CashHistory)
                {
                    sandboxSeries.Points.Add(cash);
                }

                chartView.Series.Add(sandboxSeries);
            }
        }
    }
}
