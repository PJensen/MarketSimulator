using MarketSimulator.Components;
using MarketSimulator.Core;
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
            //annotation.AnchorDataPoint = chartStrategy.Series[series].Points[firstPoint];
            annotation.SetAnchor(chartStrategy.Series[series].Points[firstPoint], chartStrategy.Series[series].Points[secondPoint]);
            annotation.Height = -2;
            annotation.Width = -2;
            annotation.LineWidth = 1;
            annotation.StartCap = LineAnchorCapStyle.Arrow;
            annotation.EndCap = LineAnchorCapStyle.None;
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

            foreach (var tick in sandbox.StrategyTickHistory)
            {
                var snapshot = tick.StrategySnapshot;
                var positionData = snapshot.PositionData;
                var buyEvent = tick.BuyEventArgs;
                var sellEvent = tick.SellEventArgs;

                if (buyEvent != null && buyEvent.Shares > 0)
                {
                    dataGridViewPositions.Rows.Add(buyEvent.Date, buyEvent.Symbol, buyEvent.TradeType, buyEvent.Shares, buyEvent.Price,
                        buyEvent.Price * buyEvent.Shares, Properties.Resources.table_add);

                    var buyPoint = new DataPoint(seriesTrades) { Tag = buyEvent, MarkerStyle = MarkerStyle.Triangle, ToolTip = buyEvent.Shares.ToString() };
                    buyPoint.SetValueXY(buyEvent.Date, buyEvent.Shares * buyEvent.Price);
                    seriesTrades.Points.Add(buyPoint);
                }

                if (sellEvent != null && sellEvent.Shares > 0)
                {
                    dataGridViewPositions.Rows.Add(sellEvent.Date, sellEvent.Symbol, sellEvent.TradeType, sellEvent.Shares, sellEvent.Price,
                        sellEvent.Price * sellEvent.Shares, sellEvent.Cancel ? Properties.Resources.coins_delete : Properties.Resources.coins);

                    var sellPoint = new DataPoint(seriesTrades) { Tag = sellEvent, MarkerStyle = MarkerStyle.Triangle, ToolTip = sellEvent.Shares.ToString() };
                    sellPoint.SetValueXY(sellEvent.Date, sellEvent.Shares * sellEvent.Price);
                    sellPoint.Color = Color.Green;
                    seriesTrades.Points.Add(sellPoint);
                }
            }

            chartStrategy.Series.Add(seriesTrades);

            chartStrategy.UpdateAnnotations();
        }
    }
}
