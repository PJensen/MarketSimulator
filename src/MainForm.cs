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
        public DataTable MarketData { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripTextBoxSecurity.Text = Properties.Settings.Default.Security;
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
            var table = new YahooDataRetriever().Retrieve(toolStripTextBoxSecurity.Text);

            foreach (var marketData in table)
            {
                chart1.Series[0].Points.AddXY(marketData.Date, marketData.AsLine);

            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
