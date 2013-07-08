using MarketSimulator.Components;
using MarketSimulator.Core;
using MarketSimulator.Events;
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
    /// StrategyView
    /// </summary>
    public partial class StrategyView : Form
    {
        private readonly StrategyExecutionSandbox sandbox;

        /// <summary>
        /// StrategyView
        /// </summary>
        public StrategyView(StrategyExecutionSandbox sandbox)
        {
            this.sandbox = sandbox;

            InitializeComponent();
        }

        /// <summary>
        /// AddLineAnnotation
        /// </summary>
        /// <param name="series"></param>
        /// <param name="index"></param>
        private void AddLineAnnotation(string series, int firstPoint, int secondPoint)
        {
            LineAnnotation annotation = new LineAnnotation();
            annotation.SetAnchor(chartStrategy.Series[series].Points[firstPoint], chartStrategy.Series[series].Points[secondPoint]);
            annotation.Height = 1;
            annotation.Width = 1;
            annotation.LineWidth = 1;
            annotation.StartCap = LineAnchorCapStyle.Round;
            annotation.EndCap = LineAnchorCapStyle.Round;
            chartStrategy.Annotations.Add(annotation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StrategyView_Load(object sender, EventArgs e)
        {
            propertyGridStrategy.SelectedObject = sandbox;

            var seriesNAV = new Series("NAV") { ChartType = SeriesChartType.Line, XValueType = ChartValueType.Date };
            var seriesTrades = new Series("Trades") { ChartType = SeriesChartType.Line, XValueType = ChartValueType.Date };

            int index = 0;
            Dictionary<DateTime, int> tickDateMap = new Dictionary<DateTime, int>();
            foreach (var snap in sandbox.StrategySnapshots)
            {
                var tmpDataPoint = new DataPoint(seriesNAV) { Tag = snap };
                tmpDataPoint.SetValueXY(snap.Date, snap.NAV);
                seriesNAV.Points.Add(tmpDataPoint);
                tickDateMap.Add(snap.Date, index++);
            }

            chartStrategy.Series.Add(seriesNAV);

            BuyEventArgs lastBuyEvent = null;

            foreach (var tick in sandbox.Strategy.StrategyTickHistory)
            {
                var snapshot = tick.StrategySnapshot;
                var positionData = snapshot.PositionData;
                var buyEvent = tick.BuyEventArgs;
                var sellEvent = tick.SellEventArgs;

                lastBuyEvent = tick.BuyEventArgs ?? lastBuyEvent;

                if (buyEvent != null && buyEvent.Shares > 0 && !buyEvent.Cancel)
                {
                    dataGridViewPositions.Rows.Add(buyEvent.Date, buyEvent.Symbol, buyEvent.TradeType, buyEvent.Shares, buyEvent.Price,
                        buyEvent.Price * buyEvent.Shares);
                }

                if (sellEvent != null && sellEvent.Shares > 0 && !sellEvent.Cancel)
                {
                    dataGridViewPositions.Rows.Add(sellEvent.Date, sellEvent.Symbol, sellEvent.TradeType, sellEvent.Shares, sellEvent.Price,
                        sellEvent.Price * sellEvent.Shares);

                    if (lastBuyEvent != null && !lastBuyEvent.Cancel)
                    {
                        AddLineAnnotation("NAV", tickDateMap[lastBuyEvent.Date],
                            tickDateMap[sellEvent.Date]);

                        lastBuyEvent = null;
                    }
                }
            }

            chartStrategy.Series.Add(seriesTrades);
            chartStrategy.UpdateAnnotations();

            R.GUI.ScrollDataGridForward(dataGridViewPositions);
        }
    }
}
