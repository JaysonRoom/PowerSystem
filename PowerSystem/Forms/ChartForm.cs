﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace PowerSystem.Forms
{
    public partial class ChartForm : Form
    {
        public ChartForm(Chart volChart,Chart eleChart)
        {
            InitializeComponent();

            vchart1.Series[0] = volChart.Series[0];
            eChart1.Series[0] = eleChart.Series[0];
        }
    }
}
