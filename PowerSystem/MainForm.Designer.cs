﻿namespace PowerSystem
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.port = new System.Windows.Forms.NumericUpDown();
            this.ipAddress = new IPAddressControlLib.IPAddressControl();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.eleChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.volChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.comboUnit = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.getPoint = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.closeTime = new System.Windows.Forms.NumericUpDown();
            this.openTime = new System.Windows.Forms.NumericUpDown();
            this.cycleNum = new System.Windows.Forms.NumericUpDown();
            this.eleVal = new System.Windows.Forms.NumericUpDown();
            this.volteVal = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.port)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eleChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volChart)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.getPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.openTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cycleNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eleVal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.volteVal)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabControl1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabControl1.ItemSize = new System.Drawing.Size(120, 60);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1282, 708);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabPage2.Location = new System.Drawing.Point(4, 64);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1274, 640);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "设备管理";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnOpen);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.port);
            this.groupBox4.Controls.Add(this.ipAddress);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(6, 11);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(382, 194);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "电源配置";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(123, 148);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 31);
            this.btnOpen.TabIndex = 5;
            this.btnOpen.Text = "打开";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(42, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 19);
            this.label9.TabIndex = 4;
            this.label9.Text = "连接";
            // 
            // port
            // 
            this.port.Location = new System.Drawing.Point(123, 95);
            this.port.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.port.Name = "port";
            this.port.Size = new System.Drawing.Size(207, 29);
            this.port.TabIndex = 3;
            this.port.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // ipAddress
            // 
            this.ipAddress.BackColor = System.Drawing.SystemColors.Window;
            this.ipAddress.Location = new System.Drawing.Point(123, 41);
            this.ipAddress.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ipAddress.MinimumSize = new System.Drawing.Size(207, 29);
            this.ipAddress.Name = "ipAddress";
            this.ipAddress.ReadOnly = false;
            this.ipAddress.Size = new System.Drawing.Size(207, 29);
            this.ipAddress.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 97);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 19);
            this.label10.TabIndex = 1;
            this.label10.Text = "端口号：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(91, 19);
            this.label11.TabIndex = 0;
            this.label11.Text = "IP地址：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.eleChart);
            this.groupBox3.Controls.Add(this.volChart);
            this.groupBox3.Location = new System.Drawing.Point(394, 20);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(870, 599);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            // 
            // eleChart
            // 
            chartArea1.AxisX.Interval = 1D;
            chartArea1.AxisX.Maximum = 100D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisX.ScaleView.Size = 20D;
            chartArea1.AxisY.LogarithmBase = 5D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "ChartArea1";
            this.eleChart.ChartAreas.Add(chartArea1);
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Name = "Legend1";
            this.eleChart.Legends.Add(legend1);
            this.eleChart.Location = new System.Drawing.Point(6, 306);
            this.eleChart.Name = "eleChart";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Color = System.Drawing.Color.Red;
            series1.Legend = "Legend1";
            series1.Name = "电流";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this.eleChart.Series.Add(series1);
            this.eleChart.Size = new System.Drawing.Size(853, 281);
            this.eleChart.TabIndex = 2;
            this.eleChart.Text = "chart2";
            // 
            // volChart
            // 
            chartArea2.AxisX.Interval = 1D;
            chartArea2.AxisY.Maximum = 20D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "ChartArea1";
            this.volChart.ChartAreas.Add(chartArea2);
            legend2.DockedToChartArea = "ChartArea1";
            legend2.MaximumAutoSize = 40F;
            legend2.Name = "Legend1";
            this.volChart.Legends.Add(legend2);
            this.volChart.Location = new System.Drawing.Point(6, 1);
            this.volChart.Name = "volChart";
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Color = System.Drawing.Color.DodgerBlue;
            series2.Legend = "Legend1";
            series2.Name = "电压";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Time;
            this.volChart.Series.Add(series2);
            this.volChart.Size = new System.Drawing.Size(853, 280);
            this.volChart.TabIndex = 1;
            this.volChart.Text = "chart1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnStop);
            this.groupBox2.Controls.Add(this.comboUnit);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.getPoint);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.closeTime);
            this.groupBox2.Controls.Add(this.openTime);
            this.groupBox2.Controls.Add(this.cycleNum);
            this.groupBox2.Controls.Add(this.eleVal);
            this.groupBox2.Controls.Add(this.volteVal);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(6, 211);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(382, 408);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(278, 367);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 31);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(278, 330);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(89, 31);
            this.btnStop.TabIndex = 19;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // comboUnit
            // 
            this.comboUnit.FormattingEnabled = true;
            this.comboUnit.Items.AddRange(new object[] {
            "小时",
            "分钟",
            "秒"});
            this.comboUnit.Location = new System.Drawing.Point(115, 287);
            this.comboUnit.Name = "comboUnit";
            this.comboUnit.Size = new System.Drawing.Size(121, 27);
            this.comboUnit.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(2, 291);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 19);
            this.label3.TabIndex = 17;
            this.label3.Text = "时间单位：";
            // 
            // getPoint
            // 
            this.getPoint.Location = new System.Drawing.Point(115, 331);
            this.getPoint.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.getPoint.Name = "getPoint";
            this.getPoint.Size = new System.Drawing.Size(120, 29);
            this.getPoint.TabIndex = 16;
            this.getPoint.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(21, 336);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 19);
            this.label14.TabIndex = 15;
            this.label14.Text = "采样点：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(243, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 19);
            this.label2.TabIndex = 11;
            this.label2.Text = "A";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(243, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "V";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(278, 291);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(89, 31);
            this.btnStart.TabIndex = 10;
            this.btnStart.Text = "执行";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // closeTime
            // 
            this.closeTime.Location = new System.Drawing.Point(115, 239);
            this.closeTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.closeTime.Name = "closeTime";
            this.closeTime.Size = new System.Drawing.Size(120, 29);
            this.closeTime.TabIndex = 9;
            // 
            // openTime
            // 
            this.openTime.Location = new System.Drawing.Point(115, 186);
            this.openTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.openTime.Name = "openTime";
            this.openTime.Size = new System.Drawing.Size(120, 29);
            this.openTime.TabIndex = 8;
            // 
            // cycleNum
            // 
            this.cycleNum.Location = new System.Drawing.Point(115, 130);
            this.cycleNum.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.cycleNum.Name = "cycleNum";
            this.cycleNum.Size = new System.Drawing.Size(120, 29);
            this.cycleNum.TabIndex = 7;
            // 
            // eleVal
            // 
            this.eleVal.Location = new System.Drawing.Point(117, 75);
            this.eleVal.Name = "eleVal";
            this.eleVal.Size = new System.Drawing.Size(120, 29);
            this.eleVal.TabIndex = 6;
            this.eleVal.ValueChanged += new System.EventHandler(this.eleVal_ValueChanged);
            // 
            // volteVal
            // 
            this.volteVal.DecimalPlaces = 2;
            this.volteVal.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.volteVal.Location = new System.Drawing.Point(117, 23);
            this.volteVal.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.volteVal.Name = "volteVal";
            this.volteVal.Size = new System.Drawing.Size(120, 29);
            this.volteVal.TabIndex = 5;
            this.volteVal.ValueChanged += new System.EventHandler(this.volteVal_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 244);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(109, 19);
            this.label8.TabIndex = 4;
            this.label8.Text = "关闭时间：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 191);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 19);
            this.label7.TabIndex = 3;
            this.label7.Text = "打开时间：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 19);
            this.label6.TabIndex = 2;
            this.label6.Text = "循环次数：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(44, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 19);
            this.label5.TabIndex = 1;
            this.label5.Text = "电流：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 19);
            this.label4.TabIndex = 0;
            this.label4.Text = "电压：";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 733);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "电源控制系统";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.port)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eleChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volChart)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.getPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.closeTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.openTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cycleNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eleVal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.volteVal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown volteVal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown port;
        private IPAddressControlLib.IPAddressControl ipAddress;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NumericUpDown closeTime;
        private System.Windows.Forms.NumericUpDown openTime;
        private System.Windows.Forms.NumericUpDown cycleNum;
        private System.Windows.Forms.NumericUpDown eleVal;
        private System.Windows.Forms.DataVisualization.Charting.Chart eleChart;
        private System.Windows.Forms.DataVisualization.Charting.Chart volChart;
        private System.Windows.Forms.NumericUpDown getPoint;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox comboUnit;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnStop;
    }
}

