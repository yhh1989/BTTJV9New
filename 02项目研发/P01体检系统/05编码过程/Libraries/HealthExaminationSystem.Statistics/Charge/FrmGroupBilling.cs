using gregn6Lib;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
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
using System.Windows.Forms;
using HealthExaminationSystem.Enumerations;
using static Sw.Hospital.HealthExaminationSystem.CustomerReport.PrintPreview;
using Newtonsoft.Json;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
    public partial class FrmGroupBilling : UserBaseForm
    {
        IClientRegAppService _cusReSultStatusApp = new ClientRegAppService();
        private GridppReport ReportMain;
        private GridppReport Reportsy = new GridppReport();
        private List<Pas> GetCus;
        public FrmGroupBilling()
        {
            InitializeComponent();
            ReportMain = new GridppReport();
        }

        private void FrmGroupBilling_Load(object sender, EventArgs e)
        {
            var Client = DefinedCacheHelper.GetClientRegNameComDto();
            comboBoxEdit1.Properties.DataSource = Client;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SearchGroupCustomerRegDto search = new SearchGroupCustomerRegDto();
            if (comboBoxEdit1.EditValue != null)
            {
                search.ClientRegId = (Guid)comboBoxEdit1.EditValue;
            }
            if (dateEditStartTime.EditValue != null)
                search.StartDateTime = DateTime.Parse(dateEditStartTime.DateTime.ToShortDateString());
            if (dateEditEndTime.EditValue != null)
                search.EndDateTime =DateTime.Parse( dateEditEndTime.DateTime.ToShortDateString() +" 23:59:59");
            if (search.StartDateTime > search.EndDateTime)
            {
                ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
                return;
            }
            if (comboBoxEditdate.Text.Contains("体检日期"))
            {
                search.DateType = 2;
            }
            else if (comboBoxEditdate.Text.Contains("登记日期"))
            { search.DateType = 1; }
            else 
            { search.DateType = 0; }
            var gridresult = _cusReSultStatusApp.GetInquireGroupCustomerRegDtos(search);
            gridControl1.DataSource = gridresult.PayPersons;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Print(true);
        }
        public void Print(bool isPreview = false)
        {
            string path = "";
            if (Shell.BrowseForFolder("请选择文件夹！", out path) != DialogResult.OK)
                return;
            var ClientName = comboBoxEdit1.Text;
            string strnewpath = path + "\\" + ClientName + System.DateTime.Now.ToString("yyyyMMdd") + "_体检结账单.xls";

            //SearchGroupCustomerRegDto search = new SearchGroupCustomerRegDto();
            //if (comboBoxEdit1.EditValue != null)
            //{
            //    search.ClientRegId = (Guid)comboBoxEdit1.EditValue;
            //}
            //if (dateEditStartTime.EditValue != null)
            //    search.StartDateTime = dateEditStartTime.DateTime;
            //if (dateEditEndTime.EditValue != null)
            //    search.EndDateTime = dateEditEndTime.DateTime;
            //if (search.StartDateTime > search.EndDateTime)
            //{
            //    ShowMessageBoxWarning("开始时间大于结束时间，请重新选择时间。");
            //    return;
            //}
           
            var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            strBarPrintName = "体检结账单.grf";
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            ReportMain.LoadFromURL(GrdPath);
           //var gridresult = _cusReSultStatusApp.GetInquireGroupCustomerRegDtos(search);
            var gridresult = gridControl1.DataSource as List<Pas>;
            ReportJson reportJson = new ReportJson();          
            reportJson.Detail = gridresult;
            reportJson.Master = new List<reprtMaster>();
            reprtMaster master = new reprtMaster();
            master.ClientName = ClientName;
            master.RenCount = gridresult.Count();
            master.ManCount = gridresult.Where(o => o.Sex == (int)Sex.Man).Count();
            master.WomenCount = gridresult.Where(o => o.Sex == (int)Sex.Woman).Count();
            master.UnCheckSateCount = gridresult.Where(o => o.CheckSate == (int)PhysicalEState.Not).Count();
            master.CheckSateCount = gridresult.Where(o => o.CheckSate != (int)PhysicalEState.Not).Count();
            master.ManCheckSateCount= gridresult.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.Sex == (int)Sex.Man).Count();
            master.WoManCheckSateCount = gridresult.Where(o => o.CheckSate != (int)PhysicalEState.Not && o.Sex == (int)Sex.Woman).Count();
            master.SumCkeckMoney = gridresult.Select(o => o.SumPrice).Sum();
            reportJson.Master.Add(master);
            var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
            ReportMain.LoadDataFromXML(reportJsonString1);
            //打印机设置
            //var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 40)?.Remarks;
            //if (!string.IsNullOrEmpty(printName))
            //{
            //    ReportMain.Printer.PrinterName = printName;
            //}
            ReportMain.PageDivideLine =false;
            ReportMain.PrintAsDesignPaper = false;
           
            ReportMain.ExportDirect(GRExportType.gretXLS, strnewpath, false, false);
            //ReportMain.ExportDirect()

            MessageBox.Show("导出成功！");
        }
    }
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportJson
    {
        public List<reprtMaster> Master { get; set; }
        /// <summary>
        /// 明细网格
        /// </summary>
        public List<Pas> Detail { get; set; }
    }
    public class reprtMaster
    {
        public string ClientName { get; set; }
        /// <summary>
        /// 总人数
        /// </summary>
        public int? RenCount { get; set; }
        /// <summary>
        /// 男性人数
        /// </summary>
        public int? ManCount { get; set; }
        /// <summary>
        /// 女性人数
        /// </summary>
        public int? WomenCount { get; set; }
        /// <summary>
        /// 未体检人数
        /// </summary>
        public int? UnCheckSateCount { get; set; }
        /// <summary>
        /// 已做体检人数
        /// </summary>
        public int? CheckSateCount { get; set; }
        /// <summary>
        /// 已做体检人数 男性
        /// </summary>
        public int? ManCheckSateCount { get; set; }
        /// <summary>
        /// 已做体检人数 女性
        /// </summary>
        public int? WoManCheckSateCount { get; set; }
        /// <summary>
        /// 体检费总计
        /// </summary>
        public decimal? SumCkeckMoney { get; set; }
    }
}
