using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes;
using Sw.Hospital.HealthExaminationSystem.Application.ClientInfoes.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult;
using Sw.Hospital.HealthExaminationSystem.Application.OccAbnormalResult.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccAbnormalResult
{
    public partial class OccAbnormalResultList : UserBaseForm
    {
        private readonly IOccAbnormalResultAppService _OccAbnormalResultAppService;
        private readonly IClientInfoesAppService _IClientInfoesAppService;
        public OccAbnormalResultList()
        {
            InitializeComponent();
            _OccAbnormalResultAppService = new OccAbnormalResultAppService();
            _IClientInfoesAppService = new ClientInfoesAppService();
        }

        private void OccAbnormalResultList_Load(object sender, EventArgs e)
        {
            var lists = new List<EnumModel>();
            lists.Add(new EnumModel { Id = 2020, Name = "2020" });
            lists.Add(new EnumModel { Id = 2019, Name = "2019" });
            lists.Add(new EnumModel { Id = 2018, Name = "2018" });
            lists.Add(new EnumModel { Id = 2017, Name = "2017" });
            comboBoxEdit1.Properties.DataSource = lists;

            OutOccAbnormalResult show = new OutOccAbnormalResult();
            var data = _OccAbnormalResultAppService.GetOccAbnormalResult(show);
            gridControl1.DataSource = data;

            ClientInfoesListInput s = new ClientInfoesListInput();
            var result = _IClientInfoesAppService.Query(s);
            textEdit1.Properties.DataSource = result;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            OutOccAbnormalResult show = new OutOccAbnormalResult();
            if (!string.IsNullOrWhiteSpace(textEdit1.Text.Trim()))
            {
                show.ClientName = textEdit1.Text.Trim();
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

          

            var data = _OccAbnormalResultAppService.GetOccAbnormalResult(show);
            gridControl1.DataSource = data;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var result = gridControl1.DataSource as List<OutOccAbnormalResult>;
            if (result != null && result.Count > 0)
            {
                var frm = new OccAbnormalResultStatic(result);
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
            saveFileDialog.FileName = "异常结果检出统计";
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
