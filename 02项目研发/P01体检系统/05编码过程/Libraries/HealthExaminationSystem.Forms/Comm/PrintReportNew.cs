using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Abp.Application.Services.Dto;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Controllers;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation;
using Sw.Hospital.HealthExaminationSystem.Application.DoctorStation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison;
using Sw.Hospital.HealthExaminationSystem.Application.HistoryComparison.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemGroup.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo;
using Sw.Hospital.HealthExaminationSystem.Application.ItemInfo.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccdiseaseConsulitation.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Picture.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.CommonTools;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.GlobalSources;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System.Net;
using Sw.Hospital.HealthExaminationSystem.Common.Bases;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;
using Sw.Hospital.HealthExaminationSystem.Application.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.Questionnaire;
using Sw.Hospital.HealthExaminationSystem.Application.Charge.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
     

    public class PrintReportNew  
    {
        #region 申明子报表
        private GridppReport[] Reportzdylist = new GridppReport[10];
        private int pathdnum=0;
        private int pathdnum1 = 0;
        private GridppReport ReportMain;
        /// <summary>
        /// 首页
        /// </summary>
        private GridppReport Reportsy = new GridppReport();
        /// <summary>
        /// 总检
        /// </summary>
        private GridppReport Reportzj = new GridppReport();
        /// <summary>
        /// 复查
        /// </summary>
        private GridppReport Reporeview = new GridppReport();
        /// <summary>
        /// 历史对比
        /// </summary>
        private GridppReport Reportlsdb = new GridppReport();
        /// <summary>
        /// 历史科室对比
        /// </summary>
        private GridppReport ReportDepartSumlsdb = new GridppReport();
        /// <summary>
        /// 历史对比项目
        /// </summary>
        private GridppReport ReportItemlsdb = new GridppReport();
        /// <summary>
        /// 历史对比项目
        /// </summary>
        private GridppReport ReportItemdb = new GridppReport();
        /// <summary>
        /// 历史对比项目
        /// </summary>
        private GridppReport ReportItemdb1 = new GridppReport();
        /// <summary>
        /// 历史对比图表
        /// </summary>
        private GridppReport ReportCharlsdb = new GridppReport();
        /// <summary>
        /// 组合状态
        /// </summary>
        private GridppReport Reportzhzt = new GridppReport();
        /// <summary>
        /// 一般检查
        /// </summary>
        private GridppReport Reportybjc = new GridppReport();
        /// <summary>
        /// 检查
        /// </summary>
        private GridppReport Reportjc = new GridppReport();
        /// <summary>
        /// 检验
        /// </summary>
        private GridppReport Reportjy = new GridppReport();
        /// <summary>
        /// 影像
        /// </summary>
        private GridppReport Reportyx = new GridppReport();
        /// <summary>
        /// 表格
        /// </summary>
        private GridppReport ReportBG = new GridppReport();
        /// <summary>
        /// 问卷
        /// </summary>
        private GridppReport ReportWJ = new GridppReport();
        /// <summary>
        /// 微信问卷
        /// </summary>
        private GridppReport ReportWXWJ = new GridppReport();
        /// <summary>
        /// 人体图
        /// </summary>
        private GridppReport ReportRTT = new GridppReport();
        /// <summary>
        /// 图像
        /// </summary>
        private GridppReport Reporttx = new GridppReport();
        /// <summary>
        /// 一张图片
        /// </summary>
        private GridppReport Reportyztp = new GridppReport();
        /// <summary>
        /// 一张图片
        /// </summary>
        private GridppReport Reportyztp1 = new GridppReport();
        #region 职业健康
        /// <summary>
        /// 职业扫描
        /// </summary>
        private GridppReport ReportzysSM = new GridppReport();
        /// <summary>
        /// 职业史
        /// </summary>
        private GridppReport Reportzys = new GridppReport();
        /// <summary>
        /// 既往史
        /// </summary>
        private GridppReport Reportjws = new GridppReport();
        /// <summary>
        /// 放射既往史
        /// </summary>
        private GridppReport RadioactiveReportjws = new GridppReport();
        /// <summary>
        /// 婚姻史
        /// </summary>
        private GridppReport MerriyReportjws = new GridppReport();

        /// <summary>
        /// 生育史
        /// </summary>
        private GridppReport Reportsys = new GridppReport();
        /// <summary>
        /// 个人生活史
        /// </summary>
        private GridppReport Reportgrsh = new GridppReport();
        /// <summary>
        /// 既往史其他
        /// </summary>
        private GridppReport Reportjwsqt = new GridppReport();
        /// <summary>
        /// 吸烟史
        /// </summary>
        private GridppReport Reportxys = new GridppReport();
        /// <summary>
        /// 症状
        /// </summary>
        private GridppReport Reportzz = new GridppReport();

        /// <summary>
        /// 复查通知单
        /// </summary>
        private GridppReport Reportfctzd = new GridppReport();
        /// <summary>
        /// 职业病通知单
        /// </summary>
        private GridppReport Reportzybtzd = new GridppReport();
        /// <summary>
        /// 禁忌证通知单
        /// </summary>
        private GridppReport Reportjjztzd = new GridppReport();

        /// <summary>
        /// 医生站
        /// </summary>
        private   IDoctorStationAppService _doctorStation  = new DoctorStationAppService();
        /// <summary>
        /// 历史项目对比图表
        /// </summary>
        GridppReport rptLSItemChar = new GridppReport();
        private IGRChart DetailChart;
        private IGRRecordset Recordset;
        private IGRField CategoryIDField;
        private IGRField CategoryNameField;
        private ArrayList m_AmtFields = new ArrayList();
        #endregion
        #endregion
        //decimal decsum = new decimal();
        public CusNameInput cusNameInput;
        /// <summary>
        /// 报告模板名
        /// </summary>
       // public string StrReportTemplateName { get; set; }

        public List<string> lstStrPdf;
        public List<string> lstStrWMF;

        CustomerRegDto lstCustomerDtos;
        /// <summary>
        /// 检查项目表
        /// </summary>
        List<TjlCustomerRegItemReoprtDto> lstCustomerRegItemReoprtDto;

        /// <summary>
        /// 历史检查项目表
        /// </summary>
        List<Application.HistoryComparison.Dto.SearchCustomerRegItemDto> lstCustomerRegHistoryItemDto;
        /// <summary>
        /// 检查项目组合
        /// </summary>
        List<CustomerItemGroupPrintViewDto> lstCustomerItemGroupPrintViewDto;
        IInspectionTotalAppService inspectionTotalAppService;
        IPrintPreviewAppService PrintPreviewAppService = new PrintPreviewAppService();
        TjlCustomerSummarizeDto tjlCustomerSummarizeDto;
        TjlCustomerSummarizeDto fctjlCustomerSummarizeDto;
        private IDoctorStationAppService CustomerItemPic;//图片
        List<CustomerDepSummaryViewDto> lstCustomerDepSummaries;
        PictureController _pictureController;
        List<TjlCustomerSummarizeBMViewDto> tjlCustomerSummarizeBMViewDtos;
        List<TjlCustomerSummarizeBMViewDto> CustomerHistorySummarizeBMDtos;

        List<ItemInfoSimpleDto> itemInfoSimpleDtosall;
        List<SimpleItemGroupDto> itemgroupDtosall;
        /// <summary>
        /// 体检人所有图片
        /// </summary>
        List<CustomerItemPicDto> lstcustomerItemPicDtos;
        private ReportOccQuesDto reportOccQuesDto;
        private List<ReportOccQuesSymptomDto> reportOccQuesSymptomDtos;

        private InputOccCusSumDto inputOccCusSumDto = new InputOccCusSumDto();
        private List<CustomerRegSimpleViewDto> revewCustRet = new List<CustomerRegSimpleViewDto>();


        /// <summary>
        /// 问卷应用服务
        /// </summary>
        private   IQuestionnaireAppService _questionnaireAppService = new QuestionnaireAppService();
        /// <summary>
        /// 所有正常项目标准
        /// </summary>
        // List<ItemStandardDto> lstItemStandardDto;
        public bool islndb;
        /// <summary>
        /// 个人头像
        /// </summary>
        PictureDto url;
        /// <summary>
        /// 问诊医师
        /// </summary>
        PictureDto 问诊签名;
        //复查数据
        List<reportCusReViewDto> cusReView = new List<reportCusReViewDto>();
        /// <summary>
        /// 照片
        /// </summary>
        public List<PictureDto> pictureDtos = new List<PictureDto>();

        private readonly ICommonAppService _commonAppService = new CommonAppService();

        private readonly IPrintPreviewAppService _printPreviewAppService = new PrintPreviewAppService();

        //职+建且报告是职业报告
        private bool iszjj = false;
        /// <summary>
        /// 历史对比项目
        /// </summary>
        private HisDbDto HisItems = new HisDbDto();
        private readonly IHistoryComparisonAppService _historyComparisonAppService = new HistoryComparisonAppService();
        private string strjcDepartment = "";
        private string TempName = "";
        

        ///<summary>
        ///释放内存
        ///</summary>
        public static void ClearMemory()
        {
            GC.Collect();
            
        }
        public PrintReportNew()
        {
            ReportMain = new GridppReport();


        }
        /// <summary>
        /// 打印报告
        /// </summary>
        /// <param name="isPreview">是否预览，是则预览，否则打印</param>
        public void Print(bool isPreview = true, string path = "", 
            string mbname = "", string image = "0", int isZYB = 0,
            bool isLocal=false,bool isReview=false )
        {

            _pictureController = new PictureController();

            inspectionTotalAppService = new InspectionTotalAppService();
            //获取基本信息
            ICustomerAppService customerAppService = new CustomerAppService();
            cusNameInput.Theme = "1";
            lstCustomerDtos = customerAppService.GetCustomerRegDto(cusNameInput);
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            var tjlb = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;
            string strBarPrintName = "";
            
            //获取历年对比模板
            if (islndb == true)
            {
                strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 1).Remarks;
            }
            else
            {
                if (mbname == "")
                {
                    strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
                    if (!string.IsNullOrEmpty(tjlb) && tjlb.Contains("职业") && isZYB != 2)
                    {
                        strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 2).Remarks;
                    }
                    else
                    {
                        #region 加载模板 
                        string[] mbls = strBarPrintName.Split('|');
                        List<string> list = new List<string>();
                        if (Variables.ISZYB != "2")
                        {
                            foreach (string mb in mbls)
                            {
                                if (mb != "")
                                {
                                    list.Add(mb);
                                }
                            }

                        }
                        if (!string.IsNullOrEmpty(tjlb))
                        {
                            var mblist = list.Where(p => p.Contains(tjlb)).ToList();
                            if (mblist.Count > 0)
                            {
                                strBarPrintName = mblist[0];
                            }
                            else
                            { strBarPrintName = list[0]; }
                        }
                        else
                        {
                            strBarPrintName = list.FirstOrDefault();
                        }
                        #endregion
                    }


                }
                else
                {
                    strBarPrintName = mbname;
                }
            }
           
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            //word报告
            #region word报告
            if (GrdPath.Contains(".rdlx") || GrdPath.Contains(".rpx"))
            {
                //var BGprintReport = new BGPrintReport();
                //var cusNameInput = new CusNameInput { Id = lstCustomerDtos.Id };
                //BGprintReport.cusNameInput = cusNameInput;
                //BGprintReport.Print(isPreview, strBarPrintName, path);
                //frmActiveReport _printReport = new frmActiveReport();
                //_printReport.printReport(lstCustomerDtos.CustomerBM, GrdPath, path, isReview);

                string args = lstCustomerDtos.CustomerBM + "|" + GrdPath + "|" + path + "|" + !isPreview;
                var reportpath = AppDomain.CurrentDomain.BaseDirectory + "\\报告查询";
                Process KHMsg = new Process();
                KHMsg.StartInfo.FileName = reportpath + "\\SearchSelf.exe";
                KHMsg.StartInfo.Arguments = args;
                KHMsg.Start();

                string mb = "";
                if (iszjj == true)
                { mb = "职业"; }
                _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = lstCustomerDtos.Id, Name = mb });
                //日志
                return;
            } 
            #endregion
            if (!GrdPath.Contains(".grf"))
            {
                GrdPath = GrdPath + ".grf";
            }
            if (lstCustomerDtos == null)
            {
                return;
            }
            //表格体检
            #region 表格体检
            if (strBarPrintName.ToString().Contains("表格"))
            {
                var WordprintReport = new WordReport();

               // WordprintReport.ExportWord(lstCustomerDtos,   path, strBarPrintName);


                return;
            } 
            #endregion
            //获取检查项目结果信息
            QueryClass queryClass = new QueryClass();
            CustomerItemPic = new DoctorStationAppService();
            //获取历年对比数据
            if (islndb == true)
            {
                EntityDto<Guid> inputlndb = new EntityDto<Guid>();
                inputlndb.Id = cusNameInput.Id;
                lstCustomerRegHistoryItemDto = inspectionTotalAppService.GetTjlCustomerHistoryItemReoprtDtos(inputlndb);
                //获取总检建议列表
                CustomerHistorySummarizeBMDtos = inspectionTotalAppService.GetHistorySummarizeBM(inputlndb);
            }
            if (tjlb != null && tjlb.Contains("职业+健康"))
            {
                if (strBarPrintName.Contains("职业"))
                {
                    isZYB = 1;
                    iszjj = true;
                }
                else
                {
                    isZYB = 2;
                }

            }
            if (tjlb != null && ((tjlb.Contains("职业")|| tjlb.Contains("放射")) && (strBarPrintName.Contains("职业") || strBarPrintName.Contains("放射"))))
            {

                EntityDto<Guid> inputzy = new EntityDto<Guid>();
                inputzy.Id = lstCustomerDtos.Id;
                //复查原始报告
                if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
                {
                    inputzy.Id = lstCustomerDtos.ReviewRegID.Value;
                }
                //职业病问卷
                reportOccQuesDto = PrintPreviewAppService.getOccQue(inputzy);
                reportOccQuesSymptomDtos = PrintPreviewAppService.getOccQuesSymptoms(inputzy);
                inputOccCusSumDto = inspectionTotalAppService.GetCusOccSumByRegId(inputzy);

            }
            queryClass.CustomerRegId = cusNameInput.Id;
            //复查原始报告
            if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
            {
                queryClass.CustomerRegId = lstCustomerDtos.ReviewRegID;
            }
            //获取总检建议列表
            if (cusNameInput.PrivacyState == 1)
            {
                tjlCustomerSummarizeBMViewDtos = inspectionTotalAppService.GetlstSummarizeBM(queryClass).Where(o => o.IsPrivacy == true).ToList();
            }
            else if (cusNameInput.PrivacyState == 2)
            {
                tjlCustomerSummarizeBMViewDtos = inspectionTotalAppService.GetlstSummarizeBM(queryClass).Where(o => o.IsPrivacy == false).ToList();
            }
            else
            {
                tjlCustomerSummarizeBMViewDtos = inspectionTotalAppService.GetlstSummarizeBM(queryClass);
            }
            //获取所有组合小结
            //lstCustomerItemGroupPrintViewDto = CustomerItemPic.GetCustomerItemGroupPrintViewDtos(queryClass).Where(n => n.CheckState != (int)ProjectIState.GiveUp).ToList();
            lstCustomerItemGroupPrintViewDto = CustomerItemPic.GetCustomerItemGroupPrintViewDtos(queryClass).ToList();
            //加上复查组合小结
            if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
            {
                QueryClass ReviewqueryClass = new QueryClass();
                ReviewqueryClass.CustomerRegId = cusNameInput.Id;
                var cusItemGrouplist= CustomerItemPic.GetCustomerItemGroupPrintViewDtos(ReviewqueryClass).ToList();
                //科室组合加上复查
                for (int i=0;i< cusItemGrouplist.Count; i++)
                {
                    cusItemGrouplist[i].DepartmentName = cusItemGrouplist[i].DepartmentName + "(复查)";
                    cusItemGrouplist[i].ItemGroupName = cusItemGrouplist[i].ItemGroupName + "(复查)";
                }
                lstCustomerItemGroupPrintViewDto.AddRange(cusItemGrouplist);
               
            }
            lstCustomerRegItemReoprtDto = CustomerItemPic.GetTjlCustomerRegAllItemReoprtDtos(queryClass).Where(o => o.ProcessState != (int)ProjectIState.Not).OrderBy(n => n.DepartmentBM?.OrderNum).ThenBy(n => n.ItemGroupBM?.OrderNum).ThenBy(o => o.ItemOrder).ToList();
            //加上复查项目
            if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
            {
                QueryClass ReviewqueryClass = new QueryClass();
                ReviewqueryClass.CustomerRegId = cusNameInput.Id;
                var cusItemGrouplist = CustomerItemPic.GetTjlCustomerRegAllItemReoprtDtos(ReviewqueryClass).Where(o => o.ProcessState != (int)ProjectIState.Not).OrderBy(n => n.DepartmentBM?.OrderNum).ThenBy(n => n.ItemGroupBM?.OrderNum).ThenBy(o => o.ItemOrder).ToList();
                //科室组合加上复查
                for (int i = 0; i < cusItemGrouplist.Count; i++)
                {
                    cusItemGrouplist[i].DepartmentBM.Name = cusItemGrouplist[i].DepartmentBM.Name + "(复查)";
                   // cusItemGrouplist[i].DepartmentBM.OrderNum = cusItemGrouplist[i].DepartmentBM.OrderNum + 1;
                    cusItemGrouplist[i].ItemGroupBM.ItemGroupName = cusItemGrouplist[i].ItemGroupBM.ItemGroupName + "(复查)";
                }
                lstCustomerRegItemReoprtDto.AddRange(cusItemGrouplist);
               
                lstCustomerRegItemReoprtDto = lstCustomerRegItemReoprtDto.OrderBy(n => n.DepartmentBM?.OrderNum).ThenBy(p=>p.DepartmentBM.Name).ThenBy(n => n.ItemGroupBM?.OrderNum).ThenBy(p=>p.ItemGroupBM.ItemGroupName).ThenBy(o => o.ItemOrder).ToList();
            }
            if ((isZYB == 1 || isZYB == 2) && tjlb != null && tjlb.Contains("职业+健康"))
            {
                lstCustomerItemGroupPrintViewDto = lstCustomerItemGroupPrintViewDto.Where(p => p.IsZYB == isZYB || !p.IsZYB.HasValue || p.IsZYB==3).ToList();
                var cusgroupids = lstCustomerItemGroupPrintViewDto.Select(p => p.Id).ToList();
                lstCustomerRegItemReoprtDto = lstCustomerRegItemReoprtDto.Where(p => cusgroupids.Contains(p.CustomerItemGroupBMid)).ToList();
                tjlCustomerSummarizeBMViewDtos = tjlCustomerSummarizeBMViewDtos.Where(p => p.IsZYB == isZYB).ToList();
            }


            //获取总检信息
            TjlCustomerQuery tjlCustomerQuery = new TjlCustomerQuery();
            tjlCustomerQuery.CustomerRegID = cusNameInput.Id;
            //复查原始报告
            if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
            {
                tjlCustomerQuery.CustomerRegID = lstCustomerDtos.ReviewRegID;
            }
            tjlCustomerSummarizeDto = inspectionTotalAppService.GetSummarize(tjlCustomerQuery);
            //本次（复查）
            TjlCustomerQuery fctjlCustomerQuery = new TjlCustomerQuery();
            fctjlCustomerQuery.CustomerRegID = cusNameInput.Id;
            fctjlCustomerSummarizeDto = inspectionTotalAppService.GetSummarize(fctjlCustomerQuery);

            //获取所有图片
            CustomerItemPic = new DoctorStationAppService();
            lstcustomerItemPicDtos = CustomerItemPic.GetCustomerItemPicDtos(queryClass);
            //加上复查图片
            string  ficsj ="";
            if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
            {
                QueryClass ReviewqueryClass = new QueryClass();
                ReviewqueryClass.CustomerRegId = cusNameInput.Id;
                var cusItemGroup = CustomerItemPic.GetCustomerItemPicDtos(ReviewqueryClass);
                lstcustomerItemPicDtos.AddRange(cusItemGroup);


                //获取基本信息              
                CusNameInput cusRegINput = new CusNameInput();
                cusRegINput.Theme = "1";
                cusRegINput.Id = lstCustomerDtos.ReviewRegID.Value;
                var FCCusInfo = customerAppService.GetCustomerRegDto(cusRegINput);
                if (FCCusInfo != null)
                {
                    if (FCCusInfo.LoginDate != null)
                    {
                        ficsj = FCCusInfo.LoginDate.Value.ToString(Variables.ShortDatePattern);
                    }
                    else
                    {
                        ficsj = FCCusInfo.BookingDate.Value.ToString(Variables.ShortDatePattern);
                    }
                }
            }

            IItemInfoAppService itemInfoAppService = new ItemInfoAppService();
            // lstItemStandardDto = itemInfoAppService.QueryItemStandardBySum();
            itemInfoSimpleDtosall = DefinedCacheHelper.GetItemInfos();
            //获取科室小结
            lstCustomerDepSummaries = CustomerItemPic.GetCustomerDepSummaries(queryClass);
            //个人头像
            if (lstCustomerDtos.Customer.CusPhotoBmId.HasValue && lstCustomerDtos.Customer.CusPhotoBmId != Guid.Empty)
            {
                url = _pictureController.GetUrl(lstCustomerDtos.Customer.CusPhotoBmId.Value);
                pictureDtos.Add(url);
            }
            //总检医生
            if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.EmployeeBM!=null &&
                tjlCustomerSummarizeDto.EmployeeBM.SignImage.HasValue)
            {
                url = _pictureController.GetUrlUser(tjlCustomerSummarizeDto.EmployeeBM.SignImage.Value);
                pictureDtos.Add(url);
            }
            //审核医生
            if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.ShEmployeeBM != null &&
               tjlCustomerSummarizeDto.ShEmployeeBM.SignImage.HasValue)
            {
                url = _pictureController.GetUrlUser(tjlCustomerSummarizeDto.ShEmployeeBM.SignImage.Value);
                pictureDtos.Add(url);
            }
            //获取所有组合、科室签名
            var picBmlist = new List<Guid>();
            //组合审核医生签名ID
            var cusGroupCheckph = lstCustomerItemGroupPrintViewDto.Where(o => o.CheckEmployeeBM?.SignImage != null).Select(o => o.CheckEmployeeBM.SignImage.Value).ToList();
            //组合检测医生签名ID
            var cusGroupEmpph = lstCustomerItemGroupPrintViewDto.Where(o => o.InspectEmployeeBM?.SignImage != null).Select(o => o.InspectEmployeeBM.SignImage.Value).ToList();
            //科室审核医生签名ID
            var cusDepartEmpph = lstCustomerDepSummaries.Where(o => o.ExamineEmployeeBM?.SignImage != null).Select(o => o.ExamineEmployeeBM.SignImage.Value).ToList();
            if (cusGroupCheckph != null && cusGroupCheckph.Count > 0)
            { picBmlist.AddRange(cusGroupCheckph); }
            if (cusGroupEmpph != null && cusGroupEmpph.Count > 0)
            { picBmlist.AddRange(cusGroupEmpph); }
            if (cusDepartEmpph != null && cusDepartEmpph.Count > 0)
            { picBmlist.AddRange(cusDepartEmpph); }
            if (picBmlist != null && picBmlist.Count > 0)
            { 
              
                picBmlist = picBmlist.Distinct().ToList();
                foreach (var id in picBmlist)
                {
                    var sin = _pictureController.GetUrlUser(id);
                    pictureDtos.Add(sin);
                }            
            }
            #region 复查对比报表
            if (strBarPrintName.ToString().Contains("复查对比") && lstCustomerDtos.ReviewRegID.HasValue)
            {
                //获取问诊
                //复查原始报告

                EntityDto<Guid> inputzy = new EntityDto<Guid>();
                inputzy.Id = lstCustomerDtos.ReviewRegID.Value;
                //职业史照片
                if (!lstCustomerDtos.OccQuesPhotoId.HasValue)
                {
                    CusNameInput cusRegINput = new CusNameInput();
                    cusRegINput.Theme = "1";
                    cusRegINput.Id = lstCustomerDtos.ReviewRegID.Value;
                    var FCCusInfo = customerAppService.GetCustomerRegDto(cusRegINput);
                    if (FCCusInfo != null)
                    {
                        lstCustomerDtos.OccQuesPhotoId = FCCusInfo.OccQuesPhotoId;
                    }
                }
                //职业病问卷
                reportOccQuesDto = PrintPreviewAppService.getOccQue(inputzy);
                reportOccQuesSymptomDtos = PrintPreviewAppService.getOccQuesSymptoms(inputzy);
                //获取复查原ID
                var oldreglist = CustomerItemPic.GetTjlCustomerRegRevew(queryClass);
                if (oldreglist.Count > 0)
                {
                    revewCustRet = oldreglist;
                }
                foreach (var oldreg in oldreglist)
                {
                    QueryClass oldqueryClass = new QueryClass();
                    oldqueryClass.CustomerRegId = oldreg.Id;
                    var GroupIdlist = lstCustomerRegItemReoprtDto.Select(p => p.ItemGroupBMId).Distinct().ToList();
                    var GrouNamelist = lstCustomerRegItemReoprtDto.Select(p => p.ItemGroupBM?.ItemGroupName).Distinct().ToList();
                    var departIdlist = lstCustomerRegItemReoprtDto.Select(p => p.DepartmentId).Distinct().ToList();

                    var OldlstCustomerItemGroupDto = CustomerItemPic.GetCustomerItemGroupPrintViewDtos(oldqueryClass).Where(
                           p => GrouNamelist.Contains(p.ItemGroupName)).ToList();

                    var oldlstCustomerRegItemReoprtDto = CustomerItemPic.GetTjlCustomerRegAllItemReoprtDtos(oldqueryClass).Where(o =>
                    o.ProcessState != (int)ProjectIState.Not && GroupIdlist.Contains(o.ItemGroupBMId)).OrderBy(n =>
                    n.DepartmentBM?.OrderNum).ThenBy(n => n.ItemGroupBM?.OrderNum).ThenBy(o => o.ItemOrder).ToList();

                    var oldlstcustomerItemPicDtos = CustomerItemPic.GetCustomerItemPicDtos(oldqueryClass);
                    var oldlstCustomerDepSummaries = CustomerItemPic.GetCustomerDepSummaries(oldqueryClass);

                    //组合
                    lstCustomerItemGroupPrintViewDto.InsertRange(0,OldlstCustomerItemGroupDto);
                    //项目小结
                    lstCustomerRegItemReoprtDto.InsertRange(0,oldlstCustomerRegItemReoprtDto);
                    //图片
                    lstcustomerItemPicDtos.InsertRange(0, oldlstcustomerItemPicDtos);
                    //科室小结
                    lstCustomerDepSummaries.InsertRange(0, oldlstCustomerDepSummaries);
                }

            }
            #endregion
            #region 绑定数据           
            if (isLocal)
            {
                #region 健康证不合格判断
                if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("食品") || tjlb.Contains("健康证"))) //健康证
                {
                    if (tjlCustomerSummarizeDto.Qualified == "不合格" && strBarPrintName.Contains("健康证"))
                    {
                        strBarPrintName = strBarPrintName.Replace(".grf", "不合格");
                    }
                }
                #endregion
                var grfpath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GridppTemplate", strBarPrintName);
                //var grfpath = System.Windows.Forms.Application.StartupPath + "\\GridppTemplate\\" + strBarPrintName ;
                if (!grfpath.Contains(".grf"))
                {
                    grfpath = grfpath + ".grf";
                }
                ReportMain.LoadFromFile(grfpath);

            }
            else
            {
                #region 健康证不合格判断
             
                if (tjlb != null && (tjlb.Contains("从业") || tjlb.Contains("食品") || tjlb.Contains("健康证"))) //健康证
                {
                    if (tjlCustomerSummarizeDto.Qualified == "不合格" && strBarPrintName.Contains("健康证"))
                    {
                        strBarPrintName = strBarPrintName.Replace(".grf", "不合格");
                        GrdPath = GridppHelper.GetTemplate(strBarPrintName);
                        if (!GrdPath.Contains(".grf"))
                        {
                            GrdPath = GrdPath + ".grf";
                        }
                    }
                }
                #endregion
                var iii = ReportMain.LoadFromURL(GrdPath);
            }
           BindMainReport(ReportMain, ficsj);
            //历史对比数据
            //var ss = ReportMain.ControlByName("历年对比项目");
            //var ss2 = ReportMain.ControlByName("历年对比");
            //var ss3 = ReportMain.ControlByName("历史项目对比图表");
            if (!string.IsNullOrEmpty(lstCustomerDtos.Customer.IDCardNo)
                && (ReportMain.ControlByName("历年对比") != null ||
ReportMain.ControlByName("历年对比项目") != null ||
ReportMain.ControlByName("历史项目对比图表") != null))
            {
                SearchHisClassDto searchHisClassDto = new SearchHisClassDto();
                searchHisClassDto.CustomerRegId = lstCustomerDtos.Id;
                searchHisClassDto.IDCardNo = lstCustomerDtos.Customer.IDCardNo;
                HisItems = _historyComparisonAppService.geHisvardReport(searchHisClassDto);
            }
            if (ReportMain.ControlByName("首页") != null)
            {
                Reportsy = ReportMain.ControlByName("首页").AsSubReport.Report;
                BindSYReport(Reportsy, isReview, ficsj);
            }
           
            if (tjlb != null && ((tjlb.Contains("职业") && strBarPrintName.Contains("职业")) ||
             (tjlb.Contains("放射") && (strBarPrintName.Contains("放射") || strBarPrintName.Contains("职业")))) && reportOccQuesDto != null)
            {

                /// 职业史扫描
                if (ReportMain.ControlByName("职业史扫描") != null && lstCustomerDtos.OccQuesSate.HasValue  && lstCustomerDtos.OccQuesSate==1)
                {
                    ReportzysSM = ReportMain.ControlByName("职业史扫描").AsSubReport.Report;
                    CusHistorySM(lstCustomerDtos.Id);
                }
                /// 职业史
                if (ReportMain.ControlByName("职业病") != null)
                {
                    Reportzys = ReportMain.ControlByName("职业病").AsSubReport.Report;
                    CusHistory();
                }
                /// 放射职业史
                if (ReportMain.ControlByName("放射职业史") != null)
                {
                    RadioactiveReportjws = ReportMain.ControlByName("放射职业史").AsSubReport.Report;
                    CusRadioactiveHistory();
                }
                /// 婚姻史
                if (ReportMain.ControlByName("婚姻史") != null)
                {
                    MerriyReportjws = ReportMain.ControlByName("婚姻史").AsSubReport.Report;
                    MerriyHistory();
                }

                /// 既往史
                if (ReportMain.ControlByName("既往史") != null)
                {
                    Reportjws = ReportMain.ControlByName("既往史").AsSubReport.Report;
                    CusPas();
                }
                //既往史其他
                if (ReportMain.ControlByName("既往史其他") != null)
                {
                    Reportjwsqt = ReportMain.ControlByName("既往史其他").AsSubReport.Report;
                    CusPasqt();
                }
                //既往史其他
                if (ReportMain.ControlByName("个人生活史") != null)
                {
                    Reportgrsh = ReportMain.ControlByName("个人生活史").AsSubReport.Report;
                    CusGRSHS();
                }
                if (lstCustomerDtos.Customer.Sex == (int)Sex.Woman)
                {
                    //生育史
                    if (ReportMain.ControlByName("生育史") != null)
                    {
                        Reportsys = ReportMain.ControlByName("生育史").AsSubReport.Report;
                        CusSYly();
                    }
                }
                ///吸烟史
                if (ReportMain.ControlByName("吸烟史") != null)
                {
                    Reportxys = ReportMain.ControlByName("吸烟史").AsSubReport.Report;
                    OccSmkReportJson();
                }

                //症状      
                if (ReportMain.ControlByName("症状") != null)
                {
                    Reportzz = ReportMain.ControlByName("症状").AsSubReport.Report;
                    Cuszzly();
                }


            }
            //复查职业总检     
            EntityDto<Guid> input = new EntityDto<Guid>();
            input.Id = cusNameInput.Id;
            var cusReSum = inspectionTotalAppService.GetCusReSum(input);
            if (ReportMain.ControlByName("总检") != null)
            {
                Reportzj = ReportMain.ControlByName("总检").AsSubReport.Report;
                BindZJReport(tjlCustomerSummarizeDto, "总检", Reportzj, lstCustomerDtos, isReview,cusReSum);
            }

           
            cusReView = inspectionTotalAppService.GetCusReView(input);
            if (cusReView != null && cusReView.Count > 0)
            {
                var reporreview = ReportMain.ControlByName("复查");
                if (reporreview != null)
                {
                    Reporeview = reporreview.AsSubReport.Report;
                    ReportJsonrevew reportJsonrevew = new ReportJsonrevew();
                    reportJsonrevew.Detail = cusReView;
                    var reportJsonForExamineString = JsonConvert.SerializeObject(reportJsonrevew);
                    Reporeview.LoadDataFromXML(reportJsonForExamineString);
                }
            }

            if (ReportMain.ControlByName("历年对比") != null)
            {
                Reportlsdb = ReportMain.ControlByName("历年对比").AsSubReport.Report;

                if (islndb == true)
                {                    //EntityDto<Guid> inputlndb = new EntityDto<Guid>();
                    //inputlndb.Id = cusNameInput.Id;
                    //CustomerHistorySummarizeBMDtos = inspectionTotalAppService.GetHistorySummarizeBM(inputlndb);
                                      
                    BindHistoryZJReport(Reportlsdb);                    
                }
                else
                {
                    if (HisItems.HisSum != null && HisItems.HisSum.Count > 0)
                    {                       
 
                        var reportJson = new ReportJsonSumDb();
                        reportJson.Detail = HisItems.HisSum;
                        var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
                        Reportlsdb.LoadDataFromXML(reportJsonForExamineString);
                    }
                }
            }
            if (ReportMain.ControlByName("历年科室小结对比") != null)
            {
                ReportDepartSumlsdb = ReportMain.ControlByName("历年科室小结对比").AsSubReport.Report;
                if (HisItems.HisDepartSum != null && HisItems.HisDepartSum.Count > 0)
                {
                    BindHistoryDepartSumReport(ReportDepartSumlsdb);
                }
            }
            if (ReportMain.ControlByName("历年对比项目") != null)
            {
                if (HisItems.HistoryItem != null && HisItems.HistoryItem.Count > 0)
                {
                    ReportItemlsdb = ReportMain.ControlByName("历年对比项目").AsSubReport.Report;
                    var reportJson = new ReportJsonHisItem();
                    reportJson.Detail = HisItems.HistoryItem.ToList();
                    var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
                    ReportItemlsdb.LoadDataFromXML(reportJsonForExamineString);
                }
            }
            if (ReportMain.ControlByName("历史项目对比图表") != null   )
            {
                rptLSItemChar = ReportMain.ControlByName("历史项目对比图表").AsSubReport.Report;
                 
                if (rptLSItemChar.ControlByName("DetailChart") != null && HisItems.HistoryItemChar != null && HisItems.HistoryItemChar.Count > 0)
                {
                 
                    DetailChart = rptLSItemChar.ControlByName("DetailChart").AsChart;

                    //连接报表事件
                    rptLSItemChar.Initialize += new _IGridppReportEvents_InitializeEventHandler(ReportInitialize);
                    rptLSItemChar.FetchRecord += new _IGridppReportEvents_FetchRecordEventHandler(ReportFetchRecord);
                    rptLSItemChar.ChartRequestData += new _IGridppReportEvents_ChartRequestDataEventHandler(ReportChartRequestData);

                    rptLSItemChar.ProcessRecord += new _IGridppReportEvents_ProcessRecordEventHandler(ReportProcessRecord);

                }
            }
            //项目3年对比
            var OldzdItemv = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisDB, 1)?.Remarks;
            var OldzdItemYC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisDB, 3)?.Remarks;
            if (!string.IsNullOrEmpty(OldzdItemYC) && OldzdItemYC == "1")
            {
                HisItems.HistoryItemDb = HisItems.HistoryItemDb.Where(p => p.Symbol != null
                && p.Symbol != "" && p.Symbol != "M").ToList();
            }

            if (!string.IsNullOrEmpty(OldzdItemv) && ReportMain.ControlByName("三年项目对比") !=null)
            {
                if (HisItems.HistoryItemDb != null && HisItems.HistoryItemDb.Count > 0)
                {
                    var deparnames = OldzdItemv.Replace("，", "|").Replace(",", "|").Split('|').ToList();
                    var dbxmlst = HisItems.HistoryItemDb.Where(p => deparnames.Contains(p.DepartBM) ||
                    deparnames.Contains(p.DepartName)).ToList();
                    ReportItemdb = ReportMain.ControlByName("三年项目对比").AsSubReport.Report;
                    var reportJson = new ReportJsonHisItemDb();
                    reportJson.Detail = dbxmlst;
                    var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
                    ReportItemdb.LoadDataFromXML(reportJsonForExamineString);
                }
            }
            var OldzdItemC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.HisDB, 2)?.Remarks;
            if (!string.IsNullOrEmpty(OldzdItemv) && ReportMain.ControlByName("三年项目对比1") != null)
            {
                if (HisItems.HistoryItemDb != null && HisItems.HistoryItemDb.Count > 0)
                {
                    var deparnames = OldzdItemC.Replace("，", "|").Replace(",", "|").Split('|').ToList();
                    var dbxmlst = HisItems.HistoryItemDb.Where(p => deparnames.Contains(p.DepartBM) ||
                    deparnames.Contains(p.DepartName)).ToList();
                    ReportItemdb1 = ReportMain.ControlByName("三年项目对比1").AsSubReport.Report;
                    var reportJson = new ReportJsonHisItemDb();
                    reportJson.Detail = dbxmlst;
                    var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
                    ReportItemdb1.LoadDataFromXML(reportJsonForExamineString);
                }
            }
            

            if (ReportMain.ControlByName("组合状态") != null)
            {
                Reportzhzt = ReportMain.ControlByName("组合状态").AsSubReport.Report;
                BindZHZTReport("组合状态", Reportzhzt, lstCustomerDtos);
            }
            if (ReportMain.ControlByName("一般检查") != null)
            {
                Reportybjc = ReportMain.ControlByName("一般检查").AsSubReport.Report;
                string strjcDepartment = "";
                strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 40).Remarks;
                if (islndb == true)
                {
                    BindChildrenJsion(lstCustomerRegHistoryItemDto, "一般检查", Reportybjc, 40, lstCustomerDtos);
                }
                else
                {
                    BindChildren(lstCustomerRegItemReoprtDto, "一般检查", Reportybjc, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
                }
            }
            if (ReportMain.ControlByName("检查") != null)
            {
                Reportjc = ReportMain.ControlByName("检查").AsSubReport.Report;

                if (islndb == true)
                {
                    BindChildrenJsion(lstCustomerRegHistoryItemDto, "检查", Reportjc, 50, lstCustomerDtos);
                }
                else
                {
                    string strjcDepartment = "";
                    strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 50).Remarks;

                    BindChildren(lstCustomerRegItemReoprtDto, "检查", Reportjc, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
                }

            }
            if (ReportMain.ControlByName("检验") != null)
            {
                Reportjy = ReportMain.ControlByName("检验").AsSubReport.Report;
                if (islndb == true)
                {
                    BindChildrenJsion(lstCustomerRegHistoryItemDto, "检验", Reportjy, 60, lstCustomerDtos);
                }
                else
                {
                    string strjcDepartment = "";
                    strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 60).Remarks;

                    BindChildren(lstCustomerRegItemReoprtDto, "检验", Reportjy, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
                }

            }
            if (ReportMain.ControlByName("影像") != null)
            {
                Reportyx = ReportMain.ControlByName("影像").AsSubReport.Report;
                if (islndb == true)
                {
                    BindChildrenJsion(lstCustomerRegHistoryItemDto, "影像", Reportyx, 70, lstCustomerDtos);
                }
                else
                {
                    string strjcDepartment = "";
                    strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 70).Remarks;

                    BindChildren(lstCustomerRegItemReoprtDto, "影像", Reportyx, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
                }

            }
            if (ReportMain.ControlByName("表格") != null)
            {
                ReportBG = ReportMain.ControlByName("表格").AsSubReport.Report;

                string strjcDepartment = "";
                strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 888)?.Remarks;
                if (!string.IsNullOrEmpty(strjcDepartment))
                {
                    bindBG(ReportBG, strjcDepartment, lstCustomerDtos);
                }

            }
            if (ReportMain.ControlByName("问卷") != null)
            {
                ReportWJ = ReportMain.ControlByName("问卷").AsSubReport.Report;

                string strjcDepartment = "";
                strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 889)?.Remarks;
                if (!string.IsNullOrEmpty(strjcDepartment))
                {
                    bindWJ(ReportWJ, strjcDepartment, lstCustomerDtos);
                }


            }
            if (ReportMain.ControlByName("微信问卷") != null)
            {
                ReportWXWJ = ReportMain.ControlByName("微信问卷").AsSubReport.Report;              
                bindWXWJ(ReportWXWJ, lstCustomerDtos);

            }
            if (ReportMain.ControlByName("人体图") != null && ReportMain.ControlByName("人体图女") != null)
            {
                if (lstCustomerDtos.Customer.Sex == 1)
                {
                    ReportRTT = ReportMain.ControlByName("人体图").AsSubReport.Report;
                    ReportMain.ControlByName("人体图女").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("人体图女" + "报表头").Visible = false;
                    if (ReportMain.ControlByName("健康建议女") != null)
                    {
                        ReportMain.ControlByName("健康建议女").AsSubReport.Visible = false;
                        ReportMain.get_ReportHeader("健康建议女" + "报表头").Visible = false;

                    }
                }
                else
                {
                    ReportRTT = ReportMain.ControlByName("人体图女").AsSubReport.Report;

                    ReportMain.ControlByName("人体图").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("人体图" + "报表头").Visible = false;
                    if (ReportMain.ControlByName("健康建议男") != null)
                    {
                        ReportMain.ControlByName("健康建议男").AsSubReport.Visible = false;
                        ReportMain.get_ReportHeader("健康建议男" + "报表头").Visible = false;
                    }
                }

                bindRTT(ReportRTT);


            }
            if (ReportMain.ControlByName("图像") != null  )
            {
                Reporttx = ReportMain.ControlByName("图像").AsSubReport.Report;
                string strjcDepartment = "";
                strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 80).Remarks;

                BindChildren(lstCustomerRegItemReoprtDto, "图像", Reporttx, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
            }
            if (ReportMain.ControlByName("一张图片") != null)
            {
                Reportyztp = ReportMain.ControlByName("一张图片").AsSubReport.Report;
                string strjcDepartment = "";
                strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 90).Remarks;

                BindChildren(lstCustomerRegItemReoprtDto, "一张图片", Reportyztp, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
               // Reportyztp.DesignPaperOrientation = GRPaperOrientation.grpoLandscape;
            }
            if (ReportMain.ControlByName("一张图片1") != null)
            {
                Reportyztp1 = ReportMain.ControlByName("一张图片1").AsSubReport.Report;
                string strjcDepartment = "";
                strjcDepartment = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 91)?.Remarks;
                if (!string.IsNullOrEmpty( strjcDepartment ) )
                {
                    BindChildren(lstCustomerRegItemReoprtDto, "一张图片1", Reportyztp1, strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
                }
                // Reportyztp.DesignPaperOrientation = GRPaperOrientation.grpoLandscape;
            }
            #region 自定义报表
            var   zdylist = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ReportAdd).ToList();
            foreach (var zdy in zdylist)
            {
                if (ReportMain.ControlByName(zdy.Text) != null)
                {
                    strjcDepartment = zdy.Remarks;
                    TempName = zdy.Text;
                    GridppReport Reportyzdy = new GridppReport();
                    Reportyzdy = ReportMain.ControlByName(zdy.Text).AsSubReport.Report;
                    Reportzdylist[pathdnum] = Reportyzdy;

                    BindChildren(lstCustomerRegItemReoprtDto, TempName, Reportzdylist[pathdnum1], strjcDepartment, lstCustomerDtos, tjlCustomerSummarizeDto);
                    pathdnum1 = pathdnum1 + 1;
                    pathdnum = pathdnum + 1;
                }
            }
        #endregion

        #endregion
        //打印前事件     
        //ReportMain.pri
        ReportMain.PrintBegin += () =>
            {
                string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 300)?.Remarks;
                if (!string.IsNullOrEmpty(strwjshow) && strwjshow == "Y")
                {
                    if (lstCustomerDtos.SummSate != (int)SummSate.Audited)
                    {
                        MessageBox.Show("未审核不能打印报告！");
                        ReportMain.AbortPrint();

                    }
                }

            };
            //打印后事件
            ReportMain.PrintEnd += () =>
            {
                string mb = "";
                if (iszjj == true)
                { mb = "职业"; }
                _printPreviewAppService.UpdateCustomerRegisterPrintState(new ChargeBM { Id = lstCustomerDtos.Id, Name= mb });
                //日志
                CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                createOpLogDto.LogBM = lstCustomerDtos.CustomerBM;
                createOpLogDto.LogName = lstCustomerDtos.Customer.Name;
                createOpLogDto.LogText = "打印个人报告";
                createOpLogDto.LogDetail = "";
                createOpLogDto.LogType = (int)LogsTypes.PrintId;
                _commonAppService.SaveOpLog(createOpLogDto);

            };
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 30)?.Remarks;
            if (!string.IsNullOrEmpty(printName))
            {
                ReportMain.Printer.PrinterName = printName;
            }
            if (path == "")
            {
                if (isPreview)
                    ReportMain.PrintPreview(true);
                else
                {
                    ReportMain.Print(false);
                    if (lstStrPdf != null)
                    {
                        foreach (var item in lstStrPdf)
                        {
                            DevExpress.XtraPdfViewer.PdfViewer pdfViewer1 = new DevExpress.XtraPdfViewer.PdfViewer();
                            pdfViewer1.LoadDocument(item);
                            DevExpress.Pdf.PdfPrinterSettings pdfPrinterSettings = new DevExpress.Pdf.PdfPrinterSettings();

                            pdfViewer1.Print(pdfPrinterSettings);
                        }
                    }
                }
            }
            else
            {
                if (image == "1")
                {
                  
                    ReportMain.ExportDirect(GRExportType.gretPDF, path, false, false);

                    ReportMain.ExportDirect(GRExportType.gretIMG, path, false, false);
                }
                else
                {
                    ReportMain.ExportDirect(GRExportType.gretPDF, path, false, false);
                }
            }
            ReportMain.UnprepareExport();
            Marshal.ReleaseComObject(ReportMain);
            ClearMemory();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="gridppReport"></param>
        private void BindSYReport(GridppReport gridppReport,bool review=false,string ficsj="")
        {
            //  gridppReport.DetailGrid.Recordset.Append();
            var reportJson = new ReportJsonMain();
            reportJson.Detail = new List<rptCusSY>();
            var reportcusinfo = new rptCusSY();
            if (lstCustomerDtos.ReportBySelf.HasValue)
            {
                var types = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.ReportSentType);
                if (types != null)
                {
                    var type = types.Find(o => o.Value == lstCustomerDtos.ReportBySelf);
                    //if (type != null)
                    //{
                    //    if (gridppReport.FieldByName("递送方式")!= null)
                    //    {
                    //        gridppReport.FieldByName("递送方式").AsString = type.Text;
                    //    }
                    //}

                }
            }
            var contactStr = "";
            if (!string.IsNullOrWhiteSpace(lstCustomerDtos.Customer.Mobile))
            {
                contactStr = lstCustomerDtos.Customer.Mobile;
            }
            if (!string.IsNullOrWhiteSpace(lstCustomerDtos.Customer.Telephone))
            {
                if (string.IsNullOrWhiteSpace(contactStr))
                    contactStr = lstCustomerDtos.Customer.Telephone;
                else
                    contactStr += "  /  " + lstCustomerDtos.Customer.Telephone;
            }
            #region 职业健康
            reportcusinfo.RiskS = lstCustomerDtos.RiskS;
            if (review == true && lstCustomerDtos.ReviewRegID.HasValue)
            {
                //获取基本信息
                ICustomerAppService customerAppService = new CustomerAppService();
                CusNameInput cusRegINput = new CusNameInput();
                cusRegINput.Theme = "1";
                cusRegINput.Id = lstCustomerDtos.ReviewRegID.Value;
                var FCCusInfo = customerAppService.GetCustomerRegDto(cusRegINput);
                if (FCCusInfo != null)
                {
                    reportcusinfo.RiskS = FCCusInfo.RiskS;

                }
            }
            reportcusinfo.PostState = lstCustomerDtos.PostState;
            reportcusinfo.WorkName = lstCustomerDtos.WorkName;
            reportcusinfo.TypeWork = lstCustomerDtos.TypeWork;
            reportcusinfo.TotalWorkAge = lstCustomerDtos.TotalWorkAge;
            reportcusinfo.WorkAgeUnit = lstCustomerDtos.WorkAgeUnit;
            reportcusinfo.InjuryAge = lstCustomerDtos.InjuryAge;
            reportcusinfo.InjuryAgeUnit = lstCustomerDtos.InjuryAgeUnit;
            

            if (reportOccQuesDto != null && reportOccQuesDto.OccCareerHistory != null)
            {
                reportcusinfo.StrWorkYears = reportOccQuesDto.OccCareerHistory.FirstOrDefault()?.StrWorkYears;
            }
                #endregion

                reportcusinfo.联系方式 = contactStr;
            if (reportOccQuesDto != null && reportOccQuesDto.OccRadioactiveCareerHistory != null && reportOccQuesDto.OccRadioactiveCareerHistory.Count>0)
            {
                reportcusinfo.RadiationIds = reportOccQuesDto.OccRadioactiveCareerHistory.FirstOrDefault()?.FormatRadiations;
            }
           
            reportcusinfo.邮政编码 = lstCustomerDtos.Customer.PostgCode;
            if (lstCustomerDtos.Customer.Degree.HasValue)
            {
                reportcusinfo.文化程度 = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(p => p.Type == BasicDictionaryType.DegreeType.ToString() && p.Value == lstCustomerDtos.Customer.Degree.Value)?.Text;
            }
            reportcusinfo.体检号 = lstCustomerDtos.CustomerBM;
            reportcusinfo.档案号 = lstCustomerDtos.Customer.ArchivesNum;
            reportcusinfo.姓名 = lstCustomerDtos.Customer.Name;
            reportcusinfo.健康卡编号 = lstCustomerDtos.JKZBM ?? "";
            reportcusinfo.合格证编号 = lstCustomerDtos.HGZBH ?? "";
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            reportcusinfo.体检类别 = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;

            //if (lstCustomerDtos.LoginDate.HasValue)
            //{
            //    var date = lstCustomerDtos.LoginDate.Value.AddYears(1);
            //    reportcusinfo.截止日期 = date.ToString("yyyy-MM-dd");
            //    reportcusinfo.截止年 = date.Year.ToString();
            //    reportcusinfo.截止月 = date.Month.ToString();
            //    reportcusinfo.截止日 = date.Day.ToString();
            //}
            reportcusinfo.性别 = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex).Replace("性", "");
            reportcusinfo.年龄 = lstCustomerDtos.Customer.Age.ToString();
            reportcusinfo.手机号 = lstCustomerDtos.Customer.Mobile;
            if (lstCustomerDtos.Customer.CusPhotoBmId.HasValue && lstCustomerDtos.Customer.CusPhotoBmId != Guid.Empty)
            {
                var result1 = _pictureController.GetUrl(lstCustomerDtos.Customer.CusPhotoBmId.Value);
                reportcusinfo.个人照片 = result1.RelativePath;
            }
            if (lstCustomerDtos.PhotoBmId.HasValue && lstCustomerDtos.PhotoBmId != Guid.Empty)
            {
                var resultreg = _pictureController.GetUrl(lstCustomerDtos.PhotoBmId.Value);
                reportcusinfo.登记照片 = resultreg.RelativePath;
            }
            if (lstCustomerDtos.Customer.Birthday.HasValue)
            {
                reportcusinfo.出生日期 = lstCustomerDtos.Customer.Birthday.Value.ToString(Variables.ShortDatePattern);
            }
            reportcusinfo.身份证号 = lstCustomerDtos.Customer.IDCardNo;
            reportcusinfo.单位编号 = lstCustomerDtos.ClientRegNum?.ToString();
            //证件类别
            if (lstCustomerDtos.Customer.IDCardType.HasValue)
            {
                var IDCardTypeList = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == BasicDictionaryType.CertificateType.ToString())?.ToList();
                var IdCard = IDCardTypeList.FirstOrDefault(o => o.Value == lstCustomerDtos.Customer.IDCardType.Value);
                reportcusinfo.证件类别 = "身份证";
                if (IdCard != null)
                {
                    reportcusinfo.证件类别 = IdCard.Text;
                }
            }

            reportcusinfo.婚姻状态 = MarrySateHelper.CustomMarrySateFormatter(lstCustomerDtos.Customer.MarriageStatus);
            if (lstCustomerDtos.CustomerRegNum.HasValue)
            {
                reportcusinfo.登记号 = lstCustomerDtos.CustomerRegNum.Value.ToString();
            }
           
                reportcusinfo.放射号 = lstCustomerDtos.PacsBM;
          

            //reportcusinfo.登记日期  = lstCustomerDtos.LoginDate.Value.ToString(Variables.ShortDatePattern);
            if (lstCustomerDtos.LoginDate != null)
            {
                reportcusinfo.登记日期 = lstCustomerDtos.LoginDate.Value.ToString(Variables.ShortDatePattern);
            }
            else
            {
                reportcusinfo.登记日期 = lstCustomerDtos.BookingDate.Value.ToString(Variables.ShortDatePattern);
            }
            if (lstCustomerDtos.BookingDate.HasValue)
            {
                reportcusinfo.体检日期 = lstCustomerDtos.BookingDate.Value.ToString(Variables.ShortDatePattern);
            }
            reportcusinfo.套餐名称 = lstCustomerDtos.ItemSuitName;
            if (lstCustomerDtos.ClientReg != null)
            {
                reportcusinfo.单位地址 = lstCustomerDtos.ClientReg.ClientInfo.Address;
                reportcusinfo.单位名称 = lstCustomerDtos.ClientReg.ClientInfo.ClientName;
                reportcusinfo.单位开始时间 = lstCustomerDtos.ClientReg.StartCheckDate.ToString(Variables.ShortDatePattern);
                reportcusinfo.单位结束时间 = lstCustomerDtos.ClientReg.EndCheckDate.ToString(Variables.ShortDatePattern);
            }
            else
            {
                reportcusinfo.单位名称 = "个人" + lstCustomerDtos.PersonnelCategory?.Name;
            }
            //ReportMain.FieldByName("总金额").AsString = decsum.ToString("0.000");

            //reportcusinfo.行业 = lstCustomerDtos.Customer.CustomerTrade;
            if (!string.IsNullOrEmpty(lstCustomerDtos.Customer.CustomerTrade)
                && int.TryParse(lstCustomerDtos.Customer.CustomerTrade,
                out int hy))
            {
                reportcusinfo.行业 = DefinedCacheHelper.GetBasicDictionary().FirstOrDefault(o => o.Type == BasicDictionaryType.Clientlndutry.ToString()
                 && o.Value == hy)?.Text;

            }
            else
            { reportcusinfo.行业 = ""; }
            reportcusinfo.职务 = lstCustomerDtos.Customer.Duty;
            reportcusinfo.民族 = lstCustomerDtos.Customer.Nation;
            reportcusinfo.地址 = lstCustomerDtos.Customer.Address;
            reportcusinfo.家庭地址 = lstCustomerDtos.Customer.HomeAddress;
            reportcusinfo.工号 = lstCustomerDtos.Customer.WorkNumber;
            reportcusinfo.部门 = lstCustomerDtos.Customer.Department;
            reportcusinfo.介绍人 = lstCustomerDtos.Introducer;
            reportcusinfo.备注 = lstCustomerDtos.Customer.Remarks;
            reportcusinfo.工种 = lstCustomerDtos.TypeWork;
            if (review==true && fctjlCustomerSummarizeDto != null && fctjlCustomerSummarizeDto.ConclusionDate.HasValue && lstCustomerDtos.ReviewRegID.HasValue)
            {
                if (!string.IsNullOrEmpty(ficsj))
                {
                    reportcusinfo.登记日期 = ficsj;
                }
                //获取基本信息
                //ICustomerAppService customerAppService = new CustomerAppService();
                //CusNameInput cusRegINput = new CusNameInput();
                //cusRegINput.Theme = "1";
                //cusRegINput.Id = lstCustomerDtos.ReviewRegID.Value;
                // var FCCusInfo = customerAppService.GetCustomerRegDto(cusRegINput);
                //if (FCCusInfo != null)
                //{
                //    if (FCCusInfo.LoginDate != null)
                //    {
                //        reportcusinfo.登记日期 = FCCusInfo.LoginDate.Value.ToString(Variables.ShortDatePattern);
                //    }
                //    else
                //    {
                //        reportcusinfo.登记日期 = FCCusInfo.BookingDate.Value.ToString(Variables.ShortDatePattern);
                //    }
                //}
                reportcusinfo.总检日期 = fctjlCustomerSummarizeDto.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
                var date = fctjlCustomerSummarizeDto.ConclusionDate.Value.AddYears(1);
                reportcusinfo.截止日期 = date.ToString("yyyy-MM-dd");
                reportcusinfo.截止年 = date.Year.ToString();
                reportcusinfo.截止月 = date.Month.ToString();
                reportcusinfo.截止日 = date.Day.ToString();
            }
           else  if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.ConclusionDate.HasValue)
            {
                reportcusinfo.总检日期 = tjlCustomerSummarizeDto.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
                var date = tjlCustomerSummarizeDto.ConclusionDate.Value.AddYears(1);
                reportcusinfo.截止日期 = date.ToString("yyyy-MM-dd");
                reportcusinfo.截止年 = date.Year.ToString();
                reportcusinfo.截止月 = date.Month.ToString();
                reportcusinfo.截止日 = date.Day.ToString();

            }
            reportcusinfo.人员类别 = lstCustomerDtos.PersonnelCategory?.Name;
            reportcusinfo.开票名称 = lstCustomerDtos.FPNo;
            reportJson.Detail.Add(reportcusinfo);

            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
            gridppReport.LoadDataFromXML(reportJsonForExamineString);
            // gridppReport.DetailGrid.Recordset.Post();
        }
        /// <summary>
        /// 主报告
        /// </summary>
        /// <param name="gridppReport"></param>
        private void BindMainReport(GridppReport gridppReport, string ficsj="")
        {
            //  gridppReport.DetailGrid.Recordset.Append();
            var reportJson = new ReportJsonMainNew();
            reportJson.Detail = new List<rptcusMain>();
            var reportcusinfo = new rptcusMain();
            var contactStr = "";
            if (!string.IsNullOrWhiteSpace(lstCustomerDtos.Customer.Mobile))
            {
                contactStr = lstCustomerDtos.Customer.Mobile;
            }
            if (!string.IsNullOrWhiteSpace(lstCustomerDtos.Customer.Telephone))
            {
                if (string.IsNullOrWhiteSpace(contactStr))
                    contactStr = lstCustomerDtos.Customer.Telephone;
                else
                    contactStr += "  /  " + lstCustomerDtos.Customer.Telephone;
            }


            reportcusinfo.体检号 = lstCustomerDtos.CustomerBM;
            reportcusinfo.档案号 = lstCustomerDtos.Customer.ArchivesNum;
            reportcusinfo.姓名 = lstCustomerDtos.Customer.Name;
            reportcusinfo.手机号 = lstCustomerDtos.Customer.Mobile;
            reportcusinfo.性别 = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex).Replace("性", "");
            reportcusinfo.年龄 = lstCustomerDtos.Customer.Age.ToString();
            // reportcusinfo.身份证号 = lstCustomerDtos.Customer.IDCardNo.ToString();
          
            if (lstCustomerDtos.OccQuesPhotoId.HasValue && lstCustomerDtos.OccQuesPhotoId != Guid.Empty)
            {
                var result1 = _pictureController.GetUrl(lstCustomerDtos.OccQuesPhotoId.Value);
                reportcusinfo.职业史照片 = result1.RelativePath;
            }
            if (lstCustomerDtos.Customer.CusPhotoBmId.HasValue && lstCustomerDtos.Customer.CusPhotoBmId != Guid.Empty)
            {
                var result1 = _pictureController.GetUrl(lstCustomerDtos.Customer.CusPhotoBmId.Value);
                reportcusinfo.个人照片 = result1.RelativePath;
            }
            if (lstCustomerDtos.PhotoBmId.HasValue && lstCustomerDtos.PhotoBmId != Guid.Empty)
            {
                var resultreg = _pictureController.GetUrl(lstCustomerDtos.PhotoBmId.Value);
                reportcusinfo.登记照片 = resultreg.RelativePath;
            }
            //reportcusinfo.登记日期 = lstCustomerDtos.LoginDate.Value.ToString(Variables.ShortDatePattern);
            if (lstCustomerDtos.LoginDate != null)
            {
                reportcusinfo.登记日期 = lstCustomerDtos.LoginDate.Value.ToString(Variables.ShortDatePattern);
            }
            else
            {
                reportcusinfo.登记日期 = lstCustomerDtos.BookingDate.Value.ToString(Variables.ShortDatePattern);
            }
            if (!string.IsNullOrEmpty(ficsj))
            {
                reportcusinfo.登记日期 = ficsj;
            }

            if (lstCustomerDtos.BookingDate.HasValue)
            {
                reportcusinfo.体检日期 = lstCustomerDtos.BookingDate.Value.ToString(Variables.ShortDatePattern);
            }
            reportcusinfo.套餐名称 = lstCustomerDtos.ItemSuitName;
            if (lstCustomerDtos.ClientReg != null)
            {
                reportcusinfo.单位名称 = lstCustomerDtos.ClientReg.ClientInfo.ClientName;

            }
            else
            {
                reportcusinfo.单位名称 = "个人" + lstCustomerDtos.PersonnelCategory?.Name;
            }
           
            reportJson.Detail.Add(reportcusinfo);

            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
            gridppReport.LoadDataFromXML(reportJsonForExamineString);
            // gridppReport.DetailGrid.Recordset.Post();
        }

        #region 职业健康问卷
        /// <summary>
        /// 职业史
        /// </summary>
        private void CusHistorySM(Guid regID)
        {

            //多张职业史照片

            OccQueCusDto inputpic = new OccQueCusDto();
            inputpic.Id = regID;
            ICustomerAppService customerAppService = new CustomerAppService();
            var cuspiclist = customerAppService.getOccQueCusList(inputpic);

            if (cuspiclist.Count>0)
            {
                var reportJson = new ReportJsonZYSSMNew();
                reportJson.Detail = new List<rptcusZYSMain>();
                foreach (var zys in cuspiclist)
                {
                    rptcusZYSMain rptcusZYS = new rptcusZYSMain();

                    if (zys.PictureBM.HasValue && zys.PictureBM != Guid.Empty)
                    {
                        var result1 = _pictureController.GetUrl(zys.PictureBM.Value);
                        rptcusZYS.职业史扫描照片 = result1.RelativePath;
                    }


                    reportJson.Detail.Add(rptcusZYS);
                    var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                    ReportzysSM.LoadDataFromXML(reportJsonString1);
                }
              
            }
        }
        /// <summary>
        /// 职业史
        /// </summary>
        private void CusHistory()
        {
            if (reportOccQuesDto.OccCareerHistory != null)
            {
                var reportJson = new OccHistoryReportJson();
             var   HasWu = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 22)?.Remarks;
                if (!string.IsNullOrEmpty(HasWu ) && HasWu=="Y")
                {
                    if (reportOccQuesDto.OccCareerHistory == null)
                    {
                        reportOccQuesDto.OccCareerHistory = new List<ReportQuesCareerHistoryDto>();
                    }
                    if (reportOccQuesDto.OccCareerHistory.Count == 0)
                    {
                        ReportQuesCareerHistoryDto reportQuesCareerHistoryDto = new ReportQuesCareerHistoryDto();
                        reportQuesCareerHistoryDto.HisHazards = "无";
                        reportQuesCareerHistoryDto.HisProtectives = "无";
                        //reportQuesCareerHistoryDto.StarTime = "无";
                        //reportQuesCareerHistoryDto.EndTime = "无";
                        reportQuesCareerHistoryDto.WorkClient = "无";
                        reportQuesCareerHistoryDto.WorkName = "无";
                        reportQuesCareerHistoryDto.WorkType = "无";
                        //reportQuesCareerHistoryDto.WorkYears = "无";
                        reportOccQuesDto.OccCareerHistory.Add(reportQuesCareerHistoryDto);
                    }
                }
                if (reportOccQuesDto.OccCareerHistory != null && reportOccQuesDto.OccCareerHistory.Count > 0 && reportOccQuesDto.ReportOccQueAll.SignaTureImage.HasValue)
                {
                    var result1 = _pictureController.GetUrl(reportOccQuesDto.ReportOccQueAll.SignaTureImage.Value);
                    reportOccQuesDto.OccCareerHistory[0].受检人签名 = result1.RelativePath;
                    reportOccQuesDto.OccCareerHistory[reportOccQuesDto.OccCareerHistory.Count - 1].受检人签名 = result1.RelativePath;
                }
                reportJson.Detail = reportOccQuesDto.OccCareerHistory;
                var reportJsonString1 = JsonConvert.SerializeObject(reportJson);

                Reportzys.LoadDataFromXML(reportJsonString1);
            }
        }
        /// <summary>
        /// 放射职业史
        /// </summary>
        private void CusRadioactiveHistory()
        {
            if (reportOccQuesDto.OccRadioactiveCareerHistory != null)
            {
                var lsralist = reportOccQuesDto.OccRadioactiveCareerHistory;
               List< OccQuesRadioactiveCareerHistoryDto >showRadlst = new List<OccQuesRadioactiveCareerHistoryDto>();
                foreach (var larra in lsralist)
                {
                    OccQuesRadioactiveCareerHistoryDto showRad = new OccQuesRadioactiveCareerHistoryDto();
                    showRad.Cumulative = larra.Cumulative;
                    showRad.Dosimeter = larra.Dosimeter;
                   
                    showRad.EndTime = larra.EndTime;
                    showRad.Overdose = larra.Overdose;
                    showRad.RadiationIds = larra.FormatRadiations;
                    showRad.Remarks = larra.Remarks;
                    showRad.StarTime = larra.StarTime;
                    showRad.TbmOccDictionaryIDs = larra.FormatDictionarys;
                    showRad.WorkClient = larra.WorkClient;
                    showRad.Workload = larra.Workload;
                    showRad.WorkName = larra.WorkName;
                    showRad.WorkType = larra.WorkType;
                    showRadlst.Add(showRad);
                }
                  var reportJsonString1 = JsonConvert.SerializeObject(showRadlst);            
                var reportJson = "{\"Detail\":" + reportJsonString1 + "}";
                RadioactiveReportjws.LoadDataFromXML(reportJson);
            }
        }
        /// <summary>
        /// 婚姻史
        /// </summary>
        private void MerriyHistory()
        {
            if (reportOccQuesDto.OccQuesMerriyHistory != null)
            {

                var reportJsonString1 = JsonConvert.SerializeObject(reportOccQuesDto.OccQuesMerriyHistory);
                var reportJson = "{\"Detail\":" + reportJsonString1 + "}";
                MerriyReportjws.LoadDataFromXML(reportJson);
            }
        }
        /// <summary>
        /// 既往史
        /// </summary>
        private void CusPas()
        {
            if (reportOccQuesDto.OccPastHistory != null)
            {
                var reportJson = new OccPastReportJson();
                if (reportOccQuesDto.OccPastHistory == null)
                {
                    reportOccQuesDto.OccPastHistory = new List<ReportOccQuesPastHistoryDto>();
                }
                if (reportOccQuesDto.OccPastHistory.Count == 0)
                {
                    ReportOccQuesPastHistoryDto QuesPastHistory = new ReportOccQuesPastHistoryDto();
                    QuesPastHistory.DiagnosisClient = "无";
                    QuesPastHistory.IllName = "无";

                    QuesPastHistory.Iscured = "无";

                    //reportQuesCareerHistoryDto.WorkYears = "无";
                    reportOccQuesDto.OccPastHistory.Add(QuesPastHistory);
                }
                if (reportOccQuesDto.OccPastHistory != null && reportOccQuesDto.OccPastHistory.Count > 0 && reportOccQuesDto.ReportOccQueAll.SignaTureImage.HasValue)
                {
                    var result1 = _pictureController.GetUrl(reportOccQuesDto.ReportOccQueAll.SignaTureImage.Value);
                    reportOccQuesDto.OccPastHistory[0].受检人签名 = result1.RelativePath;
                    reportOccQuesDto.OccPastHistory[reportOccQuesDto.OccPastHistory.Count - 1].受检人签名 = result1.RelativePath;

                }
                reportOccQuesDto.OccPastHistory[0].MedicationHistory = reportOccQuesDto.ReportOccQueAll.MedicationHistory;
                reportOccQuesDto.OccPastHistory[reportOccQuesDto.OccPastHistory.Count - 1].MedicationHistory = reportOccQuesDto.ReportOccQueAll.MedicationHistory;

                reportOccQuesDto.OccPastHistory[0].GeneticHistory = reportOccQuesDto.ReportOccQueAll.GeneticHistory;
                reportOccQuesDto.OccPastHistory[reportOccQuesDto.OccPastHistory.Count - 1].GeneticHistory = reportOccQuesDto.ReportOccQueAll.GeneticHistory;

                reportOccQuesDto.OccPastHistory[0].AllergicHistory = reportOccQuesDto.ReportOccQueAll.AllergicHistory;
                reportOccQuesDto.OccPastHistory[reportOccQuesDto.OccPastHistory.Count - 1].AllergicHistory = reportOccQuesDto.ReportOccQueAll.AllergicHistory;

                reportOccQuesDto.OccPastHistory[0].DrugTaboo = reportOccQuesDto.ReportOccQueAll.DrugTaboo;
                reportOccQuesDto.OccPastHistory[reportOccQuesDto.OccPastHistory.Count - 1].DrugTaboo = reportOccQuesDto.ReportOccQueAll.DrugTaboo;

                reportJson.Detail = reportOccQuesDto.OccPastHistory;
                var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                //var subReport = ReportMain.ControlByName("既往史").AsSubReport.Report;
                Reportjws.LoadDataFromXML(reportJsonString1);
            }
        }
        /// <summary>
        ///既往史其他
        /// </summary>
        private void CusPasqt()
        {
            if (reportOccQuesDto.ReportOccQueAll != null)
            {
                var reportJson = new OccSYReportJson();
                reportJson.Detail = new List<ReportOccQueAllDto>();


                if (reportOccQuesDto.ReportOccQueAll.SignaTureImage.HasValue)
                {
                    var result1 = _pictureController.GetUrl(reportOccQuesDto.ReportOccQueAll.SignaTureImage.Value);
                    reportOccQuesDto.ReportOccQueAll.受检人签名 = result1.RelativePath;
                }

                reportJson.Detail.Add(reportOccQuesDto.ReportOccQueAll);

                var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                //var subReport = ReportMain.ControlByName("生育史").AsSubReport.Report;
                Reportjwsqt.LoadDataFromXML(reportJsonString1);
            }
        }
        /// <summary>
        ///个人生活史
        /// </summary>
        private void CusGRSHS()
        {
            if (reportOccQuesDto.ReportOccQueAll != null)
            {
                var reportJson = new OccSYReportJson();
                reportJson.Detail = new List<ReportOccQueAllDto>();


               

                reportJson.Detail.Add(reportOccQuesDto.ReportOccQueAll);

                var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                //var subReport = ReportMain.ControlByName("生育史").AsSubReport.Report;
                Reportgrsh.LoadDataFromXML(reportJsonString1);
            }
        }
        ///// <summary>
        ///// 家族史
        ///// </summary>
        //private void CusFamily()
        //{
        //    var reportJson = new OccFamilyReportJson();
        //    reportJson.Detail = reportOccQuesDto.OccFamilyHistory;
        //    var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
        //    var subReport = ReportMain.ControlByName("既往史").AsSubReport.Report;
        //    subReport.LoadDataFromXML(reportJsonString1);
        //}
        /// <summary>
        ///生育史
        /// </summary>
        private void CusSYly()
        {
            if (reportOccQuesDto.ReportOccQueAll != null)
            {
                var reportJson = new OccSYReportJson();
                reportJson.Detail = new List<ReportOccQueAllDto>();


                if (reportOccQuesDto.ReportOccQueAll.SignaTureImage.HasValue)
                {
                    var result1 = _pictureController.GetUrl(reportOccQuesDto.ReportOccQueAll.SignaTureImage.Value);
                    reportOccQuesDto.ReportOccQueAll.受检人签名 = result1.RelativePath;
                }

                reportJson.Detail.Add(reportOccQuesDto.ReportOccQueAll);

                var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                //var subReport = ReportMain.ControlByName("生育史").AsSubReport.Report;
                Reportsys.LoadDataFromXML(reportJsonString1);
            }
        }
        /// <summary>
        ///受检人签名
        /// </summary>
        private void Signatures()
        {
            OccQuestionnaireDto occQuestionnaireDto = new OccQuestionnaireDto();
            if (occQuestionnaireDto.CustomerRegBMId != null)
            {
                Signature signature = new Signature { };
                var result = pictureDtos.FirstOrDefault(o => o.Id == occQuestionnaireDto.CustomerRegBMId.Value);
                if (result != null)
                {
                    signature.受检人签名 = result.RelativePath;
                }
            }

        }
        /// <summary>
        ///吸烟史
        /// </summary>
        private void OccSmkReportJson()
        {

            var reportJson = new OccSmkReportJson();
            reportJson.Detail = new List<OccSmk>();
            OccSmk occSmk = new OccSmk();
            if (reportOccQuesDto != null && reportOccQuesDto.ReportOccQueAll != null)
            {

                occSmk.DrinkStatusTo = reportOccQuesDto.ReportOccQueAll.IsDrink;
                if (reportOccQuesDto.ReportOccQueAll.DrinkCount.HasValue && reportOccQuesDto.ReportOccQueAll.DrinkCount != 0)
                {
                    occSmk.DrinkStatusTo += "，" + reportOccQuesDto.ReportOccQueAll.DrinkCount.Value + "ml/日";
                }
                if (reportOccQuesDto.ReportOccQueAll.DrinkYears.HasValue && reportOccQuesDto.ReportOccQueAll.DrinkYears != 0)
                {
                    occSmk.DrinkStatusTo += "，饮酒年限：" + reportOccQuesDto.ReportOccQueAll.DrinkYears.Value + "年";
                }

                occSmk.SmokStatusTo = reportOccQuesDto.ReportOccQueAll.IsSmoke;
                if (reportOccQuesDto.ReportOccQueAll.SmokeCount.HasValue && reportOccQuesDto.ReportOccQueAll.SmokeCount != 0)
                {
                    occSmk.SmokStatusTo += "，" + reportOccQuesDto.ReportOccQueAll.SmokeCount.Value + "支/天";
                }
                if (reportOccQuesDto.ReportOccQueAll.SmokeYears.HasValue && reportOccQuesDto.ReportOccQueAll.SmokeYears != 0)
                {
                    occSmk.SmokStatusTo += "，吸烟年限：" + reportOccQuesDto.ReportOccQueAll.SmokeYears.Value + "年";
                }

            }
            if (reportOccQuesDto.OccFamilyHistory != null && reportOccQuesDto.OccFamilyHistory.Count > 0)
            {
                var ls = reportOccQuesDto.OccFamilyHistory.Select(o => (o.relatives + ":" + o.IllName)).ToList();
                occSmk.FamilyHistory = string.Join("';", ls).TrimEnd(';');

            }
            else
            {
                occSmk.FamilyHistory = "无特殊";
            }
            if (reportOccQuesDto.OccQuesSymptom != null && reportOccQuesDto.OccQuesSymptom.Count > 0)
            {
                occSmk.symptom = string.Join("、", reportOccQuesDto.OccQuesSymptom.Select(o => o.Name).ToList()).TrimEnd('、');

            }
            if (reportOccQuesDto.OccQuesSymptom != null && reportOccQuesDto.ReportOccQueAll.SignaTureImage.HasValue)
            {
                var result1 = _pictureController.GetUrl(reportOccQuesDto.ReportOccQueAll.SignaTureImage.Value);
                occSmk.受检人签名 = result1.RelativePath;
            }
            if (lstCustomerDtos != null && lstCustomerDtos.LoginDate != null)
            {
                occSmk.登记日期 = lstCustomerDtos.LoginDate;
            }
            else
            {
                occSmk.登记日期 = lstCustomerDtos.LoginDate;
            }
            reportJson.Detail.Add(occSmk);
            var reportJsonString1 = JsonConvert.SerializeObject(reportJson);


            Reportxys.LoadDataFromXML(reportJsonString1);
        }
        /// <summary>
        ///症状
        /// </summary>
        private void Cuszzly()
        {
            if (reportOccQuesSymptomDtos != null)
            {
                var reportJson = new OccSymptomReportJson();
                reportJson.Detail = new List<ReportOccQuesSymptomDto>();
                if (reportOccQuesSymptomDtos.Count > 0 && reportOccQuesDto.ReportOccQueAll != null && reportOccQuesDto.ReportOccQueAll.SignaTureImage.HasValue)
                {
                    var result1 = _pictureController.GetUrl(reportOccQuesDto.ReportOccQueAll.SignaTureImage.Value);
                    reportOccQuesSymptomDtos[0].受检人签名 = result1.RelativePath;
                    reportOccQuesSymptomDtos[reportOccQuesSymptomDtos.Count - 1].受检人签名 = result1.RelativePath;
                }
                reportJson.Detail = reportOccQuesSymptomDtos;


                var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                //var subReport = ReportMain.ControlByName("症状").AsSubReport.Report;

                Reportzz.LoadDataFromXML(reportJsonString1);
            }
        }

        #endregion
 
         
      
        public void ReportCharlsdb_ReportFetchRecord()
        {

        }  


        private void ReportInitialize()
        {

            //DataRow[] lsdb = LSDBItem.Select("项目ID='" + LSDBItem.Rows[0]["项目ID"].ToString() + "'");



            var lsdb = HisItems.HistoryItemChar.Select(p => p.CheckDate).Distinct().OrderBy(p => p.Value).ToList();
            DetailChart = rptLSItemChar.ControlByName("DetailChart").AsChart;
            Recordset = rptLSItemChar.DetailGrid.Recordset;
            CategoryIDField = rptLSItemChar.FieldByName("项目ID");
            CategoryNameField = rptLSItemChar.FieldByName("项目名称");
            string MonthLabelName;
            m_AmtFields.Clear();
            for (int i = 0; i < lsdb.Count(); ++i)
            {
                MonthLabelName = "m" + (i + 1);
                m_AmtFields.Add(rptLSItemChar.FieldByName(MonthLabelName));

                // MonthLabelName = (i + 1) + "月";
                DetailChart.set_GroupLabel((short)i, lsdb[i].Value.ToString("yyyy/MM/dd"));
            }

            DetailChart.SeriesCount = 1;
            DetailChart.GroupCount = (short)lsdb.Count;

        }
        //从DataReader中载入数据
        private void ReportFetchRecord()
        {
            try
            {
                // LSDBItem = LNItemchar(cusInfo, cusRegItems); 
                string CurID = "";
                //DataTable dtls = new DataTable();
                //dtls = GetDateTablelis();
                var LSDBItem = HisItems.HistoryItemChar;
                //血压合并处理
                var XYList = LSDBItem.Where(p => p.ItemName.Contains("收缩压") ||
                 p.ItemName.Contains("舒张压")).ToList();
                if (XYList.Count > 0)
                {
                    var ITemBMs = XYList.Select(p => p.ItemBM).ToList();
                    LSDBItem = LSDBItem.Where(p => !ITemBMs.Contains(p.ItemBM)).OrderBy(p => p.ItemBM).ThenBy(p => p.CheckDate).ToList() ;
                    var XYITems = XYList.GroupBy(p => new { p.CustomerBM, p.CheckDate }).Select(
                        p => new
                        {
                            CustomerBM = p.Key.CustomerBM,
                            CheckDate = p.Key.CheckDate,
                            Items = p.Select(o => new HistoryItemValueDto
                            {
                                CheckDate = p.Key.CheckDate,
                                CustomerBM = p.Key.CustomerBM,
                                ItemBM = o.ItemBM,
                                ItemValue = o.ItemValue,
                                Stand = o.Stand,
                                DepartName = o.DepartName,
                                DepartOrder = o.DepartOrder,
                                GroupName = o.GroupName,
                                GroupOrder = o.GroupOrder,
                                ItemName = o.ItemName,
                                ItemOrder = o.ItemOrder,
                                Symbol = o.Symbol

                            })
                        }).ToList();
                    foreach (var XYI in XYITems)
                    {
                        HistoryItemValueDto historyItemValueDto = new HistoryItemValueDto();
                        historyItemValueDto = XYI.Items.FirstOrDefault();
                        var itemvalue = XYI.Items.Select(p => p.ItemName + ":" + p.ItemValue).ToList();
                        historyItemValueDto.ItemValue = string.Join("|", itemvalue);                                             
                        historyItemValueDto.ItemName = "血压";
                        historyItemValueDto.ItemBM = "XY001";
                        historyItemValueDto.ItemOrder = 0;
                        historyItemValueDto.GroupOrder = 0;
                        historyItemValueDto.DepartOrder = 0;
                        var stander = XYI.Items.Select(p => p.ItemName + ":" + p.Stand).ToList();
                        historyItemValueDto.Stand = string.Join(" ", stander);
                        LSDBItem.Insert(0,historyItemValueDto);
                    }
                   
                   // LSDBItem = LSDBItem.OrderBy(p=>p.ItemBM).ThenBy(p=>p.ItemOrder).ToList();
                }
                var checkTime = LSDBItem.Select(p => p.CheckDate).Distinct().OrderBy(p=>p.Value).ToList();
                List<CheckDateDto> checklist = new List<CheckDateDto>();
                for (int i = 0; i < checkTime.Count; i++)
                {
                    CheckDateDto checkDateDto = new CheckDateDto();
                    checkDateDto.CheckDate = checkTime[i];
                    checkDateDto.Num = i;
                    checklist.Add(checkDateDto);
                }

                foreach (var dr in LSDBItem)
                {
                    try
                    {
                        if (CurID != dr.ItemBM)
                        {
                            if (CurID != "")
                                rptLSItemChar.DetailGrid.Recordset.Post();

                            CurID = dr.ItemBM.ToString();

                            rptLSItemChar.DetailGrid.Recordset.Append();

                            CategoryIDField.Value = dr.ItemBM;
                            CategoryNameField.Value = dr.ItemName;
                            if (rptLSItemChar.FieldByName("项目参考范围") != null)
                            {
                                rptLSItemChar.FieldByName("项目参考范围").AsString = dr.Stand;
                            }                        
                            //if (dr.ItemName=="血压")
                            //{
                            //    CategoryNameField.Value = dr.ItemName.Split('|')[2].ToString().TrimEnd('$');
                            //}

                        }
                        int iMonth = checklist.FirstOrDefault(p => p.CheckDate == dr.CheckDate.Value).Num.Value;

                        if (dr.ItemName == "血压")
                        {
                          
                            ((IGRField)m_AmtFields[iMonth]).AsString = dr.ItemValue.ToString();
                        }
                        else
                        {
                           
                            ((IGRField)m_AmtFields[iMonth]).AsFloat = double.Parse(dr.ItemValue);

                        }
                        //DetailChart.set_Value(0, (short)iMonth, ((IGRField)m_AmtFields[iMonth]).AsFloat);
                    }
                    catch (Exception ex)
                    {
                        string ss = ex.ToString();
                    }
                }
                if (LSDBItem.Count > 0)
                {
                     
                    rptLSItemChar.DetailGrid.Recordset.Post();
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void ReportChartRequestData(gregn6Lib.IGRChart pSender)
        {
            var ss = ((IGRField)CategoryNameField).AsString;
            if (ss != null)
            {

                if (ss.Contains("血压"))
                {
                    DetailChart.SeriesCount = 2;
                    DetailChart.set_SeriesLabel(0, "收缩压");
                    DetailChart.set_SeriesLabel(1, "舒张压");
                  
                }
                else
                {
                    DetailChart.set_SeriesLabel(0, ss);
                    DetailChart.SeriesCount = 1;
                }
            }         
            var LSDBItem = HisItems.HistoryItemChar;
            var checkTime = LSDBItem.Select(p => p.CheckDate).Distinct().ToList();
            for (int i = 0; i < checkTime.Count; ++i)
            {
                var cod = ((IGRField)m_AmtFields[i]).AsString;
                if (cod != null)
                {
                    if (cod.Contains("|")&& (cod.Contains("收缩压") ||cod.Contains("舒张压")))
                    {

                        var itemchar = ((IGRField)m_AmtFields[i]).AsString;
                        var itemvulse = itemchar.Split('|').ToList();
                        for (int num = 0; num < itemvulse.Count; num++)
                        {
                            
                            
                            var cout = pSender.Series.Count;
                            var jg = itemvulse[num].Split(':');
                            if (itemvulse[num].Contains("收缩压"))
                            {
                                pSender.set_Value(0, (short)i, float.Parse(jg[1]));
                            }
                            else
                            {
                                pSender.set_Value(1, (short)i, float.Parse(jg[1]));
                                
                            }
                        }

                    }
                    else
                    {
                        pSender.set_Value(0, (short)i, ((IGRField)m_AmtFields[i]).AsFloat);
                     
                        //pSender.Series[1].SeriesName = ss;
                    }
                }
               
            }
            
        }

        private void ReportProcessRecord()
        {
            var ss = ((IGRField)CategoryNameField).AsString;
            if (ss != null)
            {

                if (ss.Contains("血压"))
                {
                    DetailChart.SeriesCount = 2;
                    DetailChart.set_SeriesLabel(0, "收缩压");
                    DetailChart.set_SeriesLabel(1, "舒张压");
                }
                else
                {
                    DetailChart.SeriesCount = 1;
                }
            }
            DetailChart.PrepareSnapShot();
            var LSDBItem = HisItems.HistoryItemChar;
            var checkTime = LSDBItem.Select(p => p.CheckDate).Distinct().ToList();
            for (int i = 0; i < checkTime.Count; ++i)
            {
                var cod = ((IGRField)m_AmtFields[i]).AsString;
                if (cod != null)
                {
                    if (cod.Contains("|") && (cod.Contains("收缩压") || cod.Contains("舒张压")))
                    {

                        var itemchar = ((IGRField)m_AmtFields[i]).AsString;
                        var itemvulse = itemchar.Split('|').ToList();
                        for (int num = 0; num < itemvulse.Count; num++)
                        {
                            var jg = itemvulse[num].Split(':');
                            if (itemvulse[num].Contains("收缩压"))
                            {
                                DetailChart.set_Value(0, (short)i, float.Parse(jg[1]));
                            }
                            else
                            {
                                DetailChart.set_Value(1, (short)i, float.Parse(jg[1]));
                            }


                        }

                    }
                    else
                    {
                        DetailChart.set_Value(0, (short)i, ((IGRField)m_AmtFields[i]).AsFloat);

                        DetailChart.Series[1].SeriesName = ss;
                    }
                }

            }
            DetailChart.SnapShot();
        }
        
        
        
 
        
        

        #region 组合状态
        private void BindZHZTReport(string strtype, GridppReport gridpp, CustomerRegDto lstCustomerDtos)
        {
           
            var itemgroup = lstCustomerItemGroupPrintViewDto;//.OrderBy(p=>p.CheckState).ToList();
            foreach (var item in itemgroup)
            {
                gridpp.DetailGrid.Recordset.Append();
                gridpp.FieldByName("科室名称").AsString = item.DepartmentName;
                gridpp.FieldByName("组合名称").AsString = item.ItemGroupName;
                gridpp.FieldByName("组合状态").AsString = ProjectIStateHelper.ProjectIStateFormatter(item.CheckState);
                gridpp.DetailGrid.Recordset.Post();
            }

        }
        #endregion

        #region 子报表数据绑定
        private void BindChildren(List<TjlCustomerRegItemReoprtDto> CustomerRegItemReoprtDto, string strtype, GridppReport gridpp, string department, CustomerRegDto lstCustomerDtos, 
            TjlCustomerSummarizeDto CustomerSummarizeDto)
        {

            List<string> lststr = new List<string>();
            foreach (string item in department.ToString().Split(','))
            {
                if (!lststr.Contains(item))
                {
                    lststr.Add(item);
                    lststr.Add(item +"(复查)");
                }
            }
            if (strtype == "图像")
            {
                var ss = lstCustomerRegItemReoprtDto.Where(p => p.DepartmentBM.Name.Contains("超")).ToList();
              var depart=  lstCustomerRegItemReoprtDto.Select(p=>p.DepartmentBM.Name).ToList();
            }
            var vlsitem = from c in lstCustomerRegItemReoprtDto where lststr.Contains(c.DepartmentBM?.Name) select c;
            if (vlsitem.ToList().Count <= 0)
            {
                try
                {
                    ReportMain.ControlByName(strtype).AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader(strtype + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }

            }
            var reportJson = new ReportJsonEmdicalCertificate();
            reportJson.groupEmdical = new List<rptItemGroup>();
            List<string> itemis = new List<string>();
            //报告单号
            List<string> ReportBMList = new List<string>();
            var list = vlsitem.ToList();
            foreach (var item in vlsitem)
            {
                
                List<Image> imagepath = null;                
                if (strtype == "图像" || strtype.Contains("一张图片"))
                {

                    imagepath = getImage(item, strtype);

                }
                if (imagepath == null)
                {

                    imagepath = new List<Image>();
                    Image images = null;
                    imagepath.Add(images);

                }

                //获取科室小结
                var vardepsum = from p in lstCustomerDepSummaries where p.DepartmentBMId == item.DepartmentId select p;
                CustomerDepSummaryViewDto customerDepSummary = new CustomerDepSummaryViewDto();
                if (vardepsum.ToList().Count > 0)
                {
                    customerDepSummary = vardepsum.ToList()[0];
                }
                itemgroupDtosall = DefinedCacheHelper.GetItemGroups();
                //获取组合小结
                CustomerItemGroupPrintViewDto customerItemGroupPrintViewDto = new CustomerItemGroupPrintViewDto();
                var varItemGroupsum = from p in lstCustomerItemGroupPrintViewDto where p.Id == item.CustomerItemGroupBMid select p;
                if (varItemGroupsum.ToList().Count > 0)
                {
                    customerItemGroupPrintViewDto = varItemGroupsum.ToList()[0];
                }
                //报告单号
                if (!string.IsNullOrEmpty(customerItemGroupPrintViewDto.ReportBM)
                    && ReportBMList.Contains(customerItemGroupPrintViewDto.ReportBM))
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(customerItemGroupPrintViewDto.ReportBM)
                    && !ReportBMList.Contains(customerItemGroupPrintViewDto.ReportBM))
                {
                    ReportBMList.Add(customerItemGroupPrintViewDto.ReportBM);
                }
                string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 110).Remarks;
                string strfqshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 120).Remarks;
                string strdcshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 130).Remarks;
                //结果是否加粗
                string strReB = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 170).Remarks;
                //结果是否倾斜
                string strReU = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 180).Remarks;
                //结果是否颜色
                string strReC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 190).Remarks;
                //结果颜色至
                string strReCZ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 200).Remarks;
                if (strtype == "检查" || strtype == "检验")
                    if (strwjshow == "N" && item.ProcessState.Value == 1 && item.ProcessState != null)
                    {
                        continue;
                    }
                if (strfqshow == "N" && item.ProcessState.Value == 4 && item.ProcessState != null)
                {
                    continue;
                }
                if (strdcshow == "N" && item.ProcessState.Value == 5 && item.ProcessState != null)
                {
                    continue;
                }
                //List<string> repDepartlist = new List<string>();
                //string repDepart = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 301)?.Remarks;
                //var repDepartlist = repDepart.Split('|').ToList();
                //除检验科外项目项目只显示一次
                if (!item.DepartmentBM.Category.Contains("检验"))
                {
                    if (itemis.Contains(item.ItemId.ToString() + "|" + item.CustomerRegId))
                    {
                        continue;
                    }
                    itemis.Add(item.ItemId.ToString() + "|" + item.CustomerRegId);
                }
                //检验科相同组合项目项目只显示一次（可能存错）
                else
                {
                    if (itemis.Contains(item.ItemId.ToString() + "|" + item.ItemGroupBMId.ToString() + "|"  + item.CustomerRegId))
                    {
                        continue;
                    }
                    itemis.Add(item.ItemId.ToString() + "|" + item.ItemGroupBMId.ToString() + "|" + item.CustomerRegId);
                }
                #region 图像处理
                string strsj = "";
                string strzd = "";

                strsj = item.ItemResultChar;
                strzd = item.ItemDiagnosis;

                if (item.ProcessState == (int)ProjectIState.GiveUp)
                {
                    item.ItemResultChar = "放弃";
                    strsj = "放弃";
                }
                if (item.ProcessState == (int)ProjectIState.Stay)
                {
                    strsj = "待查";
                    item.ItemResultChar = "待查";
                }

                #endregion

                int i = 0;
                foreach (Image itemimg in imagepath)
                {
                    if (strsj == null)
                    {
                        continue;
                    }

                    if (strtype.Contains("一张图片") && itemimg == null && (lstStrWMF == null || lstStrWMF.Count == 0))
                    {
                        continue;
                    }
                    //gridpp.DetailGrid.Recordset.Append();
                    var Itemdetail = new rptItemGroup();

                    Itemdetail.体检号 = lstCustomerDtos.CustomerBM;
                    Itemdetail.复查 = "初检";
                    if (lstCustomerDtos.ReviewRegID.HasValue  && item.CustomerRegId== lstCustomerDtos.Id)
                    {
                         
                        Itemdetail.复查 = "第1次复查";
                        if (revewCustRet != null && revewCustRet.Count > 1)
                        {    
                                Itemdetail.复查 = "第2次复查"; 
                        }

                    }                   
                    if (revewCustRet != null && revewCustRet.Count > 0)
                    {
                        var recus = revewCustRet.FirstOrDefault(p => p.Id == item.CustomerRegId);
                        if (recus != null && recus.ReviewSate == 2)
                        {
                            Itemdetail.复查 = "第1次复查";
                        }

                    }
                    Itemdetail.姓名 = lstCustomerDtos.Customer.Name;
                    Itemdetail.健康卡编号 = lstCustomerDtos.JKZBM ?? "";
                    if (lstCustomerDtos.LoginDate.HasValue)
                    {
                        var date = lstCustomerDtos.LoginDate.Value.AddYears(1);
                        Itemdetail.截止日期 = date.ToString("yyyy-MM-dd");
                        Itemdetail.截止年 = date.Year.ToString();
                        Itemdetail.截止月 = date.Month.ToString();
                        Itemdetail.截止日 = date.Day.ToString();
                    }
                    Itemdetail.性别 = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex);
                    Itemdetail.年龄 = lstCustomerDtos.Customer.Age.ToString();
                    Itemdetail.手机号 = lstCustomerDtos.Customer.Mobile;
                    Itemdetail.个人照片 = lstCustomerDtos.Customer.CusPhotoBM;
                    if (lstCustomerDtos.Customer.Birthday.HasValue)
                    {
                        Itemdetail.出生日期 = lstCustomerDtos.Customer.Birthday.Value.ToString(Variables.ShortDatePattern);
                    }
                    Itemdetail.身份证号 = lstCustomerDtos.Customer.IDCardNo;
                    #region 总检
                    if (CustomerSummarizeDto != null)
                    {

                        if (CustomerSummarizeDto.EmployeeBM != null)
                        {
                            Itemdetail.汇总人 = CustomerSummarizeDto.EmployeeBM.Name;
                            if (CustomerSummarizeDto.EmployeeBM.SignImage != null)
                            {

                               // var result1 = _pictureController.GetUrlUser(CustomerSummarizeDto.EmployeeBM.SignImage.Value);
                               //修改签名0308
                                var result1 = pictureDtos.FirstOrDefault(o => o.Id == CustomerSummarizeDto.EmployeeBM.SignImage.Value);
                                if (result1 != null)
                                {
                                    Itemdetail.汇总人签名 = result1.RelativePath;
                                }
                            }
                        }
                        else
                        {
                            Itemdetail.汇总人 = lstCustomerDtos.CSEmployeeBM?.Name;
                        }
                        if (CustomerSummarizeDto.ShEmployeeBM != null)
                        {
                            Itemdetail.总检人 = CustomerSummarizeDto.ShEmployeeBM.Name;
                            if (CustomerSummarizeDto.ShEmployeeBM.SignImage != null)
                            {

                                //var result2 = _pictureController.GetUrlUser(CustomerSummarizeDto.ShEmployeeBM.SignImage.Value);

                                //修改签名0308
                                var result2 = pictureDtos.FirstOrDefault(o => o.Id == CustomerSummarizeDto.ShEmployeeBM.SignImage.Value);
                                if (result2 != null)
                                {
                                    Itemdetail.总检人签名 = result2.RelativePath;
                                }
                            }
                        }
                        else
                        {
                            if (lstCustomerDtos.FSEmployeeBM != null)
                            {
                                Itemdetail.总检人 = lstCustomerDtos.FSEmployeeBM.Name;
                                // Reportzj.FieldByName("审核医生").AsString = lstCustomerDtos.FSEmployeeBM.Name;
                            }
                        }


                    }
                    #endregion
                    Itemdetail.科室名称 = item.DepartmentBM?.Name;
                    if (cusNameInput.PrivacyState == 2)
                    {
                        if (item.ItemGroupBM.PrivacyState == 1)
                        {
                            continue;
                        }
                        else
                        {
                            Itemdetail.组合名称 = item.ItemGroupBM?.ItemGroupName;
                            if (!item.DepartmentBM.Category.Contains("检验"))
                            {
                                var grouplist = vlsitem.Where(o => o.ItemId == item.ItemId && o.ProcessState != (int)ProjectIState.GiveUp).ToList();
                                if (grouplist.Count > 1)
                                {

                                    Itemdetail.组合名称 = string.Join(",", grouplist.Select(o => o.ItemGroupBM?.ItemGroupName).ToList());
                                }
                            }
                            
                            Itemdetail.组合说明 = item.ItemGroupBM?.Remarks;
                            if (customerItemGroupPrintViewDto.CustomerRegBMId != null && customerItemGroupPrintViewDto.InspectEmployeeBM != null)
                            {
                                Itemdetail.组合小结 = customerItemGroupPrintViewDto.ItemGroupSum;
                                Itemdetail.组合原生小结 = customerItemGroupPrintViewDto.ItemGroupOriginalDiag?.Replace("&","");
                                Itemdetail.组合检查医生 = customerItemGroupPrintViewDto.InspectEmployeeBM?.Name.ToString();
                                Itemdetail.组合审核医生 = customerItemGroupPrintViewDto.CheckEmployeeBM?.Name.ToString();
                                //if (customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage != null)
                                //{
                                //    var result1 = _pictureController.GetUrl(customerItemGroupPrintViewDto.InspectEmployeeBM.SignImage.Value);
                                //    Itemdetail.组合检查医生签名 = result1.RelativePath;
                                //}
                                //if (customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage != null)
                                //{
                                //    var result2 = _pictureController.GetUrl(customerItemGroupPrintViewDto.CheckEmployeeBM.SignImage.Value);
                                //    Itemdetail.组合审核医生签名 = result2.RelativePath;
                                //}
                                if (customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage != null)
                                {
                                    var result1 = pictureDtos.FirstOrDefault(o => o.Id == customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage.Value);
                                    if (result1 != null)
                                    {
                                        Itemdetail.组合检查医生签名 = result1.RelativePath;
                                    }
                                }
                                if (customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage != null)
                                {
                                    var result2 = pictureDtos.FirstOrDefault(o => o.Id == customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage.Value);
                                    if (result2 != null)
                                    {
                                        Itemdetail.组合审核医生签名 = result2.RelativePath;
                                    }
                                }

                                if (customerItemGroupPrintViewDto.BillingEmployeeBM != null)
                                {
                                    Itemdetail.组合开单医生 = customerItemGroupPrintViewDto.BillingEmployeeBM.Name.ToString();
                                }
                                Itemdetail.组合检查时间 = customerItemGroupPrintViewDto.FirstDateTime?.Date.ToString("yyyy-MM-dd");
                            }
                            OccQuestionnaireDto occQuestionnaireDto = new OccQuestionnaireDto();
                            
                            Itemdetail.项目名称 = item?.ItemName;
                            if (item.ProcessState != null)
                            {
                                Itemdetail.项目状态 = ProjectIStateHelper.ProjectIStateFormatter(item?.ProcessState.Value.ToString());
                            }
                            Itemdetail.项目单位 = item?.Unit;
                            if (string.IsNullOrEmpty(item.Unit))
                            {
                                var iteminfo = from c in itemInfoSimpleDtosall where c.Id == item.ItemId select c;
                                if (iteminfo.Count() > 0)
                                {
                                    Itemdetail.项目单位 = iteminfo?.ToList()[0].Unit;
                                }
                            }
                            if (item.Stand != null)
                            {
                                item.Stand = item.Stand.Replace(";", "\r\n");
                            }

                            Itemdetail.参考值 = item.Stand;
                            //if (string.IsNullOrEmpty(item.Stand))
                            //{
                            //    var itemstand = from c in lstItemStandardDto where c.ItemId == item.ItemId select c;
                            //    if (itemstand.Count() > 0)
                            //    {
                            //        var itemstandfrist = itemstand.ToList()[0];
                            //        if (string.IsNullOrEmpty(itemstandfrist.Symbol))
                            //        {
                            //            Itemdetail.参考值 = itemstandfrist.Symbol;
                            //        }
                            //        else
                            //        {
                            //            Itemdetail.参考值 = itemstandfrist.MinValue + "-" + itemstandfrist.MaxValue;
                            //        }
                            //    }
                            //}
                            Itemdetail.项目类别 = ItemTypeHelper.ItemTypeFormatter(item.ItemTypeBM);
                            Itemdetail.项目序号 = item.ItemOrder.Value.ToString();
                            string strRe = item.ItemResultChar;

                            //string strRexmbs = item.Symbol;
                            if (strtype.Contains("检查") || strtype == "检验")
                            {
                                if (!string.IsNullOrEmpty(item.Symbol) && item.Symbol != "M")
                                {
                                    if (strReB == "Y")
                                    {
                                        strRe = "<b>" + strRe.TrimEnd() + "</b>";
                                        //strRexmbs = "<b>" + strRexmbs + "</b>";
                                    }
                                    if (strReU == "Y")
                                    {
                                        strRe = "<u>" + strRe.TrimEnd() + "</u>";
                                        //strRexmbs = "<u>" + strRexmbs + "</u>";
                                    }
                                    if (strReC == "Y")
                                    {
                                        strRe = "<font color=" + strReCZ + ">" + strRe.TrimEnd() + "</font>";
                                        //strRexmbs = "<font color=\"" + strRexmbs + "\">" + strRe + "</font>";
                                    }
                                }
                            }
                            switch (item.Symbol)
                            {
                                case "H":
                                    Itemdetail.项目标示 = "↑";
                                    break;
                                case "HH":
                                    Itemdetail.项目标示 = "↑↑";
                                    break;
                                case "L":
                                    Itemdetail.项目标示 = "↓";
                                    break;
                                case "LL":
                                    Itemdetail.项目标示 = "↓↓";
                                    break;
                            }
                            //gridpp.FieldByName("项目标示").AsString = strRexmbs;
                            Itemdetail.项目结果 = strRe;
                            if (item.PositiveSate != null)
                            {
                                Itemdetail.阳性状态 = PositiveStateHelper.PositiveStateFormatter(item.PositiveSate);
                            }
                            if (item.IllnessSate != null)
                            {
                                Itemdetail.疾病状态 = IllnessSateHelp.PositiveStateFormatter(item.IllnessSate.Value.ToString());
                            }
                            if (item.CrisisSate != null)
                            {
                                Itemdetail.危急值状态 = CrisisSateHelper.CrisisSateFormatter(item.CrisisSate);
                            }
                            Itemdetail.仪器编号 = item.Instrument;
                            if (item.LastModificationTime != null)
                            {
                                Itemdetail.项目时间 = item.LastModificationTime.Value.ToString(Variables.FullDateTimePattern);
                            }
                            if (item.CheckEmployeeBM != null)
                            {
                                Itemdetail.项目审核医生 = item.CheckEmployeeBM.Name;
                            }
                            if (!string.IsNullOrEmpty(strsj))
                            {
                                Itemdetail.检查所见 = strsj;
                            }
                            if (!string.IsNullOrEmpty(strzd))
                            {
                                Itemdetail.检查诊断 = strzd;
                            }
                            if (itemimg != null && strtype == "图像")
                            {
                                if (strtype.Contains( "一张图片"))
                                {
                                    //去掉旋转
                                    if (itemimg.Width > itemimg.Height)
                                        itemimg.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                }

                                Itemdetail.图片1 = ImageHelper.GetByteImage(itemimg);
                            }
                            if (lstStrWMF != null && lstStrWMF.Count > 0)
                            {
                                if (lstStrWMF.Count >= (i + 1))
                                {
                                    Itemdetail.图片路径 = lstStrWMF[i];
                                }
                            }
                            Itemdetail.项目编码 = item.ItemCodeBM;
                            if (item.CheckEmployeeBM != null)
                            {
                                Itemdetail.项目审核医生 = item.CheckEmployeeBM.Name;
                            }
                            if (customerDepSummary.CustomerRegId != null)
                            {
                                Itemdetail.科室时间 = customerDepSummary.CreationTime.ToString(Variables.FullDateTimePattern);
                                Itemdetail.科室建议 = customerDepSummary.DagnosisSummary;
                                Itemdetail.科室小结 = customerDepSummary.DagnosisSummary;
                                if (customerDepSummary.ExamineEmployeeBM != null)
                                {
                                    Itemdetail.科室检查医生 = customerDepSummary.ExamineEmployeeBM.Name;
                                    Itemdetail.科室审核医生 = customerDepSummary.ExamineEmployeeBM.Name;
                                    //if (customerDepSummary.ExamineEmployeeBM.SignImage != null)
                                    //{
                                    //    var result1 = _pictureController.GetUrl(customerDepSummary.ExamineEmployeeBM.SignImage.Value);
                                    //    Itemdetail.科室检查医生签名 = result1.RelativePath;
                                    //    Itemdetail.科室审核医生签名 = result1.RelativePath;
                                    //}

                                    if (customerDepSummary.ExamineEmployeeBM?.SignImage != null)
                                    {
                                        var result1 = pictureDtos.FirstOrDefault(o => o.Id == customerDepSummary.ExamineEmployeeBM.SignImage.Value);
                                        if (result1 != null)
                                        {
                                            Itemdetail.科室检查医生签名 = result1.RelativePath;
                                            Itemdetail.科室审核医生签名 = result1.RelativePath;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (cusNameInput.PrivacyState == 1)
                    {
                        if (item.ItemGroupBM.PrivacyState != 1)
                        {
                            continue;
                        }
                        else
                        {
                            Itemdetail.组合名称 = item.ItemGroupBM?.ItemGroupName;
                            if (!item.DepartmentBM.Category.Contains("检验"))
                            {
                                var grouplist = vlsitem.Where(o => o.ItemId == item.ItemId && o.ProcessState != (int)ProjectIState.GiveUp).ToList();
                                if (grouplist.Count > 1)
                                {

                                    Itemdetail.组合名称 = string.Join(",", grouplist.Select(o => o.ItemGroupBM?.ItemGroupName).ToList());
                                }
                            }
                            Itemdetail.组合说明 = item.ItemGroupBM?.Remarks;
                            if (customerItemGroupPrintViewDto.CustomerRegBMId != null && customerItemGroupPrintViewDto.InspectEmployeeBM != null)
                            {
                                Itemdetail.组合小结 = customerItemGroupPrintViewDto.ItemGroupSum;
                                Itemdetail.组合原生小结 = customerItemGroupPrintViewDto.ItemGroupOriginalDiag?.Replace("&", "");
                                Itemdetail.组合检查医生 = customerItemGroupPrintViewDto.InspectEmployeeBM?.Name.ToString();
                                Itemdetail.组合审核医生 = customerItemGroupPrintViewDto.CheckEmployeeBM?.Name.ToString();
                                //if (customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage != null)
                                //{
                                //    var result1 = _pictureController.GetUrl(customerItemGroupPrintViewDto.InspectEmployeeBM.SignImage.Value);
                                //    Itemdetail.组合检查医生签名 = result1.RelativePath;
                                //}
                                //if (customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage != null)
                                //{
                                //    var result2 = _pictureController.GetUrl(customerItemGroupPrintViewDto.CheckEmployeeBM.SignImage.Value);
                                //    Itemdetail.组合审核医生签名 = result2.RelativePath;
                                //}
                                if (customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage != null)
                                {
                                    var result1 = pictureDtos.FirstOrDefault(o => o.Id == customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage.Value);
                                    if (result1 != null)
                                    {
                                        Itemdetail.组合检查医生签名 = result1.RelativePath;
                                    }
                                }
                                if (customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage != null)
                                {
                                    var result2 = pictureDtos.FirstOrDefault(o => o.Id == customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage.Value);
                                    if (result2 != null)
                                    {
                                        Itemdetail.组合审核医生签名 = result2.RelativePath;
                                    }
                                }

                                if (customerItemGroupPrintViewDto.BillingEmployeeBM != null)
                                {
                                    Itemdetail.组合开单医生 = customerItemGroupPrintViewDto.BillingEmployeeBM.Name.ToString();
                                }
                                Itemdetail.组合检查时间 = customerItemGroupPrintViewDto.FirstDateTime?.Date.ToString("yyyy-MM-dd");
                            }
                            Itemdetail.项目名称 = item?.ItemName;
                            if (item.ProcessState != null)
                            {
                                Itemdetail.项目状态 = ProjectIStateHelper.ProjectIStateFormatter(item?.ProcessState.Value.ToString());
                            }
                            Itemdetail.项目单位 = item?.Unit;
                            if (string.IsNullOrEmpty(item.Unit))
                            {
                                var iteminfo = from c in itemInfoSimpleDtosall where c.Id == item.ItemId select c;
                                if (iteminfo.Count() > 0)
                                {
                                    Itemdetail.项目单位 = iteminfo?.ToList()[0].Unit;
                                }
                            }
                            if (item.Stand != null)
                            {
                                item.Stand = item.Stand.Replace(";", "\r\n");
                            }

                            Itemdetail.参考值 = item.Stand;
                            //if (string.IsNullOrEmpty(item.Stand))
                            //{
                            //    var itemstand = from c in lstItemStandardDto where c.ItemId == item.ItemId select c;
                            //    if (itemstand.Count() > 0)
                            //    {
                            //        var itemstandfrist = itemstand.ToList()[0];
                            //        if (string.IsNullOrEmpty(itemstandfrist.Symbol))
                            //        {
                            //            Itemdetail.参考值 = itemstandfrist.Symbol;
                            //        }
                            //        else
                            //        {
                            //            Itemdetail.参考值 = itemstandfrist.MinValue + "-" + itemstandfrist.MaxValue;
                            //        }
                            //    }
                            //}
                            Itemdetail.项目类别 = ItemTypeHelper.ItemTypeFormatter(item.ItemTypeBM);
                            Itemdetail.项目序号 = item.ItemOrder.Value.ToString();
                            string strRe = item.ItemResultChar;

                            //string strRexmbs = item.Symbol;
                            if (strtype.Contains("检查") || strtype == "检验")
                            {
                                if (!string.IsNullOrEmpty(item.Symbol) && item.Symbol != "M")
                                {
                                    if (strReB == "Y")
                                    {
                                        strRe = "<b>" + strRe.TrimEnd() + "</b>";
                                        //strRexmbs = "<b>" + strRexmbs + "</b>";
                                    }
                                    if (strReU == "Y")
                                    {
                                        strRe = "<u>" + strRe.TrimEnd() + "</u>";
                                        //strRexmbs = "<u>" + strRexmbs + "</u>";
                                    }
                                    if (strReC == "Y")
                                    {
                                        strRe = "<font color=" + strReCZ + ">" + strRe.TrimEnd() + "</font>";
                                        //strRexmbs = "<font color=\"" + strRexmbs + "\">" + strRe + "</font>";
                                    }
                                }
                            }
                            switch (item.Symbol)
                            {
                                case "H":
                                    Itemdetail.项目标示 = "↑";
                                    break;
                                case "HH":
                                    Itemdetail.项目标示 = "↑↑";
                                    break;
                                case "L":
                                    Itemdetail.项目标示 = "↓";
                                    break;
                                case "LL":
                                    Itemdetail.项目标示 = "↓↓";
                                    break;
                            }
                            //gridpp.FieldByName("项目标示").AsString = strRexmbs;
                            Itemdetail.项目结果 = strRe;
                            if (item.PositiveSate != null)
                            {
                                Itemdetail.阳性状态 = PositiveStateHelper.PositiveStateFormatter(item.PositiveSate);
                            }
                            if (item.IllnessSate != null)
                            {
                                Itemdetail.疾病状态 = IllnessSateHelp.PositiveStateFormatter(item.IllnessSate.Value.ToString());
                            }
                            if (item.CrisisSate != null)
                            {
                                Itemdetail.危急值状态 = CrisisSateHelper.CrisisSateFormatter(item.CrisisSate);
                            }
                            Itemdetail.仪器编号 = item.Instrument;
                            if (item.LastModificationTime != null)
                            {
                                Itemdetail.项目时间 = item.LastModificationTime.Value.ToString(Variables.FullDateTimePattern);
                            }
                            if (item.CheckEmployeeBM != null)
                            {
                                Itemdetail.项目审核医生 = item.CheckEmployeeBM.Name;
                            }
                            if (!string.IsNullOrEmpty(strsj))
                            {
                                Itemdetail.检查所见 = strsj;
                            }
                            if (!string.IsNullOrEmpty(strzd))
                            {
                                Itemdetail.检查诊断 = strzd;
                            }
                            if (itemimg != null && strtype == "图像")
                            {
                                if (strtype.Contains( "一张图片"))
                                {
                                    //去掉旋转
                                    if (itemimg.Width > itemimg.Height)
                                        itemimg.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                }

                                Itemdetail.图片1 = ImageHelper.GetByteImage(itemimg);
                            }
                            if (lstStrWMF != null && lstStrWMF.Count > 0)
                            {
                                if (lstStrWMF.Count >= (i + 1))
                                {
                                    Itemdetail.图片路径 = lstStrWMF[i];
                                }
                            }
                            Itemdetail.项目编码 = item.ItemCodeBM;
                            if (item.CheckEmployeeBM != null)
                            {
                                Itemdetail.项目审核医生 = item.CheckEmployeeBM.Name;
                            }
                            if (customerDepSummary.CustomerRegId != null)
                            {
                                Itemdetail.科室时间 = customerDepSummary.CreationTime.ToString(Variables.FullDateTimePattern);
                                Itemdetail.科室建议 = customerDepSummary.DagnosisSummary;
                                Itemdetail.科室小结 = customerDepSummary.DagnosisSummary;
                                if (customerDepSummary.ExamineEmployeeBM != null)
                                {
                                    Itemdetail.科室检查医生 = customerDepSummary.ExamineEmployeeBM.Name;
                                    Itemdetail.科室审核医生 = customerDepSummary.ExamineEmployeeBM.Name;
                                    //if (customerDepSummary.ExamineEmployeeBM.SignImage != null)
                                    //{
                                    //    var result1 = _pictureController.GetUrl(customerDepSummary.ExamineEmployeeBM.SignImage.Value);
                                    //    Itemdetail.科室检查医生签名 = result1.RelativePath;
                                    //    Itemdetail.科室审核医生签名 = result1.RelativePath;
                                    //}

                                    if (customerDepSummary.ExamineEmployeeBM?.SignImage != null)
                                    {
                                        var result1 = pictureDtos.FirstOrDefault(o => o.Id == customerDepSummary.ExamineEmployeeBM.SignImage.Value);
                                        if (result1 != null)
                                        {
                                            Itemdetail.科室检查医生签名 = result1.RelativePath;
                                            Itemdetail.科室审核医生签名 = result1.RelativePath;
                                        }
                                    }


                                }

                            }
                        }
                    }
                    else
                    {
                        Itemdetail.组合名称 = item.ItemGroupBM?.ItemGroupName;
                        if (!item.DepartmentBM.Category.Contains("检验"))
                        {
                            var grouplist = vlsitem.Where(o => o.ItemId == item.ItemId && o.CustomerRegId==item.CustomerRegId && o.ProcessState != (int)ProjectIState.GiveUp).ToList();
                            if (grouplist.Count > 1)
                            {

                                Itemdetail.组合名称 = string.Join(",", grouplist.Select(o => o.ItemGroupBM?.ItemGroupName).ToList());
                            }
                        }
                        Itemdetail.组合说明 = item.ItemGroupBM?.Remarks;
                        if (customerItemGroupPrintViewDto.CustomerRegBMId != null && customerItemGroupPrintViewDto.InspectEmployeeBM != null)
                        {
                            Itemdetail.组合小结 = customerItemGroupPrintViewDto.ItemGroupSum;
                            Itemdetail.组合原生小结 = customerItemGroupPrintViewDto.ItemGroupOriginalDiag?.Replace("&", "");
                            Itemdetail.组合检查医生 = customerItemGroupPrintViewDto.InspectEmployeeBM?.Name.ToString();
                            Itemdetail.组合审核医生 = customerItemGroupPrintViewDto.CheckEmployeeBM?.Name.ToString();
                            //if (customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage != null)
                            //{
                            //    var result1 = _pictureController.GetUrl(customerItemGroupPrintViewDto.InspectEmployeeBM.SignImage.Value);
                            //    Itemdetail.组合检查医生签名 = result1.RelativePath;
                            //}
                            //if (customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage != null)
                            //{
                            //    var result2 = _pictureController.GetUrl(customerItemGroupPrintViewDto.CheckEmployeeBM.SignImage.Value);
                            //    Itemdetail.组合审核医生签名 = result2.RelativePath;
                            //}
                            if (customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage != null)
                            {
                                var result1 = pictureDtos.FirstOrDefault(o => o.Id == customerItemGroupPrintViewDto.InspectEmployeeBM?.SignImage.Value);
                                if (result1 != null)
                                {
                                    Itemdetail.组合检查医生签名 = result1.RelativePath;
                                }
                            }
                            if (customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage != null)
                            {
                                var result2 = pictureDtos.FirstOrDefault(o => o.Id == customerItemGroupPrintViewDto.CheckEmployeeBM?.SignImage.Value);
                                if (result2 != null)
                                {
                                    Itemdetail.组合审核医生签名 = result2.RelativePath;
                                }
                            }

                            if (customerItemGroupPrintViewDto.BillingEmployeeBM != null)
                            {
                                Itemdetail.组合开单医生 = customerItemGroupPrintViewDto.BillingEmployeeBM.Name.ToString();
                            }
                            Itemdetail.组合检查时间 = customerItemGroupPrintViewDto.FirstDateTime?.Date.ToString("yyyy-MM-dd");
                        }
                        Itemdetail.项目名称 = item?.ItemName;
                        if (item.ProcessState != null)
                        {
                            Itemdetail.项目状态 = ProjectIStateHelper.ProjectIStateFormatter(item?.ProcessState.Value.ToString());
                        }
                        Itemdetail.项目单位 = item?.Unit;
                        if (string.IsNullOrEmpty(item.Unit))
                        {
                            var iteminfo = from c in itemInfoSimpleDtosall where c.Id == item.ItemId select c;
                            if (iteminfo.Count() > 0)
                            {
                                Itemdetail.项目单位 = iteminfo?.ToList()[0].Unit;
                            }
                        }
                        if (item.Stand != null)
                        {
                            item.Stand = item.Stand.Replace(";", "\r\n");
                        }

                        Itemdetail.参考值 = item.Stand;
                        //if (string.IsNullOrEmpty(item.Stand))
                        //{
                        //    var itemstand = from c in lstItemStandardDto where c.ItemId == item.ItemId select c;
                        //    if (itemstand.Count() > 0)
                        //    {
                        //        var itemstandfrist = itemstand.ToList()[0];
                        //        if (string.IsNullOrEmpty(itemstandfrist.Symbol))
                        //        {
                        //            Itemdetail.参考值 = itemstandfrist.Symbol;
                        //        }
                        //        else
                        //        {
                        //            Itemdetail.参考值 = itemstandfrist.MinValue + "-" + itemstandfrist.MaxValue;
                        //        }
                        //    }
                        //}
                        if (item.ItemTypeBM.HasValue)
                        {
                            Itemdetail.项目类别 = ItemTypeHelper.ItemTypeFormatter(item.ItemTypeBM);
                        }
                        if (item.ItemOrder.HasValue)
                        {
                            Itemdetail.项目序号 = item.ItemOrder.Value.ToString();
                        }
                        string strRe = item.ItemResultChar;

                        //string strRexmbs = item.Symbol;
                        if (strtype.Contains("检查") || strtype == "检验")
                        {
                            if (!string.IsNullOrEmpty(item.Symbol) && item.Symbol != "M")
                            {
                                if (strReB == "Y")
                                {
                                    strRe = "<b>" + strRe.TrimEnd() + "</b>";
                                    //strRexmbs = "<b>" + strRexmbs + "</b>";
                                }
                                if (strReU == "Y")
                                {
                                    strRe = "<u>" + strRe.TrimEnd() + "</u>";
                                    //strRexmbs = "<u>" + strRexmbs + "</u>";
                                }
                                if (strReC == "Y")
                                {
                                    strRe = "<font color=" + strReCZ + ">" + strRe.TrimEnd() + "</font>";
                                    //strRexmbs = "<font color=\"" + strRexmbs + "\">" + strRe + "</font>";
                                }
                            }
                        }
                        switch (item.Symbol)
                        {
                            case "H":
                                Itemdetail.项目标示 = "↑";
                                break;
                            case "HH":
                                Itemdetail.项目标示 = "↑↑";
                                break;
                            case "L":
                                Itemdetail.项目标示 = "↓";
                                break;
                            case "LL":
                                Itemdetail.项目标示 = "↓↓";
                                break;
                        }
                        //gridpp.FieldByName("项目标示").AsString = strRexmbs;
                        Itemdetail.项目结果 = strRe;
                        if (item.PositiveSate != null)
                        {
                            Itemdetail.阳性状态 = PositiveStateHelper.PositiveStateFormatter(item.PositiveSate);
                        }
                        if (item.IllnessSate != null)
                        {
                            Itemdetail.疾病状态 = IllnessSateHelp.PositiveStateFormatter(item.IllnessSate.Value.ToString());
                        }
                        if (item.CrisisSate != null)
                        {
                            Itemdetail.危急值状态 = CrisisSateHelper.CrisisSateFormatter(item.CrisisSate);
                        }
                        Itemdetail.仪器编号 = item.Instrument;
                        if (item.LastModificationTime != null)
                        {
                            Itemdetail.项目时间 = item.LastModificationTime.Value.ToString(Variables.FullDateTimePattern);
                        }
                        if (item.CheckEmployeeBM != null)
                        {
                            Itemdetail.项目审核医生 = item.CheckEmployeeBM.Name;
                        }
                        if (!string.IsNullOrEmpty(strsj))
                        {
                            Itemdetail.检查所见 = strsj;
                        }
                        if (!string.IsNullOrEmpty(strzd))
                        {
                            Itemdetail.检查诊断 = strzd;
                        }
                        if (itemimg != null && (strtype == "图像" || strtype.Contains( "一张图片")))
                        {
                            if (strtype.Contains("一张图片"))
                            {
                                //去掉旋转
                                if (itemimg.Width > itemimg.Height)
                                    itemimg.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            }

                            Itemdetail.图片1 = ImageHelper.GetByteImage(itemimg);
                        }
                        if (lstStrWMF != null && lstStrWMF.Count > 0)
                        {
                            if (lstStrWMF.Count >= (i + 1))
                            {
                                Itemdetail.图片路径 = lstStrWMF[i];
                            }
                        }
                        Itemdetail.项目编码 = item.ItemCodeBM;
                        if (item.CheckEmployeeBM != null)
                        {
                            Itemdetail.项目审核医生 = item.CheckEmployeeBM.Name;
                        }
                        if (customerDepSummary.CustomerRegId != null)
                        {
                            Itemdetail.科室时间 = customerDepSummary.CreationTime.ToString(Variables.FullDateTimePattern);
                            Itemdetail.科室建议 = customerDepSummary.DagnosisSummary;
                            Itemdetail.科室小结 = customerDepSummary.DagnosisSummary;
                            if (customerDepSummary.ExamineEmployeeBM != null)
                            {
                                Itemdetail.科室检查医生 = customerDepSummary.ExamineEmployeeBM.Name;
                                Itemdetail.科室审核医生 = customerDepSummary.ExamineEmployeeBM.Name;
                                //if (customerDepSummary.ExamineEmployeeBM.SignImage != null)
                                //{
                                //    var result1 = _pictureController.GetUrl(customerDepSummary.ExamineEmployeeBM.SignImage.Value);
                                //    Itemdetail.科室检查医生签名 = result1.RelativePath;
                                //    Itemdetail.科室审核医生签名 = result1.RelativePath;
                                //}

                                if (customerDepSummary.ExamineEmployeeBM?.SignImage != null)
                                {
                                    var result1 = pictureDtos.FirstOrDefault(o => o.Id == customerDepSummary.ExamineEmployeeBM.SignImage.Value);
                                    if (result1 != null)
                                    {
                                        Itemdetail.科室检查医生签名 = result1.RelativePath;
                                        Itemdetail.科室审核医生签名 = result1.RelativePath;
                                    }
                                }


                            }

                        }
                    }
                    //相同报告单合并组合名称
                    if (!string.IsNullOrEmpty(customerItemGroupPrintViewDto.ReportBM))
                    {
                        var grouplist = lstCustomerItemGroupPrintViewDto.Where(o => o.ReportBM == customerItemGroupPrintViewDto.ReportBM && o.CheckState != (int)ProjectIState.GiveUp).ToList();
                        if (grouplist.Count > 1)
                        {

                            Itemdetail.组合名称 = string.Join(",", grouplist.Select(o => o.ItemGroupName).ToList());
                        }

                    }
                    // gridpp.DetailGrid.Recordset.Post();
                    reportJson.groupEmdical.Add(Itemdetail);
                    i = i + 1;
                    if (itemimg != null)
                    {
                        itemimg.Dispose();
                    }
                }

            }
            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
            gridpp.LoadDataFromXML(reportJsonForExamineString);


        }
        private DataTable GetDateTable()//生成通用存放数据的Table
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PostState");
            dt.Columns.Add("体检号");
            dt.Columns.Add("姓名");
            dt.Columns.Add("性别");
            dt.Columns.Add("年龄");
            dt.Columns.Add("电话");
            dt.Columns.Add("单位");
            dt.Columns.Add("手机号");
            dt.Columns.Add("身份证号");
            dt.Columns.Add("出生日期");
            dt.Columns.Add("套餐");
            dt.Columns.Add("备注");
            dt.Columns.Add("地址");
            dt.Columns.Add("照片");
            dt.Columns.Add("体检类别");
            dt.Columns.Add("体检日期");
            dt.Columns.Add("总检汇总");
            dt.Columns.Add("总检建议");
            dt.Columns.Add("诊断");
            dt.Columns.Add("总检医生");
            dt.Columns.Add("审核医生");
            dt.Columns.Add("总检时间");
            dt.Columns.Add("民族");
            dt.Columns.Add("部门");
            dt.Columns.Add("国籍");
            dt.Columns.Add("婚姻状态");

            dt.Columns.Add("检查组合");

            dt.Columns.Add("复查项目");
            dt.Columns.Add("职业病结论");
            dt.Columns.Add("职业病结论描述");
            dt.Columns.Add("疑似职业病");
            dt.Columns.Add("职业病禁忌证");
            dt.Columns.Add("处理意见");
            dt.Columns.Add("结论依据");
            dt.Columns.Add("医学建议");
            dt.Columns.Add("健康卡编号");
            dt.Columns.Add("合格证编号");
            dt.Columns.Add("总检医生签名");
            dt.Columns.Add("审核医生签名");
            return dt;
        }
        /// <summary>
        /// 表格体检
        /// </summary>
        /// <param name="gridpp"></param>
        /// <param name="department"></param>
        /// <param name="lstCustomerDtos"></param>
        private void bindBG( GridppReport gridpp, string department, CustomerRegDto lstCustomerDtos)
        {

           
            var lststr = department.ToString().Split(',').ToList();
            var vlsitem = from c in lstCustomerRegItemReoprtDto where lststr.Contains(c.DepartmentBM.Name) select c;
            string strfqshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 120).Remarks;
            if (strfqshow == "N" )
            {
                vlsitem = vlsitem.Where(o => o.ProcessState != (int)ProjectIState.GiveUp).ToList();
                
            }
            if (vlsitem.ToList().Count <= 0)
            {
                try
                {
                    ReportMain.ControlByName("表格").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("表格" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }

            }
            DataTable dataTable = GetDateTable();
            DataRow dataRow = dataTable.NewRow();
            dataRow["健康卡编号"]  = lstCustomerDtos.JKZBM ?? "";
            dataRow["合格证编号"] = lstCustomerDtos.HGZBH ?? "";             
            dataRow["体检号"] = lstCustomerDtos.CustomerBM;
            dataRow["姓名"] = lstCustomerDtos.Customer.Name;
            dataRow["性别"] = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex).Replace("性", "");
            dataRow["年龄"] = lstCustomerDtos.Customer.Age;
            dataRow["电话"] = lstCustomerDtos.Customer.Mobile;
            dataRow["民族"] = lstCustomerDtos.Customer.Nation;
            dataRow["部门"] = lstCustomerDtos.Customer.Department;
            dataRow["出生日期"] = lstCustomerDtos.Customer.Birthday;
            dataRow["照片"] = url?.RelativePath;
            dataRow["单位"] = lstCustomerDtos.ClientReg?.ClientInfo.ClientName;
            dataRow["身份证号"] = lstCustomerDtos.Customer.IDCardNo;
            dataRow["套餐"] = lstCustomerDtos.ItemSuitName;
            dataRow["备注"] = lstCustomerDtos.Customer.Remarks;
            dataRow["地址"] = lstCustomerDtos.Customer.Address;
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            dataRow["体检类别"] = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;
            //增加体检类别字段
            if (lstCustomerDtos.PhysicalType.HasValue && !string.IsNullOrEmpty(dataRow["体检类别"].ToString()))
            {
                foreach (var tjlb in Clientcontract)
                {
                    if (!dataTable.Columns.Contains(tjlb.Text))
                    {
                        dataTable.Columns.Add(tjlb.Text);

                    }                   
                }
                dataRow[dataRow["体检类别"].ToString()] = "√";
            }
            dataRow["体检日期"] = lstCustomerDtos.LoginDate;
            dataRow["总检汇总"] = tjlCustomerSummarizeDto?.CharacterSummary;
            var adviceform = FormatAdvice();
            dataRow["总检建议"] = adviceform.adviceContent;
            dataRow["诊断"] = adviceform.adviceName;
            //dataRow["婚姻状态"] = lstCustomerDtos.Customer.MarriageStatus;
            dataRow["婚姻状态"] = MarrySateHelper.CustomMarrySateFormatter(lstCustomerDtos.Customer.MarriageStatus);
            dataRow["PostState"] = lstCustomerDtos.PostState;
            dataRow["总检医生"] = tjlCustomerSummarizeDto?.EmployeeBM?.Name;
            dataRow["审核医生"] = tjlCustomerSummarizeDto?.ShEmployeeBM?.Name;
            if (tjlCustomerSummarizeDto != null)
            {
                if (tjlCustomerSummarizeDto.EmployeeBM?.SignImage != null)
                {

                    //var result1 = _pictureController.GetUrlUser(tjlCustomerSummarizeDto.EmployeeBM.SignImage.Value);
                    //修改签名0308
                    var result1 = pictureDtos.FirstOrDefault(o => o.Id == tjlCustomerSummarizeDto.EmployeeBM.SignImage.Value);
                    if (result1!=null)
                    {
                        dataRow["总检医生签名"] = result1.RelativePath;
                    }
                }
                if (tjlCustomerSummarizeDto.ShEmployeeBM?.SignImage != null)
                {

                   // var result2 = _pictureController.GetUrlUser(tjlCustomerSummarizeDto.ShEmployeeBM.SignImage.Value);
                    //修改签名0308
                    var result2 = pictureDtos.FirstOrDefault(o => o.Id == tjlCustomerSummarizeDto.ShEmployeeBM.SignImage.Value);
                    if (result2 != null)
                    {
                        dataRow["审核医生签名"] = result2.RelativePath;
                    }
                }

            }

            var Group = lstCustomerRegItemReoprtDto.Select(p=>p.ItemGroupBM?.ItemGroupName).Distinct().ToList();
            dataRow["检查组合"] = string.Join("、", Group);
            if (tjlCustomerSummarizeDto!=null &&  tjlCustomerSummarizeDto.ConclusionDate.HasValue)
            {
                dataRow["总检时间"] = tjlCustomerSummarizeDto?.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
            }
            #region 总检相关

            if (tjlCustomerSummarizeDto != null)
            {

                dataRow["复查项目"] = tjlCustomerSummarizeDto.ReviewContent;
                if (string.IsNullOrEmpty(tjlCustomerSummarizeDto.ReviewContent))
                {
                    dataRow["复查项目"] = "无";
                }
                if (inputOccCusSumDto != null && inputOccCusSumDto.OccCustomerHazardSumDto != null && inputOccCusSumDto.OccCustomerHazardSumDto.Count>0)
                {
                  
                     dataRow["职业病结论"] =string.Join("\r\n", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p=> p.OccHazardFactors==null ? p.Conclusion: p.OccHazardFactors.Text +":"+p.Conclusion).ToList());
                   
                    dataRow["职业病结论描述"] = string.Join("\r\n", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p => p.OccHazardFactors == null ? p.Description : p.OccHazardFactors.Text + ":" + p.Description).ToList());

                    //var occdis = inputOccCusSumDto.CraetOccCustomerOccDiseasesDto?.Select(o => o.Text).ToList();
                    var occdisName = "无";
                    var   occContrName = "无";
                    foreach (var occ in inputOccCusSumDto.OccCustomerHazardSumDto)
                    {
                        if (occ.OccHazardFactors != null)
                        {
                            occdisName += occ.OccHazardFactors.Text + ":" + string.Join(",", occ.OccCustomerOccDiseases.Select(p => p.Text).ToList());
                            occContrName += occ.OccHazardFactors.Text + ":" + string.Join(",", occ.OccDictionarys.Select(p => p.Text).ToList());
                        }
                        else
                        {

                            occdisName +=   string.Join(",", occ.OccCustomerOccDiseases.Select(p => p.Text).ToList());
                            occContrName +=   string.Join(",", occ.OccDictionarys.Select(p => p.Text).ToList());
                        }
                    }
                 
                
                   dataRow["疑似职业病"] = occdisName;                  
                    
                    dataRow["职业病禁忌证"] = occContrName;
                    dataRow["处理意见"] = string.Join("\r\n", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p => p.OccHazardFactors == null ? p.Advise : p.OccHazardFactors.Text + ":" + p.Advise).ToList());  
                   // dataRow["结论依据"] = string.Join(";", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p => p.OccHazardFactors.Text + ":" + p.Standard).ToList()); //inputOccCusSumDto.CreatOccCustomerSumDto.Standard;
                    dataRow["医学建议"] = string.Join("\r\n", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p =>  p.MedicalAdvice).ToList()); //inputOccCusSumDto.CreatOccCustomerSumDto.MedicalAdvice;

                }
                
 
            }
            #endregion
            //检查结果         
            var reitemName = vlsitem.GroupBy(o => o.ItemName).Select(
                o => new { itemname = o.Key, count = o.Count() }).ToList();
            var reitems = reitemName.Where(p => p.count > 1).Select(o => o.itemname).ToList();
        

           
          //字典
            var QueryDict = new QueryClassTwo();
            QueryDict.DepartmentBMList = vlsitem.Select(p => p.DepartmentId).Distinct().ToList();
            var _itemDictionarySys = _doctorStation.GetItemDictionarylist(QueryDict);
            foreach (var cusitem in vlsitem)
            {
               
                #region 字典
                var conDictionname = "";
                if (reitems.Contains(cusitem.ItemName))
                {
                    conDictionname = cusitem.ItemGroupBM.ItemGroupName + cusitem.ItemName;
                }
                else
                {
                    conDictionname = cusitem.ItemName;
                }
                if (conDictionname.Contains("心电图"))
                {

                }
                if (!dataTable.Columns.Contains(conDictionname + "结果"))
                {
                    dataTable.Columns.Add(conDictionname + "结果");
                    dataRow[conDictionname + "结果"] = cusitem.ItemResultChar;



                 
                    dataTable.Columns.Add(conDictionname + "诊断");
                    dataTable.Columns.Add(conDictionname + "单位");
                    dataTable.Columns.Add(conDictionname + "参考值");
                    dataTable.Columns.Add(conDictionname + "标识");
               
                    dataRow[conDictionname + "诊断"] = cusitem.ItemDiagnosis;
                    dataRow[conDictionname + "单位"] = cusitem.Unit;
                    dataRow[conDictionname + "参考值"] = cusitem.Stand;
                    switch (cusitem.Symbol)
                    {
                        case "H":
                            dataRow[conDictionname + "标识"] = "↑";
                            break;
                        case "HH":
                            dataRow[conDictionname + "标识"] = "↑↑";
                            break;
                        case "L":
                            dataRow[conDictionname + "标识"] = "↓";
                            break;
                        case "LL":
                            dataRow[conDictionname + "标识"] = "↓↓";
                            break;
                    }
                }
                var nowiDiclist = _itemDictionarySys.Where(p => p.iteminfoBMId ==
                cusitem.ItemId).ToList();
                foreach (var dic in nowiDiclist)
                {
                    if (cusitem.ItemResultChar.Contains(dic.Word))
                    {
                        dataTable.Columns.Add(cusitem.ItemName + "_" + dic.Word);
                        dataRow[cusitem.ItemName + "_" + dic.Word] = "√";
                    }
                }
                #endregion
                //var conitemname = "";
                //if (reitems.Contains(cusitem.ItemName))
                //{
                //    conitemname = cusitem.ItemGroupBM.ItemGroupName + cusitem.ItemName;
                //}
                //else
                //{
                //    conitemname = cusitem.ItemName;
                //}
                //if (conitemname.Contains("心电图"))
                //{

                //}
                //if (!dataTable.Columns.Contains(conitemname + "结果"))
                //{

                //    dataTable.Columns.Add(conitemname + "结果");
                //    dataTable.Columns.Add(conitemname + "诊断");
                //    dataTable.Columns.Add(conitemname + "单位");
                //    dataTable.Columns.Add(conitemname + "参考值");
                //    dataTable.Columns.Add(conitemname + "标识");
                //    dataRow[conitemname + "结果"] = cusitem.ItemResultChar;
                //    dataRow[conitemname + "诊断"] = cusitem.ItemDiagnosis;
                //    dataRow[conitemname + "单位"] = cusitem.Unit;
                //    dataRow[conitemname + "参考值"] = cusitem.Stand;
                //    switch (cusitem.Symbol)
                //    {
                //        case "H":
                //            dataRow[conitemname + "标识"] = "↑";
                //            break;
                //        case "HH":
                //            dataRow[conitemname + "标识"] = "↑↑";
                //            break;
                //        case "L":
                //            dataRow[conitemname + "标识"] = "↓";
                //            break;
                //        case "LL":
                //            dataRow[conitemname + "标识"] = "↓↓";
                //            break;
                //    }

                //}
            }
            var departSum = lstCustomerDepSummaries.Where(p => lststr.Contains(p.DepartmentName)).ToList();
            //科室小结
            foreach (var cusDepart in departSum)
            {
               
                if (!dataTable.Columns.Contains(cusDepart.DepartmentName + "科室小结"))
                {
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室小结");
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室医生");
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室时间");
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室检查医生签名");
                    dataTable.Columns.Add(cusDepart.DepartmentName + "科室审核医生签名");
                    dataRow[cusDepart.DepartmentName + "科室小结"] = cusDepart.DagnosisSummary;
                    dataRow[cusDepart.DepartmentName + "科室医生"] = cusDepart.ExamineEmployeeBM?.Name;
                    dataRow[cusDepart.DepartmentName + "科室时间"] = cusDepart.CheckDate;

                    if (cusDepart.ExamineEmployeeBM?.SignImage != null)
                    {
                        var result1 = pictureDtos.FirstOrDefault(o => o.Id == cusDepart.ExamineEmployeeBM.SignImage.Value);
                        if (result1 != null)
                        {
                            dataRow[cusDepart.DepartmentName + "科室检查医生签名"] = result1.RelativePath;
                            dataRow[cusDepart.DepartmentName + "科室审核医生签名"] = result1.RelativePath;
                        }
                    }
                }
            }
            var GroupSum = lstCustomerItemGroupPrintViewDto.Where(p => lststr.Contains(p.DepartmentName)).ToList();
            //组合
            foreach (var cusGroup in GroupSum)
            {
                if (!dataTable.Columns.Contains(cusGroup.ItemGroupName + "组合小结"))
                {
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合小结");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合检查医生");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合审核医生");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合检查时间");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合检查医生签名");
                    dataTable.Columns.Add(cusGroup.ItemGroupName + "组合审核医生签名");
                    if (!string.IsNullOrEmpty(cusGroup.ItemGroupDiagnosis))
                    {
                        dataRow[cusGroup.ItemGroupName + "组合小结"] = cusGroup.ItemGroupDiagnosis;
                    }
                    else
                    { dataRow[cusGroup.ItemGroupName + "组合小结"] = cusGroup.ItemGroupSum; }
                    dataRow[cusGroup.ItemGroupName + "组合检查医生"] = cusGroup.CheckEmployeeBM?.Name;
                    dataRow[cusGroup.ItemGroupName + "组合审核医生"] = cusGroup.InspectEmployeeBM?.Name;
                    dataRow[cusGroup.ItemGroupName + "组合检查时间"] = cusGroup.FirstDateTime;
                    if (cusGroup.InspectEmployeeBM?.SignImage != null)
                    {
                        var result1 = pictureDtos.FirstOrDefault(o => o.Id == cusGroup.InspectEmployeeBM?.SignImage.Value);
                        if (result1 != null)
                        {
                            dataRow[cusGroup.ItemGroupName + "组合检查医生签名"] = result1.RelativePath;
                        }
                    }
                    if (cusGroup.CheckEmployeeBM?.SignImage != null)
                    {
                        var result2 = pictureDtos.FirstOrDefault(o => o.Id == cusGroup.CheckEmployeeBM?.SignImage.Value);
                        if (result2 != null)
                        {
                            dataRow[cusGroup.ItemGroupName + "组合审核医生签名"] = result2.RelativePath;
                        }
                    }
                }
            }

            dataTable.Rows.Add(dataRow);
            var reportJsonStringsy = JsonConvert.SerializeObject(dataTable);
            var reportJson = "{\"Detail\":" + reportJsonStringsy + "}";          
            gridpp.LoadDataFromXML(reportJson);
        }
        private void bindWJ(GridppReport gridpp, string department, CustomerRegDto lstCustomerDtos)
        {


            var lststr = department.ToString().Split(',').ToList();
            var vlsitem = from c in lstCustomerRegItemReoprtDto where lststr.Contains(c.DepartmentBM.Name) select c;
            string strfqshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 120).Remarks;
            if (strfqshow == "N")
            {
                vlsitem = vlsitem.Where(o => o.ProcessState != (int)ProjectIState.GiveUp).ToList();

            }
            if (vlsitem.ToList().Count <= 0)
            {
                try
                {
                    ReportMain.ControlByName("问卷").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("问卷" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }

            }
            DataTable dataTable = GetDateTable();
            DataRow dataRow = dataTable.NewRow();
            dataRow["体检号"] = lstCustomerDtos.CustomerBM;
            dataRow["姓名"] = lstCustomerDtos.Customer.Name;
            dataRow["性别"] = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex).Replace("性", "");
            dataRow["年龄"] = lstCustomerDtos.Customer.Age;
            dataRow["电话"] = lstCustomerDtos.Customer.Mobile;
            dataRow["民族"] = lstCustomerDtos.Customer.Nation;
            dataRow["部门"] = lstCustomerDtos.Customer.Department;
            dataRow["出生日期"] = lstCustomerDtos.Customer.Birthday;
            dataRow["照片"] = url?.RelativePath;
            dataRow["单位"] = lstCustomerDtos.ClientReg?.ClientInfo.ClientName;
            dataRow["身份证号"] = lstCustomerDtos.Customer.IDCardNo;
            dataRow["套餐"] = lstCustomerDtos.ItemSuitName;
            dataRow["备注"] = lstCustomerDtos.Customer.Remarks;
            dataRow["地址"] = lstCustomerDtos.Customer.Address;
            //dataRow["国籍"] = lstCustomerDtos.Customer.von;
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            dataRow["体检类别"] = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;
            dataRow["体检日期"] = lstCustomerDtos.LoginDate;
            dataRow["总检汇总"] = tjlCustomerSummarizeDto?.CharacterSummary;
            var adviceform = FormatAdvice();
            dataRow["总检建议"] = adviceform.adviceContent;
            dataRow["诊断"] = adviceform.adviceName;

            dataRow["总检医生"] = tjlCustomerSummarizeDto?.EmployeeBM?.Name;
            dataRow["审核医生"] = tjlCustomerSummarizeDto?.ShEmployeeBM?.Name;
            if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.ConclusionDate.HasValue)
            {
                dataRow["总检时间"] = tjlCustomerSummarizeDto?.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
            }

            //检查结果         
            var reitemName = vlsitem.GroupBy(o => o.ItemName).Select(
                o => new { itemname = o.Key, count = o.Count() }).ToList();
            var reitems = reitemName.Where(p => p.count > 1).Select(o => o.itemname).ToList();
            var QueryDict = new QueryClassTwo();
            QueryDict.DepartmentBMList = vlsitem.Select(p=>p.DepartmentId).Distinct().ToList();
           var  _itemDictionarySys = _doctorStation.GetItemDictionarylist(QueryDict);
            foreach (var cusitem in vlsitem)
            {

                var conitemname = "";
                if (reitems.Contains(cusitem.ItemName))
                {
                    conitemname = cusitem.ItemGroupBM.ItemGroupName + cusitem.ItemName;
                }
                else
                {
                    conitemname = cusitem.ItemName;
                }
                if (!dataTable.Columns.Contains(conitemname + "结果"))
                {
                    dataTable.Columns.Add(conitemname + "结果");
                    dataRow[conitemname + "结果"] = cusitem.ItemResultChar;
                }
                var nowiDiclist = _itemDictionarySys.Where(p=>p.iteminfoBMId==
                cusitem.ItemId).ToList();
                foreach (var dic in nowiDiclist)
                {
                    if (cusitem.ItemResultChar.Contains(dic.Word))
                    {
                        dataTable.Columns.Add(cusitem.ItemName+"_" + dic.Word);
                        dataRow[cusitem.ItemName + "_" + dic.Word] = "√";
                    }
                }
            }
           // var departSum = lstCustomerDepSummaries.Where(p => lststr.Contains(p.DepartmentName)).ToList();
           
           
            dataTable.Rows.Add(dataRow);
            var reportJsonStringsy = JsonConvert.SerializeObject(dataTable);
            var reportJson = "{\"Detail\":" + reportJsonStringsy + "}";
            gridpp.LoadDataFromXML(reportJson);
        }
        /// <summary>
        /// 微信问卷
        /// </summary>
        /// <param name="gridpp"></param>
        /// <param name="department"></param>
        /// <param name="lstCustomerDtos"></param>
        private void bindWXWJ(GridppReport gridpp, CustomerRegDto lstCustomerDtos)
        {

            var dataTable = _questionnaireAppService.QueryQuestionBomRecordByRegID(new EntityDto<Guid>(lstCustomerDtos.Id));


            if (dataTable.Count <= 0)
            {
                try
                {
                    ReportMain.ControlByName("微信问卷").AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader("微信问卷" + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }

            }
            else
            {
                var reportJsonStringsy = JsonConvert.SerializeObject(dataTable);
                var reportJson = "{\"Detail\":" + reportJsonStringsy + "}";
                gridpp.LoadDataFromXML(reportJson);
            }
           
        }
        private void bindRTT(GridppReport gridpp)
        {
            
            var vlsitem = lstCustomerRegItemReoprtDto.ToList();          
            vlsitem = vlsitem.Where(o => o.ProcessState != (int)ProjectIState.GiveUp).ToList();          
           
            DataTable dataTable = GetDateTable();
            DataRow dataRow = dataTable.NewRow();
            dataRow["体检号"] = lstCustomerDtos.CustomerBM;
            dataRow["姓名"] = lstCustomerDtos.Customer.Name;
            dataRow["性别"] = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex).Replace("性", "");
            dataRow["年龄"] = lstCustomerDtos.Customer.Age;
            dataRow["电话"] = lstCustomerDtos.Customer.Mobile;
            dataRow["民族"] = lstCustomerDtos.Customer.Nation;
            dataRow["部门"] = lstCustomerDtos.Customer.Department;
            dataRow["出生日期"] = lstCustomerDtos.Customer.Birthday;
            dataRow["照片"] = url?.RelativePath;
            dataRow["单位"] = lstCustomerDtos.ClientReg?.ClientInfo.ClientName;
            dataRow["身份证号"] = lstCustomerDtos.Customer.IDCardNo;
            dataRow["套餐"] = lstCustomerDtos.ItemSuitName;
            dataRow["备注"] = lstCustomerDtos.Customer.Remarks;
            dataRow["地址"] = lstCustomerDtos.Customer.Address;
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            dataRow["体检类别"] = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;
            dataRow["体检日期"] = lstCustomerDtos.LoginDate;
            dataRow["总检汇总"] = tjlCustomerSummarizeDto?.CharacterSummary;
            var adviceform = FormatAdvice();
            dataRow["总检建议"] = adviceform.adviceContent;
            dataRow["诊断"] = adviceform.adviceName;

            dataRow["总检医生"] = tjlCustomerSummarizeDto?.EmployeeBM?.Name;
            dataRow["审核医生"] = tjlCustomerSummarizeDto?.ShEmployeeBM?.Name;
            if (tjlCustomerSummarizeDto != null && tjlCustomerSummarizeDto.ConclusionDate.HasValue)
            {
                dataRow["总检时间"] = tjlCustomerSummarizeDto?.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
            }

            //检查结果         
         
            var bodys = DefinedCacheHelper.GetBasicDictionarys(BasicDictionaryType.Body );
            if (bodys != null && bodys.Count > 0)
            {
                foreach (var cusitem in bodys)
                {
                    dataTable.Columns.Add(cusitem.Text);
                    var groupNames = cusitem.Remarks.Split('|').ToList();
                    var IllNum = vlsitem.Where(p => p.ItemGroupBM!=null && groupNames.Contains(p.ItemGroupBM?.ItemGroupName)
                    && (p.Symbol.Contains("H")|| p.Symbol.Contains("L")|| p.Symbol.Contains("P"))).Count();
                    if (IllNum > 0)
                    {
                        dataRow[cusitem.Text] = "*";                        
                    }
                }
            }        

            dataTable.Rows.Add(dataRow);
            var reportJsonStringsy = JsonConvert.SerializeObject(dataTable);
            var reportJson = "{\"Detail\":" + reportJsonStringsy + "}";
            gridpp.LoadDataFromXML(reportJson);
        }
        #region 获取数据
        private void BindChildrenJsion(List<Application.HistoryComparison.Dto.SearchCustomerRegItemDto> HistoryCustomerRegItemDto, string strtype, GridppReport gridpp, int departmentnum, CustomerRegDto lstCustomerDtos)
        {
            string department = "";

            department = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, departmentnum).Remarks;


            List<Guid> lststr = new List<Guid>();
            foreach (string item in department.ToString().Split(','))
            {
                var Depats = DefinedCacheHelper.GetDepartments().Where(o => o.Name == item).ToList();

                if (Depats.Count > 0 && !lststr.Contains(Depats[0].Id))
                {
                    lststr.Add(Depats[0].Id);
                }
            }

            var vlsitem = from c in HistoryCustomerRegItemDto where lststr.Contains(c.DepartmentId) select c;
            if (vlsitem.ToList().Count <= 0)
            {
                try
                {
                    ReportMain.ControlByName(strtype).AsSubReport.Visible = false;
                    ReportMain.get_ReportHeader(strtype + "报表头").Visible = false;
                }
                catch (Exception)
                {
                }

            }
            var reportJson = new ReportJsonHistory();
            reportJson.Detail = new List<ItemEmdicalCertificate>();
            foreach (var item in vlsitem)
            {
                
                //获取科室小结
                var vardepsum = from p in lstCustomerDepSummaries where p.DepartmentBMId == item.DepartmentId select p;
                CustomerDepSummaryViewDto customerDepSummary = new CustomerDepSummaryViewDto();
                if (vardepsum.ToList().Count > 0)
                {
                    customerDepSummary = vardepsum.ToList()[0];
                }                
                //获取组合小结
                CustomerItemGroupPrintViewDto customerItemGroupPrintViewDto = new CustomerItemGroupPrintViewDto();
                var varItemGroupsum = from p in lstCustomerItemGroupPrintViewDto where p.Id == item.CustomerItemGroupBMid select p;
                if (varItemGroupsum.ToList().Count > 0)
                {
                    customerItemGroupPrintViewDto = varItemGroupsum.ToList()[0];
                }

                string strwjshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 110).Remarks;
                string strKJGshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 111).Remarks;
                string strfqshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 120).Remarks;
                string strdcshow = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 130).Remarks;
                //结果是否加粗
                string strReB = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 170).Remarks;
                //结果是否倾斜
                string strReU = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 180).Remarks;
                //结果是否颜色
                string strReC = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 190).Remarks;
                //结果颜色至
                string strReCZ = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 200).Remarks;
                if (strtype == "检查" || strtype == "检验")
                    if (strwjshow == "N" && item.ProcessState.Value == 1)
                    {
                        continue;
                    }

                if (strKJGshow == "N" && string.IsNullOrEmpty(item.ItemResultChar))
                {
                    continue;
                }
                if (strfqshow == "N" && item.ProcessState.Value == 4)
                {
                    continue;
                }
                if (strdcshow == "N" && item.ProcessState.Value == 5)
                {
                    continue;
                }
                #region 图像处理
                string strsj = "";
                string strzd = "";
                if (strtype == "图像")
                {
                    strsj = item.ItemResultChar;
                    strzd = item.ItemDiagnosis;
                }
                #endregion

                if (item.ItemResultChar == null)
                {
                    continue;
                }


                var Itemdetail = new ItemEmdicalCertificate();
                Itemdetail.CustomerBM = lstCustomerDtos.CustomerBM;
                Itemdetail.Name = lstCustomerDtos.Customer.Name;
                Itemdetail.Sex = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex);
                Itemdetail.Age = lstCustomerDtos.Customer.Age.ToString();
                Itemdetail.Mobile = lstCustomerDtos.Customer.Mobile;
                // Itemdetail.Photo = url.Thumbnail;

                Itemdetail.ChekDateYear = item.CustomerRegBM.LoginDate?.ToString("yyyy-MM-dd");
                if (lstCustomerDtos.Customer.Birthday.HasValue)
                {
                    Itemdetail.Birthday = lstCustomerDtos.Customer.Birthday.Value.ToString(Variables.ShortDatePattern);
                }
                Itemdetail.IDCardNo = lstCustomerDtos.Customer.IDCardNo;
                Itemdetail.ItemSuitName = lstCustomerDtos.ItemSuitName;
                var depart = DefinedCacheHelper.GetDepartments().Where(o => o.Id == item.DepartmentId).ToList();
                if (depart.Count > 0)
                {
                    if (depart[0].Name.Contains("彩超"))
                    {
                    }
                    Itemdetail.DepartmentName = depart[0].Name;
                }
                var group = DefinedCacheHelper.GetItemGroups().Where(o => o.Id == item.ItemGroupBMId).ToList();
                if (group.Count > 0)
                {
                    Itemdetail.ItemGroupName = group[0].ItemGroupName;
                    Itemdetail.ItemGroupDiagnosisText = group[0].Remarks;
                }
                if (customerItemGroupPrintViewDto.CustomerRegBMId != null && customerItemGroupPrintViewDto.InspectEmployeeBM != null)
                {
                    Itemdetail.ItemGroupSum = customerItemGroupPrintViewDto.ItemGroupSum;
                    Itemdetail.InspectEmployeeBM = customerItemGroupPrintViewDto.InspectEmployeeBM.Name.ToString();
                    Itemdetail.CheckEmployeeBM = customerItemGroupPrintViewDto.CheckEmployeeBM.Name.ToString();
                    if (customerItemGroupPrintViewDto.BillingEmployeeBM != null)
                    {
                        Itemdetail.BillingEmployeeBM = customerItemGroupPrintViewDto.BillingEmployeeBM.Name.ToString();

                    }
                    Itemdetail.CheckDateTime = customerItemGroupPrintViewDto.FirstDateTime?.Date.ToString("yyyy-MM-dd");

                }
                Itemdetail.ItemName = item?.ItemName;

                if (item.ProcessState != null)
                {
                    Itemdetail.ItemState = ProjectIStateHelper.ProjectIStateFormatter(item?.ProcessState.Value.ToString());

                }
                Itemdetail.Unit = item?.Unit;

                if (string.IsNullOrEmpty(item.Unit))
                {
                    var iteminfo = from c in itemInfoSimpleDtosall where c.Id == item.ItemId select c;
                    if (iteminfo.Count() > 0)
                    {
                        Itemdetail.Unit = iteminfo?.ToList()[0].Unit;
                    }
                }
                if (item.Stand != null)
                {
                    item.Stand = item.Stand.Replace(";", "\r\n");
                }
                Itemdetail.Stand = item.Stand;

                //if (string.IsNullOrEmpty(item.Stand))
                //{
                //    var itemstand = from c in lstItemStandardDto where c.ItemId == item.ItemId select c;
                //    if (itemstand.Count() > 0)
                //    {
                //        var itemstandfrist = itemstand.ToList()[0];
                //        if (string.IsNullOrEmpty(itemstandfrist.Symbol))
                //        {
                //            Itemdetail.Stand = itemstandfrist.Symbol;

                //        }
                //        else
                //        {
                //            Itemdetail.Stand = itemstandfrist.MinValue + "-" + itemstandfrist.MaxValue;

                //        }
                //    }
                //}
                Itemdetail.ItemOrder = item.ItemOrder.Value.ToString();


                string strRe = item.ItemResultChar;
                //string strRexmbs = item.Symbol;
                if (strtype.Contains("检查") || strtype == "检验")
                {
                    if (!string.IsNullOrEmpty(item.Symbol) && item.Symbol != "M")
                    {
                        //if (strReB == "Y")
                        //{
                        //    strRe = "<b>" + strRe + "</b>";
                        //    //strRexmbs = "<b>" + strRexmbs + "</b>";
                        //}
                        //if (strReU == "Y")
                        //{
                        //    strRe = "<u>" + strRe + "</u>";
                        //    //strRexmbs = "<u>" + strRexmbs + "</u>";
                        //}
                        //if (strReC == "Y")
                        //{
                        //    strRe = "<font color=\"" + strReCZ + "\">" + strRe + "</font>";
                        //    //strRexmbs = "<font color=\"" + strRexmbs + "\">" + strRe + "</font>";
                        //}



                        if (strReB == "Y")
                        {
                            strRe = "<b>" + strRe.TrimEnd() + "</b>";
                            //strRexmbs = "<b>" + strRexmbs + "</b>";
                        }
                        if (strReU == "Y")
                        {
                            strRe = "<u>" + strRe.TrimEnd() + "</u>";
                            //strRexmbs = "<u>" + strRexmbs + "</u>";
                        }
                        if (strReC == "Y")
                        {
                            strRe = "<font color=" + strReCZ + ">" + strRe.TrimEnd() + "</font>";
                            //strRexmbs = "<font color=\"" + strRexmbs + "\">" + strRe + "</font>";
                        }
                    }
                }
                switch (item.Symbol)
                {
                    case "H":
                        Itemdetail.Itemfalg = "↑";

                        break;
                    case "HH":
                        Itemdetail.Itemfalg = "↑↑";
                        break;
                    case "L":
                        Itemdetail.Itemfalg = "↓";
                        break;
                    case "LL":
                        Itemdetail.Itemfalg = "↓↓";
                        break;
                }
                //gridpp.FieldByName("项目标示").AsString = strRexmbs;
                Itemdetail.ItemResult = strRe;

                if (item.PositiveSate != null)
                {
                    Itemdetail.PositiveState = PositiveStateHelper.PositiveStateFormatter(item.PositiveSate);

                }
                if (item.IllnessSate != null)
                {
                    Itemdetail.IllState = IllnessSateHelp.PositiveStateFormatter(item.IllnessSate.Value.ToString());

                }
                if (item.CrisisSate != null)
                {
                    Itemdetail.CrisisState = CrisisSateHelper.CrisisSateFormatter(item.CrisisSate);

                }
                Itemdetail.Instrument = item.Instrument;


                if (!string.IsNullOrEmpty(strsj))
                {

                    Itemdetail.Summ = strsj;
                }
                if (!string.IsNullOrEmpty(strzd))
                {

                    Itemdetail.diagnosis = strzd;
                }

                Itemdetail.ItemCodeBM = item.ItemCodeBM;
                if (customerDepSummary.CustomerRegId != null)
                {
                    Itemdetail.DepartTime = customerDepSummary.CreationTime.ToString(Variables.FullDateTimePattern);
                    Itemdetail.DepartAdvice = customerDepSummary.DagnosisSummary;
                    Itemdetail.DepartSum = customerDepSummary.DagnosisSummary;

                    if (customerDepSummary.ExamineEmployeeBM != null)
                    {
                        Itemdetail.DepartCheckEmp = customerDepSummary.ExamineEmployeeBM.Name;
                        Itemdetail.DepartInspectEmp = customerDepSummary.ExamineEmployeeBM.Name;

                    }

                }
                reportJson.Detail.Add(Itemdetail);
            }
            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
            gridpp.LoadDataFromXML(reportJsonForExamineString);


        }
        #endregion
        #region 获取图片
        private List<Image> getImage(TjlCustomerRegItemReoprtDto customerRegItemReoprtDto, string strImgType)
        {
            List<Image> imagepic = null;
            lstStrWMF = null;

            if (lstcustomerItemPicDtos.Count > 0)
            {
                var lstpic = from c in lstcustomerItemPicDtos where c.ItemBMID == customerRegItemReoprtDto.Id select c;
                List<Image> lststrimg = new List<Image>();
                List<string> PicPath = new List<string>();
               //var listBM = lstpic.Select(p=>p.PictureBM).Distinct().ToList();
                foreach (var item in lstpic)
                {
                    var result1 = _pictureController.GetUrl(item.PictureBM.Value);
                    if (!PicPath.Contains(result1.RelativePath))
                    { PicPath.Add(result1.RelativePath); }
                    else
                    {
                        continue;
                    }
                    if (result1.RelativePath.Contains(".pdf"))
                    {
                        
                        string strnew = System.AppDomain.CurrentDomain.BaseDirectory +
                    "image";
                        if (!Directory.Exists(strnew))
                        {
                            Directory.CreateDirectory(strnew);
                        }
                        strnew = strnew + "\\" + Path.GetFileNameWithoutExtension(result1.RelativePath) + ".pdf";
                        if (!File.Exists(strnew))
                        {
                            HttpDldFile.Download(result1.RelativePath, strnew);
                        }
                        if (lstStrPdf == null)
                        {
                            lstStrPdf = new List<string>();
                        }
                        lstStrPdf.Add(strnew);
                        result1 = null;
                    }
                    else if (result1.RelativePath.Contains("wmf"))
                    {
                        // HttpDldFile.Download(result1.RelativePath, strnew);
                        if (lstStrWMF == null)
                        {
                            lstStrWMF = new List<string>();
                        }
                        lstStrWMF.Add(result1.RelativePath);
                    }
                    else
                    {
                        if (strImgType == "图像")
                        {
                            if (ImageHelper.TryGetUriImage(new Uri(result1.RelativePath), out var image))
                            {
                                lststrimg.Add(image);
                            }
                        }
                        else
                        {
                            if (lstStrWMF == null)
                            {
                                lstStrWMF = new List<string>();
                            }
                            lstStrWMF.Add(result1.RelativePath);
                            if (ImageHelper.TryGetUriImage(new Uri(result1.RelativePath), out var image))
                            {
                                var mageall = image;
                                //缩小图片
                                if (image.Width > 1654)
                                {
                                      mageall = ImageHelper.ChangePicSize(image, 1487);
                                }
                                lststrimg.Add(mageall);
                            }
                            else
                            {
                                Image nimage = null;
                                lststrimg.Add(nimage);
                            }
                        }
                    }

                }
                if (lststrimg.Count == 1)
                {
                    imagepic = lststrimg;
                }
                else if (lststrimg.Count == 0)
                {
                    imagepic = null;
                }
                else
                {
                    if (strImgType == "图像")
                    {
                        string strw = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 140).Remarks;
                        string strh = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 150).Remarks;
                        int iw = 578;
                        int ih = 455;
                        if (!string.IsNullOrEmpty(strw))
                        {
                            iw = Convert.ToInt16(strw);
                        }
                        if (!string.IsNullOrEmpty(strh))
                        {
                            ih = Convert.ToInt16(strh);
                        }
                        if (lststrimg.Count == 2)
                        {
                            ih = ih / 2;
                        }
                        Image mageall = ImageHelper.DrawImageReport(iw, ih, lststrimg.ToArray());
                        List<Image> images = new List<Image>();
                        images.Add(mageall);

                        imagepic = images;
                        foreach (var iamgels in lststrimg)
                        {if (iamgels != null)
                            {
                                iamgels.Dispose();
                            }
                        }
                        
                    }
                    else
                    {
                        imagepic = lststrimg;
                    }
                }
            }
            return imagepic;
        }
        #endregion

        private void BindZJReport(TjlCustomerSummarizeDto CustomerSummarizeDto, string strtype, GridppReport gridpp, CustomerRegDto lstCustomerDtos ,bool isReview, reportCusReSumDto reportCusReSum)
        {

            var reportJson = new ReportJsonEmdicalCertificate();
            reportJson.groupEmdical = new List<rptItemGroup>();
            var reportcusinfo = new rptItemGroup();
            reportcusinfo.体检号 = lstCustomerDtos.CustomerBM;
            reportcusinfo.姓名 = lstCustomerDtos.Customer.Name;
            reportcusinfo.健康卡编号 = lstCustomerDtos.JKZBM ?? "";
            reportcusinfo.PostState = lstCustomerDtos.PostState;
            if (lstCustomerDtos.LoginDate.HasValue)
            {
                var date = lstCustomerDtos.LoginDate.Value.AddYears(1);
                reportcusinfo.截止日期 = date.ToString("yyyy-MM-dd");
                reportcusinfo.截止年 = date.Year.ToString();
                reportcusinfo.截止月 = date.Month.ToString();
                reportcusinfo.截止日 = date.Day.ToString();
            }
            string strsex = SexHelper.CustomSexFormatter(lstCustomerDtos.Customer.Sex);
            if (lstCustomerDtos.Customer.Sex == (int)Sex.Man)
                strsex = "先生";
            else
                strsex = "女士";
            reportcusinfo.性别 = strsex;

            reportcusinfo.年龄 = lstCustomerDtos.Customer.Age.ToString();
            reportcusinfo.手机号 = lstCustomerDtos.Customer.Mobile;
            reportcusinfo.个人照片 = lstCustomerDtos.Customer.CusPhotoBM;

            if (lstCustomerDtos.Customer.Birthday.HasValue)
            {

                reportcusinfo.出生日期 = lstCustomerDtos.Customer.Birthday.Value.ToString(Variables.ShortDatePattern);
            }
            reportcusinfo.身份证号 = lstCustomerDtos.Customer.IDCardNo;
            reportcusinfo.婚姻状态 = MarrySateHelper.CustomMarrySateFormatter(lstCustomerDtos.Customer.MarriageStatus);

            if (lstCustomerDtos.CustomerRegNum.HasValue)
            {
                reportcusinfo.登记号 = lstCustomerDtos.CustomerRegNum.Value.ToString();
            }
            reportcusinfo.登记日期 = lstCustomerDtos.LoginDate.ToString();
            if (lstCustomerDtos.BookingDate.HasValue)
            {
                reportcusinfo.体检日期 = lstCustomerDtos.BookingDate.Value.ToString(Variables.ShortDatePattern);
            }
            reportcusinfo.套餐名称 = lstCustomerDtos.ItemSuitName;

            if (lstCustomerDtos.ClientReg != null)
            {
                reportcusinfo.单位名称 = lstCustomerDtos.ClientReg.ClientInfo.ClientName;
                reportcusinfo.单位开始时间 = lstCustomerDtos.ClientReg.StartCheckDate.ToString(Variables.ShortDatePattern);

                reportcusinfo.单位结束时间 = lstCustomerDtos.ClientReg.EndCheckDate.ToString(Variables.ShortDatePattern);

            }
            else
            {
                reportcusinfo.单位名称 = "个人" + lstCustomerDtos.PersonnelCategory?.Name;

            }
            var adviceform = FormatAdvice();
            reportcusinfo.总检建议 = adviceform.adviceContent;
            reportcusinfo.诊断结论 = adviceform.adviceName;
            if (CustomerSummarizeDto != null && CustomerSummarizeDto.ConclusionDate.HasValue)
            {
                reportcusinfo.总检日期 = CustomerSummarizeDto.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
                reportcusinfo.汇总日期 = CustomerSummarizeDto.ConclusionDate.Value.ToString(Variables.ShortDatePattern);
            }
            if (CustomerSummarizeDto != null && CustomerSummarizeDto.ExamineDate.HasValue)
            {
                reportcusinfo.汇总日期 = CustomerSummarizeDto.ExamineDate.Value.ToString(Variables.ShortDatePattern);
            }
            
            reportcusinfo.RiskS = lstCustomerDtos.RiskS;
            reportcusinfo.InjuryAge = lstCustomerDtos.InjuryAge;
            reportcusinfo.InjuryAgeUnit = lstCustomerDtos.InjuryAgeUnit;
            reportcusinfo.TotalWorkAge = lstCustomerDtos.TotalWorkAge;
            reportcusinfo.WorkAgeUnit = lstCustomerDtos.WorkAgeUnit;

            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            reportcusinfo.体检类别 = Clientcontract.FirstOrDefault(o => o.Value == lstCustomerDtos.PhysicalType)?.Text;


            if (CustomerSummarizeDto != null)
            {
                reportcusinfo.总检汇总 = CustomerSummarizeDto.CharacterSummary;

                if (CustomerSummarizeDto.EmployeeBM != null)
                {
                    reportcusinfo.总检医生 = CustomerSummarizeDto.EmployeeBM.Name;
                    if (CustomerSummarizeDto.EmployeeBM.SignImage != null)
                    {

                        //var result1 = _pictureController.GetUrlUser(CustomerSummarizeDto.EmployeeBM.SignImage.Value);
                        
                        var result1 = pictureDtos.FirstOrDefault(o => o.Id == CustomerSummarizeDto.EmployeeBM.SignImage.Value);
                        if(result1!=null)
                        reportcusinfo.总检医生签名 = result1.RelativePath;

                    }
                }
                else
                {
                    reportcusinfo.总检医生 = lstCustomerDtos.CSEmployeeBM?.Name;
                }
                if (CustomerSummarizeDto.ShEmployeeBM != null)
                {
                    reportcusinfo.审核医生 = CustomerSummarizeDto.ShEmployeeBM.Name;
                    if (CustomerSummarizeDto.ShEmployeeBM.SignImage != null)
                    {

                         //var result2 = _pictureController.GetUrlUser(CustomerSummarizeDto.ShEmployeeBM.SignImage.Value);

                        //修改签名0308
                        var result2 = pictureDtos.FirstOrDefault(o => o.Id == CustomerSummarizeDto.ShEmployeeBM.SignImage.Value);
                        if (result2 != null)
                        {
                            reportcusinfo.审核医生签名 = result2.RelativePath;
                        }

                    }
                }
                else
                {
                    if (lstCustomerDtos.FSEmployeeBM != null)
                    {
                        reportcusinfo.审核医生 = lstCustomerDtos.FSEmployeeBM.Name;
                        // Reportzj.FieldByName("审核医生").AsString = lstCustomerDtos.FSEmployeeBM.Name;
                    }
                }
               
                reportcusinfo.保健建议 = CustomerSummarizeDto.Jkzs;
                reportcusinfo.复查项目 = CustomerSummarizeDto.ReviewContent;
                if (string.IsNullOrEmpty(CustomerSummarizeDto.ReviewContent))
                {
                    reportcusinfo.复查项目 = "无";
                }
               
                reportcusinfo.复查处理意见 = reportCusReSum.Opinions;
                reportcusinfo.职业病结论描述 = reportCusReSum.Description;

                reportcusinfo.复查总检医生 = reportCusReSum.sumEmp;
                reportcusinfo.复查总检时间 = reportCusReSum.sumTime.ToString();
                reportcusinfo.复查时间 = reportCusReSum.ReviewDate.ToString();
                //if (inputOccCusSumDto != null && inputOccCusSumDto.CreatOccCustomerSumDto != null)
                //{
                //    reportcusinfo.职业病结论 = inputOccCusSumDto.CreatOccCustomerSumDto.Conclusion;
                //    reportcusinfo.职业病结论描述 = inputOccCusSumDto.CreatOccCustomerSumDto.Description;
                //    //if (!reportcusinfo.职业病结论.Contains("复查"))
                //    //{
                //    //    reportcusinfo.复查项目 = "";
                //    //}
                //    var occdis = inputOccCusSumDto.CraetOccCustomerOccDiseasesDto?.Select(o => o.Text).ToList();
                //    var occdisName = string.Join("、", occdis).TrimEnd('、');
                //    if (occdisName == "")
                //    {
                //        occdisName = "无";
                //    }
                //    reportcusinfo.疑似职业病 = occdisName;


                //    var occContr = inputOccCusSumDto.CreatOccCustomerContraindicationDto?.Select(o => o.Text).ToList();
                //    var occContrName = string.Join("、", occdis).TrimEnd('、');
                //    if (occContrName == "")
                //    {
                //        occContrName = "无";
                //    }
                //    reportcusinfo.职业病禁忌证 = occContrName;
                //    reportcusinfo.处理意见 = inputOccCusSumDto.CreatOccCustomerSumDto.Advise;
                //    reportcusinfo.结论依据 = inputOccCusSumDto.CreatOccCustomerSumDto.Standard;
                //    reportcusinfo.医学建议 = inputOccCusSumDto.CreatOccCustomerSumDto.MedicalAdvice;

                //}

                if (inputOccCusSumDto != null && inputOccCusSumDto.OccCustomerHazardSumDto != null && inputOccCusSumDto.OccCustomerHazardSumDto.Count > 0)
                {

                  
                   
                    //var occsumlist = inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Conclusion != null
                    //&& p.Conclusion != "").Select(p => p.OccHazardFactors == null 
                    //? p.Conclusion : p.OccHazardFactors?.Text + ":" + p.Conclusion).ToList();
                  
                    //if (occsumlist != null && occsumlist.Count > 0)
                    //{
                    //    reportcusinfo.职业病结论 = string.Join("\r\n", occsumlist);
                        
                    //}


                    var strConclusion = "";
                    var Conclusionlist = inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Conclusion != null && p.Conclusion != "").GroupBy(p => p.Conclusion);
                    if (Conclusionlist.Count() == 1)
                    {
                        strConclusion = Conclusionlist.FirstOrDefault().FirstOrDefault().Conclusion;
                    }
                    else
                    {
                        foreach (var clyj in Conclusionlist)
                        {
                            var riskName = string.Join("、", clyj.Where(p => p.OccHazardFactors != null).Select(p => p.OccHazardFactors?.Text));
                            if (!string.IsNullOrEmpty(riskName))
                            {
                                strConclusion += riskName + ":" + clyj.FirstOrDefault()?.Conclusion + "\r\n";
                            }
                            else
                            {
                                strConclusion += clyj.FirstOrDefault()?.Conclusion + "\r\n";

                            }
                        }
                    }
                    reportcusinfo.职业病结论 = strConclusion.Replace("\r\n", "$").TrimEnd('$').Replace("$", "\r\n"); ;

                    //大于一条显示危害因素
                    var jlms = inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Description != null && p.Description != "").Count();
                    if (jlms > 1)
                    {
                        var occsumms = inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Description != null && p.Description != "").Select(p => p.OccHazardFactors?.Text + ":" + p.Description).ToList();
                        if (occsumms != null && occsumms.Count > 0)
                        {
                            reportcusinfo.职业病结论描述 = string.Join("\r\n", occsumms);
                        }
                    }
                    else
                    {
                        var occsumms = inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Description != null && p.Description != "").Select(p => p.Description).ToList();
                        if (occsumms != null && occsumms.Count > 0)
                        {
                            reportcusinfo.职业病结论描述 = string.Join("\r\n", occsumms);
                        }
                    }

                    //var occdis = inputOccCusSumDto.CraetOccCustomerOccDiseasesDto?.Select(o => o.Text).ToList();
                    var occdisName = "";
                    var occContrName = "";
                    foreach (var occ in inputOccCusSumDto.OccCustomerHazardSumDto)
                    {
                        
                        if (occ!=null && occ.OccCustomerOccDiseases!=null && occ.OccCustomerOccDiseases.Count > 0)
                        {
                            if (occ.OccHazardFactors != null)
                            {
                                occdisName += occ.OccHazardFactors.Text + ":" + string.Join(",", occ.OccCustomerOccDiseases.Select(p => p.Text).ToList());
                            }
                            else
                            { occdisName +=  string.Join(",", occ.OccCustomerOccDiseases.Select(p => p.Text).ToList()); }
                        }
                        if (occ != null && occ.OccDictionarys != null && occ.OccDictionarys.Count > 0)
                        {
                            if (occ.OccHazardFactors != null)
                            {
                                occContrName += occ.OccHazardFactors.Text + ":" + string.Join(",", occ.OccDictionarys.Select(p => p.Text).ToList());
                            }
                            else
                            { occContrName +=  string.Join(",", occ.OccDictionarys.Select(p => p.Text).ToList()); }
                        }
                    }
                    if (occdisName == "")
                    { occdisName = "无"; }
                    if (occContrName == "")
                    { occContrName = "无"; }

                    reportcusinfo.疑似职业病 = occdisName;                    
                    reportcusinfo.职业病禁忌证 = occContrName;
                    var strclyj = "";
                    var clyjlist = inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Advise != null && p.Advise != "").GroupBy(p => p.Advise);
                    if (clyjlist.Count() == 1)
                    {
                        strclyj = clyjlist.FirstOrDefault().FirstOrDefault().Advise;
                    }
                    else
                    {
                        foreach (var clyj in clyjlist)
                        {
                            var riskName = string.Join("、", clyj.Where(p=>p.OccHazardFactors!=null).Select(p => p.OccHazardFactors?.Text));
                            if (!string.IsNullOrEmpty(riskName))
                            {
                                strclyj += riskName + ":" + clyj.FirstOrDefault()?.Advise + "\r\n";
                            }
                            else
                            {
                                strclyj +=   clyj.FirstOrDefault()?.Advise + "\r\n";

                            }
                        }
                    }
                    //  reportcusinfo.处理意见 = string.Join("\r\n", inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Advise != null && p.Advise != "").Select(p => p.OccHazardFactors == null ? p.Advise : p.OccHazardFactors?.Text + ":" + p.Advise).ToList());
                    reportcusinfo.处理意见 = strclyj.Replace("\r\n","$").TrimEnd('$').Replace("$", "\r\n");
                    // dataRow["结论依据"] = string.Join(";", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p => p.OccHazardFactors.Text + ":" + p.Standard).ToList()); //inputOccCusSumDto.CreatOccCustomerSumDto.Standard;
                    reportcusinfo.医学建议 = string.Join("\r\n", inputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.MedicalAdvice != null && p.MedicalAdvice != "").Select(p =>   p.MedicalAdvice).ToList()); //inputOccCusSumDto.CreatOccCustomerSumDto.MedicalAdvice;
                                                                                                                                                                                                              //复查的
                    #region 只显示主结论
                    //只显示主建议
                    string sumSet = DefinedCacheHelper.GetBasicDictionaryByValue(
                BasicDictionaryType.PresentationSet, 333)?.Remarks;
                    if (sumSet != null && sumSet == "1")
                    {

                        var mainSum = inputOccCusSumDto.OccCustomerHazardSumDto.FirstOrDefault(p =>
                          p.Conclusion != null
                     && p.Conclusion != "" && p.IsMain == 1);
                        if (mainSum != null)
                        {
                            reportcusinfo.职业病结论 = mainSum.Conclusion;
                            reportcusinfo.疑似职业病 = string.Join(",", mainSum.OccCustomerOccDiseases.Select(p => p.Text).ToList());
                            reportcusinfo.职业病禁忌证 = string.Join(",", mainSum.OccDictionarys.Select(p => p.Text).ToList());
                            reportcusinfo.处理意见 = mainSum.Advise;
                            reportcusinfo.医学建议 = mainSum.MedicalAdvice;
                            reportcusinfo.职业病结论描述 = mainSum.Description;
                        }


                    }
                    #endregion                                                                                                                                                                                   //复查原始报告

                    //复查建议


                    if (isReview == true && lstCustomerDtos.ReviewRegID.HasValue)
                    {
                        QueryClass queryClass = new QueryClass();
                        queryClass.CustomerRegId = lstCustomerDtos.Id;
                        var fctjlCustomerSummarizeBMViewDtos = inspectionTotalAppService.GetlstSummarizeBM(queryClass);
                        var fcadviceform = fcFormatAdvice(fctjlCustomerSummarizeBMViewDtos);
                        reportcusinfo.复查总检建议 = fcadviceform.adviceContent;
                        if (fctjlCustomerSummarizeDto != null && fctjlCustomerSummarizeDto.ConclusionDate.HasValue)
                        {
                            reportcusinfo.复查总检时间 = fctjlCustomerSummarizeDto.ConclusionDate.Value.ToString(Variables.ShortDatePattern);

                            if (fctjlCustomerSummarizeDto.EmployeeBM != null)
                            {
                                reportcusinfo.复查总检医生 = fctjlCustomerSummarizeDto.EmployeeBM.Name;
                            }
                            if (fctjlCustomerSummarizeDto.ShEmployeeBM != null)
                            {
                                reportcusinfo.复查审核医生 = fctjlCustomerSummarizeDto.ShEmployeeBM.Name;

                            }


                        }
                        EntityDto<Guid> inputzy = new EntityDto<Guid>();
                        inputzy.Id = lstCustomerDtos.Id;
                         
                      var  fcinputOccCusSumDto = inspectionTotalAppService.GetCusOccSumByRegId(inputzy);

                        //reportcusinfo.复查总检医生 = reportcusinfo.总检医生;
                        //reportcusinfo.复查总检时间 = reportcusinfo.总检日期;
                        reportcusinfo.复查时间 = lstCustomerDtos.LoginDate.ToString();

                        reportcusinfo.复查职业病结论 = string.Join("\r\n", fcinputOccCusSumDto.OccCustomerHazardSumDto.Where(p=>p.Conclusion!=null  && p.Conclusion!="").Select(p => p.OccHazardFactors == null ? p.Conclusion : p.OccHazardFactors?.Text + ":" + p.Conclusion).ToList());

                        reportcusinfo.复查职业病结论描述 = string.Join("\r\n", fcinputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Description != null && p.Description != "").Select(p =>  p.Description).ToList());

                        //var occdis = inputOccCusSumDto.CraetOccCustomerOccDiseasesDto?.Select(o => o.Text).ToList();
                        var fcoccdisName = "";
                        var fcoccContrName = "";
                        if (fcinputOccCusSumDto.OccCustomerHazardSumDto != null)
                        {
                            foreach (var occ in fcinputOccCusSumDto.OccCustomerHazardSumDto)
                            {
                                if (occ.OccCustomerOccDiseases != null && occ.OccCustomerOccDiseases.Count > 0)
                                {
                                    if (occ.OccHazardFactors != null)
                                    { fcoccdisName += occ.OccHazardFactors.Text + ":" + string.Join(",", occ.OccCustomerOccDiseases.Select(p => p.Text).ToList()); }
                                    else
                                    { fcoccdisName +=   string.Join(",", occ.OccCustomerOccDiseases.Select(p => p.Text).ToList()); }
                                  
                                }
                                if (occ.OccCustomerOccDiseases != null && occ.OccDictionarys.Count > 0)
                                {
                                    if (occ.OccHazardFactors != null)
                                    { fcoccContrName += occ.OccHazardFactors.Text + ":" + string.Join(",", occ.OccDictionarys.Select(p => p.Text).ToList()); }
                                    else
                                    {
                                        fcoccContrName +=  string.Join(",", occ.OccDictionarys.Select(p => p.Text).ToList());
                                    }
                                }
                            }
                        }

                        if (fcoccdisName == "")
                        { fcoccdisName = "无"; }
                        if (fcoccContrName == "")
                        { fcoccContrName = "无"; }

                        reportcusinfo.复查疑似职业病 = fcoccdisName;

                        reportcusinfo.复查职业病禁忌证 = fcoccContrName;
                        reportcusinfo.复查处理意见 = string.Join("\r\n", fcinputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.Advise != null && p.Advise != "").Select(p => p.OccHazardFactors == null ? p.Advise : p.OccHazardFactors.Text + ":" + p.Advise).ToList());
                        // dataRow["结论依据"] = string.Join(";", inputOccCusSumDto.OccCustomerHazardSumDto.Select(p => p.OccHazardFactors.Text + ":" + p.Standard).ToList()); //inputOccCusSumDto.CreatOccCustomerSumDto.Standard;
                        reportcusinfo.复查医学建议 = string.Join("\r\n", fcinputOccCusSumDto.OccCustomerHazardSumDto.Where(p => p.MedicalAdvice != null && p.MedicalAdvice != "").Select(p =>  p.MedicalAdvice).ToList()); //inputOccCusSumDto.CreatOccCustomerSumDto.MedicalAdvice;
                                                                                                                                                                                //复查的
                                                                                                                                                                                //复查原始报告

                    }

                }

                reportcusinfo.是否合格 = CustomerSummarizeDto.Qualified;

                #region 职加健             
                reportcusinfo.职业总检汇总 = CustomerSummarizeDto.occCharacterSummary;
                reportcusinfo.职业总检医生 = CustomerSummarizeDto.occEmployeeBM?.Name;
                reportcusinfo.职业总检日期 = CustomerSummarizeDto.occConclusionDate?.ToString();
                reportcusinfo.职业审核医生 = CustomerSummarizeDto.occShEmployeeBM?.Name;
                if (iszjj == true)
                {
                    reportcusinfo.总检汇总 = reportcusinfo.职业总检汇总;
                    reportcusinfo.总检医生 = reportcusinfo.职业总检医生;
                    reportcusinfo.总检日期 = reportcusinfo.职业总检日期;
                    reportcusinfo.审核医生 = reportcusinfo.职业审核医生;
                }
                #endregion
            }
           


            reportJson.groupEmdical.Add(reportcusinfo);

            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
            Reportzj.LoadDataFromXML(reportJsonForExamineString);

            if (reportcusinfo.职业病结论!=null &&  reportcusinfo.职业病结论.Contains("复查") && ReportMain.ControlByName("复查通知单") != null)
            {
                Reportfctzd = ReportMain.ControlByName("复查通知单").AsSubReport.Report;
                Reportfctzd.LoadDataFromXML(reportJsonForExamineString);
            }
            if (reportcusinfo.职业病结论 != null && reportcusinfo.职业病结论.Contains("禁忌证") && ReportMain.ControlByName("禁忌证通知单") != null)
            {
                Reportjjztzd = ReportMain.ControlByName("禁忌证通知单").AsSubReport.Report;
                Reportjjztzd.LoadDataFromXML(reportJsonForExamineString);
            }
            if (reportcusinfo.职业病结论 != null && reportcusinfo.职业病结论.Contains("职业病") && ReportMain.ControlByName("职业病通知单") != null)
            {
                Reportzybtzd = ReportMain.ControlByName("职业病通知单").AsSubReport.Report;
                Reportzybtzd.LoadDataFromXML(reportJsonForExamineString);
            }
        }    
        private void BindHistoryZJReport(GridppReport gridpp)
        {
            var History = CustomerHistorySummarizeBMDtos.GroupBy(o => o.CustomerRegID).ToList();
            var reportJson = new ReportJsonEmdicalCertificate();
            reportJson.ItemEmdical = new List<ItemEmdicalCertificate>();
            for (int num = 0; num < History.Count; num++)
            {
                var itememd = new ItemEmdicalCertificate();
                var customerregid = History[num].First().CustomerRegID;
                itememd.CustomerBM = "1";
                itememd.ChekDateYear1 = HisItems?.HisDepartSum?.Where(o => o.regId == customerregid).FirstOrDefault()?.CheckDate;
                itememd.diagnosis = FormatHistoryAdvice(History[num].ToList());
                reportJson.ItemEmdical.Add(itememd);
            }
            var reportJsonForExamineString = JsonConvert.SerializeObject(reportJson);
            gridpp.LoadDataFromXML(reportJsonForExamineString);

        }
        /// <summary>
        /// 历年科室小结对比
        /// </summary>
        /// <param name="gridpp"></param>
        private void BindHistoryDepartSumReport(GridppReport gridpp)
        {
            
            var reportJsonStringsy = JsonConvert.SerializeObject(HisItems.HisDepartSum);
            var reportJson = "{\"Detail\":" + reportJsonStringsy + "}";
            gridpp.LoadDataFromXML(reportJson);
       

        }
        #region 格式化总检建议
        private adviceNames FormatAdvice()
        {
            adviceNames adviceNames = new adviceNames();
            string strAdvice = "";
            //建议格式
            string atrAdviceType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 160).Remarks;
            string stradviceitem = "";
            //诊断格式
            string stradviceNames = "";
            string atrAdviceNameType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 161)?.Remarks;
            string stradviceName = "";
            if (atrAdviceNameType == null)
            {
                atrAdviceNameType = "";
            }
            if (tjlCustomerSummarizeBMViewDtos.Count > 0)
            {
                foreach (var item in tjlCustomerSummarizeBMViewDtos.Where(o => o.ParentAdviceId == Guid.Empty || o.ParentAdviceId == null).OrderBy(o => o.SummarizeOrderNum))
                {  //建议内容
                    stradviceitem = atrAdviceType.Replace("【序号】",
                      item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                      item.SummarizeName.Trim()).Replace("【建议内容】", item.Advice?.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                    strAdvice += stradviceitem;
                    //诊断格式
                    stradviceName = atrAdviceNameType.Replace("【序号】",
                    item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                    item.SummarizeName.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                    stradviceNames += stradviceName;

                    var Children = tjlCustomerSummarizeBMViewDtos.Where(o => o.ParentAdviceId == item.Id)?.OrderBy(o => o.SummarizeOrderNum).ToList();
                    if (Children != null && Children.Count() > 0)
                    {
                        foreach (var itemChildren in Children)
                        {
                            //建议格式
                            stradviceitem = atrAdviceNameType.Replace("【序号】",
                             "").Replace("【建议名称】",
                             itemChildren.SummarizeName.Trim()).Replace("【建议内容】", itemChildren.Advice.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                            strAdvice += stradviceitem;

                            //诊断格式
                            stradviceName = atrAdviceType.Replace("【序号】",
                            item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                            item.SummarizeName.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                            stradviceNames += stradviceName;
                        }
                    }
                }

            }
            adviceNames.adviceContent = strAdvice;
            adviceNames.adviceName = stradviceNames;
            if (adviceNames.adviceName == "")
            {
                var adls = tjlCustomerSummarizeBMViewDtos.OrderBy(r => r.SummarizeOrderNum).Select(o => (o.SummarizeOrderNum + "、" + o.SummarizeName));
                adviceNames.adviceName = string.Join("\r", adls);
            }
            return adviceNames;
        }

   
        private adviceNames fcFormatAdvice(List<TjlCustomerSummarizeBMViewDto> fccusSum)
        {
            adviceNames adviceNames = new adviceNames();
            string strAdvice = "";
            //建议格式
            string atrAdviceType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 160).Remarks;
            string stradviceitem = "";
            //诊断格式
            string stradviceNames = "";
            string atrAdviceNameType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 161)?.Remarks;
            string stradviceName = "";
            if (atrAdviceNameType == null)
            {
                atrAdviceNameType = "";
            }
            if (fccusSum.Count > 0)
            {
                foreach (var item in fccusSum.Where(o => o.ParentAdviceId == Guid.Empty || o.ParentAdviceId == null).OrderBy(o => o.SummarizeOrderNum))
                {  //建议内容
                    stradviceitem = atrAdviceType.Replace("【序号】",
                      item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                      item.SummarizeName.Trim()).Replace("【建议内容】", item.Advice.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                    strAdvice += stradviceitem;
                    //诊断格式
                    stradviceName = atrAdviceNameType.Replace("【序号】",
                    item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                    item.SummarizeName.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                    stradviceNames += stradviceName;

                    var Children = fccusSum.Where(o => o.ParentAdviceId == item.Id)?.OrderBy(o => o.SummarizeOrderNum).ToList();
                    if (Children != null && Children.Count() > 0)
                    {
                        foreach (var itemChildren in Children)
                        {
                            //建议格式
                            stradviceitem = atrAdviceNameType.Replace("【序号】",
                             "").Replace("【建议名称】",
                             itemChildren.SummarizeName.Trim()).Replace("【建议内容】", itemChildren.Advice.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                            strAdvice += stradviceitem;

                            //诊断格式
                            stradviceName = atrAdviceType.Replace("【序号】",
                            item.SummarizeOrderNum?.ToString()).Replace("【建议名称】",
                            item.SummarizeName.Trim()).Replace("【空格】", "\n").Replace("【换行】", "\r");
                            stradviceNames += stradviceName;
                        }
                    }
                }

            }
            adviceNames.adviceContent = strAdvice;
            adviceNames.adviceName = stradviceNames;
            if (adviceNames.adviceName == "")
            {
                var adls = fccusSum.OrderBy(r => r.SummarizeOrderNum).Select(o => (o.SummarizeOrderNum + "、" + o.SummarizeName));
                adviceNames.adviceName = string.Join("\r", adls);
            }
            return adviceNames;
        }

        private string FormatHistoryAdvice(List<TjlCustomerSummarizeBMViewDto> Historyadvices)
        {
            string strAdvice = "";
            string atrAdviceType = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 160).Remarks;
            string stradviceitem = "";
            if (Historyadvices.Count > 0)
            {
                var sumbm = Historyadvices.Where(o => o.ParentAdviceId == Guid.Empty || o.ParentAdviceId == null).OrderBy(o => o.SummarizeOrderNum);
                int num = 0;
                foreach (var item in sumbm)
                {

                    if (!stradviceitem.Contains(item.SummarizeName.Trim()))
                    {
                        num = num + 1;
                        stradviceitem = atrAdviceType.Replace("【序号】",
                       num.ToString()).Replace("【建议名称】",
                       item.SummarizeName.Trim()).Replace("【换行】", "\r").Replace("【建议内容】", "").Replace("n", "\n").Replace("r", "\r");
                        strAdvice += stradviceitem;
                    }
                    var Children = Historyadvices.Where(o => o.ParentAdviceId == item.Id)?.OrderBy(o => o.SummarizeOrderNum).ToList();
                    if (Children != null && Children.Count() > 0)
                    {
                        foreach (var itemChildren in Children)
                        {

                            if (!stradviceitem.Contains(itemChildren.SummarizeName.Trim()))
                            {
                                num = num + 1;
                                stradviceitem = atrAdviceType.Replace("【序号】",
                            num.ToString()).Replace("【建议名称】",
                             itemChildren.SummarizeName.Trim()).Replace("【换行】", "\r").Replace("【建议内容】", "").Replace("n", "\n").Replace("r", "\r");
                                strAdvice += stradviceitem;
                            }
                        }
                    }
                }

            }
            return strAdvice;
        }
        #endregion
        #endregion

        #region 健康证明
        //public void PrintingEmdicalCertificate(bool isPreview)
        //{
        //    //获取模板路径
        //    var gridppUrl = GridppHelper.GetTemplate("健康证明.grf");
        //    if (string.IsNullOrWhiteSpace(gridppUrl))
        //    {
        //        return;
        //    }
        //    //获取基本信息
        //    ICustomerAppService customerAppService = new CustomerAppService();
        //    cusNameInput.Theme = "1";
        //    lstCustomerDtos = customerAppService.GetCustomerRegDto(cusNameInput);
        //    if (lstCustomerDtos == null)
        //    {
        //        return;
        //    }
        //    //获取检查项目结果信息
        //    QueryClass queryClass = new QueryClass();
        //    queryClass.CustomerRegId = cusNameInput.Id;
        //    CustomerItemPic = new DoctorStationAppService();

        //    //lstCustomerRegItemReoprtDto = CustomerItemPic.GetTjlCustomerRegItemReoprtDtos(queryClass).Where(n => n.ProcessState != (int)ProjectIState.GiveUp && n.ProcessState != (int)ProjectIState.Not).OrderBy(n => n.DepartmentBM.OrderNum).ThenBy(n => n.ItemGroupBM.OrderNum).ThenBy(o => o.ItemOrder).ToList();
        //    //获取所有组合小结
        //    lstCustomerItemGroupPrintViewDto = CustomerItemPic.GetCustomerItemGroupPrintViewDtos(queryClass);

        //    //绑定数据
        //    var report = new GridppReport();
        //    report.LoadFromURL(gridppUrl);
        //    var Report = new ReportJsonEmdicalCertificate();
        //    Report.Parameter = new List<ParameterEmdicalCertificate>();
        //    Report.ItemEmdical = new List<ItemEmdicalCertificate>();
        //    var Parameter = new ParameterEmdicalCertificate();

        //    foreach (var item in lstCustomerItemGroupPrintViewDto)
        //    {

        //    }
        //    var reportJsonString = JsonConvert.SerializeObject(Report);
        //    report.LoadDataFromXML(reportJsonString);
        //    ////获取所有图片
        //    //CustomerItemPic = new DoctorStationAppService();
        //    //lstcustomerItemPicDtos = CustomerItemPic.GetCustomerItemPicDtos(queryClass);

        //    ////获取总检信息
        //    //TjlCustomerQuery tjlCustomerQuery = new TjlCustomerQuery();
        //    //tjlCustomerQuery.CustomerRegID = cusNameInput.Id;
        //    //tjlCustomerSummarizeDto = inspectionTotalAppService.GetSummarize(tjlCustomerQuery);

        //    //IItemInfoAppService itemInfoAppService = new ItemInfoAppService();
        //    //lstItemStandardDto = itemInfoAppService.~Que~ryItemStandardBySum();
        //    //itemInfoSimpleDtosall = CacheHelper.GetItemInfos();
        //    ////获取科室小结
        //    //lstCustomerDepSummaries = CustomerItemPic.GetCustomerDepSummaries(queryClass);
        //    ////获取总检建议列表
        //    //tjlCustomerSummarizeBMViewDtos = inspectionTotalAppService.GetlstSummarizeBM(queryClass);

        //}
        #endregion
    }


}
