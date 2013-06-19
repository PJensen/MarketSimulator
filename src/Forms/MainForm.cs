using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Core;
using MarketSimulator.Strategies;

namespace MarketSimulator.Forms
{
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// MainForm
        /// </summary>
        public MainForm(MarketSimulator marketSimulator)
        {
            InitializeComponent();

            MarketSimulator = marketSimulator;
            MarketSimulator.CurrentStrategy = new Issue12Strategy();
            MarketTick += MarketSimulator.CurrentStrategy.MarketTick;
            MarketTick += MainForm_MarketTick;
            MarketSimulator.CurrentStrategy.BuyEvent += CurrentStrategy_BuyEvent;
            MarketSimulator.CurrentStrategy.SellEvent += CurrentStrategy_SellEvent;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentStrategy_SellEvent(object sender, Events.SellEventArgs e)
        {
            MarketSimulator.OnSellEvent(e);

            if (e.Cancel)
                return;

            richTextBox1.Text += "Sold " + e.Shares + Environment.NewLine;


            var tradeValuation = e.MarketData.Close * e.Shares;

            dataGridViewPositions.Rows.Add("SELL", e.Shares, e.MarketData.Close, tradeValuation);

            var tradePoint = chart1.Series["Trade"].Points.Count;

            chart1.Series["Trade"].Points.AddXY(MarketSimulator.MarketData[Tick].Date, e.MarketData.Close);
            chart1.Series["Trade"].Points[tradePoint].MarkerStyle = MarkerStyle.Square;
            chart1.Series["Trade"].Points[tradePoint].Tag = MarketSimulator.Balance;
            chart1.Series["Trade"].Points[tradePoint].MarkerSize = (int)Math.Log(e.Shares);

            chart1.Series["Trade"].Points[tradePoint].Color = MarketSimulator.MadeMoney()
                ? System.Drawing.Color.Green : System.Drawing.Color.Red;

            if (MarketSimulator.Shares <= 0)
            {
                tradePoint = chart1.Series["Trade"].Points.Count;
                chart1.Series["Trade"].Points.AddXY(MarketSimulator.MarketData[Tick].Date, new object[] { DBNull.Value });
                chart1.Series["Trade"].Points[tradePoint].IsEmpty = true;
            }

            if (MarketSimulator.Shares <= 0)
                MarketSimulator.ActiveTradeString.Clear();

            ScrollPositionsForward();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CurrentStrategy_BuyEvent(object sender, Events.BuyEventArgs e)
        {
            MarketSimulator.OnBuyEvent(e);

            if (e.Cancel)
                return;

            richTextBox1.Text += "Bought " + e.Shares + Environment.NewLine;

            var tradeValuation = e.MarketData.Close * e.Shares;

            dataGridViewPositions.Rows.Add("BUY", e.Shares, e.MarketData.Close, tradeValuation);

            var tradePoint = chart1.Series["Trade"].Points.Count;

            chart1.Series["Trade"].Points.AddXY(MarketSimulator.MarketData[Tick].Date, e.MarketData.Close);

            chart1.Series["Trade"].Points[tradePoint].MarkerStyle = MarkerStyle.Diamond;
            chart1.Series["Trade"].Points[tradePoint].Color = System.Drawing.Color.Orange;
            chart1.Series["Trade"].Points[tradePoint].MarkerSize = (int)Math.Log(e.Shares);
            chart1.Series["Trade"].Points[tradePoint].Tag = MarketSimulator.Balance;





            ScrollPositionsForward();
        }

        double SlopeAt(DataPointCollection points, int index)
        {
            if (points.Count <= 0)
                return double.NaN;

            return points[index].YValues[0] / points[index - 1].YValues[0];
        }

        /// <summary>
        /// ScrollPositionsForward
        /// </summary>
        private void ScrollPositionsForward()
        {
            dataGridViewPositions.FirstDisplayedScrollingRowIndex =
                dataGridViewPositions.Rows.Count - 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_MarketTick(object sender, MarketTickEventArgs e)
        {
            toolStripLabelCurrentPrice.Text =
                string.Format("Cash:{0} Shares:{1}, Paper Value: {2}",
                MarketSimulator.Cash, MarketSimulator.Shares, MarketSimulator.PaperValue);

            chart1.Series["NAV"].Points.AddXY(MarketSimulator.MarketData[Tick].Date, MarketSimulator.PaperValue + MarketSimulator.Cash);

            propertyGrid1.SelectedObject = MarketSimulator.Instance;
            MarketSimulator.OnTickEvent(e);
        }

        /// <summary>
        /// MarketSimulator
        /// </summary>
        private MarketSimulator MarketSimulator { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            toolStripTextBoxSecurity.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            foreach (var previousSecurity in Properties.Settings.Default.PreviousSecurities)
                toolStripTextBoxSecurity.AutoCompleteCustomSource.Add(previousSecurity);

            var message = string.Empty;
            var fail = MarketSimulator.Instance.LoadMarketData(Properties.Settings.Default.Security, out message);
            toolStripTextBoxSecurity.Text = fail ? message : Properties.Settings.Default.Security;
        }


        /// <summary>
        /// workingDirectoryToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void workingDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new WorkingDirectoryForm().ShowDialog(this);
        }

        /// <summary>
        /// toolStripTextBoxSecurity_TextChanged
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void toolStripTextBoxSecurity_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Security = toolStripTextBoxSecurity.Text;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// aboutToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        /// <summary>
        /// rSIToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void rSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Properties.Settings.Default.PreviousSecurities.Contains(toolStripTextBoxSecurity.Text))
                Properties.Settings.Default.PreviousSecurities.Add(toolStripTextBoxSecurity.Text);
            timerMain.Start();
        }

        private void Start()
        {
            timerMain.Enabled = true;
        }

        private void Stop()
        {
            timerMain.Enabled = false;
        }

        /// <summary>
        /// timer1_Tick
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            Tick++;

            if (Tick > MarketSimulator.MarketData.Count)
            {
                Stop();

                return;
            }

            chart1.Series[0].Points.AddXY(MarketSimulator.MarketData[Tick].Date,
                                          MarketSimulator.MarketData[(int)Tick].AsLine);

            chart1.Series["Cash"].Points.AddXY(MarketSimulator.MarketData[Tick].Date,
                                               MarketSimulator.Cash);


            toolStripLabelCurrentPrice.Text =
                MarketSimulator.MarketData[Tick].Close.ToString(CultureInfo.InvariantCulture);

            if ((int)MarketSimulator.MarketData[Tick].High > toolStripProgressBarPriceMax.Maximum)
                toolStripProgressBarPriceMax.Maximum = (int)MarketSimulator.MarketData[Tick].High;

            if ((int)MarketSimulator.MarketData[Tick].Low < toolStripProgressBarPriceMax.Minimum)
                toolStripProgressBarPriceMax.Minimum = (int)MarketSimulator.MarketData[Tick].Low;

            toolStripProgressBarPriceMax.Value = (int)MarketSimulator.MarketData[Tick].Close;

            var tmpMarketTickEventArgs = new MarketTickEventArgs(MarketSimulator.MarketData[Tick]);

            if (TickOffset > 14)
            {
                chart1.DataManipulator.FinancialFormula(FinancialFormula.RelativeStrengthIndex, "14", chart1.Series["Series1"],
                    chart1.Series["RelativeStrengthIndex"]);
                tmpMarketTickEventArgs.RSI = chart1.Series["RelativeStrengthIndex"].Points[RSIOffset].YValues[0];
                RSIOffset++;
            }

            if (TickOffset > 13)
            {
                chart1.DataManipulator.FinancialFormula(FinancialFormula.ExponentialMovingAverage, "13", chart1.Series["Series1"], chart1.Series["EMA13"]);
                tmpMarketTickEventArgs.EMA13 = chart1.Series["EMA13"].Points[EMA13Offset].YValues[0];
                EMA13Offset++;
            }

            if (TickOffset > 26)
            {
                chart1.DataManipulator.FinancialFormula(FinancialFormula.MovingAverageConvergenceDivergence, "12,26", chart1.Series["Series1"], chart1.Series["MACD"]);
                tmpMarketTickEventArgs.MACDHistogram = chart1.Series["MACD"].Points[MACDOffset].YValues[0];
                MACDOffset++;
            }

            TickOffset++;

            if (MarketTick != null)
                MarketTick(this, tmpMarketTickEventArgs);
        }

        int RSIOffset = 0;
        int MACDOffset = 0;
        int EMA13Offset = 0;

        int TickOffset = 0;

        /// <summary>
        /// MarketTick
        /// </summary>
        private event EventHandler<MarketTickEventArgs> MarketTick;

        /// <summary>
        /// Tick
        /// </summary>
        public int Tick
        {
            get { return MarketSimulator.Tick; }
            set { MarketSimulator.Tick = value; }
        }

        /// <summary>
        /// exitToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataRetrievalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// MainForm_FormClosing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!R.ExitConfirmation)
                return;

            var frmQuit = new QuitDialog();
            var dialogResult = frmQuit.ShowDialog(this);
            var @checked = frmQuit.checkBoxDontAsk.Checked;

            e.Cancel = (dialogResult == System.Windows.Forms.DialogResult.Cancel);

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                R.ExitConfirmation = !@checked;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
