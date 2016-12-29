using DMSkin;
using PowerSystem.DALCommom;
using PowerSystem.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static PowerSystem.DALCommom.CommonMethod;

namespace PowerSystem
{
    public partial class MainForm : DMSkinForm
    {
        //打开//关闭
        System.Windows.Forms.Timer startTimer = new System.Windows.Forms.Timer();        
        System.Windows.Forms.Timer closeTimer = new System.Windows.Forms.Timer();

        //打开//关闭
        System.Windows.Forms.Timer startTimer2 = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer closeTimer2 = new System.Windows.Forms.Timer();

        public MainForm(string TitleName)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Text = TitleName;
            //初始化打开                        
            startTimer.Tick += OpenTimerTick;
            //初始化关闭           
            closeTimer.Tick += CloseTimerTick;

            //初始化打开                        
            startTimer2.Tick += OpenTimerTick2;
            //初始化关闭           
            closeTimer2.Tick += CloseTimerTick2;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //居中显示
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //显示应用程序在任务栏中的图标
            this.ShowInTaskbar = true;

            //首先要从ini文件读取仪器的参数信息
            CommonMethod.GetAlltheInstumentsParasFromIniFile();
            ipAddress.Text = CGloabal.g_InstrPowerModule.ipAdress;
            port.Value = CGloabal.g_InstrPowerModule.port;

            ipAddress2.Text = CGloabal.g_InstrPowerModule2.ipAdress;
            port2.Value = CGloabal.g_InstrPowerModule2.port;

            comboUnit.SelectedIndex =0;//设置
            InitChart();

            comboUnit2.SelectedIndex = 0;//设置
            InitChart2();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            string strIP;
            UInt32 nPort;
            string resourceName;
            int error = 0;
            if (btnOpen.Text == "打开")//用户要连接仪器
            {
                strIP = this.ipAddress.Text;
                nPort = (UInt32)this.port.Value;
                //连接设备
                resourceName = "TCPIP0::" + strIP + "::inst0::INSTR";

                error = CommonMethod.ConnectSpecificInstrument(CGloabal.g_InstrPowerModule.strInstruName, resourceName);
                if (error < 0)//连接失败
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, "电源打开失败！");
                    btnOpen.Text = "打开";
                }
                else//连接成功,则要将当前用户输入的IP地址和端口号保存到ini文件中
                {
                    CommonMethod.SaveInputNetInforsToIniFile(CGloabal.g_InstrPowerModule.strInstruName, strIP, nPort);
                    btnOpen.Text = "关闭";
                }
            }
            else//此时用户要断开连接
            {
                error = CommonMethod.CloseSpecificInstrument(CGloabal.g_InstrPowerModule.strInstruName);
                if (error < 0)//断开失败，则还要将switchConnect恢复为连接状态      
                {
                    btnOpen.Text = "关闭";
                }
                else
                {
                    btnOpen.Text = "打开";
                }
            }
        }

        public bool TestSign = false;//执行测试标志
        DateTime StartTime; //开始执行测试时间
        private void btnStart_Click(object sender, EventArgs e)
        {           
            TestSign = true;

            volChart.Series[0].Points.Clear();
            eleChart.Series[0].Points.Clear();

          

            double vlo = (double)volteVal.Value;
            double ele = (double)eleVal.Value;
            var cyc = cycleNum.Value;
           
            var open = openTime.Value;
            var close = closeTime.Value ;
            int point = (int)getPoint.Value;

            StartTime = System.DateTime.Now;

            int error = 0;
            string strErrMsg = "";
            //设置电压和电流
            error = PowerDriver.SetVolAndEle(CGloabal.g_InstrPowerModule.nHandle, vlo, ele, strErrMsg);
            if (error < 0)
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                return;
            }

            //打开命令
            error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
            if (error < 0)
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                return;
            }

            //触发开始
            BeginTime = DateTime.Now;
            startTimer.Interval = MixHelper.ReturnInterval(comboUnit.Text,1, open,close,point);
            startTimer.Enabled = true;
        }

        //
        int openPoint = 0;
        
        DateTime BeginTime;

        private void OpenTimerTick(object sender, EventArgs e) {
            //采样点数
            int point = (int)getPoint.Value;
            DateTime curr = System.DateTime.Now;
           
            TimeSpan ts = curr.Subtract(StartTime).Duration();
            //dateDiff = ts.Days.ToString() + "天" + ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分钟" + ts.Seconds.ToString() + "秒";
            double total = ts.Days * 86400 + ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds;
           // int MaxVal = SumTime();

            openPoint++;
            double reVlote = 0; double reElect = 0;string strErrMsg = "";
            if (openPoint > point)
            {
                if (OverTest() < 0)
                {
                    return;
                }
                startTimer.Enabled = false;
                openPoint = 0;
                //已采样结束，发送关闭指令
                int error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
                if (error < 0)
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                    return;
                }

                //触发结束 
                var close = closeTime.Value;
                closeTimer.Interval = MixHelper.ReturnInterval(comboUnit.Text, 0, 0, close, point);
                closeTimer.Enabled = true;
            }
            else {
                //读取电压和电流        
                TimeSpan xVal = DateTime.Now - BeginTime;
                string xTime = xVal.Minutes + ":" + xVal.Seconds;
                PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule.nHandle, ref reVlote, ref reElect);
                volChart.Series[0].Points.AddXY(xTime, reVlote);
                eleChart.Series[0].Points.AddXY(xTime, reElect);
            }
        }

        int closePoint = 0;
        private void CloseTimerTick(object sender, EventArgs e)
        {
            int point = (int)getPoint.Value;
            closePoint++;
            double reVlote = 0; double reElect = 0; string strErrMsg = "";
            if (closePoint > point)
            {
                if (OverTest()<0)
                {
                    return;
                } 
                closeTimer.Enabled = false;
                closePoint = 0;
                //已采样结束，发送关闭指令
                int error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
                if (error < 0)
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                    return;
                }

                //触发开始 
                var open = openTime.Value;
                startTimer.Interval = MixHelper.ReturnInterval(comboUnit.Text, 1, open, 0, point);
                startTimer.Enabled = true;
            }
            else
            {
                TimeSpan xVal = DateTime.Now - BeginTime;
                string xTime = xVal.Minutes + ":" + xVal.Seconds;
                //读取电压和电流        
                PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule.nHandle, ref reVlote, ref reElect);
                volChart.Series[0].Points.AddXY(xTime, reVlote);
                eleChart.Series[0].Points.AddXY(xTime, reElect);
            }
        }

        //全部结束就结束测试
        private int  OverTest()
        {
            int Pointcount = volChart.Series[0].Points.Count;
            int sum = SumTime();

            if (Pointcount>=sum)
            {
                startTimer.Enabled = false;
                openPoint = 0;
                closeTimer.Enabled = false;
                closePoint = 0;

                return -1;
            }
            else
            {
                return 1;
            }
            
        }


        private void cycleNum_ValueChanged(object sender, EventArgs e)
        {       
            //int MaxVal = SumTime();// (int)((openT + closeT) * xVal);

            //volChart.ChartAreas[0].AxisX.Maximum = MaxVal;
            //eleChart.ChartAreas[0].AxisX.Maximum = MaxVal;
        }

        private void eleVal_ValueChanged(object sender, EventArgs e)
        {
            double xVal = (double)(sender as NumericUpDown).Value;
            //eleChart.ChartAreas[0].AxisY.Maximum = xVal*2;
        }

        private void volteVal_ValueChanged(object sender, EventArgs e)
        {
            double xVal = (double)(sender as NumericUpDown).Value;
            volChart.ChartAreas[0].AxisY.Maximum = xVal*2;           
        }

        private void openTime_ValueChanged(object sender, EventArgs e)
        {           
            //int MaxVal = SumTime();// (int)((openT + closeT) * xVal);

            //volChart.ChartAreas[0].AxisX.Maximum = MaxVal;
            //eleChart.ChartAreas[0].AxisX.Maximum = MaxVal;
        }

        private void closeTime_ValueChanged(object sender, EventArgs e)
        {
            //int MaxVal = SumTime();// (int)((openT + closeT) * xVal);

            //volChart.ChartAreas[0].AxisX.Maximum = MaxVal;
            //eleChart.ChartAreas[0].AxisX.Maximum = MaxVal;
        }

        private int SumTime()
        {
            double openT = (double)openTime.Value;
            double closeT = (double)closeTime.Value;           
            double xVal = (double)cycleNum.Value;
            double point = (double)getPoint.Value;
            string sign = comboUnit.Text;           

            int MaxVal = (int)(2* point * xVal);

            return MaxVal;
        }
        /// <summary>
        /// 初始化Chart控件
        /// </summary>
        private void InitChart() {
            

            volChart.ChartAreas[0].AxisX.LabelStyle.Format="mm:ss";
            volChart.ChartAreas[0].AxisX.ScaleView.Size = 20;
            volChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            volChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            eleChart.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
            eleChart.ChartAreas[0].AxisX.ScaleView.Size = 20;
            eleChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            eleChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            double openT = (double)openTime.Value;
            double closeT = (double)closeTime.Value;
            double xVal = (double)cycleNum.Value;

            string sign = comboUnit.Text;
            DateTime curD = System.DateTime.Now;
            int MaxVal = (int)((openT + closeT) * xVal);
            if (sign == "小时")
            {
                curD.AddHours(MaxVal);
            }
            else if (sign == "分钟")
            {
                curD.AddMinutes(MaxVal);
            }
            else if (sign == "秒")
            {
                curD.AddSeconds(MaxVal);
            }

            //int MaxVal = (int)((openT + closeT) * xVal);
           // volChart.ChartAreas[0].AxisX.Maximum = curD.ToString("mm:ss");// MaxVal;
            //eleChart.ChartAreas[0].AxisX.Maximum = MaxVal;            
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            startTimer.Enabled = false;
            openPoint = 0;
            closeTimer.Enabled = false;
            closePoint = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // volChart.Series[0].Points
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", System.Type.GetType("System.String"));
            dt.Columns.Add("时间", System.Type.GetType("System.String"));
            dt.Columns.Add("电压", System.Type.GetType("System.String"));
            dt.Columns.Add("电流", System.Type.GetType("System.String"));


            for (int i = 0; i < volChart.Series[0].Points.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = i+1;
                dr["时间"] = volChart.Series[0].Points[i].AxisLabel;
                dr["电压"] = volChart.Series[0].Points[i].YValues[0];
                dr["电流"] = eleChart.Series[0].Points[i].YValues[0];

                dt.Rows.Add(dr);
            }

            CommonMethod.SaveToCSV(dt, "");//
           // CommonMethod.SaveToExcel(dt, "");
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        //--------------------------------------------------------第2部分电源------------------------------------------------

        private void btnOpen2_Click(object sender, EventArgs e)
        {
            string strIP;
            UInt32 nPort;
            string resourceName;
            int error = 0;
            if (btnOpen.Text == "打开")//用户要连接仪器
            {
                strIP = this.ipAddress2.Text;
                nPort = (UInt32)this.port2.Value;
                //连接设备
                resourceName = "TCPIP0::" + strIP + "::inst0::INSTR";

                error = CommonMethod.ConnectSpecificInstrument(CGloabal.g_InstrPowerModule2.strInstruName, resourceName);
                if (error < 0)//连接失败
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, "电源打开失败！");
                    btnOpen.Text = "打开";
                }
                else//连接成功,则要将当前用户输入的IP地址和端口号保存到ini文件中
                {
                    CommonMethod.SaveInputNetInforsToIniFile(CGloabal.g_InstrPowerModule2.strInstruName, strIP, nPort);
                    btnOpen.Text = "关闭";
                }
            }
            else//此时用户要断开连接
            {
                error = CommonMethod.CloseSpecificInstrument(CGloabal.g_InstrPowerModule2.strInstruName);
                if (error < 0)//断开失败，则还要将switchConnect恢复为连接状态      
                {
                    btnOpen.Text = "关闭";
                }
                else
                {
                    btnOpen.Text = "打开";
                }
            }
        }

        private void volteVal2_ValueChanged(object sender, EventArgs e)
        {
            double xVal = (double)(sender as NumericUpDown).Value;
            volChart.ChartAreas[0].AxisY.Maximum = xVal * 2;
        }
        bool TestSign2 = false;
        DateTime StartTime2;

        private void btnStart2_Click(object sender, EventArgs e)
        {
            TestSign2 = true;
            volChart2.Series[0].Points.Clear();
            eleChart2.Series[0].Points.Clear();            

            StartTime2 = System.DateTime.Now;
            OutSign2 = false;
            //Begin2Time = DateTime.Now;
            //触发开始
            // startTimer2.Interval = MixHelper.ReturnInterval(comboUnit2.Text, 1, open, close, point);
            // startTimer2.Enabled = true;

            Thread t = new Thread(new ThreadStart(RunProcess2));
            t.IsBackground = true;
            t.Start();
        }
        bool OutSign2 = false;
        private void RunProcess2() {
            double vlo = (double)volteVal2.Value;
            double ele = (double)eleVal2.Value;
            var cyc = cycleNum2.Value;

            var open = openTime2.Value;
            var close = closeTime2.Value;
            int point = (int)getPoint2.Value;

            int error = 0;
            string strErrMsg = "";
            double reVlote = 0, reElect = 0;
            //设置电压和电流
            error = PowerDriver.SetVolAndEle(CGloabal.g_InstrPowerModule2.nHandle, vlo, ele, strErrMsg);
            if (error < 0)
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                return;
            }

            //读取应该间隔多少时间取一个点
            int OpenReadtimer = MixHelper.ReturnInterval(comboUnit2.Text, 1, open, close, point);
            int CloseReadTimer = MixHelper.ReturnInterval(comboUnit2.Text, 0, open, close, point);

            //打开命令
            error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg);
            if (error < 0)
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                return;
            }

            while (cyc > 0)
            {
                cyc--;
                //打开命令
                error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg);
                if (error < 0)
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                    return;
                }
                for (int i = 0; i < point; i++)
                {
                    if (OutSign2)
                    {//为true时则终止测试
                        error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg);
                        return;
                    }
                    Thread.Sleep(OpenReadtimer);
                    TimeSpan ts = DateTime.Now - StartTime2;
                    string xOpenVal4 = ts.Minutes + ":" + ts.Seconds;
                    //读取电压和电流        
                    PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule2.nHandle, ref reVlote, ref reElect);
                    volChart2.Series[0].Points.AddXY(xOpenVal4, reVlote);
                    eleChart2.Series[0].Points.AddXY(xOpenVal4, reElect);
                }

                //发送关闭指令
                error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg);
                if (error < 0)
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                    return;
                }
                for (int i = 0; i < point; i++)
                {
                    if (OutSign2)
                    {//为true时则终止测试
                        error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg); 
                        return;
                    }
                    Thread.Sleep(CloseReadTimer);
                    TimeSpan ts = DateTime.Now - StartTime2;
                    string xCloseVal4 = ts.Minutes + ":" + ts.Seconds;
                    //读取电压和电流        
                    PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule2.nHandle, ref reVlote, ref reElect);
                    volChart2.Series[0].Points.AddXY(xCloseVal4, reVlote);
                    eleChart2.Series[0].Points.AddXY(xCloseVal4, reElect);
                }
            }
        }       

        //
        int openPoint2 = 0;
        DateTime Begin2Time;

        private void OpenTimerTick2(object sender, EventArgs e)
        {
            ////采样点数
            //int point = (int)getPoint2.Value;
            //DateTime curr = System.DateTime.Now;
            //TimeSpan ts = curr.Subtract(StartTime2).Duration();           
            //double total = ts.Days * 86400 + ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds;        

            //openPoint2++;
            //double reVlote = 0; double reElect = 0; string strErrMsg = "";
            //if (openPoint2 > point)
            //{
            //    if (OverTest2() < 0)
            //    {
            //        return;
            //    }
            //    startTimer2.Enabled = false;
            //    openPoint2 = 0;
            //    //已采样结束，发送关闭指令
            //    int error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg);
            //    if (error < 0)
            //    {
            //        CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //        return;
            //    }

            //    //触发结束 
            //    var close = closeTime2.Value;
            //    closeTimer.Interval = MixHelper.ReturnInterval(comboUnit2.Text, 0, 0, close, point);
            //    closeTimer.Enabled = true;
            //}
            //else
            //{
            //    TimeSpan xVal = DateTime.Now - BeginTime;
            //    string xTime = xVal.Minutes + ":" + xVal.Seconds;
            //    //读取电压和电流        
            //    PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule2.nHandle, ref reVlote, ref reElect);
            //    volChart2.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reVlote);
            //    eleChart2.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reElect);
            //}
        }

        int closePoint2 = 0;
        private void CloseTimerTick2(object sender, EventArgs e)
        {
            //int point = (int)getPoint2.Value;
            //closePoint2++;
            //double reVlote = 0; double reElect = 0; string strErrMsg = "";
            //if (closePoint2 > point)
            //{
            //    if (OverTest2() < 0)
            //    {
            //        return;
            //    }
            //    closeTimer2.Enabled = false;
            //    closePoint2 = 0;
            //    //已采样结束，发送关闭指令
            //    int error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule2.nHandle, strErrMsg);
            //    if (error < 0)
            //    {
            //        CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //        return;
            //    }

            //    //触发开始 
            //    var open = openTime2.Value;
            //    startTimer2.Interval = MixHelper.ReturnInterval(comboUnit2.Text, 1, open, 0, point);
            //    startTimer2.Enabled = true;
            //}
            //else
            //{
            //    TimeSpan x2Val = DateTime.Now - Begin2Time;
            //    string x2Time = x2Val.Minutes + ":" + x2Val.Seconds;
            //    //读取电压和电流        
            //    PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule2.nHandle, ref reVlote, ref reElect);
            //    volChart2.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reVlote);
            //    eleChart2.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reElect);
            //}
        }

        private void btnStop2_Click(object sender, EventArgs e)
        {
            OutSign2 = true;
            //startTimer2.Enabled = false;
            //openPoint2 = 0;
            //closeTimer2.Enabled = false;
            //closePoint2 = 0;
        }

        private void btnSave2_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("序号", System.Type.GetType("System.String"));
            dt.Columns.Add("时间", System.Type.GetType("System.String"));
            dt.Columns.Add("电压", System.Type.GetType("System.String"));
            dt.Columns.Add("电流", System.Type.GetType("System.String"));


            for (int i = 0; i < volChart2.Series[0].Points.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["序号"] = i + 1;
                dr["时间"] = volChart2.Series[0].Points[i].AxisLabel;
                dr["电压"] = volChart2.Series[0].Points[i].YValues[0];
                dr["电流"] = eleChart2.Series[0].Points[i].YValues[0];

                dt.Rows.Add(dr);
            }

            CommonMethod.SaveToCSV(dt, "");//
        }

        //全部结束就结束测试
        private int OverTest2()
        {
            int Pointcount2 = volChart2.Series[0].Points.Count;
            int sum = SumTime2();

            if (Pointcount2 >= sum)
            {
                startTimer2.Enabled = false;
                openPoint2 = 0;
                closeTimer2.Enabled = false;
                closePoint2 = 0;

                return -1;
            }
            else
            {
                return 1;
            }

        }
        private int SumTime2()
        {
            double openT = (double)openTime2.Value;
            double closeT = (double)closeTime2.Value;
            double xVal = (double)cycleNum2.Value;
            double point = (double)getPoint2.Value;
            string sign = comboUnit2.Text;

            int MaxVal = (int)(2 * point * xVal);

            return MaxVal;
        }
        private void InitChart2()
        {
            volChart2.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
            volChart2.ChartAreas[0].AxisX.ScaleView.Size = 20;
            volChart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            volChart2.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            eleChart2.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
            eleChart2.ChartAreas[0].AxisX.ScaleView.Size = 20;
            eleChart2.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            eleChart2.ChartAreas[0].AxisX.ScrollBar.Enabled = true;

            double openT = (double)openTime2.Value;
            double closeT = (double)closeTime2.Value;
            double xVal = (double)cycleNum2.Value;

            string sign = comboUnit2.Text;
            DateTime curD = System.DateTime.Now;
            int MaxVal = (int)((openT + closeT) * xVal);
            if (sign == "小时")
            {
                curD.AddHours(MaxVal);
            }
            else if (sign == "分钟")
            {
                curD.AddMinutes(MaxVal);
            }
            else if (sign == "秒")
            {
                curD.AddSeconds(MaxVal);
            }

            //int MaxVal = (int)((openT + closeT) * xVal);
            // volChart.ChartAreas[0].AxisX.Maximum = curD.ToString("mm:ss");// MaxVal;
            //eleChart.ChartAreas[0].AxisX.Maximum = MaxVal;            
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm(volChart ,eleChart);
            cf.ShowDialog();
            ////volChart.ChartAreas[0].AxisX.Interval = 0.05;
            ////volChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            ////eleChart.ChartAreas[0].AxisX.Interval = 0.05;
            ////eleChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;

            //volChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            //volChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            //volChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //volChart.ChartAreas[0].CursorY.IsUserEnabled = true;
            //volChart.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
            //volChart.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            ////将滚动条嵌入到坐标轴中
            //volChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            //volChart.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = true;

            ////设置滚动条的按钮风格，显示所有滚动条
            //volChart.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.All;
            //volChart.ChartAreas[0].AxisY.ScrollBar.ButtonStyle = System.Windows.Forms.DataVisualization.Charting.ScrollBarButtonStyles.All;

            ////设置自动放大和缩小
            //volChart.ChartAreas[0].AxisX.ScaleView.SmallScrollSize = double.NaN;
            //volChart.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;
            //volChart.ChartAreas[0].AxisY.ScaleView.SmallScrollSize = double.NaN;
            //volChart.ChartAreas[0].AxisX.ScaleView.SmallScrollMinSize = 1;

        }

        private void btnView2_Click(object sender, EventArgs e)
        {
            ChartForm cf = new ChartForm(volChart2, eleChart2);
            cf.ShowDialog();
        }      

        private void picBox1_Click(object sender, EventArgs e)
        {

        }

        private void picBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
