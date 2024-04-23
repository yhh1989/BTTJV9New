using DevExpress.XtraEditors;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class frmGRPayment : UserBaseForm
    {
        private readonly IClientRegAppService _clientRegAppService;
        public frmGRPayment()
        {
            InitializeComponent();
            _clientRegAppService = new ClientRegAppService();
        }

        private void frmGRPayment_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = SexHelper.GetSexForPerson();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ApplicationfromDto applicationfromDto = new ApplicationfromDto();
            if (txtStartCheckDate.EditValue != null)
            {                
                applicationfromDto.StartDateTime = Convert.ToDateTime(txtStartCheckDate.DateTime.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            }
            if (txtEndCheckDate.EditValue != null)
            {                
                applicationfromDto.EndDateTime = Convert.ToDateTime(txtEndCheckDate.DateTime.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            var result = _clientRegAppService.GetGRPayMent(applicationfromDto);
            gridControl1.DataSource = result;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "自费单位统计";
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

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ApplicationfromDto applicationfromDto = new ApplicationfromDto();
            
            if (dateStar.EditValue != null)
            {
                applicationfromDto.StartDateTime = Convert.ToDateTime(dateStar.DateTime.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            }
            if (dateEnd.EditValue != null)
            {
                applicationfromDto.EndDateTime = Convert.ToDateTime(dateEnd.DateTime.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            var result = _clientRegAppService.GetClientCusMoneyList(applicationfromDto);
            gridControl2.DataSource = result;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "自费单位明细统计";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            string FileName = saveFileDialog.FileName;
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
        
            gridControl2.ExportToXls(saveFileDialog.FileName);
            if (XtraMessageBox.Show("保存成功，是否打开文件？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) ==
            DialogResult.Yes)
                Process.Start(FileName); //打开指定路径下的文件
        }
    }
}
