using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
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
    public partial class FrmMoneySummary : UserBaseForm
    {
        private readonly IClientRegAppService _clientRegAppService;
        public FrmMoneySummary()
        {
            InitializeComponent();
            _clientRegAppService = new ClientRegAppService();
        }

        private void FrmMoneySummary_Load(object sender, EventArgs e)
        {            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ApplicationfromDto applicationfromDto = new ApplicationfromDto();
            if (txtStartCheckDate.EditValue != null)
            {
                //applicationfromDto.StartDateTime =(DateTime?)txtStartCheckDate.EditValue;
                applicationfromDto.StartDateTime = Convert.ToDateTime(txtStartCheckDate.DateTime.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            }
            if (txtEndCheckDate.EditValue != null)
            {
                //applicationfromDto.EndDateTime = (DateTime?)txtEndCheckDate.EditValue;
                applicationfromDto.EndDateTime = Convert.ToDateTime(txtEndCheckDate.DateTime.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            if (comDateType.Text.Contains("收费日期"))
            { applicationfromDto.DateType = 1; }
            else if (comDateType.Text.Contains("申请单日期"))
            { applicationfromDto.DateType = 2; }
           

            var result = _clientRegAppService.GetMonrySummary(applicationfromDto);
            gridControl1.DataSource = result;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "体检费用总表";
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
