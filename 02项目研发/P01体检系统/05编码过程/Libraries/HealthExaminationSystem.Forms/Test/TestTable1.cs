using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Test;
using Sw.Hospital.HealthExaminationSystem.Application.Test.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;

namespace Sw.Hospital.HealthExaminationSystem.Test
{
    public partial class TestTable1 : UserBaseForm
    {
        private readonly ITestTable1AppService _testTable1AppService;

        private Stopwatch _stopwatch;

        public TestTable1()
        {
            InitializeComponent();

            _testTable1AppService = new TestTable1AppService();
            _stopwatch = new Stopwatch();
        }

        private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            if (e.OldPage == tabNavigationPage1)
            {
                gridControl1.DataSource = null;
            }
            else if (e.OldPage == tabNavigationPage2)
            {
                gridControl2.DataSource = null;
            }
            else if (e.OldPage == tabNavigationPage3)
            {
                gridControl3.DataSource = null;
            }

            if (e.Page == tabNavigationPage1)
            {
                Load1();
            }
            else if (e.Page == tabNavigationPage2)
            {
                Load2();
            }
            else if (e.Page == tabNavigationPage3)
            {
                Load3();
            }
        }

        private void TestTable1_Load(object sender, EventArgs e)
        {
            Load1();
        }

        private void Load1()
        {
            _stopwatch.Restart();
            var result = _testTable1AppService.GetAll();
            gridControl1.DataSource = result;
            _stopwatch.Stop();
            labelControl1.Text = _stopwatch.ElapsedMilliseconds.ToString("N");
        }

        private void Load2()
        {
            _stopwatch.Restart();
            var result = _testTable1AppService.GetAll1();
            gridControl2.DataSource = result;
            _stopwatch.Stop();
            labelControl2.Text = _stopwatch.ElapsedMilliseconds.ToString("N");
        }

        private void Load3()
        {
            _stopwatch.Restart();
            var result = _testTable1AppService.Query(new TestTable1Dto());
            gridControl3.DataSource = result;
            _stopwatch.Stop();
            labelControl3.Text = _stopwatch.ElapsedMilliseconds.ToString("N");
        }
    }
}