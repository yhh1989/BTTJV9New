using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview;
using Sw.Hospital.HealthExaminationSystem.Application.OccReview.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
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
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccReview
{
    public partial class OccReviewList : UserBaseForm
    {
        private readonly IOccReviewAppService _OccReviewAppService;
        private readonly IClientInfoesAppService _IClientInfoesAppService;

        public OccReviewList()
        {
            InitializeComponent();
            _OccReviewAppService = new OccReviewAppService();
            _IClientInfoesAppService = new ClientInfoesAppService();
        }

        private void OccReviewList_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = SexHelper.GetSexForPerson();
            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = System.DateTime.Now.Year, Name = System.DateTime.Now.Year.ToString() });
            lists.Add(new EnumModel { Id = System.DateTime.Now.AddYears(-1).Year, Name = System.DateTime.Now.AddYears(-1).Year.ToString() });
            lists.Add(new EnumModel { Id = System.DateTime.Now.AddYears(-2).Year, Name = System.DateTime.Now.AddYears(-2).Year.ToString() });
            lists.Add(new EnumModel { Id = System.DateTime.Now.AddYears(-3).Year, Name = System.DateTime.Now.AddYears(-3).Year.ToString() });
            comboBoxEdit1.Properties.DataSource = lists;

            OutOccReviewDto show = new OutOccReviewDto();
            var data = _OccReviewAppService.GetOutOccReviewDto(show);
            gridControl1.DataSource = data;

            //ClientInfoesListInput s = new ClientInfoesListInput();
            //var result = _IClientInfoesAppService.Query(s);
            textEdit1.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OutOccReviewDto show = new OutOccReviewDto();
            if (!string.IsNullOrWhiteSpace(textEdit1.EditValue?.ToString()))
            {
                show.ClientRegId =(Guid)textEdit1.EditValue;
            }
            if (dateEditStartTime.EditValue != null)
                show.StartCheckDate = dateEditStartTime.DateTime;

            if (dateEditEndTime.EditValue != null)
                show.EndCheckDate = dateEditEndTime.DateTime;

            if (show.StartCheckDate > show.EndCheckDate)
            {
                dxErrorProvider.SetError(dateEditStartTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                dxErrorProvider.SetError(dateEditEndTime, string.Format(Variables.GreaterThanTips, "开始时间", "结束时间"));
                return;
            }
            if (comboBoxEdit1.EditValue != null)
                show.YearTime = comboBoxEdit1.Text.Trim();

           

            var data = _OccReviewAppService.GetOutOccReviewDto(show);
            gridControl1.DataSource = data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var result = gridControl1.DataSource as List<OutOccReviewDto>;
            if (result != null && result.Count > 0)
            {
                var frm = new OccReviewStatic(result);
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("没有数据！");
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "复查项目统计";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            var ds = gridControl1.DataSource;
            gridControl1.DataSource = ds;
            gridControl1.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }
    }
}
