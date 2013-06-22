namespace MarketSimulator.Components
{
    partial class MarketSimulatorComponent
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.marketSimulatorWorker = new System.ComponentModel.BackgroundWorker();
            // 
            // marketSimulatorWorker
            // 
            this.marketSimulatorWorker.WorkerReportsProgress = true;
            this.marketSimulatorWorker.WorkerSupportsCancellation = true;
            this.marketSimulatorWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.marketSimulatorWorker_DoWork);
            this.marketSimulatorWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.marketSimulatorWorker_ProgressChanged);
            this.marketSimulatorWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.marketSimulatorWorker_RunWorkerCompleted);

        }

        #endregion

        public System.ComponentModel.BackgroundWorker marketSimulatorWorker;

    }
}
