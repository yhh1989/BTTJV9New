using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Roster
{
    public partial class FormBusCus : UserBaseForm
    {
        private   ICustomerAppService _customerAppService;
        private readonly ICommonAppService _commonAppService;
        public FormBusCus()
        {
            InitializeComponent();
            _customerAppService = new CustomerAppService();
            _commonAppService = new CommonAppService();
        }

        private void FormBusCus_Load(object sender, EventArgs e)
        {
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStart.DateTime = date;
            var clientreg = DefinedCacheHelper.GetClientRegNameComDto();
            //单位、分组
            txtClientRegID.Properties.DataSource = clientreg;

          var  _currentUserdtoSys = DefinedCacheHelper.GetComboUsers();
            //绑定检查医生
            cglueJianChaYiSheng.Properties.DataSource = _currentUserdtoSys;
            cglueJianChaYiSheng.EditValue = CurrentUser.Id;
            gridView1.Columns[conPrintSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conPrintSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(CheckSateHelper.PrintSateFormatter);
            gridView1.Columns[conSummSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conSummSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(SummSateHelper.SummSateFormatter);
            gridView1.Columns[conCheckSate.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conCheckSate.FieldName].DisplayFormat.Format =
                new CustomFormatter(CheckSateHelper.ProjectIStateFormatter);
            gridView1.Columns[conRegisterState.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView1.Columns[conRegisterState.FieldName].DisplayFormat.Format =
                new CustomFormatter(RegisterStateHelper.ExaminationFormatter);
            
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            InSearchBusiCusDto input = new InSearchBusiCusDto();
            if (!string.IsNullOrEmpty(dateEditStart.EditValue?.ToString()))
            {
                input.StarDate = dateEditStart.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEditEnd.EditValue?.ToString()))
            {
                input.EndDate = dateEditEnd.DateTime.AddDays(1);
            }
            if (!string.IsNullOrEmpty(txtClientRegID.EditValue?.ToString()))
            {
                input.ClientRegId = (Guid)txtClientRegID.EditValue;
            }
            if (!string.IsNullOrEmpty(cglueJianChaYiSheng.EditValue?.ToString()))
            {
                input.UserId =  (long)cglueJianChaYiSheng.EditValue;
            }
            var result = _customerAppService.SearchBusiCus(input);
            gridControlCus.DataSource = result;
        }

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            // Export();
            //var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //saveFileDialog.FileName = "体检档案";
            //saveFileDialog.Title = "导出Excel";
            //saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            //saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            //var dialogResult = saveFileDialog.ShowDialog();
            //if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    return;
            //gridControlCus.ExportToXls(saveFileDialog.FileName);

            ExcelHelper.ExportToExcel("订单进展", gridControlCus);
        }
    }
}
