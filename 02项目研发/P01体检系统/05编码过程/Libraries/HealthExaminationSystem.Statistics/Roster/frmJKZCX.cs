using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    public partial class frmJKZCX : UserBaseForm
    {
        public frmJKZCX()
        {
            InitializeComponent();
            _inspectionTotalService = new InspectionTotalAppService();
        }
        private readonly IInspectionTotalAppService _inspectionTotalService;
        private void frmJKZCX_Load(object sender, EventArgs e)
        {
            repositoryItemLookUpEdit1.DataSource = SexHelper.GetSexForPerson();
        }
        public void LoadData(string cusRegBM = "")
        {
            AutoLoading(() =>
            {
                gridControl1.DataSource = null;

                // gridView1.FocusedRowHandle = -1;
                var input = new InSearchCusDto();
                if (cusRegBM != "")
                { input.CusNameBM = cusRegBM; }
                else
                {
                    if (!string.IsNullOrEmpty(textName.Text.Trim()))
                    {
                        input.CusNameBM = textName.Text.Trim();
                    }

                    if (textName.Text.Trim() == string.Empty)
                    {

                            if (dateSumStar.EditValue != null)
                                input.SumStar = dateSumStar.DateTime;
                            if (dateSumEnd.EditValue != null)
                                input.SumEnd = dateSumEnd.DateTime.AddDays(1);
                            if (input.SumStar > input.SumEnd)
                            {
                                ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                                return;
                            }
                       
                        }
                    }

                    if (radioIsOK.EditValue != null && radioIsOK.EditValue?.ToString() != "全部")
                    {
                        input.Qualified = radioIsOK.EditValue?.ToString();
                    }
               
                var output = _inspectionTotalService.GetJKZOutCuslist(input).ToList();
                var sum = output.Count();
               
                gridControl1.DataSource = output;


            });
        }
        private void butSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            // Export();
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "健康证查询";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            gridControl1.ExportToXls(saveFileDialog.FileName);
        }
    }
}
