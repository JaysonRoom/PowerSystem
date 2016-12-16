using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace PowerSystem.DALCommom
{
    public static class CommonMethod
    {
        [DllImport("kernel32")]
        //section：要读取的段落名
        // key: 要读取的键
        //defVal: 读取异常的情况下的缺省值
        //retVal: key所对应的值，如果该key不存在则返回空值
        //size: 值允许的大小
        //filePath: INI文件的完整路径和文件名
        private static extern int GetPrivateProfileString(string section, string key, string defVal, StringBuilder retVal, int size, string filePath);

        [DllImport("kernel32")]
        //section: 要写入的段落名
        //key: 要写入的键，如果该key存在则覆盖写入
        //val: key所对应的值
        //filePath: INI文件的完整路径和文件名
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /*************************************************
        * 函数原型：private void GetAlltheInstumentsParasFromImiFile()
        * 函数功能：从ini文件中获取所有的仪器的参数信息.
        * 输入参数：
        * 输出参数：
        * 创 建 者：yzx
        * 创建日期：2016.7.23
        * 修改说明：
       */
        public static void GetAlltheInstumentsParasFromIniFile()
        {
            string strPort;
            string strFilePath;
            string strBusType;

            //获取ini文件的相对路径
            strFilePath = System.Windows.Forms.Application.StartupPath + "\\APP_INFOS.ini";
            if (File.Exists(strFilePath))//首先判读INI文件是否存在
            {

                CGloabal.g_InstrPowerModule.ipAdress = GetValueFromIniFile(strFilePath, "电源", "IP地址");
                strPort = GetValueFromIniFile(strFilePath, "电源", "端口号");
                CGloabal.g_InstrPowerModule.port = int.Parse(strPort);

                CGloabal.g_InstrPowerModule2.ipAdress = GetValueFromIniFile(strFilePath, "电源2", "IP地址");
                strPort = GetValueFromIniFile(strFilePath, "电源2", "端口号");
                CGloabal.g_InstrPowerModule2.port = int.Parse(strPort);
            }
            else
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, "APP_INFOS.ini文件不存在！");
            }
        }

        /*************************************************
    * 函数原型：private string GetContentValueFromFile(string strFilePath,string Section, string key)
    * 函数功能：从ini文件的指定段中指定的key中获取字符串数值信息
    * 输入参数：strFilePath，ini文件的路径信息；Section，段信息；key，key信息。
    * 输出参数：返回获得字符串类型的数值信息
    * 创 建 者：yzx
    * 创建日期：2016.7.23
    * 修改说明：
   */
        public static string GetValueFromIniFile(string strFilePath, string Section, string key)
        {
            int nRntCount;
            StringBuilder temp = new StringBuilder(1024);
            nRntCount = GetPrivateProfileString(Section, key, "", temp, 1024, strFilePath);
            if (nRntCount == 0)
            {
                return null;
            }
            else
            {
                return temp.ToString();
            }

        }

        public static int ConnectSpecificInstrument(string strInstruName, string resourceName)
        {
            int error = 0;
            string strError = "";
            if (strInstruName == "电源")
            {
                if (CGloabal.g_InstrPowerModule.nHandle == 0)
                {
                    error = PowerDriver.Init(resourceName, ref CGloabal.g_InstrPowerModule.nHandle, strError);
                    if (error < 0)
                    {
                        CGloabal.g_InstrPowerModule.bInternet = false;
                        CommonMethod.ShowHintInfor(eHintInfoType.error, CGloabal.g_InstrPowerModule.strInstruName + "连接失败");
                    }
                    else
                    {

                        CGloabal.g_InstrPowerModule.bInternet = true;
                    }
                }
                else
                {
                    CommonMethod.ShowHintInfor(eHintInfoType.hint, CGloabal.g_InstrPowerModule.strInstruName + "已经处于连接状态");
                }

            }
            else
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, "错误的仪器名");
                return -1;
            }

            return error;

        }




        /******************************************************************************************
        * 函数原型：CloseSpecificInstrument(string strInstruName)
        * 函数功能：断开指定的仪器的网络连接
        * 输入参数：strInstruName，仪器名字
        * 输出参数：
        * 创 建 者：yzx
        * 创建日期：2016.7.27
        * 修改说明：
        * */
        public static int CloseSpecificInstrument(string strInstruName)
        {
            string strError = "";
            int error = 0;
            if (strInstruName == "电源")
            {
                if (CGloabal.g_InstrPowerModule.nHandle > 0)
                {
                    error = PowerDriver.Close(CGloabal.g_InstrPowerModule.nHandle, strError);
                    if (error < 0)
                    {
                        CommonMethod.ShowHintInfor(eHintInfoType.error, "电源断开失败");
                    }
                    else//断开成功，要将此时的连接状态更新到仪器参数中
                    {
                        CGloabal.g_InstrPowerModule.bInternet = false;
                    }
                }

            }
            else
            {
                CommonMethod.ShowHintInfor(eHintInfoType.error, "错误的仪器名");
                return -1;
            }
            return error;
        }

        /******************************************************************************************
         * 函数原型：int SaveInputNetInforsToIniFile(string strInstruName,string strIP,UInt32 port )
         * 函数功能：当用户在界面上正确连接设备后，要将该此时的IP地址和端口号保存到ini文件中并更新仪器的参数信息
         *            这个函数只有在仪器连接成功后，才能调用。
         * 输入参数：strInstruName，仪器名字。
         * 输出参数：
         * 返 回 值：
         * 创 建 者：yzx
         * 创建日期：2016.7.27
         * 修改说明：
         * */
        public static int SaveInputNetInforsToIniFile(string strInstruName, string strIP, UInt32 port)
        {
            string strFilePath;
            //获取ini文件的相对路径
            strFilePath = System.Windows.Forms.Application.StartupPath + "\\APP_INFOS.ini";
            if (File.Exists(strFilePath))//先判断INI文件是否存在
            {
                if (strInstruName == "电源")
                {
                    //保存到ini文件
                    CommonMethod.WriteValueToIniFile(strFilePath, "电源", "IP地址", strIP);
                    CommonMethod.WriteValueToIniFile(strFilePath, "电源", "端口号", port.ToString());
                    //更新当前仪器的参数信息
                    CGloabal.g_InstrPowerModule.ipAdress = strIP;
                    CGloabal.g_InstrPowerModule.port = (int)port;
                    CGloabal.g_InstrPowerModule.bInternet = true;
                }
                if (strInstruName == "电源2")
                {
                    //保存到ini文件
                    CommonMethod.WriteValueToIniFile(strFilePath, "电源2", "IP地址", strIP);
                    CommonMethod.WriteValueToIniFile(strFilePath, "电源2", "端口号", port.ToString());
                    //更新当前仪器的参数信息
                    CGloabal.g_InstrPowerModule2.ipAdress = strIP;
                    CGloabal.g_InstrPowerModule2.port = (int)port;
                    CGloabal.g_InstrPowerModule2.bInternet = true;
                }
                else
                {
                    MessageBox.Show("错误的仪器名", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return -1;
                }
            }
            return 0;
        }

        /*************************************************
       * 函数原型：private long WriteValueToIniFile(string strFilePath,string Section, string key,string strValue)
       * 函数功能：向ini文件的指定段中指定的key中写入字符串数值信息
       * 输入参数：strFilePath，ini文件的路径信息；Section，段信息；key，key信息。
       * 输出参数：
       * 创 建 者：yzx
       * 创建日期：2016.7.23
       * 修改说明：
      */
        public static long WriteValueToIniFile(string strFilePath, string Section, string key, string strValue)
        {

            long error = 0;

            error = WritePrivateProfileString(Section, key, strValue, strFilePath);
            if (error < 0)
            {
                MessageBox.Show("ini文件写入出错！");
            }
            return error;
        }

        public static void SaveToExcel(DataTable dt, string saveFileName)
        {            
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel文件|*.xls";
            saveDialog.FileName = "电源控制测试结果" + System.DateTime.Now.ToString("yyyyMMddHHmmss"); ;
            saveDialog.ShowDialog();
            saveFileName = saveDialog.FileName;
            if (saveFileName.IndexOf(":") < 0) return; //被点了取消 
            Microsoft.Office.Interop.Excel.Application xlApp;
            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();
            }
            catch (Exception)
            {
                MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                return;
            }
            finally
            {
            }
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                  //写Title
            int titleRowCount = 0;
            //写入列标题
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                worksheet.Cells[titleRowCount + 1, i + 1] = dt.Columns[i].ColumnName;
            }
            //写入数值
            for (int r = 0; r <= dt.Rows.Count - 1; r++)
            {
                for (int i = 0; i <= dt.Columns.Count - 1; i++)
                {
                    worksheet.Cells[r + titleRowCount + 2, i + 1] = dt.Rows[r][i].ToString();
                }
                System.Windows.Forms.Application.DoEvents();
            }
            worksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            if (saveFileName != "")
            {
                try
                {
                    workbook.Saved = true;
                    workbook.SaveCopyAs(saveFileName);
                    //fileSaved = true;
                }
                catch (Exception ex)
                {
                    //fileSaved = false;
                    MessageBox.Show("导出文件时出错,文件可能正被打开！n" + ex.Message);
                }
            }

            xlApp.Quit();
            GC.Collect();//强行销毁 
        }

        public static void SaveToCSV(DataTable dt ,string fullPath)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.FileName = "电源控制测试结果" + System.DateTime.Now.ToString("yyyyMMddHHmmss");
            //saveFile.Filter = ".csv";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                fullPath = saveFile.FileName;
            }
            else {
                return;
            }


            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
              fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);                     
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string data = "";
                     //写出列名称
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
             }
            sw.WriteLine(data);
            //写出各行数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//替换英文冒号 英文冒号需要换成两个冒号
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //含逗号 冒号 换行符的需要放到引号中
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            DialogResult result = MessageBox.Show("CSV文件保存成功！");
            if (result == DialogResult.OK)
            {
                //System.Diagnostics.Process.Start("explorer.exe", Common.PATH_LANG);
            }
        }


        //定义提示信息枚举体
        public enum eHintInfoType
        {
            hint = 0,
            waring = 1,
            error = 2,
        }

        /*************************************************
     * 函数原型：void RangeCheck(ref byte nInputVal,byte nMinVal,byte nMaxVal)
     * 函数功能：对输入的数据进行范围限定。
     * 输入参数：
     * 输出参数：
     * 创 建 者：yzx
     * 创建日期：2016.7.23
     * 修改说明：
    */
        public static void RangeCheck(ref byte nInputVal, byte nMinVal, byte nMaxVal)
        {
            if (nInputVal < nMinVal)
            {
                nInputVal = nMinVal;
            }
            else if (nInputVal > nMaxVal)
            {
                nInputVal = nMaxVal;
            }
            else
            {
                ;
            }
        }

        /******************************************************************
         * 函数原型：void ShowHintInfor(eHintInfoType eInfoType, string strInfor)
         * 函数功能：提示信息
         * 输入参数：InfoType，提示的类型；strInfor，提示的内容
         * 输出参数：
         * 返 回 值：
         * 创 建 者：yzx
         * 创建日期：
         * 修改说明：
         * */
        public static void ShowHintInfor(eHintInfoType eInfoType, string strInfor)
        {
            switch (eInfoType)
            {
                case eHintInfoType.hint://提示类型的信息
                    MessageBox.Show(strInfor, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case eHintInfoType.waring://警告类型的信息
                    MessageBox.Show(strInfor, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    break;
                case eHintInfoType.error://错误类型的信息
                    MessageBox.Show(strInfor, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }

        }
    }
}
