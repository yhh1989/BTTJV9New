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
    public partial class IndividualityStatic : UserBaseForm
    {
        private readonly IChargeAppService _chargeAppService;

        public IndividualityStatic()
        {
            _chargeAppService = new ChargeAppService();
            InitializeComponent();
        }

        private void IndividualityStatic_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            IndividualityDto i = new IndividualityDto();
            if (dateEdit1.EditValue != null)
            {
                i.StartDate = Convert.ToDateTime(dateEdit1.DateTime.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            }
            if (dateEdit2.EditValue != null)
            {
                i.EndDate = Convert.ToDateTime(dateEdit2.DateTime.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            var result = _chargeAppService.GetIndividuality(i);
            gridControl1.DataSource = result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IndividualitiesDto dto = new IndividualitiesDto();
            if (dateEdit3.EditValue != null)
            {
                dto.StartDate = Convert.ToDateTime(dateEdit3.DateTime.Date.ToString("yyyy-MM-dd") + " 00:00:00");
            }
            if (dateEdit4.EditValue != null)
            {
                dto.EndDate = Convert.ToDateTime(dateEdit4.DateTime.Date.ToString("yyyy-MM-dd") + " 23:59:59");
            }
            var result = _chargeAppService.GetIndividualities(dto);
            gridControl2.DataSource = result;
        }

        private void button3_Click(object sender, EventArgs e)
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

        private void button4_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "个人报告2";
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

        private void gridView1_CellMerge(object sender, DevExpress.XtraGrid.Views.Grid.CellMergeEventArgs e)
        {
            string str1 = gridView1.GetRowCellValue(e.RowHandle1, "CusRegBM").ToString();
            string str2 = gridView1.GetRowCellValue(e.RowHandle2, "CusRegBM").ToString();
           
            if (str1!= str2)
            {
                
                e.Handled = true;
            }

        }
    }
}
