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
        public MainForm()
        {
            InitializeComponent();

            toolStripTextBoxTicker.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            toolStripTextBoxTicker.AutoCompleteSource = AutoCompleteSource.CustomSource;
            toolStripTextBoxTicker.AutoCompleteCustomSource = new AutoCompleteStringCollection();

            marketSimulatorComponent.marketSimulatorWorker.ProgressChanged += marketSimulatorWorker_ProgressChanged;
            marketSimulatorComponent.marketSimulatorWorker.RunWorkerCompleted += marketSimulatorWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// toolStripButtonGo_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            string tmpInitMessage = string.Empty;

            if (marketSimulatorComponent.Initialize(toolStripTextBoxTicker.Text, out tmpInitMessage))
            {
                marketSimulatorComponent.marketSimulatorWorker.RunWorkerAsync();
            }
            else 
            {
                toolStripStatusLabelWorker.Text = tmpInitMessage;
            }
        }

        /// <summary>
        /// marketSimulatorWorker_RunWorkerCompleted changes the text of the status bar.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">run worker completed event arguments</param>
        void marketSimulatorWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            string tmpStatusMessage = "Completed!";

            if (e.Cancelled)
            {
                tmpStatusMessage = "Cancelled!";
            }
            else if (e.Error != null)
            {
                tmpStatusMessage = e.Error.Message;
            }

            toolStripStatusLabelWorker.Text = tmpStatusMessage;
        }

        /// <summary>
        /// marketSimulatorWorker_ProgressChanged event wired to the progress bar
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        void marketSimulatorWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            toolStripProgressBarMain.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // load auto complete ticker source from previous runs.
            foreach (var previousSecurity in Properties.Settings.Default.PreviousSecurities)
                toolStripTextBoxTicker.AutoCompleteCustomSource.Add(previousSecurity);

            var fail = false;
            var message = string.Empty;

            //var fail = MarketSimulator.Instance.LoadMarketData(Properties.Settings.Default.Security, out message);
           // toolStripTextBoxSecurity.Text = fail ? message : Properties.Settings.Default.Security;
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
            //Properties.Settings.Default.Security = toolStripTextBoxSecurity.Text;
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
            //if (!Properties.Settings.Default.PreviousSecurities.Contains(toolStripTextBoxSecurity.Text))
             //   Properties.Settings.Default.PreviousSecurities.Add(toolStripTextBoxSecurity.Text);
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
    }
}
