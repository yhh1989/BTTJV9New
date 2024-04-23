﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using CacheHelper = Sw.Hospital.HealthExaminationSystem.Common.UserCache.CacheHelper;

namespace HealthExaminationSystem.Win
{
    public partial class StartupTest : XtraForm
    {
        public StartupTest()
        {
            InitializeComponent();
            // This line of code is generated by Data Source Configuration Wizard
            // Fill a ExcelDataSource
            //excelDataSource1.Fill();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                excelDataSource1.FileName = openFileDialog1.FileName;
                excelDataSource1.Fill();
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //AppearanceObject.DefaultMenuFont = new Font(AppearanceObject.DefaultMenuFont.Name, AppearanceObject.DefaultMenuFont.Size - 5);
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            //var count = gridView1.RowCount;
            //for (int i = 0; i < count; i++)
            //{
            //    var row = gridView1.GetRow(i);
            //    //var t = excelDataSource1.r;
            //}
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(0.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(1.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(11.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(1111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(11111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(1111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(11111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(1111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(11111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(111111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(1111111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(11111111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(111111111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(1111111111111111.11));
            XtraMessageBox.Show(CommonHelper.ConvertToChinese(11111111111111111.11));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(56.23));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(-56.23));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(56));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(-56));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(0));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(-0));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(5));
            //XtraMessageBox.Show(CommonHelper.ConvertToChinese(-5));
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show(CacheHelper.GetCacheItem("8B2970E9-95AF-4482-9861-535894E3ADFE", IntResult).ToString());
        }

        private int IntResult()
        {
            return 10;
        }

        private int IntResult1()
        {
            return 20;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show(CacheHelper.UpdateCacheItem("8B2970E9-95AF-4482-9861-535894E3ADFE", IntResult1).ToString());
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var result = VerificationHelper.IsManByIdCard("130435199807091823");
            if (result == true)
            {
                XtraMessageBox.Show("男");
            }
            else if (result == false)
            {
                XtraMessageBox.Show("女");
            }
            else
            {
                XtraMessageBox.Show("身份证不正确");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var result = VerificationHelper.GetBirthdayByIdCard("130435199002281814");
            if (result.HasValue)
            {
                XtraMessageBox.Show(result.Value.ToString("d"));
            }
            else
            {
                XtraMessageBox.Show("身份证不正确");
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            object a = "";
            if (a.Equals(string.Empty))
            {
                XtraMessageBox.Show("ddd");
            }
        }
    }

    public class Ttttt : IParameter
    {
        public string Name { get; }

        public Type Type { get; }

        public object Value { get; set; }
    }
}
