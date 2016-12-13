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
        public MainForm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

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

            //初始化图表
            //volChart.ChartAreas[0].AxisX.Maximum = 100;
            //volChart.ChartAreas[0].AxisY.Maximum = 100;
            //eleChart.ChartAreas[0].AxisX.Maximum = 100;
            //eleChart.ChartAreas[0].AxisY.Maximum = 100;

            //后台工作
            bgWork.DoWork += bgWork_Dowork;
            bgWork.ProgressChanged += bgWork_ProgressChanged;
            bgWork.RunWorkerCompleted += bgWork_RunWorkerCompleted;
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
      
        private void btnStart_Click(object sender, EventArgs e)
        {           
            TestSign = true;

            volChart.Series[0].Points.Clear();
            eleChart.Series[0].Points.Clear();

            double vlo = (double)volteVal.Value;
            double ele = (double)eleVal.Value;
            var cyc = cycleNum.Value;
           
            var open = openTime.Value * 1000;
            var close = closeTime.Value * 1000;

           

            int error = 0;
            string strErrMsg = "";
            //error = PowerDriver.SetVolAndEle(CGloabal.g_InstrPowerModule.nHandle, vlo, ele, strErrMsg);
            //if (error < 0)
            //{
            //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //    return;
            //}
            double vloValue = 0; double eleValue = 0;
            //while (cyc > 0)
            //{
              
            //}
            bgWork.RunWorkerAsync();
            //    if (!TestSign)
            //    {
            //        return;
            //    }
            //    //打开
            //    error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
            //    if (error < 0)
            //    {
            //        CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //        return;
            //    }
            //    for (int i = 0; i < 2; i++)
            //    {
            //        Thread.Sleep(1000);


            //        error = PowerDriver.ReadVolteCommand(CGloabal.g_InstrPowerModule.nHandle, ref vloValue, strErrMsg);
            //        if (error < 0)
            //        {
            //            CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //            return;
            //        }
            //        error = PowerDriver.ReadElectCommand(CGloabal.g_InstrPowerModule.nHandle, ref eleValue, strErrMsg);
            //        if (error < 0)
            //        {
            //            CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //            return;
            //        }

            //        volData[intkk] = vloValue;
            //        eleData[intkk] = eleValue;

            //        intkk++;
            //    }                

            //    Thread.Sleep((int)open-2000);

            //    //关闭
            //    error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
            //    if (error < 0)
            //    {
            //        CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //        return;
            //    }

            //    for (int i = 0; i < 2; i++)
            //    {
            //        Thread.Sleep(1000);

            //        error = PowerDriver.ReadVolteCommand(CGloabal.g_InstrPowerModule.nHandle, ref vloValue, strErrMsg);
            //        if (error < 0)
            //        {
            //            CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //            return;
            //        }
            //        error = PowerDriver.ReadElectCommand(CGloabal.g_InstrPowerModule.nHandle, ref eleValue, strErrMsg);
            //        if (error < 0)
            //        {
            //            CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //            return;
            //        }
            //        volData[intkk] = vloValue;
            //        eleData[intkk] = eleValue;

            //        intkk++;
            //    }

            //    Thread.Sleep((int)close-2000);
            //    cyc--;
            //}
            //for (int i = 0; i < volData.Length; i++)
            //{
            //    volChart.Series[0].Points.Add(volData[i]);
            //}
            //for (int i = 0; i < eleData.Length; i++)
            //{
            //    eleChart.Series[0].Points.Add(eleData[i]);
            //}            
        }


        private void bgWork_Dowork(object sender ,DoWorkEventArgs e)
        {
            int error = 0;
            string strErrMsg = "";

            var open = openTime.Value * 1000;
            var close = closeTime.Value * 1000;

            //打开
            //error = PowerDriver.SetOpenCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
            //if (error < 0)
            //{
            //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //    return;
            //}
            for (int i = 0; i < 2; i++)
            {
                bgWork.ReportProgress(10);
            }
           



            //关闭
            //error = PowerDriver.SetCloseCommand(CGloabal.g_InstrPowerModule.nHandle, strErrMsg);
            //if (error < 0)
            //{
            //    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
            //    return;
            //}

            
            for (int i = 0; i < 2; i++)
            {
                bgWork.ReportProgress(20);
            }
        }
        double[] volData;
        double[] eleData;
        private void bgWork_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int error = 0;
            double vloValue=0; double eleValue=0;
            string strErrMsg = "";

            var cyc = cycleNum.Value;
         
            var open = openTime.Value * 1000;
            var close = closeTime.Value * 1000;

            int ArrLength =(int)((open + close) * cyc);//总长度
            volData = new double[ArrLength];
            eleData = new double[ArrLength];

            for (int i = 0; i < (int)(openTime.Value); i++)
            {
                Thread.Sleep(1000);

                //读取电压电流值
                error = PowerDriver.ReadVolAndEleCommand(CGloabal.g_InstrPowerModule.nHandle, ref vloValue, ref eleValue);
                if (error < 0)
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.error, strErrMsg);
                    return;
                }
                volChart.Series[0].Points.Add(vloValue);
                eleChart.Series[0].Points.Add(eleValue);
            }
        }

        private void bgWork_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {


        }      
       

        private void btnStop_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            TestSign = false;
        }

        private void cycleNum_ValueChanged(object sender, EventArgs e)
        {
            double xVal =(double) (sender as NumericUpDown).Value;
            volChart.ChartAreas[0].AxisX.Maximum = xVal;
            eleChart.ChartAreas[0].AxisX.Maximum = xVal;
        }

        private void eleVal_ValueChanged(object sender, EventArgs e)
        {
            double xVal = (double)(sender as NumericUpDown).Value;           
            eleChart.ChartAreas[0].AxisY.Maximum = xVal*2;
        }

        private void volteVal_ValueChanged(object sender, EventArgs e)
        {
            double xVal = (double)(sender as NumericUpDown).Value;
            volChart.ChartAreas[0].AxisX.Maximum = xVal*2;           
        }
    }
}
