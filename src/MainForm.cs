using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MarketSimulator
{
    public partial class MainForm : Form
    {
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
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripTextBoxSecurity.Text = Properties.Settings.Default.Security;
            MarketData = new YahooDataRetriever().Retrieve(toolStripTextBoxSecurity.Text)
                ;
            MarketData.Reverse();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void marketDataBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var aboutBox = new AboutBox();
            aboutBox.Show();
        }

        private void toolStripTextBoxSecurity_TextChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.Security = toolStripTextBoxSecurity.Text;
            Properties.Settings.Default.Save();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void rSIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        public long tick = 0;

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;

            chart1.Series[0].Points.AddXY(MarketData[(int)tick].Date, MarketData[(int)tick].AsLine);
        }

    }
}
