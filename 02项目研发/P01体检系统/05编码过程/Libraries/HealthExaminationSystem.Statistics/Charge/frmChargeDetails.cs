using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Charge;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class frmChargeDetails : UserBaseForm
    {
        private readonly List<ProjectIStateModel> _ProjectIState;
        public CommonAppService CommonAppSrv;
        private IChargeAppService ChargeAppService;
        public frmChargeDetails()
        {
            ChargeAppService = new ChargeAppService();
            CommonAppSrv = new CommonAppService();
            _ProjectIState = ProjectIStateHelper.GetProjectIStateModels();

            InitializeComponent();
        }

        private void frmChargeDetails_Load(object sender, EventArgs e)
        {
            // 初始化时间框        
            var date = CommonAppSrv.GetDateTimeNow().Now;
            txtEndDate.DateTime = date;
            txtStarDate.DateTime = date;
            ChargeBM chargeBM = new ChargeBM();
            chargeBM.Name = "";
            var ChargeType = ChargeAppService.ChargeType(chargeBM);
            comPaymentMethod.Properties.DataSource = ChargeType;

            //单位
            var companies = DefinedCacheHelper.GetClientRegNameComDto();
            TeDW.Properties.DataSource = companies;

            //绑定医生        
            SfEdit.Properties.DataSource = DefinedCacheHelper.GetComboUsers();
        }       
       

        

        private void butSearCh_Click(object sender, EventArgs e)
        {
            var closeWait = false;
            if (!splashScreenManager.IsSplashFormVisible)
            {
                splashScreenManager.ShowWaitForm();
                closeWait = true;
            }
            try
            {
                List<MReceiptInfoDto> ReceiptInfo = new List<MReceiptInfoDto>();
                SearchInvoiceDto searchInvoice = new SearchInvoiceDto();
                if (!string.IsNullOrWhiteSpace(searchNames.Text.Trim()))
                    searchInvoice.SearchName = searchNames.Text.Trim();
                //时间
                if (txtStarDate.EditValue != null && txtEndDate.EditValue != null)
                {
                    searchInvoice.StarDate = DateTime.Parse(txtStarDate.EditValue.ToString());
                    searchInvoice.EndDate = DateTime.Parse(txtEndDate.EditValue.ToString());
                }
                searchInvoice.UserType = 2;

                if (comSFType.Text.Trim() == "收费")
                {
                    int receoptState = (int)InvoiceStatus.Valid;
                    searchInvoice.ReceiptSate = receoptState;
                }
                else if (comSFType.Text.Trim() == "作废")
                {
                    int receoptState = (int)InvoiceStatus.Cancellation;
                    searchInvoice.ReceiptSate = receoptState;
                }
                if (comPaymentMethod.EditValue != null)
                {
                    searchInvoice.ChargeType =Guid.Parse(comPaymentMethod.EditValue.ToString());
                }
                if (SfEdit.EditValue != null)
                {
                    searchInvoice.SFUserId = long.Parse(SfEdit.EditValue.ToString());
                }
                if (TeDW.EditValue != null)
                {
                    searchInvoice.ClientRegId= TeDW.EditValue as Guid?;
                }
                ReceiptInfo = ChargeAppService.GetReceiptlist(searchInvoice);
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.FileName = "日报明细";
            saveFileDialog.Title = "导出Excel";
            saveFileDialog.Filter = "Excel文件(*.xls)|*.xls";
            saveFileDialog.OverwritePrompt = false; //已存在文件是否覆盖提示
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            GridViewInvice.ExportToXls(saveFileDialog.FileName);
        }
    }
}
