using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
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
    public partial class DoctorDeparmentStatic : UserBaseForm
    {
        private readonly IChargeAppService _chargeAppService;

        public DoctorDeparmentStatic()
        {
            _chargeAppService = new ChargeAppService();
            InitializeComponent();
        }

        private void DoctorDeparmentStatic_Load(object sender, EventArgs e)
        {
            //DoctorDeparmentDto i = new DoctorDeparmentDto();
            //var result = _chargeAppService.GetDoctorDeparment(i);
            //gridControl1.DataSource = result;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DoctorDeparmentDto i = new DoctorDeparmentDto();
            if (dateEdit1.EditValue != null)
            {
                i.StartDate = (DateTime)dateEdit1.EditValue;
            }
            if (dateEdit2.EditValue != null)
            {
                i.EndDate = (DateTime)dateEdit2.EditValue;
            }
            var result = _chargeAppService.GetDoctorDeparment(i);
            gridControl1.DataSource = result;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "个人报告1";
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
