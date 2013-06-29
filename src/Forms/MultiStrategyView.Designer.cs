namespace MarketSimulator.Forms
{
    partial class MultiStrategyView
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiStrategyView));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageCharting = new System.Windows.Forms.TabPage();
            this.chartView = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tabControl.SuspendLayout();
            this.tabPageCharting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartView)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageCharting);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(636, 479);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageCharting
            // 
            this.tabPageCharting.Controls.Add(this.chartView);
            this.tabPageCharting.Location = new System.Drawing.Point(4, 22);
            this.tabPageCharting.Name = "tabPageCharting";
            this.tabPageCharting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCharting.Size = new System.Drawing.Size(628, 453);
            this.tabPageCharting.TabIndex = 0;
            this.tabPageCharting.Text = "Charting";
            this.tabPageCharting.UseVisualStyleBackColor = true;
            // 
            // chartView
            // 
            chartArea1.Name = "ChartArea1";
            this.chartView.ChartAreas.Add(chartArea1);
            this.chartView.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartView.Legends.Add(legend1);
            this.chartView.Location = new System.Drawing.Point(3, 3);
            this.chartView.Name = "chartView";
            this.chartView.Size = new System.Drawing.Size(622, 447);
            this.chartView.TabIndex = 2;
            this.chartView.Text = "chart1";
            // 
            // MultiStrategyView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 479);
            this.Controls.Add(this.tabControl);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MultiStrategyView";
            this.ShowInTaskbar = false;
            this.Text = "MultiStrategyView";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MultiStrategyView_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageCharting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageCharting;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartView;

    }
}