using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PowerSystem.Forms
{
    public partial class IndexForm : Form
    {
        Timer showTimer = new Timer();
        public IndexForm()
        {
            InitializeComponent();
            //居中显示
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Fixed3D;
            //显示应用程序在任务栏中的图标
            this.ShowInTaskbar = true;
            this.Opacity = 100;


            showTimer.Interval = 1000;
            showTimer.Tick += FN_ShowMainForm;
            showTimer.Enabled = true;
        }
        private void FN_ShowMainForm(object sender, EventArgs e) {

            this.Opacity = this.Opacity - 10;
            if (this.Opacity == 0)
            {
                this.Hide();
                showTimer.Enabled = false;
                MainForm mform = new MainForm("电源控制系统");
                mform.Show();
            }
        }

        private void btnPower1_Click(object sender, EventArgs e)
        {           
            MainForm mform = new MainForm("电源控制系统-电源1");
            mform.Show();
        }

        private void btnPower2_Click(object sender, EventArgs e)
        {
            MainForm mform = new MainForm("电源控制系统-电源2");
            mform.Show();
        }
    }
}
