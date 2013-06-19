namespace MarketSimulator
{
    partial class StrategyControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panelStrategy = new System.Windows.Forms.Panel();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.textBoxTicker = new System.Windows.Forms.TextBox();
            this.panelStrategy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStrategy
            // 
            this.panelStrategy.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStrategy.Controls.Add(this.chart1);
            this.panelStrategy.Controls.Add(this.textBoxTicker);
            this.panelStrategy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStrategy.Location = new System.Drawing.Point(0, 0);
            this.panelStrategy.Name = "panelStrategy";
            this.panelStrategy.Size = new System.Drawing.Size(292, 223);
            this.panelStrategy.TabIndex = 0;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(0, 0);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(290, 201);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chartStrategy";
            // 
            // textBoxTicker
            // 
            this.textBoxTicker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxTicker.Location = new System.Drawing.Point(0, 201);
            this.textBoxTicker.Name = "textBoxTicker";
            this.textBoxTicker.Size = new System.Drawing.Size(290, 20);
            this.textBoxTicker.TabIndex = 2;
            // 
            // StrategyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelStrategy);
            this.Name = "StrategyControl";
            this.Size = new System.Drawing.Size(292, 223);
            this.panelStrategy.ResumeLayout(false);
            this.panelStrategy.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelStrategy;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBoxTicker;


    }
}
