using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MarketSimulator.Core;

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
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// MarketData
        /// </summary>
        public List<MarketData> MarketData { get; private set; }

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
            MarketData = R.Convert(new YahooDataRetriever().Retrieve(toolStripTextBoxSecurity.Text));
            MarketData.Reverse();
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
            timer1.Start();
        }

        /// <summary>
        /// timer1_Tick
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            chart1.Series[0].Points.AddXY(MarketData[(int)tick].Date, MarketData[(int)tick].AsLine);
        }

        /// <summary>
        /// tick
        /// </summary>
        public long tick = 0;
    }
}
