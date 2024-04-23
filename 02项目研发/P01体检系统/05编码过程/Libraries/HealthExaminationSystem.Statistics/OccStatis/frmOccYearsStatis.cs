using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccConclusionStatistics;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.OccConclusionStatistics.Dto;
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

namespace Sw.Hospital.HealthExaminationSystem.Statistics.OccStatis
{
    public partial class frmOccYearsStatis : UserBaseForm
    { 
        private OccConclusionStatisticsAppService occConclusionStatisticsAppService = new OccConclusionStatisticsAppService();
        GridppReport Report = new GridppReport();
        GridppReport ClientReport = new GridppReport();
        GridppReport RiskReport = new GridppReport();
        GridppReport zybReport = new GridppReport();
        GridppReport jjzReport = new GridppReport();
        private OccYearsAllStaticsDto dwtj = new OccYearsAllStaticsDto();
        private readonly ICommonAppService _commonAppService;
        public frmOccYearsStatis()
        {
            InitializeComponent();
            _commonAppService = new CommonAppService();
        }

        private void frmOccYearsStatis_Load(object sender, EventArgs e)
        {
          
            var rep = GridppHelper.GetTemplate("职业健康检查结果汇总表.grf");
            Report.LoadFromURL(rep);
            ToYearStyle(dateEditYear);
            dateEditYear.EditValue= _commonAppService.GetDateTimeNow().Now;
            //rptDaily.Stop();
            ////Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            ////Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            //rptDaily.Report = Report;
            //rptDaily.Start();
        }
        void ToYearStyle(DevExpress.XtraEditors.DateEdit dateEdit, bool touchUI = false)
        {
            dateEdit.Properties.Mask.EditMask = "yyyy";
            if (touchUI)
            {
                dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            }
            else
                dateEdit.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Vista;
            dateEdit.Properties.ShowToday = false;
            dateEdit.Properties.ShowMonthHeaders = false;
            dateEdit.Properties.VistaCalendarInitialViewStyle = DevExpress.XtraEditors.VistaCalendarInitialViewStyle.YearsGroupView;
            dateEdit.Properties.VistaCalendarViewStyle = DevExpress.XtraEditors.VistaCalendarViewStyle.YearsGroupView;

            dateEdit.Properties.Mask.UseMaskAsDisplayFormat = false;

            dateEdit.Properties.DisplayFormat.FormatString = "yyyy";
            dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEdit.Properties.EditFormat.FormatString = "yyyy";
            dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;


        }
        private void ReportFetchRecord()
        {
            Report.DetailGrid.Recordset.Append();
            //Report.FieldByName("打印日期").AsString = _commonAppService.GetDateTimeNow().Now.Date.ToLongDateString();
            //Report.FieldByName("结算日期").AsString = this.dateSelect.DateTime.ToLongDateString();
            //Report.FieldByName("收费员").AsString = this.lueCollector.Text;
            //Report.FieldByName("发票号段").AsString = $"{dailyReport.MinInvoiceNum} - {dailyReport.MaxInvoiceNum}";
            //Report.FieldByName("个人体检").AsCurrency = dailyReport.IndividualInvoiceMoney.Value;
            //Report.FieldByName("个人体检张数").AsInteger = dailyReport.IndividualInvoiceCount.Value;
            //Report.FieldByName("团体体检").AsCurrency = dailyReport.GroupInvoiceMoney.Value;
            //Report.FieldByName("团体体检张数").AsInteger = dailyReport.GroupInvoiceCount.Value;
            //Report.FieldByName("实收金额").AsCurrency = dailyReport.ActualMoney.Value;
            //Report.FieldByName("大写金额").AsCurrency = dailyReport.ActualMoney.Value;
            Report.DetailGrid.Recordset.Post();

        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ShowReport();
        }
        private void Seach()
        {

            rptDaily.Stop();
            OccYearsStatisticsDto occYearsStatistics = new OccYearsStatisticsDto();
            occYearsStatistics.StarDate = dateEditYear.DateTime;

             dwtj = occConclusionStatisticsAppService.getOCCYears(occYearsStatistics);

            if (Report.ControlByName("人单位统计") != null)
            {
                
                var subReportExamine = Report.ControlByName("人单位统计").AsSubReport.Report;
                var reportJsondwtjl = JsonConvert.SerializeObject(dwtj.ClientStatic);
                var reportJsondwtj = "{\"Detail\":" + reportJsondwtjl + "}";
                subReportExamine.LoadDataFromXML(reportJsondwtj);
            }
            if (Report.ControlByName("害因素类别统计") != null)
            {              
                
                var subReportExamine = Report.ControlByName("害因素类别统计").AsSubReport.Report;
                var reportJsondwtjl = JsonConvert.SerializeObject(dwtj.RiskTypeStatic);
                var reportJsondwtj = "{\"Detail\":" + reportJsondwtjl + "}";
                subReportExamine.LoadDataFromXML(reportJsondwtj);
            }
            if (Report.ControlByName("禁忌证") != null)
            {

                var subReportExamine = Report.ControlByName("禁忌证").AsSubReport.Report;
                var reportJsondwtjl = JsonConvert.SerializeObject(dwtj.JJZStatistics);
                var reportJsondwtj = "{\"Detail\":" + reportJsondwtjl + "}";
                subReportExamine.LoadDataFromXML(reportJsondwtj);
            }
            if (Report.ControlByName("职业病") != null)
            {

                var subReportExamine = Report.ControlByName("职业病").AsSubReport.Report;
                var reportJsondwtjl = JsonConvert.SerializeObject(dwtj.ZYBStatistics);
                var reportJsondwtj = "{\"Detail\":" + reportJsondwtjl + "}";
                subReportExamine.LoadDataFromXML(reportJsondwtj);
            }
            rptDaily.Report = Report;
            rptDaily.Start();
        }

        private void ShowReport()
        {
            OccYearsStatisticsDto occYearsStatistics = new OccYearsStatisticsDto();
            occYearsStatistics.StarDate = dateEditYear.DateTime;

             dwtj = occConclusionStatisticsAppService.getOCCYears(occYearsStatistics);
            rptDaily.Stop();
            Report.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            Report.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
            if (dwtj.ClientStatic != null)
            {
                ClientReport = Report.ControlByName("人单位统计").AsSubReport.Report;
                ClientReport.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(FetchClient);
                ClientReport.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(FetchClient);
            }
            if (dwtj.RiskTypeStatic != null)
            {
                RiskReport = Report.ControlByName("害因素类别统计").AsSubReport.Report;
                RiskReport.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(FetchRisk);
                RiskReport.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(FetchRisk);
            }

            if (dwtj.ZYBStatistics != null)
            {
                zybReport = Report.ControlByName("职业病").AsSubReport.Report;
                zybReport.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(Fetchzyb);
                zybReport.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Fetchzyb);
            }
            if (dwtj.JJZStatistics != null)
            {

                jjzReport = Report.ControlByName("禁忌证").AsSubReport.Report;
                jjzReport.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(Fetchjjz);
                jjzReport.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(Fetchjjz);
            }

            //SubInvalidInvoices = Report.ControlByName("SubInvalidInvoices").AsSubReport.Report;
            //SubInvalidInvoices.FetchRecord -= new _IGridppReportEvents_FetchRecordEventHandler(FetchInvalidInvoices);
            //SubInvalidInvoices.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(FetchInvalidInvoices);

            rptDaily.Report = Report;
            rptDaily.Start();
        }
        /// <summary>
        /// 按单位
        /// </summary>
        private void FetchClient()
        {
            if (dwtj.ClientStatic != null)
            {
                foreach (var Client in dwtj.ClientStatic)
                {
                    ClientReport.DetailGrid.Recordset.Append();
                    ClientReport.FieldByName("SaticsName").AsString = Client.SaticsName;
                    ClientReport.FieldByName("BeforeCheckCount").AsString = Client.BeforeCheckCount?.ToString();
                    ClientReport.FieldByName("BeforeJJZCount").AsString = Client.BeforeJJZCount?.ToString();
                    ClientReport.FieldByName("OnCheckCount").AsString = Client.OnCheckCount?.ToString();
                    ClientReport.FieldByName("OnJJZCount").AsString = Client.OnJJZCount?.ToString();
                    ClientReport.FieldByName("OnZYBCount").AsString = Client.OnZYBCount?.ToString();
                    ClientReport.FieldByName("OnFCCount").AsString = Client.OnFCCount?.ToString();
                    ClientReport.FieldByName("AfterCheckCount").AsString = Client.AfterCheckCount?.ToString();
                    ClientReport.FieldByName("AfterZYBCount").AsString = Client.AfterZYBCount?.ToString();
                    ClientReport.FieldByName("AfterFCCount").AsString = Client.AfterFCCount?.ToString();
                    ClientReport.DetailGrid.Recordset.Post();
                }
            }
        }
        /// <summary>
        /// 按危害因素
        /// </summary>
        private void FetchRisk()
        {
            if (dwtj.RiskTypeStatic != null)
            {
                foreach (var Client in dwtj.RiskTypeStatic)
                {
                    RiskReport.DetailGrid.Recordset.Append();
                    RiskReport.FieldByName("SaticsName").AsString = Client.SaticsName;
                    RiskReport.FieldByName("BeforeCheckCount").AsString = Client.BeforeCheckCount?.ToString();
                    RiskReport.FieldByName("BeforeJJZCount").AsString = Client.BeforeJJZCount?.ToString();
                    RiskReport.FieldByName("OnCheckCount").AsString = Client.OnCheckCount?.ToString();
                    RiskReport.FieldByName("OnJJZCount").AsString = Client.OnJJZCount?.ToString();
                    RiskReport.FieldByName("OnZYBCount").AsString = Client.OnZYBCount?.ToString();
                    RiskReport.FieldByName("OnFCCount").AsString = Client.OnFCCount?.ToString();
                    RiskReport.FieldByName("AfterCheckCount").AsString = Client.AfterCheckCount?.ToString();
                    RiskReport.FieldByName("AfterZYBCount").AsString = Client.AfterZYBCount?.ToString();
                    RiskReport.FieldByName("AfterFCCount").AsString = Client.AfterFCCount?.ToString();
                    RiskReport.DetailGrid.Recordset.Post();
                }
            }
        }
        /// <summary>
        /// 按职业病
        /// </summary>
        private void Fetchzyb()
        {
            if (dwtj.ZYBStatistics != null)
            {
                foreach (var Client in dwtj.ZYBStatistics)
                {
                    zybReport.DetailGrid.Recordset.Append();
                    zybReport.FieldByName("ClientNamed").AsString = Client.ClientNamed;
                    zybReport.FieldByName("Name").AsString = Client.Name?.ToString();
                    zybReport.FieldByName("Sex").AsString = Client.Sex?.ToString();
                    zybReport.FieldByName("Age").AsString = Client.Age?.ToString();
                    zybReport.FieldByName("InjurAge").AsString = Client.InjurAge?.ToString();
                    zybReport.FieldByName("RiskName").AsString = Client.RiskName?.ToString();
                    zybReport.FieldByName("PostName").AsString = Client.PostName?.ToString();
                    zybReport.FieldByName("ZYBName").AsString = Client.ZYBName?.ToString();
                    zybReport.FieldByName("ReportBM").AsString = Client.ReportBM?.ToString();
                    zybReport.FieldByName("Remark").AsString = Client.Remark?.ToString();
                    zybReport.DetailGrid.Recordset.Post();
                }
            }
        }
        /// <summary>
        /// 按禁忌症
        /// </summary>
        private void Fetchjjz()
        {
            if (dwtj.JJZStatistics != null)
            {
                foreach (var Client in dwtj.JJZStatistics)
                {
                    jjzReport.DetailGrid.Recordset.Append();
                    jjzReport.FieldByName("ClientNamed").AsString = Client.ClientNamed;
                    jjzReport.FieldByName("Name").AsString = Client.Name?.ToString();
                    jjzReport.FieldByName("Sex").AsString = Client.Sex?.ToString();
                    jjzReport.FieldByName("Age").AsString = Client.Age?.ToString();
                    jjzReport.FieldByName("InjurAge").AsString = Client.InjurAge?.ToString();
                    jjzReport.FieldByName("RiskName").AsString = Client.RiskName?.ToString();
                    jjzReport.FieldByName("PostName").AsString = Client.PostName?.ToString();
                    jjzReport.FieldByName("ZYBName").AsString = Client.ZYBName?.ToString();
                    jjzReport.FieldByName("ReportBM").AsString = Client.ReportBM?.ToString();
                    jjzReport.FieldByName("Remark").AsString = Client.Remark?.ToString();
                    jjzReport.DetailGrid.Recordset.Post();
                }
            }
        }
    }
}
