using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class GroupToCheck : UserBaseForm
    {
        private readonly IChargeAppService _chargeAppService;

        public GroupToCheck()
        {
            _chargeAppService = new ChargeAppService();
            InitializeComponent();
        }

        private void GroupToCheck_Load(object sender, EventArgs e)
        {
            //ThreeBallCheckDto show = new ThreeBallCheckDto();
            //var result = _chargeAppService.GetThreeBallChecks(show);
            //gridControl1.DataSource = result;


            lookClient.Properties.DataSource = DefinedCacheHelper.GetClientRegNameComDto();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ThreeBallCheckDto show = new ThreeBallCheckDto();
            if (Startdate.EditValue != null)
            {
                show.StartDateTime = (DateTime)Startdate.EditValue;
            }
            if (Enddate.EditValue != null)
            {
                var endTime= (DateTime)Enddate.EditValue;
                
                show.EndDateTime = endTime.AddDays(1);
            }
            if (lookClient.EditValue != null && lookClient.EditValue != "")
            {
                show.ClientRegId = (Guid)lookClient.EditValue;
            }
            var result = _chargeAppService.GetThreeBallChecks(show);
            gridControl1.DataSource = result;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "团体统计3";
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
