namespace PowerSystem.Forms
{
    partial class ChartForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.vchart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.eChart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.vchart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eChart1)).BeginInit();
            this.SuspendLayout();
            // 
            // vchart1
            // 
            chartArea3.Name = "ChartArea1";
            this.vchart1.ChartAreas.Add(chartArea3);
            legend3.BorderWidth = 3;
            legend3.DockedToChartArea = "ChartArea1";
            legend3.Name = "Legend1";
            this.vchart1.Legends.Add(legend3);
            this.vchart1.Location = new System.Drawing.Point(13, 24);
            this.vchart1.Name = "vchart1";
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "电压";
            this.vchart1.Series.Add(series3);
            this.vchart1.Size = new System.Drawing.Size(802, 300);
            this.vchart1.TabIndex = 0;
            this.vchart1.Text = "chart1";
            // 
            // eChart1
            // 
            chartArea4.Name = "ChartArea1";
            this.eChart1.ChartAreas.Add(chartArea4);
            legend4.BorderWidth = 3;
            legend4.DockedToChartArea = "ChartArea1";
            legend4.Name = "Legend1";
            this.eChart1.Legends.Add(legend4);
            this.eChart1.Location = new System.Drawing.Point(13, 330);
            this.eChart1.Name = "eChart1";
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Red;
            series4.Legend = "Legend1";
            series4.Name = "电流";
            this.eChart1.Series.Add(series4);
            this.eChart1.Size = new System.Drawing.Size(802, 300);
            this.eChart1.TabIndex = 1;
            this.eChart1.Text = "chart1";
            // 
            // ChartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 701);
            this.Controls.Add(this.eChart1);
            this.Controls.Add(this.vchart1);
            this.Name = "ChartForm";
            this.Text = "电压电流汇总";
            ((System.ComponentModel.ISupportInitialize)(this.vchart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eChart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart vchart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart eChart1;
    }
}