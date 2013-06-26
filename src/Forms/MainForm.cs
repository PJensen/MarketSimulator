using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MarketSimulator.Core;
using MarketSimulator.Strategies;
using MarketSimulator.Controls;

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

            #region events

            // marketSimulatorWorker events
            marketSimulatorComponent.marketSimulatorWorker.ProgressChanged += marketSimulatorWorker_ProgressChanged;
            marketSimulatorComponent.marketSimulatorWorker.RunWorkerCompleted += marketSimulatorWorker_RunWorkerCompleted;

            // properties.settings
            Properties.Settings.Default.SettingsSaving += Default_SettingsSaving;
            Properties.Settings.Default.SettingsLoaded += Default_SettingsLoaded;

            #endregion

            #region Add known strategies
            // eventually they'll be loaded reflectively.
            AddStrategyNode(new RandomStrategy());
            AddStrategyNode(new RandomStrategy2());
            #endregion

            marketSimulatorComponent.AddStrategy(new RandomStrategy());
            marketSimulatorComponent.AddStrategy(new RandomStrategy2());
        }

        /// <summary>
        /// AddStrategyNode
        /// </summary>
        /// <param name="strategy"></param>
        private void AddStrategyNode(StrategyBase strategy)
        {
            checkedListBoxStrategies.Items.Add(strategy);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Default_SettingsLoaded(object sender, System.Configuration.SettingsLoadedEventArgs e)
        {
            SetStatus("Loaded settings!");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Default_SettingsSaving(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SetStatus(string.Format("Settings saved! {0}", DateTime.Now));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="e"></param>
        private void propertyGridExecSettings_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            SetStatus("Changed {0} to {1}", e.ChangedItem.PropertyDescriptor.DisplayName, e.OldValue.ToString());
        }

        /// <summary>
        /// LockGUI
        /// </summary>
        public void LockGUI()
        {
            toolStripTextBoxTicker.ReadOnly = true;
            propertyGridExecSettings.Enabled = false;
        }

        /// <summary>
        /// UnLockGUI
        /// </summary>
        public void UnLockGUI()
        {
            toolStripTextBoxTicker.ReadOnly = false;
            propertyGridExecSettings.Enabled = true;
        }

        /// <summary>
        /// SetStatus
        /// </summary>
        /// <param name="message"></param>
        public void SetStatus(string frmt, params object[] argv)
        {
            toolStripStatusLabelStatus.Text = string.Format(frmt, argv);
        }

        /// <summary>
        /// toolStripButtonGo_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonGo_Click(object sender, EventArgs e)
        {
            var tmpCursor = Cursor;
            Cursor = Cursors.WaitCursor;
            string tmpInitMessage = string.Empty;

            foreach (var s in checkedListBoxStrategies.CheckedItems)
            {
                marketSimulatorComponent.AddStrategy((StrategyBase)s);
            }

            if (marketSimulatorComponent.marketSimulatorWorker.IsBusy)
            {
                SetStatus("Busy!");
            }
            else if (marketSimulatorComponent.Initialize(null))
            {
                LockGUI();

                if (GlobalExecutionSettings.Instance.StartDate < marketSimulatorComponent.SecurityMaster.MinimumDate)
                {
                    GlobalExecutionSettings.Instance.StartDate = marketSimulatorComponent.SecurityMaster.MinimumDate;
                }

                if (GlobalExecutionSettings.Instance.EndDate > marketSimulatorComponent.SecurityMaster.MaximumDate)
                {
                    GlobalExecutionSettings.Instance.EndDate = marketSimulatorComponent.SecurityMaster.MaximumDate;
                }

                marketSimulatorComponent.marketSimulatorWorker.RunWorkerAsync();
            }
            else
            {
                toolStripStatusLabelStatus.Text = tmpInitMessage;
            }

            Cursor = tmpCursor;
        }

        /// <summary>
        /// marketSimulatorWorker_RunWorkerCompleted changes the text of the status bar.
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">run worker completed event arguments</param>
        void marketSimulatorWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            UnLockGUI();

            string tmpStatusMessage = "Completed!";

            if (e.Cancelled)
            {
                tmpStatusMessage = "Cancelled!";
            }
            else if (e.Error != null)
            {
                tmpStatusMessage = e.Error.Message;
            }
            else
            {
                toolStripProgressBarMain.Value = 100;
            }

            SetStatus(tmpStatusMessage);

            marketSimulatorComponent.Sandboxes.Sort();
            flowLayoutPanelMain.Controls.Add(new MultiStrategyView(marketSimulatorComponent) { Visible = true, TopLevel = false });
            foreach (var sandbox in marketSimulatorComponent.Sandboxes)
            {
                flowLayoutPanelMain.Controls.Add(new StrategyExecutionSandboxControl(sandbox));
            }


        }

        /// <summary>
        /// marketSimulatorWorker_ProgressChanged event wired to the progress bar
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        void marketSimulatorWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (toolStripProgressBarMain != null && toolStripProgressBarMain.Value > 0)
            {
                toolStripProgressBarMain.Value = e.ProgressPercentage;
            }

            SetStatus((e.UserState ?? string.Empty).ToString());
        }

        /// <summary>
        /// MainForm_Load
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            // set text value to whatever was previously loaded or just APPL
            toolStripTextBoxTicker.Text = Properties.Settings.Default.Security ?? "AAPL";

            // load auto complete ticker source from previous runs.
            foreach (var previousSecurity in Properties.Settings.Default.PreviousSecurities)
                toolStripTextBoxTicker.AutoCompleteCustomSource.Add(previousSecurity);

            // Get user default settings
            propertyGridExecSettings.SelectedObject = GlobalExecutionSettings.Instance;
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
        /// aboutToolStripMenuItem_Click
        /// </summary>
        /// <param name="sender">event args</param>
        /// <param name="e">event</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
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

        /// <summary>
        /// toolStripButtonAddTicker_Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButtonAddTicker_Click(object sender, EventArgs e)
        {
            if (GlobalExecutionSettings.Instance.AddTicker(toolStripTextBoxTicker.Text))
            {
                SetStatus("Added: {0}", toolStripTextBoxTicker.Text);
            }
        }
    }
}
