using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using gregn6Lib;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod;
using Sw.Hospital.HealthExaminationSystem.Application.PaymentMethod.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.PaymentManager.DailyReport
{
    public partial class DailyReport : UserBaseForm
    {
        GridppReport Report = new GridppReport();
        GridppReport SubChargeType = new GridppReport();
        GridppReport SubInvalidInvoices = new GridppReport();
        ViewDailyReportDto dailyReport = new ViewDailyReportDto();
        private readonly IUserAppService _userAppService;
        private readonly ICommonAppService _commonAppService;
        private readonly IPaymentMethodAppService _paymentMethodAppService;
        private readonly IPaymentStatisticAppService _paymentStatisticAppService;
        public DailyReport()
        {
            _userAppService = new UserAppService();
            _commonAppService = new CommonAppService();
            _paymentMethodAppService = new PaymentMethodAppService();
            _paymentStatisticAppService = new PaymentStatisticAppService();
            InitializeComponent();
            InitializeDefaultData();
            //ShowReport();
        }
        private void InitializeDefaultData()
        {
            var date = _commonAppService.GetDateTimeNow().Now;
            datestar.DateTime = date;
            this.dateSelect.DateTime = date; //AddDays for saving time section
            List<UserFormDto> users = _userAppService.GetUsers();
            lueCollector.Properties.DataSource = users;

            //lueCollector.Properties.Columns.Clear();
            //lueCollector.Properties.Columns.Add(new LookUpColumnInfo(nameof(UserFormDto.Name), 
            //    "收费员"));

            lueCollector.Properties.DisplayMember = nameof(UserFormDto.Name);
            lueCollector.Properties.ValueMember = nameof(UserFormDto.Id);
            lueCollector.EditValue = CurrentUser.Id;
            string ReportFile = GridppHelper.GetTemplate("日报.grf");
            Report.LoadFromURL(ReportFile);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        private void ShowReport()
        {
            var input = new SearchReceiptInfoDto
            {
              
                strChargeDate = this.datestar.DateTime,
                ChargeDate = this.dateSelect.DateTime,
                GroupCharge = (int)ChargeApply.Company,
                PersonalCharge = (int)ChargeApply.Personal,
                ReceiptState = (int)ReceiptState.UnSettled,
                InvoiceStatus = Convert.ToInt16(InvoiceStatus.Cancellation).ToString()
            };
           if (!string.IsNullOrEmpty(lueCollector.EditValue?.ToString()))
            {
                input.UserId = (long)this.lueCollector.EditValue;
            }
            dailyReport = _paymentStatisticAppService.GetDailyReport(input);
            
            rptDaily.Stop();
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);

            SubChargeType = Report.ControlByName("SubChargeType").AsSubReport.Report;
            SubChargeType.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(FetchChargeTypes);
            SubChargeType.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(FetchChargeTypes);

            //SubInvalidInvoices = Report.ControlByName("SubInvalidInvoices").AsSubReport.Report;
            //SubInvalidInvoices.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(FetchInvalidInvoices);
            //SubInvalidInvoices.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(FetchInvalidInvoices);

            rptDaily.Report = Report;
            rptDaily.Start();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            rptDaily.Stop();Close();
        }
        private void ReportFetchRecord()
        {
            Report.DetailGrid.Recordset.Append();
            Report.FieldByName("打印日期").AsString = _commonAppService.GetDateTimeNow().Now.Date.ToLongDateString();
            Report.FieldByName("结算日期").AsString = this.dateSelect.DateTime.ToLongDateString();
            Report.FieldByName("收费员").AsString = this.lueCollector.Text;
            Report.FieldByName("发票号段").AsString = $"{dailyReport.MinInvoiceNum} - {dailyReport.MaxInvoiceNum}";
            Report.FieldByName("个人体检").AsCurrency = dailyReport.IndividualInvoiceMoney.Value;
            Report.FieldByName("个人体检张数").AsInteger = dailyReport.IndividualInvoiceCount.Value;
            Report.FieldByName("团体体检").AsCurrency = dailyReport.GroupInvoiceMoney.Value;
            Report.FieldByName("团体体检张数").AsInteger = dailyReport.GroupInvoiceCount.Value;
            Report.FieldByName("实收金额").AsCurrency = dailyReport.ActualMoney.Value;
            Report.FieldByName("大写金额").AsCurrency = dailyReport.ActualMoney.Value;
            Report.DetailGrid.Recordset.Post();
        }
        private void FetchChargeTypes()
        {
            foreach (var chargeType in dailyReport.ChargeTypes)
            {
                SubChargeType.DetailGrid.Recordset.Append();
                SubChargeType.FieldByName("收费方式").AsString = chargeType.TypeName;
                if (chargeType.TypeName == "健康卡")
                {
                    SubChargeType.FieldByName("实收金额").AsCurrency =0;
               
                    SubChargeType.FieldByName("收费总额").AsCurrency =0;

                    SubChargeType.FieldByName("作废金额").AsCurrency = 0;
                }
                else
                {
                    var money = dailyReport.ChargeTypes.Where(p => p.TypeName == chargeType.TypeName).Sum(p => p.TypeTotal) ?? 0;
                    //金东方改成按人员
                    // SubChargeType.FieldByName("实收金额").AsCurrency = chargeType.TypeTotal.Value;
                    SubChargeType.FieldByName("实收金额").AsCurrency = dailyReport.ChargeTypes.Where(p => p.TypeName == chargeType.TypeName).Sum(p=>p.TypeTotal)??0;
                    SubChargeType.FieldByName("收费总额").AsCurrency = chargeType.allMoney ?? 0;

                    SubChargeType.FieldByName("作废金额").AsCurrency = chargeType.ZFMoney ?? 0;
                    if (SubChargeType.FieldByName("收费人")!=null)
                    {
                        SubChargeType.FieldByName("收费人").AsString = chargeType.UserName;
                    }
                }
                SubChargeType.DetailGrid.Recordset.Post();
            }
        }
        private void FetchInvalidInvoices()
        {
            SubInvalidInvoices.ControlByName("作废张数").AsStaticBox.Text = $"共 {dailyReport.InvalidInvoices.Count()} 张";
            foreach (var invalidInvoice in dailyReport.InvalidInvoices)
            {
                SubInvalidInvoices.DetailGrid.Recordset.Append();
                SubInvalidInvoices.FieldByName("作废票据").AsString = invalidInvoice.InvoiceNum;
                SubInvalidInvoices.FieldByName("作废金额").AsCurrency = invalidInvoice.InvoiceMoney;
                SubInvalidInvoices.DetailGrid.Recordset.Post();
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
            rptDaily.Report.PrintPreview(true);
        }

        private void DailyReport_Load(object sender, EventArgs e)
        {

        }
    }
}
