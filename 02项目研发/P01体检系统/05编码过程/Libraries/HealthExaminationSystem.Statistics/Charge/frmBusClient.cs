using DevExpress.Utils;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
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
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class frmBusClient : UserBaseForm
    {
        private ICustomerAppService _customerAppService;
        private readonly ICommonAppService _commonAppService;
        public frmBusClient()
        {
            InitializeComponent();
            _customerAppService = new CustomerAppService();
            _commonAppService = new CommonAppService();
        }

        private void frmBusClient_Load(object sender, EventArgs e)
        {
            // 初始化时间框
            var date = _commonAppService.GetDateTimeNow().Now;
            dateEditEnd.DateTime = date;
            dateEditStart.DateTime = date;

            dateEditStarGR.DateTime = date;
            dateEditendGR.DateTime = date;
            var clientreg = DefinedCacheHelper.GetClientRegNameComDto();      
            //单位、分组
            txtClientRegID.Properties.DataSource = clientreg;

            var _currentUserdtoSys = DefinedCacheHelper.GetComboUsers();
            //绑定检查医生
            cglueJianChaYiSheng.Properties.DataSource = _currentUserdtoSys;
            cglueJianChaYiSheng.EditValue = CurrentUser.Id;

            gridView4.Columns[conSex.FieldName].DisplayFormat.FormatType = FormatType.Custom;
            gridView4.Columns[conSex.FieldName].DisplayFormat.Format =
                new CustomFormatter(SexHelper.CustomSexFormatter);
        }

        private void simpleButtonExport_Click(object sender, EventArgs e)
        {
            //// Export();
            //var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //saveFileDialog.FileName = "商务订单（团检）";
            //saveFileDialog.Title = "导出Excel";
            //saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            //saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            //var dialogResult = saveFileDialog.ShowDialog();
            //if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    return;
            //gridControl1.ExportToXls(saveFileDialog.FileName);

            ExcelHelper.ExportToExcel("商务订单（团检）", gridControlCus);
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
                input.UserId = (long)cglueJianChaYiSheng.EditValue;
            }
            var result = _customerAppService.SearchBusiCount(input);

            gridControl1.DataSource = result;
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            InSearchBusiCusDto input = new InSearchBusiCusDto();
            if (!string.IsNullOrEmpty(dateEditStarGR.EditValue?.ToString()))
            {
                input.StarDate = dateEditStarGR.DateTime;
            }
            if (!string.IsNullOrEmpty(dateEditendGR.EditValue?.ToString()))
            {
                input.EndDate = dateEditendGR.DateTime.AddDays(1);
            }           
            if (!string.IsNullOrEmpty(cglueJianChaYiSheng1.EditValue?.ToString()))
            {
                input.UserId = (long)cglueJianChaYiSheng1.EditValue;
            }
            var result = _customerAppService.GRBusiCount(input);
            gridControlCus.DataSource = result;

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //// Export();
            //var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            //saveFileDialog.FileName = "商务订单（个检）";
            //saveFileDialog.Title = "导出Excel";
            //saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            //saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            //var dialogResult = saveFileDialog.ShowDialog();
            //if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
            //    return;
            //gridControlCus.ExportToXls(saveFileDialog.FileName);

            ExcelHelper.ExportToExcel("商务订单（个检）", gridControlCus);
        }
    }
}
