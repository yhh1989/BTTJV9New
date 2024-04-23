using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using gregn6Lib;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using Sw.Hospital.HealthExaminationSystem.GroupReport;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
    public class PrintClientReport
    {
        #region 申明子报表
        private GridppReport ReportMain;
        /// <summary>
        /// 首页
        /// </summary>
        private GridppReport Reportsy = new GridppReport();
        /// <summary>
        /// 年龄分布
        /// </summary>
        private GridppReport Reportnlfb = new GridppReport();
        /// <summary>
        /// 单位分组信息
        /// </summary>
        private GridppReport Reportfzxx = new GridppReport();
        /// <summary>
        /// 异常结果汇总
        /// </summary>
        private GridppReport Reportycjghz = new GridppReport();
        /// <summary>
        /// 常结果前10名统计图
        /// </summary>
        private GridppReport Reportycjgtb = new GridppReport();
        /// <summary>
        /// 疾病统计
        /// </summary>
        private GridppReport Reportjbtj = new GridppReport();
        /// <summary>
        /// 疾病统计前10
        /// </summary>
        private GridppReport Reportjbtj10 = new GridppReport();
        /// <summary>
        /// 疾病统计男前10
        /// </summary>
        private GridppReport Reportjbtjm10 = new GridppReport();
        /// <summary>
        ///疾病统计女前10
        /// </summary>
        private GridppReport Reportjbtjw10 = new GridppReport();


        /// <summary>
        ///图疾病统计前10
        /// </summary>
        private GridppReport ReportjbtjT10 = new GridppReport();
        /// <summary>
        /// 图疾病统计男前10
        /// </summary>
        private GridppReport ReportjbtjmT10 = new GridppReport();
        /// <summary>
        ///图疾病统计女前10
        /// </summary>
        private GridppReport ReportjbtjwT10 = new GridppReport();

        /// <summary>
        /// 阳性汇总
        /// </summary>
        private GridppReport Reportyxhz = new GridppReport();
        /// <summary>
        /// 未到检人员
        /// </summary>
        private GridppReport Reportwjry= new GridppReport();
        /// <summary>
        /// 未检项目
        /// </summary>
        private GridppReport Reportwjxm= new GridppReport();
        #endregion
        private IGroupReportAppService GReportAppService;
        private IClientRegAppService CRegAppService;
        private ClientRegIdDto ClientRegID;
        //单位已检人员
        private List<GroupClientCusDto> groupAllClientCusDtos = new List<GroupClientCusDto>();
        //单位已检人员
        private List<GroupClientCusDto> groupClientCusDtos = new List<GroupClientCusDto>();
        //单位未检人员
        private List<GroupClientCusDto> groupClientCuswjDtos = new List<GroupClientCusDto>();
        //单位诊断
        private List<GroupClientSumDto> groupClientSumDtos = new List<GroupClientSumDto>();

        //历史单位诊断
        private List<HistoryGroupClientSumDto> HistoryGroupClientSumDtos = new List<HistoryGroupClientSumDto>();
        private int AllNum = 0;
        private int zrs = 0;
        private int manNum = 0;
        private int womenNum = 0;
        private List<string> groupSumBMDtos = new List<string>();
        private bool isSum = false;
        private List<Guid> _cuslist = new List<Guid>();
        private string isBFL = "";
        public PrintClientReport()
        {
            ReportMain = new GridppReport();
            GReportAppService = new GroupReportAppService();
            CRegAppService = new ClientRegAppService();
        }
        /// <summary>
        /// 打印团报
        /// </summary>
        /// <param name="isPreview">是否预览，是则预览，否则打印</param>
        public void Print(bool isPreview, List<Guid> input, string path = "",bool isok=false,string strBarPrintName = "团体报告-健康体检-简约经典.grf",List<Guid> cuslist=null,int isreport = 0,bool isfc=true )
        {

            isBFL = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.GroupReportSet, 11)?.Remarks;

            if (strBarPrintName == "")
            { strBarPrintName = "团体报告-健康体检-简约经典.grf"; }
            //ClientRegIdDto ClientRegID = new ClientRegIdDto();
            ClientRegID = new ClientRegIdDto();
            ClientRegID.Idlist = new List<Guid>();
            ClientRegID.Idlist = input;
            //ClientRegID = input;
            //var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            //strBarPrintName = "团体报告-健康体检-简约经典.grf";
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            //单位所有人员
            groupAllClientCusDtos = GReportAppService.GRClientRegCus(ClientRegID).ToList();
            //过滤复查人员
            if (isfc == false)
            {
                groupAllClientCusDtos = groupAllClientCusDtos.Where(p => p.ReviewSate != (int)ReviewSate.Review).ToList();
            }
            //按人员
            if (cuslist != null && cuslist.Count > 0)
            {
                _cuslist = cuslist.ToList();
                groupAllClientCusDtos = groupAllClientCusDtos.Where(o => cuslist.Contains(o.Id)).ToList();
            }
            //到检人员(按登记人员计算)
            int RegState = (int)RegisterState.No;
            groupClientCusDtos = groupAllClientCusDtos.Where(o=>o.RegisterState!= RegState).ToList();
            //按人员
            if (cuslist != null && cuslist.Count>0)
            {
                _cuslist = cuslist.ToList();
                groupClientCusDtos = groupClientCusDtos.Where(o=> cuslist.Contains(o.Id)).ToList();
            }
            AllNum = groupAllClientCusDtos.Count;
           zrs = groupClientCusDtos.Count;
            if (zrs == 0)
            {
                XtraMessageBox.Show("该单位没有已总检人员，不能生成团报！");
                return;

            }
            //疾病筛选
            if (isok == true)
            {
                var allSumBMDtos = GReportAppService.getClientSum(ClientRegID);
               
                if (allSumBMDtos.Count > 0)
                {
                    Sumselect sumselect = new Sumselect();
                    sumselect.AllSumBMDtos = allSumBMDtos;
                    if (sumselect.ShowDialog() == DialogResult.OK)
                    {
                        groupSumBMDtos = sumselect.OutSelectedSumBMDtos.Select(o=>o.SummarizeName).ToList();
                        if (groupSumBMDtos.Count > 0)
                        {
                            isSum = true;
                        }
                    }
                }
            }
            //未到检人员          
            groupClientCuswjDtos = groupAllClientCusDtos.Where(o => o.RegisterState == RegState).ToList();
            //按人员
            if (cuslist != null && cuslist.Count > 0)
            {
                groupClientCuswjDtos = groupClientCuswjDtos.Where(o=> cuslist.Contains(o.Id)).ToList();
            }
            //人员诊断
            if (isreport == 1)
            {
                groupClientSumDtos = GReportAppService.GRClientCusSum(ClientRegID).Where(o => o.IsPrivacy == true).ToList();

                HistoryGroupClientSumDtos = GReportAppService.GRHisClientCusSum(ClientRegID).Where(o => o.IsPrivacy == true).ToList();
            }
            else if (isreport == 2)
            {
                groupClientSumDtos = GReportAppService.GRClientCusSum(ClientRegID).Where(o => o.IsPrivacy == false).ToList();

                HistoryGroupClientSumDtos = GReportAppService.GRHisClientCusSum(ClientRegID).Where(o => o.IsPrivacy == false).ToList();
            }
            else
            {
                groupClientSumDtos = GReportAppService.GRClientCusSum(ClientRegID);
                HistoryGroupClientSumDtos = GReportAppService.GRHisClientCusSum(ClientRegID);
            }


            //过滤复查人员
            if (isfc == false)
            {
                groupClientSumDtos = groupClientSumDtos.Where(p => p.CustomerReg.ReviewSate != (int)ReviewSate.Review).ToList();
            }
            //按人员
            if (cuslist != null && cuslist.Count > 0)
            {
                groupClientSumDtos = groupClientSumDtos.Where(o=> cuslist.Contains(o.CustomerRegID)).ToList();
            }
            //疾病筛选
            if (isSum == true)
            {
                groupClientSumDtos = groupClientSumDtos.Where(o => groupSumBMDtos.Contains(o.SummarizeName)).ToList();
            }
            
            int man = (int)Sex.Man;
            int woman = (int)Sex.Woman;
            manNum = groupClientCusDtos.Where(o => o.Customer.Sex == man).Count();
            womenNum = groupClientCusDtos.Where(o => o.Customer.Sex == woman).Count();

            #region 绑定数据
            var iii = ReportMain.LoadFromURL(GrdPath);
            Reportsy = ReportMain.ControlByName("首页").AsSubReport.Report;
            Reportfzxx = ReportMain.ControlByName("单位分组信息").AsSubReport.Report;
            Reportnlfb = ReportMain.ControlByName("年龄分布").AsSubReport.Report;

            Reportycjghz = ReportMain.ControlByName("参检人员异常结果汇总").AsSubReport.Report;
            Reportycjgtb = ReportMain.ControlByName("参检人员异常结果前10名统计图").AsSubReport.Report;
            Reportjbtj = ReportMain.ControlByName("疾病统计").AsSubReport.Report;
            Reportjbtj10 = ReportMain.ControlByName("疾病统计前10").AsSubReport.Report;
            Reportjbtjm10 = ReportMain.ControlByName("疾病统计男前10").AsSubReport.Report;
            Reportjbtjw10 = ReportMain.ControlByName("疾病统计女前10").AsSubReport.Report;
            if ( ReportMain.ControlByName("图疾病统计前10")!=null)
            {
                ReportjbtjT10 = ReportMain.ControlByName("图疾病统计前10").AsSubReport.Report;
                ReportjbtjmT10 = ReportMain.ControlByName("图疾病统计男前10").AsSubReport.Report;
                ReportjbtjwT10 = ReportMain.ControlByName("图疾病统计女前10").AsSubReport.Report;
            }


            Reportyxhz = ReportMain.ControlByName("阳性汇总").AsSubReport.Report;
            Reportwjry = ReportMain.ControlByName("未到检人员名单").AsSubReport.Report;
            Reportwjxm = ReportMain.ControlByName("未检项目汇总").AsSubReport.Report;
          
           
           
            //主报表
            ReportMain.FetchRecord -= ReportMain_ReportFetchRecord;
            ReportMain.FetchRecord += ReportMain_ReportFetchRecord;
            //首页
            Reportsy.FetchRecord -= Reportsy_ReportFetchRecord;
            Reportsy.FetchRecord += Reportsy_ReportFetchRecord;

            //单位分组
            Reportfzxx.FetchRecord -= Reportfzxx_ReportFetchRecord;
            Reportfzxx.FetchRecord += Reportfzxx_ReportFetchRecord;

            //年龄分布
            Reportnlfb.FetchRecord -= Reportnlfb_ReportFetchRecord;
            Reportnlfb.FetchRecord += Reportnlfb_ReportFetchRecord;

            //疾病汇总
            Reportycjghz.FetchRecord -= Reportycjghz_ReportFetchRecord;
            Reportycjghz.FetchRecord += Reportycjghz_ReportFetchRecord;
            //疾病图表
            Reportycjgtb.FetchRecord -= Reportycjgtb_ReportFetchRecord;
            Reportycjgtb.FetchRecord += Reportycjgtb_ReportFetchRecord;

            //疾病统计
            Reportjbtj.FetchRecord -= Reportjbtj_ReportFetchRecord;
            Reportjbtj.FetchRecord += Reportjbtj_ReportFetchRecord;

           
            Reportjbtj10.FetchRecord -= Reportjbtj10_ReportFetchRecord;
            Reportjbtj10.FetchRecord += Reportjbtj10_ReportFetchRecord;
            

            Reportjbtjm10.FetchRecord -= Reportjbtjm10_ReportFetchRecord;
            Reportjbtjm10.FetchRecord += Reportjbtjm10_ReportFetchRecord;
            

            Reportjbtjw10.FetchRecord -= Reportjbtjw10_ReportFetchRecord;
            Reportjbtjw10.FetchRecord += Reportjbtjw10_ReportFetchRecord;

            #region 图表
            if (ReportMain.ControlByName("图疾病统计前10") !=null)
            {
                ReportjbtjT10.FetchRecord -= ReportjbtjT10_ReportFetchRecord;
                ReportjbtjT10.FetchRecord += ReportjbtjT10_ReportFetchRecord;
                ReportjbtjT10.ChartRequestData += new _IGridppReportEvents_ChartRequestDataEventHandler(ReportChartRequestData);
           
                ReportjbtjmT10.FetchRecord -= ReportjbtjmT10_ReportFetchRecord;
                ReportjbtjmT10.FetchRecord += ReportjbtjmT10_ReportFetchRecord;
                ReportjbtjmT10.ChartRequestData += new _IGridppReportEvents_ChartRequestDataEventHandler(ReportChartRequestData);
            
                ReportjbtjwT10.FetchRecord -= ReportjbtjwT10_ReportFetchRecord;
                ReportjbtjwT10.FetchRecord += ReportjbtjwT10_ReportFetchRecord;
                ReportjbtjwT10.ChartRequestData += new _IGridppReportEvents_ChartRequestDataEventHandler(ReportChartRequestData);
            }
            #endregion

            Reportyxhz.FetchRecord -= Reportyxhz_ReportFetchRecord;
            Reportyxhz.FetchRecord += Reportyxhz_ReportFetchRecord;

            Reportwjry.FetchRecord -= Reportwjry_ReportFetchRecord;
            Reportwjry.FetchRecord += Reportwjry_ReportFetchRecord;


            Reportwjxm.FetchRecord -= Reportwjxm_ReportFetchRecord;
            Reportwjxm.FetchRecord += Reportwjxm_ReportFetchRecord;

            #endregion
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 40)?.Remarks;
            if (!string.IsNullOrEmpty(printName))
            {
                ReportMain.Printer.PrinterName = printName;
            }
            if (isPreview)
                ReportMain.PrintPreview(true);
            else
            {
                if (path != "")
                { ReportMain.ExportDirect(GRExportType.gretPDF, path, false, false); }
                else
                ReportMain.Print(false);
            }

        }
        #region Grid++数据绑定
        private void ReportMain_ReportFetchRecord()
        {
            BindMainReport(ReportMain);

        }
        private void Reportsy_ReportFetchRecord()
        {
            BindMainReport(Reportsy);
        }
        //分组信息
        private void Reportfzxx_ReportFetchRecord()
        {

            // Reportfzxx.DetailGrid.Recordset.Append();
            List<GroupClientItemsDto> groupClientItemss = GReportAppService.GRClientRegItems(ClientRegID);
            //按人员
            if (_cuslist !=null && _cuslist.Count>0 && groupAllClientCusDtos != null && groupAllClientCusDtos.Count > 0)
            {
                var teamIDs = groupAllClientCusDtos.Select(p => p.ClientTeamInfoId).ToList();
                groupClientItemss = groupClientItemss.Where(p => teamIDs.Contains(p.ClientTeamInfoId)).ToList();
            }
            List<GroupClientCusDto> groupClientItems = groupAllClientCusDtos;
             int notcheck = (int)RegisterState.No;
            foreach (GroupClientItemsDto groupClientItem in groupClientItemss)
            {
                Reportfzxx.DetailGrid.Recordset.Append();
                Reportfzxx.FieldByName("分组编码").AsString = groupClientItem.ClientTeamInfo.TeamBM.ToString();
                Reportfzxx.FieldByName("分组名称").AsString = groupClientItem.ClientTeamInfo.TeamName.ToString();
                Reportfzxx.FieldByName("分组性别").AsString = SexHelper.CustomSexFormatter(groupClientItem.ClientTeamInfo.Sex);
                Reportfzxx.FieldByName("分组套餐").AsString = groupClientItem.ClientTeamInfo.ItemSuitName?.ToString();
                Reportfzxx.FieldByName("分组人数").AsString = groupClientItems.Where(o=> o.ClientTeamInfo!=null && o.ClientTeamInfo.TeamName == groupClientItem.ClientTeamInfo.TeamName.ToString() && o.RegisterState!= notcheck).Count().ToString();
                Reportfzxx.FieldByName("分组项目").AsString = groupClientItem.ItemGroupName;
                Reportfzxx.DetailGrid.Recordset.Post();
            }
            
        }
        /// <summary>
        /// 年龄分布
        /// </summary>
        private void Reportnlfb_ReportFetchRecord()
        {
            //表格
            int allcout = groupClientCusDtos.Count;
            int mancount = groupClientCusDtos.Where(o => o.Customer.Sex == (int)Sex.Man).Count();
            int womancount = groupClientCusDtos.Where(o => o.Customer.Sex == (int)Sex.Woman).Count();
            int agemin = 0;
            int agemax = 0;
            int dw = 25;
            List<int> mannumber = new List<int>();
            List<int> womannumber = new List<int>();
            List<int> allnumber = new List<int>();
            for (int agecount = 0; agecount < 7; agecount++)
            {

                string agename = "";
                if (agecount == 0)
                {
                    agename = "24岁以下";
                    agemin = 0;
                    agemax = 24;
                }
                else if (agecount == 1)
                {
                    agename = "25~34";
                    agemin = 25;
                    agemax = 34;
                }
                else if (agecount == 5)
                {
                    agename = "65岁以上";
                    agemin = 65;
                    agemax = 200;
                }
                else if (agecount == 6)
                {
                    agename = "合计";
                    agemin = 0;
                    agemax = 200;
                }
                else
                {
                    agemin += 10;
                    agemax += 10;
                    agename = agemin + "~" + agemax + "岁";
                }
                Reportnlfb.DetailGrid.Recordset.Append();
                Reportnlfb.FieldByName("AgeFw").AsString = agename;
                int manrs = groupClientCusDtos.Where(o => o.Customer.Age <= agemax && o.Customer.Age >= agemin && o.Customer.Sex == (int)Sex.Man).Count();
                int womanrs = groupClientCusDtos.Where(o => o.Customer.Age <= agemax && o.Customer.Age >= agemin && o.Customer.Sex == (int)Sex.Woman).Count();
                int agers = groupClientCusDtos.Where(o => o.Customer.Age <= agemax && o.Customer.Age >= agemin).Count();
                mannumber.Add(manrs);
                womannumber.Add(womanrs);
                allnumber.Add(agers);
                Reportnlfb.FieldByName("Mrs").AsString = manrs.ToString();
                Reportnlfb.FieldByName("Mbl").AsString = ((decimal)manrs * 100 / (decimal)allcout).ToString("0.00") + "%";
                Reportnlfb.FieldByName("Wrs").AsString = womanrs.ToString();
                Reportnlfb.FieldByName("Wbl").AsString = ((decimal)womanrs * 100 / (decimal)allcout).ToString("0.00") + "%";
                Reportnlfb.FieldByName("Zrs").AsString = agers.ToString();
                Reportnlfb.FieldByName("Zbl").AsString = ((decimal)agers * 100 / (decimal)allcout).ToString("0.00") + "%";
                Reportnlfb.FieldByName("Zs").AsString = allcout.ToString();
                Reportnlfb.FieldByName("Ms").AsString = mancount.ToString();
                Reportnlfb.FieldByName("Ws").AsString = womancount.ToString();
                if (Reportnlfb.FieldByName("AllNum") != null)
                {
                    Reportnlfb.FieldByName("AllNum").AsString = AllNum.ToString();
                }
                Reportnlfb.DetailGrid.Recordset.Post();
            }
            //图表
            IGRChart pChart = Reportnlfb.ControlByName("distribution").AsChart;
            pChart.SeriesCount = 3;
            pChart.GroupCount = 6;

            pChart.set_SeriesLabel(0, "男");
            pChart.set_SeriesLabel(1, "女");
            pChart.set_SeriesLabel(2, "总人数");
            pChart.set_GroupLabel(0, "<=24岁");
            pChart.set_GroupLabel(1, "25-34岁");
            pChart.set_GroupLabel(2, "35-44岁");
            pChart.set_GroupLabel(3, "45-54岁");
            pChart.set_GroupLabel(4, "55-64岁");
            pChart.set_GroupLabel(5, ">=65岁");

            pChart.set_Value(0, 0, Convert.ToDouble(mannumber[0].ToString()));
            pChart.set_Value(0, 1, Convert.ToDouble(mannumber[1].ToString()));
            pChart.set_Value(0, 2, Convert.ToDouble(mannumber[2].ToString()));
            pChart.set_Value(0, 3, Convert.ToDouble(mannumber[3].ToString()));
            pChart.set_Value(0, 4, Convert.ToDouble(mannumber[4].ToString()));
            pChart.set_Value(0, 5, Convert.ToDouble(mannumber[5].ToString()));

            pChart.set_Value(1, 0, Convert.ToDouble(womannumber[0].ToString()));
            pChart.set_Value(1, 1, Convert.ToDouble(womannumber[1].ToString()));
            pChart.set_Value(1, 2, Convert.ToDouble(womannumber[2].ToString()));
            pChart.set_Value(1, 3, Convert.ToDouble(womannumber[3].ToString()));
            pChart.set_Value(1, 4, Convert.ToDouble(womannumber[4].ToString()));
            pChart.set_Value(1, 5, Convert.ToDouble(womannumber[5].ToString()));

            pChart.set_Value(2, 0, Convert.ToDouble(allnumber[0].ToString()));
            pChart.set_Value(2, 1, Convert.ToDouble(allnumber[1].ToString()));
            pChart.set_Value(2, 2, Convert.ToDouble(allnumber[2].ToString()));
            pChart.set_Value(2, 3, Convert.ToDouble(allnumber[3].ToString()));
            pChart.set_Value(2, 4, Convert.ToDouble(allnumber[4].ToString()));
            pChart.set_Value(2, 5, Convert.ToDouble(allnumber[5].ToString()));


        }
        //private ArrayList mannumber = new ArrayList();
        //private ArrayList womannumber = new ArrayList();
        //private ArrayList allnumber = new ArrayList();
        private List<int> mannumber = new List<int>();
        private List<int> womannumber = new List<int>();
        private List<int> allnumber = new List<int>();

        private List<string> Amannumber = new List<string>();
        private List<string> Awomannumber = new List<string>();
        private List<string> Aallnumber = new List<string>();
        //疾病汇总
        private void Reportycjghz_ReportFetchRecord()
        {

            //已登记人数
            var HasRegNum = groupAllClientCusDtos.Where(p=>p.RegisterState!=(int)RegisterState.No).Count();
            //已登记男人数
            var HasRegManNum = groupAllClientCusDtos.Where(p => p.RegisterState != (int)RegisterState.No 
            && p.Customer.Sex==(int)Sex.Man).Count();
            //已登记女人数
            var HasRegWomanNum = groupAllClientCusDtos.Where(p => p.RegisterState != (int)RegisterState.No
    && p.Customer.Sex == (int)Sex.Woman).Count();
            //未登记人数
            var NoRegNum = groupAllClientCusDtos.Where(p => p.RegisterState == (int)RegisterState.No).Count();
            //未登记男
            var NoManRegNum = groupAllClientCusDtos.Where(p => p.RegisterState == (int)RegisterState.No
             && p.Customer.Sex == (int)Sex.Man).Count();
            //未登记女
            var NoWomanRegNum = groupAllClientCusDtos.Where(p => p.RegisterState == (int)RegisterState.No
          && p.Customer.Sex == (int)Sex.Woman).Count();
            //已登记体检中人数
         var IncompleteNum = groupAllClientCusDtos.Where(p=>p.RegisterState!=(int)RegisterState.No 
         && p.CheckSate==(int)PhysicalEState.Process).Count();
            //已登记体检中男人数
            var IncompleteManNum = groupAllClientCusDtos.Where(p => p.RegisterState != (int)RegisterState.No
            && p.CheckSate == (int)PhysicalEState.Process      && p.Customer.Sex==(int)Sex.Man).Count();
            //已登记体检中女人数
            var IncompleteWomanNum = groupAllClientCusDtos.Where(p => p.RegisterState != (int)RegisterState.No
            && p.CheckSate == (int)PhysicalEState.Process && p.Customer.Sex == (int)Sex.Woman).Count();
            // groupClientSumDtos
            //人数
            var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
                mancout = g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Man).Count(),
                womancout = g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Woman).Count(),
                coutbl = (decimal)g.Count() * 100 / (decimal)zrs,
                mancoutbl = (decimal)g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Man).Count() * 100 / (decimal)zrs,
                womancoutbl = (decimal)g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Woman).Count() * 100 / (decimal)zrs
            })).OrderByDescending(o => o.cout).ToList();
            if (!string.IsNullOrEmpty(isBFL) && isBFL == "1")
            {
                sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
                {
                    Name = g.Key,
                    cout = g.Count(),
                    mancout = g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Man).Count(),
                    womancout = g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Woman).Count(),
                    coutbl = (decimal)g.Count() * 100 / (decimal)zrs,
                    mancoutbl = (decimal)g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Man).Count() * 100 / (decimal)HasRegManNum,
                    womancoutbl = (decimal)g.Where(n => n.CustomerReg.Customer.Sex == (int)Sex.Woman).Count() * 100 / (decimal)HasRegWomanNum
                })).OrderByDescending(o => o.cout).ToList();
            }
            var clietId = HistoryGroupClientSumDtos.OrderByDescending(p => p.ClientRegDate).Select(p => p.ClientRegId);

            var clieregId1 = clietId.FirstOrDefault();

            var clieregId2 = clietId.LastOrDefault();
            //人数
            var Hissumlis1 = HistoryGroupClientSumDtos.Where(p=>p.ClientRegId== clieregId1).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
                mancout = g.Where(n => n.Sex == (int)Sex.Man).Count(),
                womancout = g.Where(n => n.Sex == (int)Sex.Woman).Count(),
                coutbl = (decimal)g.Count() * 100 / (decimal)zrs,
                mancoutbl = (decimal)g.Where(n => n.Sex == (int)Sex.Man).Count() * 100 / (decimal)zrs,
                womancoutbl = (decimal)g.Where(n => n.Sex == (int)Sex.Woman).Count() * 100 / (decimal)zrs
            })).OrderByDescending(o => o.cout).ToList();

            //人数
            var Hissumlis2 = HistoryGroupClientSumDtos.Where(p => p.ClientRegId == clieregId2).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
                mancout = g.Where(n => n.Sex == (int)Sex.Man).Count(),
                womancout = g.Where(n => n.Sex == (int)Sex.Woman).Count(),
                coutbl = (decimal)g.Count() * 100 / (decimal)zrs,
                mancoutbl = (decimal)g.Where(n => n.Sex == (int)Sex.Man).Count() * 100 / (decimal)zrs,
                womancoutbl = (decimal)g.Where(n => n.Sex == (int)Sex.Woman).Count() * 100 / (decimal)zrs
            })).OrderByDescending(o => o.cout).ToList();


            for (int i = 0; i < sumlis.Count(); i++)
            {
                Reportycjghz.DetailGrid.Recordset.Append();
                Reportycjghz.FieldByName("tjjl").AsString = sumlis[i].Name;
                Reportycjghz.FieldByName("tjjlcountmen").AsString = sumlis[i].mancout.ToString();
                Reportycjghz.FieldByName("tjjlcountwomen").AsString = sumlis[i].womancout.ToString();
                Reportycjghz.FieldByName("tjjlcountoal").AsString = sumlis[i].cout.ToString();
                Reportycjghz.FieldByName("tjjlbfcountmen").AsString = sumlis[i].mancoutbl.ToString("0.00") + "%";
                Reportycjghz.FieldByName("tjjlbfcountwomen").AsString = sumlis[i].womancoutbl.ToString("0.00") + "%";
                Reportycjghz.FieldByName("tjjlbfcountoal").AsString = sumlis[i].coutbl.ToString("0.00") + "%";
                if (Reportycjghz.FieldByName("HasRegNum") != null)
                {
                    Reportycjghz.FieldByName("HasRegNum").AsString = HasRegNum.ToString();
                }
                if (Reportycjghz.FieldByName("HasRegManNum") != null)
                {
                    Reportycjghz.FieldByName("HasRegManNum").AsString = HasRegManNum.ToString();
                }
                if (Reportycjghz.FieldByName("HasRegWomanNum") != null)
                {
                    Reportycjghz.FieldByName("HasRegWomanNum").AsString = HasRegWomanNum.ToString();
                }
                if (Reportycjghz.FieldByName("NoRegNum") != null)
                {
                    Reportycjghz.FieldByName("NoRegNum").AsString = NoRegNum.ToString();
                }
                if (Reportycjghz.FieldByName("NoManRegNum") != null)
                {
                    Reportycjghz.FieldByName("NoManRegNum").AsString = NoManRegNum.ToString();
                }
                if (Reportycjghz.FieldByName("NoWomanRegNum") != null)
                {
                    Reportycjghz.FieldByName("NoWomanRegNum").AsString = NoWomanRegNum.ToString();
                }
                if (Reportycjghz.FieldByName("IncompleteNum") != null)
                {
                    Reportycjghz.FieldByName("IncompleteNum").AsString = IncompleteNum.ToString();
                }
                if (Reportycjghz.FieldByName("IncompleteManNum") != null)
                {
                    Reportycjghz.FieldByName("IncompleteManNum").AsString = IncompleteManNum.ToString();
                }
                if (Reportycjghz.FieldByName("IncompleteWomanNum") != null)
                {
                    Reportycjghz.FieldByName("IncompleteWomanNum").AsString = IncompleteWomanNum.ToString();
                }

                if (clietId.Count() >= 1)
                {
                    var ClientRegid = clietId.LastOrDefault();
                    var HisSum1 = Hissumlis1.FirstOrDefault(p => p.Name == sumlis[i].Name);
                    if (HisSum1 != null)
                    {
                        if (Reportycjghz.FieldByName("tjjl1") != null)
                        {
                            Reportycjghz.FieldByName("tjjl1").AsString = HisSum1.Name;
                        }
                        if (Reportycjghz.FieldByName("tjjlcountmen1") != null)
                        {
                            Reportycjghz.FieldByName("tjjlcountmen1").AsString = HisSum1.mancout.ToString();
                        }
                        if (Reportycjghz.FieldByName("tjjlcountwomen1") != null)
                        {
                            Reportycjghz.FieldByName("tjjlcountwomen1").AsString = HisSum1.womancout.ToString();
                        }
                        if (Reportycjghz.FieldByName("tjjlcountoal1") != null)
                        {
                            Reportycjghz.FieldByName("tjjlcountoal1").AsString = HisSum1.cout.ToString();
                        }
                        if (Reportycjghz.FieldByName("tjjlbfcountmen1") != null)
                        {
                            Reportycjghz.FieldByName("tjjlbfcountmen1").AsString = HisSum1.mancoutbl.ToString("0.00") + "%";
                        }
                        if (Reportycjghz.FieldByName("tjjlbfcountwomen1") != null)
                        {
                            Reportycjghz.FieldByName("tjjlbfcountwomen1").AsString = HisSum1.womancoutbl.ToString("0.00") + "%";
                        }
                        if (Reportycjghz.FieldByName("tjjlbfcountoal1") != null)
                        {
                            Reportycjghz.FieldByName("tjjlbfcountoal1").AsString = HisSum1.coutbl.ToString("0.00") + "%";
                        }
                    }


                }
                if (clietId.Count() == 2)
                {

                    var ClientRegid = clietId.LastOrDefault();
                    var HisSum1 = Hissumlis2.FirstOrDefault(p => p.Name == sumlis[i].Name);
                    if (HisSum1 != null)
                    {
                        if (Reportycjghz.FieldByName("tjjl2") != null)
                        {
                            Reportycjghz.FieldByName("tjjl2").AsString = HisSum1.Name;
                        }
                        if (Reportycjghz.FieldByName("tjjlcountmen2") != null)
                        {
                            Reportycjghz.FieldByName("tjjlcountmen2").AsString = HisSum1.mancout.ToString();
                        }
                        if (Reportycjghz.FieldByName("tjjlcountwomen2") != null)
                        {
                            Reportycjghz.FieldByName("tjjlcountwomen2").AsString = HisSum1.womancout.ToString();
                        }
                        if (Reportycjghz.FieldByName("tjjlcountoal2") != null)
                        {
                            Reportycjghz.FieldByName("tjjlcountoal2").AsString = HisSum1.cout.ToString();
                        }
                        if (Reportycjghz.FieldByName("tjjlbfcountmen2") != null)
                        {
                            Reportycjghz.FieldByName("tjjlbfcountmen2").AsString = HisSum1.mancoutbl.ToString("0.00") + "%";
                        }
                        if (Reportycjghz.FieldByName("tjjlbfcountwomen2") != null)
                        {
                            Reportycjghz.FieldByName("tjjlbfcountwomen2").AsString = HisSum1.womancoutbl.ToString("0.00") + "%";
                        }
                        if (Reportycjghz.FieldByName("tjjlbfcountoal2") != null)
                        {
                            Reportycjghz.FieldByName("tjjlbfcountoal2").AsString = HisSum1.coutbl.ToString("0.00") + "%";
                        }
                    }

                }
                Reportycjghz.DetailGrid.Recordset.Post();
            }


        }
        //
        private void Reportycjgtb_ReportFetchRecord()
        {
            // Reportycjgtb.DetailGrid.Recordset.Append();


            IGRChart pChartBar1 = Reportycjgtb.ControlByName("distribution").AsChart;
            produceChartIllnessNum(pChartBar1, "不限");

            IGRChart pChartBarmen = Reportycjgtb.ControlByName("Chartmen").AsChart;
            produceChartIllnessNum(pChartBarmen, "男");

            IGRChart pChartBarwomen = Reportycjgtb.ControlByName("Chartwomen").AsChart;
            produceChartIllnessNum(pChartBarwomen, "女");

            // Reportycjgtb.DetailGrid.Recordset.Post();
        }
        private void produceChartIllnessNum(IGRChart pChartBar, string cusSex)
        {
            var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            int m = 10;
            pChartBar.set_GroupLabel(0,"");
              
            if (cusSex == "男")
            {
                sumlis = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Man).GroupBy(o => o.SummarizeName).Select(g => (new
                {
                    Name = g.Key,
                    cout = g.Count(),
                })).OrderByDescending(o => o.cout).ToList();
            }
            else if (cusSex == "女")
            {
                sumlis = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Woman).GroupBy(o => o.SummarizeName).Select(g => (new
                {
                    Name = g.Key,
                    cout = g.Count(),
                })).OrderByDescending(o => o.cout).ToList();
            }
            if (sumlis.Count < 10)
            {
                m = sumlis.Count;
            }
            for (short j = 0; j < m; j++)
            {
                pChartBar.set_SeriesLabel(j, sumlis[j].Name);
                pChartBar.set_Value(j, 0, Convert.ToDouble(sumlis[j].cout));
            }
        }
        //疾病统计
        public int getStringLength(string str)
        {
            if (str.Equals(string.Empty))
                return 0;
            int strlen = 0;
            ASCIIEncoding strData = new ASCIIEncoding();
            //将字符串转换为ASCII编码的字节数字
            byte[] strBytes = strData.GetBytes(str);
            for (int i = 0; i <= strBytes.Length - 1; i++)
            {
                if (strBytes[i] == 63)  //中文都将编码为ASCII编码63,即"?"号
                    strlen++;
                strlen++;
            }
            return strlen;
        }
        private void Reportjbtj_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            for (int i = 0; i < sumlis.Count(); i++)
            {
                
                var culis = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs = culis.Count;
                int summamrs = culis.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culis.Where(o => o.CustomerReg.Customer.Sex == woman).Count();
                for (int m = 0; m < culis.Count(); m++)
                {

                    Reportjbtj.DetailGrid.Recordset.Append();
                    Reportjbtj.FieldByName("manNum").AsString = manNum.ToString();
                    Reportjbtj.FieldByName("womenNum").AsString = womenNum.ToString();
                    Reportjbtj.FieldByName("zrs").AsString = zrs.ToString();

                    Reportjbtj.FieldByName("sumzrs").AsString = sumzrs.ToString();
                    Reportjbtj.FieldByName("summrs").AsString = summamrs.ToString();
                    Reportjbtj.FieldByName("sumwrs").AsString = sumwomenrs.ToString();

                    Reportjbtj.FieldByName("sumzrsbl").AsString = ((decimal)sumzrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtj.FieldByName("summrsbl").AsString = ((decimal)summamrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtj.FieldByName("sumwrsbl").AsString = ((decimal)sumwomenrs * 100 / (decimal)zrs).ToString("0.00");

                    Reportjbtj.FieldByName("Jbmc").AsString = sumlis[i].Name;
                    Reportjbtj.FieldByName("Name").AsString = culis[m].CustomerReg.Customer.Name;
                    Reportjbtj.FieldByName("Sex").AsString = SexHelper.CustomSexFormatter(culis[m].CustomerReg.Customer.Sex);
                    Reportjbtj.FieldByName("Age").AsString = culis[m].CustomerReg.Customer.Age.ToString();                   
                    Reportjbtj.FieldByName("ArchivesNum").AsString = culis[m].CustomerReg.CustomerBM;
                    if ( Reportjbtj.FieldByName("Department") != null)
                    { Reportjbtj.FieldByName("Department").AsString = culis[m].CustomerReg.Customer.Department; }
                    Reportjbtj.FieldByName("mobile").AsString = culis[m].CustomerReg.Customer.Mobile;
                    Reportjbtj.FieldByName("Dadvice").AsString = culis[m].SummarizeAdvice?.SummAdvice?? culis[m].Advice;
                    Reportjbtj.FieldByName("疾病解释").AsString = culis[m].SummarizeAdvice?.DiagnosisExpain;
                    Reportjbtj.DetailGrid.Recordset.Post();
                }
                
               
              

            }

        }

        //Reportjbtj10_ReportFetchRecord
        //疾病统计前10   
        private IGRChart DetailChart;
        /// <summary>
        /// 每个疾病年龄分布
        /// </summary>
        public int rows = 0;
        private void SumAge(List<GroupClientSumDto> IllcusCount, GridppReport gridppReport,
            string jbmc,int zrs,int Mrs,int Wrs)
        {
            if (gridppReport.ControlByName("distribution") == null)
            {
                return;
            }
            
            //表格
            int allcout = IllcusCount.Count;
            int mancount = IllcusCount.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Man).Count();
            int womancount = IllcusCount.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Woman).Count();
            int agemin = 0;
            int agemax = 0;
            int dw = 25;
            string advice = "";
            string content = "";
            if (allcout > 0)
            {
                advice = IllcusCount[0].SummarizeAdvice?.SummAdvice ?? IllcusCount[0].Advice;
                content = IllcusCount[0].SummarizeAdvice?.DiagnosisExpain;
            }
            for (int agecount = 0; agecount < 7; agecount++)
            {

                string agename = "";
                if (agecount == 0)
                {
                    agename = "24岁以下";
                    agemin = 0;
                    agemax = 24;
                }
                else if (agecount == 1)
                {
                    agename = "25~34";
                    agemin = 25;
                    agemax = 34;
                }
                else if (agecount == 5)
                {
                    agename = "65岁以上";
                    agemin = 65;
                    agemax = 200;
                }
                else if (agecount == 6)
                {
                    agename = "合计";
                    agemin = 0;
                    agemax = 200;
                }
                else
                {
                    agemin += 10;
                    agemax += 10;
                    agename = agemin + "~" + agemax + "岁";
                }
                var ageManCount = groupClientCusDtos.Where(o => o.Customer.Age <= agemax && o.Customer.Age >= agemin && o.Customer.Sex == (int)Sex.Man).Count();
                var agewomanCount = groupClientCusDtos.Where(o => o.Customer.Age <= agemax && o.Customer.Age >= agemin && o.Customer.Sex == (int)Sex.Woman).Count();
                var ageCount = groupClientCusDtos.Where(o => o.Customer.Age <= agemax && o.Customer.Age >= agemin ).Count();

                int manrs = IllcusCount.Where(o => o.CustomerReg.Customer.Age <= agemax && o.CustomerReg.Customer.Age >= agemin && o.CustomerReg.Customer.Sex == (int)Sex.Man).Count();
                int womanrs = IllcusCount.Where(o => o.CustomerReg.Customer.Age <= agemax && o.CustomerReg.Customer.Age >= agemin && o.CustomerReg.Customer.Sex == (int)Sex.Woman).Count();
                int agers = IllcusCount.Where(o => o.CustomerReg.Customer.Age <= agemax && o.CustomerReg.Customer.Age >= agemin).Count();


                mannumber.Add(manrs);
                womannumber.Add(womanrs);
                allnumber.Add(agers);
                var mbfb = ageManCount == 0 ? "0" : ((decimal)manrs * 100 / (decimal)ageManCount).ToString("0.00");
                var wbfb= agewomanCount==0?"0":((decimal)womanrs * 100 / (decimal)agewomanCount).ToString("0.00");
                var Abfb = ageCount == 0 ? "0" : ((decimal)agers * 100 / (decimal)ageCount).ToString("0.00");
                Amannumber.Add(mbfb);
                Awomannumber.Add(wbfb);
                Aallnumber.Add(Abfb);

                gridppReport.FieldByName("Mrs" + agecount.ToString()).AsString = manrs.ToString();
                gridppReport.FieldByName("Mbl" + agecount.ToString()).AsString = ((decimal)manrs * 100 / (decimal)allcout).ToString("0.00") + "%";
                //对比该年龄断总男性人数
                if (!string.IsNullOrEmpty(isBFL) && isBFL=="1")
                {
                    gridppReport.FieldByName("Mbl" + agecount.ToString()).AsString = mbfb + "%";
                }
                gridppReport.FieldByName("Wrs" + agecount.ToString()).AsString = womanrs.ToString();
                gridppReport.FieldByName("Wbl" + agecount.ToString()).AsString = ((decimal)womanrs * 100 / (decimal)allcout).ToString("0.00") + "%";
                //对比该年龄断总女性人数
                if (!string.IsNullOrEmpty(isBFL) && isBFL == "1")
                {
                    gridppReport.FieldByName("Wbl" + agecount.ToString()).AsString = wbfb + "%";
                }
                gridppReport.FieldByName("Zrs" + agecount.ToString()).AsString = agers.ToString();
                gridppReport.FieldByName("Zbl" + agecount.ToString()).AsString = ((decimal)agers * 100 / (decimal)allcout).ToString("0.00") + "%";
                //对比该年龄断总女性人数
                if (!string.IsNullOrEmpty(isBFL) && isBFL == "1")
                {
                    gridppReport.FieldByName("Zbl" + agecount.ToString()).AsString = Abfb + "%";
                }

                gridppReport.FieldByName("Zs" + agecount.ToString()).AsString = allcout.ToString();
                gridppReport.FieldByName("Ms" + agecount.ToString()).AsString = mancount.ToString();
                gridppReport.FieldByName("Ws" + agecount.ToString()).AsString = womancount.ToString();

                gridppReport.FieldByName("Dadvice").AsString = advice; //culis[m].SummarizeAdvice?.SummAdvice ?? culis[m].Advice;
                gridppReport.FieldByName("疾病解释").AsString = content;// culis[m].SummarizeAdvice?.DiagnosisExpain;
                gridppReport.FieldByName("Jbmc").AsString = jbmc;// sumlis[i].Name; 
                gridppReport.FieldByName("sumzrs").AsString = zrs.ToString();// sumzrs.ToString();
                gridppReport.FieldByName("summrs").AsString = Mrs.ToString();// summamrs.ToString();
                gridppReport.FieldByName("sumwrs").AsString = Wrs.ToString(); //sumwomenrs.ToString();

            }
           

        }
        private void ReportChartRequestData(gregn6Lib.IGRChart pChart )
        {


            pChart.SeriesCount = 3;
            pChart.GroupCount = 6;
            if (!string.IsNullOrEmpty(isBFL) && isBFL == "1")
            {
                pChart.set_SeriesLabel(0, "男(%)");
                pChart.set_SeriesLabel(1, "女(%)");
                pChart.set_SeriesLabel(2, "总人数(%)");
            }
            else
            {
                pChart.set_SeriesLabel(0, "男");
                pChart.set_SeriesLabel(1, "女");
                pChart.set_SeriesLabel(2, "总人数");
            }
            pChart.set_GroupLabel(0, "<=24岁");
            pChart.set_GroupLabel(1, "25-34岁");
            pChart.set_GroupLabel(2, "34-44岁");
            pChart.set_GroupLabel(3, "44-54岁");
            pChart.set_GroupLabel(4, "54-64岁");
            pChart.set_GroupLabel(5, ">=64岁");

            if (!string.IsNullOrEmpty(isBFL) && isBFL == "1")
            {
                pChart.set_Value(0, 0, Convert.ToDouble(Amannumber[0 + (rows * 7)].ToString()));
                pChart.set_Value(0, 1, Convert.ToDouble(Amannumber[1 + (rows * 7)].ToString()));
                pChart.set_Value(0, 2, Convert.ToDouble(Amannumber[2 + (rows * 7)].ToString()));
                pChart.set_Value(0, 3, Convert.ToDouble(Amannumber[3 + (rows * 7)].ToString()));
                pChart.set_Value(0, 4, Convert.ToDouble(Amannumber[4 + (rows * 7)].ToString()));
                pChart.set_Value(0, 5, Convert.ToDouble(Amannumber[5 + (rows * 7)].ToString()));

                pChart.set_Value(1, 0, Convert.ToDouble(Awomannumber[0 + (rows * 7)].ToString()));
                pChart.set_Value(1, 1, Convert.ToDouble(Awomannumber[1 + (rows * 7)].ToString()));
                pChart.set_Value(1, 2, Convert.ToDouble(Awomannumber[2 + (rows * 7)].ToString()));
                pChart.set_Value(1, 3, Convert.ToDouble(Awomannumber[3 + (rows * 7)].ToString()));
                pChart.set_Value(1, 4, Convert.ToDouble(Awomannumber[4 + (rows * 7)].ToString()));
                pChart.set_Value(1, 5, Convert.ToDouble(Awomannumber[5 + (rows * 7)].ToString()));

                pChart.set_Value(2, 0, Convert.ToDouble(Aallnumber[0 + (rows * 7)].ToString()));
                pChart.set_Value(2, 1, Convert.ToDouble(Aallnumber[1 + (rows * 7)].ToString()));
                pChart.set_Value(2, 2, Convert.ToDouble(Aallnumber[2 + (rows * 7)].ToString()));
                pChart.set_Value(2, 3, Convert.ToDouble(Aallnumber[3 + (rows * 7)].ToString()));
                pChart.set_Value(2, 4, Convert.ToDouble(Aallnumber[4 + (rows * 7)].ToString()));
                pChart.set_Value(2, 5, Convert.ToDouble(Aallnumber[5 + (rows * 7)].ToString()));
            }
            else
            {
                pChart.set_Value(0, 0, Convert.ToDouble(mannumber[0 + (rows * 7)].ToString()));
                pChart.set_Value(0, 1, Convert.ToDouble(mannumber[1 + (rows * 7)].ToString()));
                pChart.set_Value(0, 2, Convert.ToDouble(mannumber[2 + (rows * 7)].ToString()));
                pChart.set_Value(0, 3, Convert.ToDouble(mannumber[3 + (rows * 7)].ToString()));
                pChart.set_Value(0, 4, Convert.ToDouble(mannumber[4 + (rows * 7)].ToString()));
                pChart.set_Value(0, 5, Convert.ToDouble(mannumber[5 + (rows * 7)].ToString()));

                pChart.set_Value(1, 0, Convert.ToDouble(womannumber[0 + (rows * 7)].ToString()));
                pChart.set_Value(1, 1, Convert.ToDouble(womannumber[1 + (rows * 7)].ToString()));
                pChart.set_Value(1, 2, Convert.ToDouble(womannumber[2 + (rows * 7)].ToString()));
                pChart.set_Value(1, 3, Convert.ToDouble(womannumber[3 + (rows * 7)].ToString()));
                pChart.set_Value(1, 4, Convert.ToDouble(womannumber[4 + (rows * 7)].ToString()));
                pChart.set_Value(1, 5, Convert.ToDouble(womannumber[5 + (rows * 7)].ToString()));

                pChart.set_Value(2, 0, Convert.ToDouble(allnumber[0 + (rows * 7)].ToString()));
                pChart.set_Value(2, 1, Convert.ToDouble(allnumber[1 + (rows * 7)].ToString()));
                pChart.set_Value(2, 2, Convert.ToDouble(allnumber[2 + (rows * 7)].ToString()));
                pChart.set_Value(2, 3, Convert.ToDouble(allnumber[3 + (rows * 7)].ToString()));
                pChart.set_Value(2, 4, Convert.ToDouble(allnumber[4 + (rows * 7)].ToString()));
                pChart.set_Value(2, 5, Convert.ToDouble(allnumber[5 + (rows * 7)].ToString()));
            }
            rows += 1;

        }
        //图疾病统计前10
        private void ReportjbtjT10_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            rows = 0;
            mannumber.Clear();
            womannumber.Clear();
            allnumber.Clear();

            Amannumber.Clear();
            Awomannumber.Clear();
            Aallnumber.Clear();

            for (int i = 0; i < sumlis.Count(); i++)
            {


                var culis = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs = culis.Count;
                int summamrs = culis.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culis.Where(o => o.CustomerReg.Customer.Sex == woman).Count();

                ReportjbtjT10.DetailGrid.Recordset.Append();
                SumAge(culis, ReportjbtjT10, sumlis[i].Name, sumzrs, summamrs, sumwomenrs);
                ReportjbtjT10.DetailGrid.Recordset.Post();               

                if (i >= 9)
                {
                    return;
                }
            }


        }
        //图病统计男性前10
        private void ReportjbtjmT10_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Man).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            if (sumlis.Count == 0)
            {
                try
                {
                    ReportMain.ControlByName("疾病统计男前10").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("疾病统计女男10" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }
            }
            rows = 0;
            mannumber.Clear();
            womannumber.Clear();
            allnumber.Clear();

            Amannumber.Clear();
            Awomannumber.Clear();
            Aallnumber.Clear();
            for (int i = 0; i < sumlis.Count(); i++)
            {
                var culisall = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs = culisall.Count;
                int summamrs = culisall.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culisall.Where(o => o.CustomerReg.Customer.Sex == woman).Count();
                var cuslsM = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Man && o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                ReportjbtjmT10.DetailGrid.Recordset.Append();
                SumAge(cuslsM, ReportjbtjmT10, sumlis[i].Name, sumzrs, summamrs, sumwomenrs);
                ReportjbtjmT10.DetailGrid.Recordset.Post();
              
                if (i >= 9)
                {
                    return;
                }
            }

        }
        //图疾病统计女性前10
        private void ReportjbtjwT10_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Woman).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            if (sumlis.Count == 0)
            {
                try
                {
                    ReportMain.ControlByName("疾病统计女前10").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("疾病统计女前10" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }
            }
            rows = 0;
            mannumber.Clear();
            womannumber.Clear();
            allnumber.Clear();

            Amannumber.Clear();
            Awomannumber.Clear();
            Aallnumber.Clear();
            for (int i = 0; i < sumlis.Count(); i++)
            {

                var culisall = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs = culisall.Count;
                int summamrs = culisall.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culisall.Where(o => o.CustomerReg.Customer.Sex == woman).Count();

                var culisW = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Woman && o.SummarizeName == sumlis[i].Name).Distinct().ToList();

                ReportjbtjwT10.DetailGrid.Recordset.Append();
                SumAge(culisW, ReportjbtjwT10, sumlis[i].Name, sumzrs, summamrs, sumwomenrs);
                ReportjbtjwT10.DetailGrid.Recordset.Post();
                if (i >= 9)
                {
                    return;
                }
            }

        }
        //疾病统计前10
        private void Reportjbtj10_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();         
            for (int i = 0; i < sumlis.Count(); i++)
            {

               
                var culis = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();               
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs= culis.Count;
                int summamrs = culis.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culis.Where(o => o.CustomerReg.Customer.Sex == woman).Count();           


                for (int m = 0; m < culis.Count(); m++)
                {
                    Reportjbtj10.DetailGrid.Recordset.Append();
                    Reportjbtj10.FieldByName("manNum").AsString = manNum.ToString();
                    Reportjbtj10.FieldByName("womenNum").AsString = womenNum.ToString();
                    Reportjbtj10.FieldByName("zrs").AsString = zrs.ToString();

                    Reportjbtj10.FieldByName("sumzrs").AsString = sumzrs.ToString();
                    Reportjbtj10.FieldByName("summrs").AsString = summamrs.ToString();
                    Reportjbtj10.FieldByName("sumwrs").AsString = sumwomenrs.ToString();

                    Reportjbtj10.FieldByName("sumzrsbl").AsString = ((decimal)sumzrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtj10.FieldByName("summrsbl").AsString = ((decimal)summamrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtj10.FieldByName("sumwrsbl").AsString = ((decimal)sumwomenrs * 100 / (decimal)zrs).ToString("0.00");

                    Reportjbtj10.FieldByName("Jbmc").AsString = sumlis[i].Name;
                    Reportjbtj10.FieldByName("Name").AsString = culis[m].CustomerReg.Customer.Name;
                    Reportjbtj10.FieldByName("Sex").AsString = SexHelper.CustomSexFormatter(culis[m].CustomerReg.Customer.Sex);
                    Reportjbtj10.FieldByName("Age").AsString = culis[m].CustomerReg.Customer.Age.ToString();
                    Reportjbtj10.FieldByName("ArchivesNum").AsString = culis[m].CustomerReg.CustomerBM;
                    Reportjbtj10.FieldByName("mobile").AsString = culis[m].CustomerReg.Customer.Mobile;
                    Reportjbtj10.FieldByName("Dadvice").AsString = culis[m].SummarizeAdvice?.SummAdvice ?? culis[m].Advice;
                    if (Reportjbtj10.FieldByName("Department") != null)
                    { Reportjbtj10.FieldByName("Department").AsString = culis[m].CustomerReg.Customer.Department; }
                    Reportjbtj10.FieldByName("疾病解释").AsString = culis[m].SummarizeAdvice?.DiagnosisExpain;

                    Reportjbtj10.DetailGrid.Recordset.Post();
                }

                if (i >= 9)
                {
                    return;
                }
            }
           

        }
      
        //Reportjbtjm10_ReportFetchRecord
        //疾病统计男性前10
        private void Reportjbtjm10_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.Where(o=>o.CustomerReg.Customer.Sex==(int)Sex.Man).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            if (sumlis.Count == 0)
            {
                try
                {
                    ReportMain.ControlByName("疾病统计男前10").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("疾病统计女男10" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }
            }          
            for (int i = 0; i < sumlis.Count(); i++)
            {
                var culisall = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs = culisall.Count;
                int summamrs = culisall.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culisall.Where(o => o.CustomerReg.Customer.Sex == woman).Count();
                var cuslsM= groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Man && o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                for (int m = 0; m < cuslsM.Count(); m++)
                {
                    //if (culis[m].CustomerReg.Customer.Sex != (int)Sex.Man)
                    //{
                    //    continue;
                    //}
                    Reportjbtjm10.DetailGrid.Recordset.Append();
                    Reportjbtjm10.FieldByName("manNum").AsString = manNum.ToString();
                    Reportjbtjm10.FieldByName("womenNum").AsString = womenNum.ToString();
                    Reportjbtjm10.FieldByName("zrs").AsString = zrs.ToString();

                    Reportjbtjm10.FieldByName("sumzrs").AsString = sumzrs.ToString();
                    Reportjbtjm10.FieldByName("summrs").AsString = summamrs.ToString();
                    Reportjbtjm10.FieldByName("sumwrs").AsString = sumwomenrs.ToString();

                    Reportjbtjm10.FieldByName("sumzrsbl").AsString = ((decimal)sumzrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtjm10.FieldByName("summrsbl").AsString = ((decimal)summamrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtjm10.FieldByName("sumwrsbl").AsString = ((decimal)sumwomenrs * 100 / (decimal)zrs).ToString("0.00");

                    Reportjbtjm10.FieldByName("Jbmc").AsString = sumlis[i].Name;
                    Reportjbtjm10.FieldByName("Name").AsString = cuslsM[m].CustomerReg.Customer.Name;
                    Reportjbtjm10.FieldByName("Sex").AsString = SexHelper.CustomSexFormatter(cuslsM[m].CustomerReg.Customer.Sex);
                    Reportjbtjm10.FieldByName("Age").AsString = cuslsM[m].CustomerReg.Customer.Age.ToString();
                    Reportjbtjm10.FieldByName("ArchivesNum").AsString = cuslsM[m].CustomerReg.CustomerBM;
                    if (Reportjbtjm10.FieldByName("Department") != null)
                    { Reportjbtjm10.FieldByName("Department").AsString = cuslsM[m].CustomerReg.Customer.Department; }
                    Reportjbtjm10.FieldByName("mobile").AsString = cuslsM[m].CustomerReg.Customer.Mobile;
                    Reportjbtjm10.FieldByName("Dadvice").AsString = cuslsM[m].SummarizeAdvice?.SummAdvice ?? cuslsM[m].Advice;
                    Reportjbtjm10.FieldByName("疾病解释").AsString = cuslsM[m].SummarizeAdvice?.DiagnosisExpain;
                    
                    Reportjbtjm10.DetailGrid.Recordset.Post();
                }
                if (i >= 9)
                {
                    return;
                }
            }

        }
        //疾病统计女性前10
        private void Reportjbtjw10_ReportFetchRecord()
        {
            var sumlis = groupClientSumDtos.Where(o=>o.CustomerReg.Customer.Sex==(int)Sex.Woman).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            if (sumlis.Count == 0)
            {
                try
                {
                    ReportMain.ControlByName("疾病统计女前10").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("疾病统计女前10" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }
            }          
            for (int i = 0; i < sumlis.Count(); i++)
            {

                var culisall = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int man = (int)Sex.Man;
                int woman = (int)Sex.Woman;
                int sumzrs = culisall.Count;
                int summamrs = culisall.Where(o => o.CustomerReg.Customer.Sex == man).Count();
                int sumwomenrs = culisall.Where(o => o.CustomerReg.Customer.Sex == woman).Count();

                var culisW = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Woman && o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                for (int m = 0; m < culisW.Count(); m++)
                {
                   
                    Reportjbtjw10.DetailGrid.Recordset.Append();
                    Reportjbtjw10.FieldByName("manNum").AsString = manNum.ToString();
                    Reportjbtjw10.FieldByName("womenNum").AsString = womenNum.ToString();
                    Reportjbtjw10.FieldByName("zrs").AsString = zrs.ToString();

                    Reportjbtjw10.FieldByName("sumzrs").AsString = sumzrs.ToString();
                    Reportjbtjw10.FieldByName("summrs").AsString = summamrs.ToString();
                    Reportjbtjw10.FieldByName("sumwrs").AsString = sumwomenrs.ToString();

                    Reportjbtjw10.FieldByName("sumzrsbl").AsString = ((decimal)sumzrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtjw10.FieldByName("summrsbl").AsString = ((decimal)summamrs * 100 / (decimal)zrs).ToString("0.00");
                    Reportjbtjw10.FieldByName("sumwrsbl").AsString = ((decimal)sumwomenrs * 100 / (decimal)zrs).ToString("0.00");

                    Reportjbtjw10.FieldByName("Jbmc").AsString = sumlis[i].Name;
                    Reportjbtjw10.FieldByName("Name").AsString = culisW[m].CustomerReg.Customer.Name;
                    Reportjbtjw10.FieldByName("Sex").AsString = SexHelper.CustomSexFormatter(culisW[m].CustomerReg.Customer.Sex);
                    Reportjbtjw10.FieldByName("Age").AsString = culisW[m].CustomerReg.Customer.Age.ToString();
                    Reportjbtjw10.FieldByName("ArchivesNum").AsString = culisW[m].CustomerReg.CustomerBM;
                    if (Reportjbtjw10.FieldByName("Department") != null)
                    { Reportjbtjw10.FieldByName("Department").AsString = culisW[m].CustomerReg.Customer.Department; }
                    Reportjbtjw10.FieldByName("mobile").AsString = culisW[m].CustomerReg.Customer.Mobile;
                    Reportjbtjw10.FieldByName("Dadvice").AsString = culisW[m].SummarizeAdvice?.SummAdvice ?? culisW[m].Advice;
                    Reportjbtjw10.FieldByName("疾病解释").AsString = culisW[m].SummarizeAdvice?.DiagnosisExpain;
                    
                    Reportjbtjw10.DetailGrid.Recordset.Post();
                }
                if (i >= 9)
                {
                    return;
                }
            }

        }
        //阳性汇总Reportyxhz_ReportFetchRecord
        private void Reportyxhz_ReportFetchRecord()
        {
            var cusreglist = groupClientSumDtos.GroupBy(o => o.CustomerRegID).ToList();
            for (int n = 0; n < cusreglist.Count; n++)
            {
                Reportyxhz.DetailGrid.Recordset.Append();
                Reportyxhz.FieldByName("体检号").AsString = cusreglist[n].First().CustomerReg.CustomerBM;
                Reportyxhz.FieldByName("姓名").AsString = cusreglist[n].First().CustomerReg.Customer.Name;
                Reportyxhz.FieldByName("性别").AsString = SexHelper.CustomSexFormatter(cusreglist[n].First().CustomerReg.Customer.Sex);
                Reportyxhz.FieldByName("年龄").AsString = cusreglist[n].First().CustomerReg.Customer.Age.ToString();
                if (Reportyxhz.FieldByName("部门") != null)
                {
                    Reportyxhz.FieldByName("部门").AsString = cusreglist[n].First().CustomerReg.Customer.Department;
                }
                var sumlist = cusreglist[n].Select(o=>new { name= o.SummarizeName,order=o.SummarizeOrderNum}).OrderBy(o=>o.order).ToList();
                string sums = "";
                for (int i = 0; i < sumlist.Count; i++)
                {
                    sums += sumlist[i].order + "." + sumlist[i].name + "\r\n";
                }
                Reportyxhz.FieldByName("诊断").AsString = sums;
                Reportyxhz.DetailGrid.Recordset.Post();
            }
        }

        private void Reportwjry_ReportFetchRecord()
        {
            for (int n = 0; n < groupClientCuswjDtos.Count(); n++)
            {
                Reportwjry.DetailGrid.Recordset.Append();
                Reportwjry.FieldByName("体检号").AsString = groupClientCuswjDtos[n].CustomerBM;
                Reportwjry.FieldByName("姓名").AsString = groupClientCuswjDtos[n].Customer.Name;
                Reportwjry.FieldByName("性别").AsString =SexHelper.CustomSexFormatter(groupClientCuswjDtos[n].Customer.Sex);
                Reportwjry.FieldByName("年龄").AsString = groupClientCuswjDtos[n].Customer.Age.ToString();
                Reportwjry.FieldByName("电话").AsString = groupClientCuswjDtos[n].Customer.Mobile;
                if (Reportwjry.FieldByName("部门") != null)
                {
                    Reportwjry.FieldByName("部门").AsString = groupClientCuswjDtos[n].Customer.Department;

                }
                Reportwjry.DetailGrid.Recordset.Post();
            }
        }
        //未检项目Reportwjxm_ReportFetchRecord
        private void Reportwjxm_ReportFetchRecord()
        {
            int notcheck=(int)ProjectIState.Not;
            var cusnocheckgroup = groupClientCusDtos.Where(o => o.CustomerItemGroup.Any(n => n.CheckState == notcheck && n.IsAddMinus != (int)AddMinusType.Minus 
            && n.DepartmentBM?.Category != "耗材" )).ToList();

            for (int n = 0; n < cusnocheckgroup.Count(); n++)
            {
                Reportwjxm.DetailGrid.Recordset.Append();
                Reportwjxm.FieldByName("体检号").AsString = cusnocheckgroup[n].CustomerBM;
                Reportwjxm.FieldByName("姓名").AsString = cusnocheckgroup[n].Customer.Name;
                Reportwjxm.FieldByName("性别").AsString = SexHelper.CustomSexFormatter(cusnocheckgroup[n].Customer.Sex);
                Reportwjxm.FieldByName("年龄").AsString = cusnocheckgroup[n].Customer.Age.ToString();
                //PhysicalEState
                if (Reportwjxm.FieldByName("部门") != null)
                {
                    Reportwjxm.FieldByName("部门").AsString = cusnocheckgroup[n].Customer.Department;
                }
                Reportwjxm.FieldByName("体检状态").AsString = CheckSateHelper.PhysicalEStateFormatter(cusnocheckgroup[n].CheckSate);
                var groups = cusnocheckgroup[n].CustomerItemGroup.Where(a => a.CheckState == notcheck).ToList();
                string nogrous = "";
                foreach (var group in groups)
                {
                    if (group.DepartmentBM?.Category != "耗材" && group.IsAddMinus != (int)AddMinusType.Minus)
                    {
                        nogrous += group.ItemGroupName + ",";
                    }

                }
                Reportwjxm.FieldByName("未检项目").AsString = nogrous.Trim(',');
                Reportwjxm.DetailGrid.Recordset.Post();
            }
        }

        #region 生成首页和主报表数据
        private void BindMainReport(GridppReport gridppReport)
        {
            gridppReport.DetailGrid.Recordset.Append();
            EntityDto<Guid> entity = new EntityDto<Guid>();
            entity.Id = ClientRegID.Idlist[0];
            ClientRegDto clientRegDto = CRegAppService.GetClientRegByID(entity);
           
 
            gridppReport.FieldByName("单位名称").AsString = clientRegDto.ClientInfo.ClientName;
            var ClientName = "";
            if (ClientRegID.Idlist.Count > 1)
            {
                foreach (var regId in ClientRegID.Idlist)
                {
                    EntityDto<Guid> nentity = new EntityDto<Guid>();
                    nentity.Id = regId;
                    var nowclientReg  = CRegAppService.GetClientRegByID(nentity);
                    ClientName += nowclientReg.ClientInfo.ClientName+"\r\n";
                }
                gridppReport.FieldByName("单位名称").AsString = ClientName;
            }
            gridppReport.FieldByName("预约开始时间").AsString = clientRegDto.StartCheckDate.ToShortDateString();
            gridppReport.FieldByName("预约结束时间").AsString = clientRegDto.EndCheckDate.ToShortDateString();
            gridppReport.FieldByName("联系人").AsString = clientRegDto.linkMan.ToString();
            if (gridppReport.FieldByName("联系电话")!=null && clientRegDto.ClientInfo.Mobile != null)
            {
                gridppReport.FieldByName("联系电话").AsString = clientRegDto.ClientInfo.Mobile.ToString();
            }
            if (clientRegDto.ClientInfo.ClientEmail != null)
            {
                gridppReport.FieldByName("E_MAIL").AsString = clientRegDto.ClientInfo.ClientEmail.ToString();
            }
            if (gridppReport.FieldByName("联系地址")!=null && clientRegDto.ClientInfo.Address != "")
            {
                gridppReport.FieldByName("联系地址").AsString = clientRegDto.ClientInfo.Address.ToString();
            }
            if (gridppReport.FieldByName("单位预约编码")!=null)
            {
                gridppReport.FieldByName("单位预约编码").AsString = clientRegDto.ClientRegBM.ToString();
            }
            gridppReport.FieldByName("单位编码").AsString = clientRegDto.ClientInfo.ClientBM.ToString();
            gridppReport.FieldByName("单位简称").AsString = clientRegDto.ClientInfo.ClientAbbreviation.ToString();
           string jbtjq10ml = "";
           string jbtjmq10ml = "";
           string jbtjwq10ml = "";
        //总
        var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            int mun = 10;
            if (sumlis.Count() < 10)
            {
                mun = sumlis.Count();

            }
            for (int i = 0; i < mun; i++)
            {
                var culis = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                int sumzrs = culis.Count;
                #region 目录
                string   sumname =(i+1) + ". " + culis.First().SummarizeName.Replace("\r\n","") + "(" + sumzrs + "人)";
                int slt = getStringLength(sumname);
                while (getStringLength(sumname) < 56)
                {
                    sumname += "-";
                }
                jbtjq10ml += sumname + "\n";
                if (i >= 9)
                {
                    break;
                }
                #endregion
            }
            //女
            var sumwlis = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Woman).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
             mun = 10;
            if (sumwlis.Count() < 10)
            {
                mun = sumwlis.Count();

            }
            for (int i = 0; i < mun; i++)
            {
                var culis = sumwlis[i];
                //var culis = sumwlis.ToList();
                int sumzrs = culis.cout;
                #region 目录
                string sumname = (i + 1) + ". " + culis.Name.Replace("\r\n", "") + "(" + sumzrs + "人)";
                int slt = getStringLength(sumname);
                while (getStringLength(sumname) < 56)
                {
                    sumname += "-";
                }
                jbtjwq10ml += sumname + "\n";
                if (i >=9)
                {
                    break;
                }
                #endregion
            }
            //男
            var summlis = groupClientSumDtos.Where(o => o.CustomerReg.Customer.Sex == (int)Sex.Man).GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            mun = 10;
            if (summlis.Count() < 56)
            {
                mun = summlis.Count();

            }
            for (int i = 0; i < mun; i++)
            {
                // var culis = groupClientSumDtos.Where(o => o.SummarizeName == summlis[i].Name).Distinct().ToList();
                var culis = summlis[i];
                int sumzrs = culis.cout;
                #region 目录
                string sumname = (i + 1) + ". " + culis.Name.Replace("\r\n", "") + "(" + sumzrs + "人)";
                int slt = getStringLength(sumname);
                while (getStringLength(sumname) < 56)
                {
                    sumname += "-";
                }
                jbtjmq10ml += sumname + "\n";
                if (i >= 9)
                {
                    break;
                }
                #endregion
            }
            gridppReport.FieldByName("jblb10").AsString = jbtjq10ml;
            gridppReport.FieldByName("jblbM10").AsString = jbtjmq10ml;
            gridppReport.FieldByName("jblbW10").AsString = jbtjwq10ml;
           
            gridppReport.DetailGrid.Recordset.Post();
        }

        #endregion



        #endregion

    }
}
