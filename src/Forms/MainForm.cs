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
            MarketSimulator.CurrentStrategy = new RSIStrategy();
            MarketSimulator.CurrentStrategy.BuyEvent += new EventHandler<MarketTickEventArgs>(CurrentStrategy_BuyEvent);
            MarketSimulator.CurrentStrategy.sellEvent += new EventHandler<MarketTickEventArgs>(CurrentStrategy_sellEvent);
            MarketTick += MarketSimulator.CurrentStrategy.MarketTick;
            MarketTick += MainForm_MarketTick;
            
        }

        void CurrentStrategy_sellEvent(object sender, MarketTickEventArgs e)
        {
            richTextBox1.Text += string.Format("Sold {0} at {1}, Balance: {2}\n", MarketSimulator.Instance.Shares, e.marketData.Close, MarketSimulator.Instance.Balance);
        }

        void CurrentStrategy_BuyEvent(object sender, MarketTickEventArgs e)
        {
            richTextBox1.Text += string.Format("Bought one at {0}, Balance: {1}\n", e.marketData.Close, MarketSimulator.Instance.Balance);
        }

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MainForm_MarketTick(object sender, MarketTickEventArgs e)
        {
            toolStripLabelCurrentPrice.Text =
                string.Format("Balance:{0} Shares:{1}",
                MarketSimulator.Balance, MarketSimulator.Shares);

            chart1.Series["NAV"].Points.AddXY(MarketSimulator.MarketData[tick].Date, MarketSimulator.Balance);
            propertyGrid1.SelectedObject = MarketSimulator.Instance.CurrentStrategy;
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

            toolStripTextBoxSecurity.Text = Properties.Settings.Default.Security;
            MarketSimulator.MarketData = R.Convert(new YahooDataRetriever().Retrieve(toolStripTextBoxSecurity.Text));
            MarketSimulator.MarketData.Reverse();

            
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

        /// <summary>
        /// timer1_Tick
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;

            chart1.Series[0].Points.AddXY(MarketSimulator.MarketData[tick].Date,
                                          MarketSimulator.MarketData[(int)tick].AsLine);

            toolStripLabelCurrentPrice.Text =
                MarketSimulator.MarketData[tick].Close.ToString(CultureInfo.InvariantCulture);

            if ((int)MarketSimulator.MarketData[tick].High > toolStripProgressBarPriceMax.Maximum)
                toolStripProgressBarPriceMax.Maximum = (int)MarketSimulator.MarketData[tick].High;

            if ((int)MarketSimulator.MarketData[tick].Low < toolStripProgressBarPriceMax.Minimum)
                toolStripProgressBarPriceMax.Minimum = (int)MarketSimulator.MarketData[tick].Low;

            toolStripProgressBarPriceMax.Value = (int)MarketSimulator.MarketData[tick].Close;

            var tmpMarketTickEventArgs = new MarketTickEventArgs { marketData = MarketSimulator.MarketData[tick] };
            try
            {

                chart1.DataManipulator.FinancialFormula(FinancialFormula.RelativeStrengthIndex, chart1.Series["Series1"],
                    chart1.Series["RelativeStrengthIndex"]);

                tmpMarketTickEventArgs.RSI = chart1.Series["RelativeStrengthIndex"].Points[tick - 11].YValues[0];


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
        /// tick
        /// </summary>
        public int tick = 0;
    }
}
