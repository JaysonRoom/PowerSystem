using PowerSystem.DALCommom;
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
    public partial class MainForm : Form
    {
        //打开
        System.Windows.Forms.Timer startTimer = new System.Windows.Forms.Timer();
        //关闭
        System.Windows.Forms.Timer closeTimer = new System.Windows.Forms.Timer();

        public MainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            //初始化打开
            
            startTimer.Tick += OpenTimerTick;
            //初始化关闭           
            closeTimer.Tick += CloseTimerTick;
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

            comboUnit.SelectedIndex = 1;//设置
            InitChart();
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
            //error = PowerDriver.SetVolAndEle(CGloabal.g_InstrPowerModule.nHandle, vlo, ele, strErrMsg);
            //if (error < 0)
            //{
            //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //    return;
            //}

            //打开命令         
            //error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
            //if (error < 0)
            //{
            //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //    return;
            //}

            //触发开始
            startTimer.Interval = MixHelper.ReturnInterval(comboUnit.Text,1, open,close,point);
            startTimer.Enabled = true;
        }

        //
        int openPoint = 0;

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
                //int error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
                //if (error < 0)
                //{
                //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                //    return;
                //}

                //触发结束 
                var close = closeTime.Value;
                closeTimer.Interval = MixHelper.ReturnInterval(comboUnit.Text, 0, 0, close, point);
                closeTimer.Enabled = true;
            }
            else {
                //读取电压和电流        
                PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule.nHandle, ref reVlote, ref reElect);
                volChart.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"),reVlote);
                eleChart.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reElect);
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
                //int error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
                //if (error < 0)
                //{
                //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                //    return;
                //}

                //触发开始 
                var open = openTime.Value;
                startTimer.Interval = MixHelper.ReturnInterval(comboUnit.Text, 1, open, 0, point);
                startTimer.Enabled = true;
            }
            else
            {
                //读取电压和电流        
                PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule.nHandle, ref reVlote, ref reElect);
                volChart.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reVlote);
                eleChart.Series[0].Points.AddXY(DateTime.Now.ToString("mm:ss"), reElect);
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
            eleChart.ChartAreas[0].AxisY.Maximum = xVal*2;
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
        }
    }
}
