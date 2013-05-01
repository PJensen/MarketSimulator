using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MarketSimulator.Forms
{
    /// <summary>
    /// WorkingDirectoryForm
    /// </summary>
    public partial class WorkingDirectoryForm : Form
    {
        /// <summary>
        /// WorkingDirectoryForm
        /// </summary>
        public WorkingDirectoryForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// textBoxWorkingDirectory_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event</param>
        private void textBoxWorkingDirectory_Click(object sender, EventArgs e)
        {
            switch (folderBrowserDialog.ShowDialog(this))
            {
                case DialogResult.OK:
                    R.WorkingDirectory = folderBrowserDialog.SelectedPath;
                    break;
            }
        }

        /// <summary>
        /// WorkingDirectoryForm_Load
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event</param>
        private void WorkingDirectoryForm_Load(object sender, EventArgs e)
        {
            folderBrowserDialog.SelectedPath = BestDirectory(R.CurrentDirectory, R.WorkingDirectory);
            textBoxWorkingDirectory.Text = R.WorkingDirectory;
        }

        /// <summary>
        /// buttonAccept_Click
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event</param>
        private void buttonAccept_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// WorkingDirectoryForm_KeyUp
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void WorkingDirectoryForm_KeyUp(object sender, KeyEventArgs e)
        {
            handleKeyPress(e);
        }

        /// <summary>
        /// textBoxWorkingDirectory_KeyUp
        /// </summary>
        /// <param name="sender">event sender</param>
        /// <param name="e">event args</param>
        private void textBoxWorkingDirectory_KeyUp(object sender, KeyEventArgs e)
        {
            handleKeyPress(e);
        }

        /// <summary>
        /// handleKeyPress
        /// </summary>
        /// <param name="e"></param>
        private void handleKeyPress(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
        }

        /// <summary>
        /// BestDirectory
        /// </summary>
        /// <param name="default"></param>
        /// <param name="argv"></param>
        /// <returns></returns>
        private static string BestDirectory(string @default, params string[] argv)
        {
            foreach (var s in argv.Where(Directory.Exists))
                return s;
            return @default;
        }
    }
}
