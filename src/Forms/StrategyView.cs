using MarketSimulator.Components;
using MarketSimulator.Core;
using MarketSimulator.Core.Indicators;
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
        private void AddLineAnnotation(string series, int firstPoint, int secondPoint, Func<Color> coloring)
        {
            LineAnnotation annotation = new LineAnnotation();
            annotation.SetAnchor(chartStrategy.Series[series].Points[firstPoint], chartStrategy.Series[series].Points[secondPoint]);
            annotation.Height = 1;
            annotation.Width = 1;
            annotation.LineWidth = 1;
            annotation.StartCap = LineAnchorCapStyle.Round;
            annotation.EndCap = LineAnchorCapStyle.Round;
            annotation.LineColor = coloring();
            chartStrategy.Annotations.Add(annotation);
        }

        private void AddArrowAnnotation(string series, int firstPoint, Func<Color> coloring)
        {
            LineAnnotation annotation = new LineAnnotation();
            annotation.AnchorDataPoint = chartStrategy.Series[series].Points[firstPoint];
            
            annotation.Height = -2;
            annotation.Width = 0;
            annotation.LineWidth = 1;
            annotation.StartCap = LineAnchorCapStyle.Arrow;
            annotation.EndCap = LineAnchorCapStyle.None;
            annotation.LineColor = coloring();
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

            // TODO: Generalize and refactor if possible.
            if (sandbox.Strategy.HasTechnical<SMA50, double>())
            {
                var sma50Historical = sandbox.Strategy.GetTechnical<SMA50, double>().Historical;

                var seriesSMA50 = new Series("SMA 50")
                {
                    ChartType = SeriesChartType.Line,
                    XValueType = ChartValueType.Date,
                    YAxisType = AxisType.Secondary
                };

                foreach (var sma50Tick in sma50Historical)
                {
                    seriesSMA50.Points.AddXY(sma50Tick.Key, sma50Tick.Value);
                }

                chartStrategy.Series.Add(seriesSMA50);
            }

            var lastBuyEvents = new List<BuyEventArgs>();

            foreach (var tick in sandbox.Strategy.StrategyTickHistory)
            {
                var snapshot = tick.StrategySnapshot;
                var positionData = snapshot.PositionData;
                var buyEvent = tick.BuyEventArgs;
                var sellEvent = tick.SellEventArgs;

                if (sellEvent != null && sellEvent.Shares > 0 && !sellEvent.Cancel)
                {
                    if (lastBuyEvents.Where(b => sellEvent.Symbol.Equals(b.Symbol)).Count() <= 0)
                        continue;

                    var purchasedMarketValue = 0.0d;
                    var purchasedShares = 0;
                    var soldMarketValue = sellEvent.Shares * sellEvent.Price;

                    purchasedMarketValue = lastBuyEvents.Where(b => sellEvent.Symbol.Equals(b.Symbol)).Sum(b => b.Price * b.Shares);
                    purchasedShares = lastBuyEvents.Where(b => sellEvent.Symbol.Equals(b.Symbol)).Sum(b => b.Shares);

                    var remainingShares = purchasedShares - sellEvent.Shares;
                    var tmpBuy = lastBuyEvents.Where(b => sellEvent.Symbol.Equals(b.Symbol)).OrderBy(b => b.Date).Last();
                    var remainingMarketValue = remainingShares * tmpBuy.Price;
                    var totalProfit = soldMarketValue - ((purchasedMarketValue - remainingMarketValue));

                    foreach (var b in lastBuyEvents.Where(b => sellEvent.Symbol.Equals(b.Symbol)))
                    {
                        AddArrowAnnotation("NAV", tickDateMap[b.Date], () => { return Color.Black; });
                    }

                    lastBuyEvents.Clear();

                    if (remainingShares > 0)
                    {
                        lastBuyEvents.Add(new BuyEventArgs(tick.MarketTickEventArgs, remainingShares)
                        {
                            Price = tmpBuy.Price,
                        });
                    }

                    AddLineAnnotation("NAV", tickDateMap[tmpBuy.Date],
                        tickDateMap[sellEvent.Date], () => { return totalProfit > 0 ? Color.Green : Color.Red; });
                }

                if (buyEvent != null && !buyEvent.Cancel && buyEvent.Shares > 0)
                {
                    lastBuyEvents.Add(buyEvent);
                }
            }

            /*
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

                    if (lastBuyEvent != null && !lastBuyEvent.Cancel && lastBuyEvent.Symbol.Equals(buyEvent.Symbol))
                    {
                        AddLineAnnotation("NAV", tickDateMap[lastBuyEvent.Date],
                            tickDateMap[sellEvent.Date]);

                        lastBuyEvent = null;
                    }
                }
            }*/

            chartStrategy.Series.Add(seriesTrades);
            chartStrategy.UpdateAnnotations();

            R.GUI.ScrollDataGridForward(dataGridViewPositions);
        }
    }
}
