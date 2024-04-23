using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionStatistics;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccConclusionStatistics
{
    public partial class OccConclusionStatistics : UserBaseForm
    {
        private readonly IOccConclusionStatisticsAppService _OccConclusionStatisticsAppService;
        private readonly IClientInfoesAppService _IClientInfoesAppService;

        public OccConclusionStatistics()
        {
            InitializeComponent();
            _OccConclusionStatisticsAppService = new OccConclusionStatisticsAppService();
            _IClientInfoesAppService = new ClientInfoesAppService();
        }

        private void OccConclusionStatistics_Load(object sender, EventArgs e)
        {
            var occ = new OccStatisticsShowGet();
           // var results = _OccConclusionStatisticsAppService.GetCus(occ);
            //gridControl2.DataSource = DefinedCacheHelper.GetClientRegNameComDto();

            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = 2020, Name = "2020" });
            lists.Add(new EnumModel { Id = 2019, Name = "2019" });
            lists.Add(new EnumModel { Id = 2018, Name = "2018" });
            lists.Add(new EnumModel { Id = 2017, Name = "2017" });
            lookUpEdit1.Properties.DataSource = lists;

            // ClientInfoesListInput s=new ClientInfoesListInput();
            //var result = _IClientInfoesAppService.Query(s);
            var clientreglist = DefinedCacheHelper.GetClientRegNameComDto();
            comboBoxEdit1.Properties.DataSource = clientreglist;
        }
        void LoadData()
        {
            try
            {                            
                var occ = new OccStatisticsShowGet();
                if (!string.IsNullOrWhiteSpace(comboBoxEdit1.EditValue?.ToString()))
                    occ.ClientegId = (Guid)comboBoxEdit1.EditValue;

                if (comTimeType.Text.Contains("登记"))
                {
                    occ.TimeType = 1;
                }
                else
                { occ.TimeType = 2; }
                if (Startdate.EditValue != null)
                    occ.NavigationStartTime = Startdate.DateTime;

                if (Enddate.EditValue != null)
                    occ.NavigationEndTime = Enddate.DateTime.AddDays(1);
                if (lookUpEdit1.EditValue != null)
                    occ.YearTime = lookUpEdit1.Text.Trim();

                var results = _OccConclusionStatisticsAppService.GetCus(occ);
                gridControl2.DataSource = results;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBoxError(ex.ToString());
            }
          
        }

        private void simpleButton12_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void repositoryItemHyperLinkEdit1_DoubleClick(object sender, EventArgs e)
        {
 
         
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            var result = gridControl2.DataSource as List<OccConclusionStatisticsShowDto>;
            if (result != null && result.Count > 0)
            {
                var frm = new StatistalChart(result);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
            
        }

        private void simpleButton13_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "体检结论统计表";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            var ds = gridControl2.DataSource;           
            gridControl2.DataSource = ds;
            gridControl2.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }
    }
}
