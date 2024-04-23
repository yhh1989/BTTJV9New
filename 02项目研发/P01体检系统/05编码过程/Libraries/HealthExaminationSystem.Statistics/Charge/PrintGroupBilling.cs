using gregn6Lib;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.Statistics.Charge
{
   public class PrintGroupBilling
    {
        private GridppReport ReportMain;

        //public void Print(bool isPreview,string strBarPrintName = "体检结账单.grf")
        //{
        //    if (strBarPrintName == "")
        //    { strBarPrintName = "体检结账单.grf"; }
        //    var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
        //    var iii = ReportMain.LoadFromURL(GrdPath);
        //    Reportsy = ReportMain.ControlByName("首页").AsSubReport.Report;   



        //    //主报表
        //    ReportMain.FetchRecord -= ReportMain_ReportFetchRecord;
        //    ReportMain.FetchRecord += ReportMain_ReportFetchRecord;
        //    //打印机设置
        //    var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 40)?.Remarks;
        //    if (!string.IsNullOrEmpty(printName))
        //    {
        //        ReportMain.Printer.PrinterName = printName;
        //    }
        //    if (isPreview)
        //        ReportMain.PrintPreview(true);
        //    else
        //    {
        //        if (path != "")
        //        { ReportMain.ExportDirect(GRExportType.gretPDF, path, false, false); }
        //        else
        //            ReportMain.Print(false);
        //    }

        //}
    }
}
