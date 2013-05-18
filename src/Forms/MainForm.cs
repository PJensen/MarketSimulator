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
            MarketSimulator.CurrentStrategy = new PeteStrategy();
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

            dataGridViewPositions.Rows.Add("SELL", e.Shares, e.MarketData.Close, e.MarketData.Close * e.Shares);
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

            dataGridViewPositions.Rows.Add("BUY", e.Shares, e.MarketData.Close, e.MarketData.Close * e.Shares);

            ScrollPositionsForward();
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

            var tmpMarketTickEventArgs = new MarketTickEventArgs { marketData = MarketSimulator.MarketData[Tick] };
            try
            {

                chart1.DataManipulator.FinancialFormula(FinancialFormula.RelativeStrengthIndex, chart1.Series["Series1"],
                    chart1.Series["RelativeStrengthIndex"]);

                tmpMarketTickEventArgs.RSI = chart1.Series["RelativeStrengthIndex"].Points[Tick - 11].YValues[0];



            }
            catch
            {
            }

            if (MarketTick != null)
                MarketTick(this, tmpMarketTickEventArgs);
        }

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
    }
}
