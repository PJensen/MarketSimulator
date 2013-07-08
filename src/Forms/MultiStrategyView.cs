﻿using MarketSimulator.Components;
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
            Dictionary<DateTime, int> tickDateMap = new Dictionary<DateTime, int>();

            foreach (var sandbox in simulator.Sandboxes)
            {
                var seriesSandbox = new Series(sandbox.Name)
                {
                    ChartType = SeriesChartType.Area,
                    XValueType = ChartValueType.Date 
                };

                int index = 0;
                foreach (var snap in sandbox.StrategySnapshots)
                {
                    var tmpDataPoint = new DataPoint(seriesSandbox) { Tag = snap };
                    tmpDataPoint.SetValueXY(snap.Date, snap.NAV);
                    seriesSandbox.Points.Add(tmpDataPoint);

                    if (!tickDateMap.ContainsKey(snap.Date))
                    {
                        tickDateMap.Add(snap.Date, index++);
                    }
                }

                chartView.Series.Add(seriesSandbox);
            }
        }
    }
}
