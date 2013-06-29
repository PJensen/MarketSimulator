namespace MarketSimulator.Controls
{
    partial class StrategyExecutionSandboxControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartSandbox = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.propertyGridSandbox = new System.Windows.Forms.PropertyGrid();
            this.contextMenuStripSandbox = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.chartSandbox)).BeginInit();
            this.contextMenuStripSandbox.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartSandbox
            // 
            chartArea6.Name = "ChartArea1";
            this.chartSandbox.ChartAreas.Add(chartArea6);
            this.chartSandbox.Dock = System.Windows.Forms.DockStyle.Top;
            legend6.Name = "Legend1";
            this.chartSandbox.Legends.Add(legend6);
            this.chartSandbox.Location = new System.Drawing.Point(0, 0);
            this.chartSandbox.Name = "chartSandbox";
            this.chartSandbox.Size = new System.Drawing.Size(419, 191);
            this.chartSandbox.TabIndex = 0;
            this.chartSandbox.Text = "chartSandbox";
            // 
            // propertyGridSandbox
            // 
            this.propertyGridSandbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridSandbox.Location = new System.Drawing.Point(0, 191);
            this.propertyGridSandbox.Name = "propertyGridSandbox";
            this.propertyGridSandbox.Size = new System.Drawing.Size(419, 196);
            this.propertyGridSandbox.TabIndex = 1;
            // 
            // contextMenuStripSandbox
            // 
            this.contextMenuStripSandbox.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStripSandbox.Name = "contextMenuStripSandbox";
            this.contextMenuStripSandbox.Size = new System.Drawing.Size(153, 48);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Image = global::MarketSimulator.Properties.Resources.chart_bar_delete;
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.removeToolStripMenuItem.Text = "RemovePosition";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // StrategyExecutionSandboxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ContextMenuStrip = this.contextMenuStripSandbox;
            this.Controls.Add(this.propertyGridSandbox);
            this.Controls.Add(this.chartSandbox);
            this.DoubleBuffered = true;
            this.Name = "StrategyExecutionSandboxControl";
            this.Size = new System.Drawing.Size(419, 387);
            this.Load += new System.EventHandler(this.StrategyExecutionSandboxControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartSandbox)).EndInit();
            this.contextMenuStripSandbox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartSandbox;
        private System.Windows.Forms.PropertyGrid propertyGridSandbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripSandbox;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}
