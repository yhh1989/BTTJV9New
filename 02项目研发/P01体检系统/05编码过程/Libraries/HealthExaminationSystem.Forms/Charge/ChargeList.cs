using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;
using Sw.His.Common.Functional.Unit.CustomTools;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Charge
{
    public partial class ChargeList : UserBaseForm
    {
        private string _customerBm;
        private CommonAppService CommonAppSrv;
        private IChargeAppService ChargeAppService;
        public ChargeList()
        {
            InitializeComponent();
            ChargeAppService = new ChargeAppService();
            CommonAppSrv = new CommonAppService();
        }
        public ChargeList(string customerBm):this()
        {
            _customerBm = customerBm;
        }
        private void ChargeList_Load(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            try
            {
                GridViewInvice.Columns[ReceiptSate.FieldName].DisplayFormat.Format = new CustomFormatter(FormatReceiptSate);
                List<MReceiptInfoDto> ReceiptInfo = new List<MReceiptInfoDto>();
                SearchInvoiceDto searchInvoice = new SearchInvoiceDto();
                if (!string.IsNullOrWhiteSpace(_customerBm))
                    searchInvoice.SearchName = _customerBm;               
              
                ReceiptInfo = ChargeAppService.GetInvalidReceipt(searchInvoice);
                gridInvice.DataSource = ReceiptInfo;
            }
            catch (UserFriendlyException ex)
            {
                ShowMessageBox(ex);
            }
            finally
            {
                if (closeWait)
                {
                    if (splashScreenManager.IsSplashFormVisible)
                        splashScreenManager.CloseWaitForm();
                }
            }
        }
        private string FormatReceiptSate(object arg)
        {
            try
            {
                return InvoiceStatusHelper.PayerCatInvoiceStatus(arg);
            }
            catch
            {
                return "";
            }
        }
        private void butExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ExportList_Click(object sender, EventArgs e)
        {
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.FileName = "收费列表";
            //saveFileDialog.DefaultExt = "xls";
            //saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx|Excel文件(*.xls)|*.xls";
            //if (saveFileDialog.ShowDialog() != DialogResult.OK)   //(saveFileDialog.ShowDialog() == DialogResult.OK)重名则会提示重复是否要覆盖
            //    return;

            // Export(gridInvice, saveFileDialog);
            ExcelHelper.ExportToExcel("收费列表", gridInvice);
            //saveFileDialog.FileName = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf("\\")) + "\\单位分组信息.xls";
            //XtraMessageBox.Show("保存成功");
        }
        public void Export(GridControl gridControl, SaveFileDialog saveFileDialog)
        {

            XlsExportOptions options = new XlsExportOptions();
            var FileName = saveFileDialog.FileName;
            if (FileName.LastIndexOf(".xlsx") >= FileName.Length - 5)
            {
                XlsxExportOptions xoptions = new XlsxExportOptions();
                gridControl.ExportToXlsx(FileName, xoptions);
            }
            else
            {
                gridControl.ExportToXls(FileName, options);
            }
        }

        private void butPrintInvoice_Click(object sender, EventArgs e)
        {

        }

      
    }
}
