using Abp.Application.Services.Dto;
using DevExpress.XtraEditors;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccGroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.Company;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.GroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport;
using Sw.Hospital.HealthExaminationSystem.Application.OccGroupReport.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using HealthExaminationSystem.Enumerations;
using HealthExaminationSystem.Enumerations.Helpers;

namespace Sw.Hospital.HealthExaminationSystem.Comm
{
 public    class PrintOccClientReport
    {
        #region 申明子报表
        private GridppReport ReportMain;

        #endregion
        private EntityDto<Guid> ClientRegID=new EntityDto<Guid>();
        private IClientRegAppService CRegAppService=new ClientRegAppService();
        private IOccGroupReportAppService occGroupReportAppService = new OccGroupReportAppService();
        private IGroupReportAppService GReportAppService =new GroupReportAppService();
        //单位信息
        private ClientRegDto clientRegDto = new ClientRegDto();
        private  List<CusInfo> cusInfos = new List<CusInfo>();
        //目标疾病
        private List<OccTargetCountDto> occTargetCountDtos = new List<OccTargetCountDto>();
        //单位体检人信息
        // private List<OccCustomerSumDto> OccCustomerReglis = new List<OccCustomerSumDto>();
        //单位体检人信息
        private List<OccCustomerRegHazardSumDto> OccCustomerReglis = new List<OccCustomerRegHazardSumDto>();
        //单位已检人员
        private List<GroupClientCusDto> groupClientCusDtos = new List<GroupClientCusDto>();
        //单位人员信息
        private List<GroupClientCusDto> ClientCusDtos = new List<GroupClientCusDto>();


        
        //单位诊断
        private List<GroupClientSumDto> groupClientSumDtos = new List<GroupClientSumDto>();
        //复查项目
        private List<CusReviewGroupDto> CusReviewGroups = new List<CusReviewGroupDto>();
        private int allCount=0;
        private int CheckCount=0;
        private int ReCount = 0;
        private List<Guid> _cuslist = new List<Guid>();
        public PrintOccClientReport()
        {
            ReportMain = new GridppReport();           
        }
        /// <summary>
        /// 打印团报
        /// </summary>
        /// <param name="isPreview">是否预览，是则预览，否则打印</param>
        public void Print(bool isPreview, EntityDto<Guid> input, string path = "",List<Guid> cuslst=null,string star="" , string end ="",string Reference="",string mb="",bool isfc=true)
        {
            ClientRegID = input;
            var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            strBarPrintName = "团体报告-职业病团报.grf";
            if (!string.IsNullOrEmpty(mb))
            { strBarPrintName = mb; }
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            ReportMain.LoadFromURL(GrdPath);
            clientRegDto = CRegAppService.GetClientRegByID(ClientRegID);
            string no4 = "";
            string no5 = "";
            string no6 = "";
            string no7 = "";
            string no8 = "";
            string no9 = "";
            string no10= "";
            //获取单位体检人信息(已总检)
            OccCustomerReglis = occGroupReportAppService.getCustomerRegHazardSum(ClientRegID);
            //过滤复查人员
            if (isfc == false)
            {
                OccCustomerReglis = OccCustomerReglis.Where(p => p.CustomerRegBM.ReviewSate != (int)ReviewSate.Review).ToList();
            }
            if (cuslst != null && cuslst.Count > 0)
            {
                _cuslist = cuslst.ToList();
                OccCustomerReglis = OccCustomerReglis.Where(o => cuslst.Contains(o.CustomerRegBMId.Value)).ToList();
            }
            // OccCustomerReglis= OccCustomerReglis.Where(o=>o.p)
            //目标疾病
            InOcccCusIDDto inOcccCusIDDto = new InOcccCusIDDto();
            inOcccCusIDDto.Id = ClientRegID.Id;
            if (cuslst != null && cuslst.Count > 0)
            {
                inOcccCusIDDto.CusRegIDList= cuslst;
            }
            //过滤复查人员
            if (isfc == false)
            {
                inOcccCusIDDto.isfc = false;
            }
            occTargetCountDtos = occGroupReportAppService.getTargetCount(inOcccCusIDDto);
            ClientRegIdDto clientRegIdDto = new ClientRegIdDto();
            clientRegIdDto.Idlist = new List<Guid>();
            clientRegIdDto.Idlist.Add(ClientRegID.Id);
            //单位所有体检人
            ClientCusDtos = GReportAppService.GRClientRegCus(clientRegIdDto).ToList();
            //过滤复查人员
            if (isfc == false)
            {
                ClientCusDtos = ClientCusDtos.Where(p => p.ReviewSate != (int)ReviewSate.Review).ToList();
            }
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ExaminationType);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract && o.Text.Contains("职业")).Select(p=>p.Value).ToList();
            if (Clientcontract != null && Clientcontract.Count>0)
            {
                ClientCusDtos = ClientCusDtos.Where(o => o.PhysicalType!=null && Clientcontract.Contains( o.PhysicalType.Value)).ToList();
            }
            //已选人员
            if (cuslst != null && cuslst.Count > 0)
            {
                ClientCusDtos = ClientCusDtos.Where(o => cuslst.Contains(o.Id)).ToList();
            }
            //到检人员
            int checkstate = (int)PhysicalEState.Not;
            groupClientCusDtos = ClientCusDtos.Where(o => o.CheckSate != checkstate).ToList();

            allCount = ClientCusDtos.Count();
            CheckCount = OccCustomerReglis.Select(p=>p.CustomerRegBMId).Distinct().Count();
            ReCount = ClientCusDtos.Where(p => p.ReviewSate == (int)ReviewSate.Review).Count();
            if (CheckCount == 0)
            {
                XtraMessageBox.Show("该单位没有已总检人员，不能生成职业病团报！");
                return;

            }
            //人员诊断
            groupClientSumDtos = GReportAppService.GRClientCusSum(clientRegIdDto);
            //过滤复查人员
            if (isfc == false)
            {
                groupClientSumDtos = groupClientSumDtos.Where(p => p.CustomerReg.ReviewSate != (int)ReviewSate.Review).ToList();
            }
            //项目异常结果
            var     CusItemResult = occGroupReportAppService.getCusItemResult(ClientRegID);
            //已选人员
            if (cuslst != null && cuslst.Count > 0)
            {
                groupClientSumDtos = groupClientSumDtos.Where(o => cuslst.Contains(o.CustomerRegID)).ToList();
            }
            //复查信息
            var cusreglist = groupClientSumDtos.Select(p=>p.CustomerRegID).ToList();
            EntityDto<List<Guid>> reinput = new EntityDto<List<Guid>>();
            reinput.Id= cusreglist;
            CusReviewGroups = GReportAppService.ReVewGroupCus(reinput).OrderBy(p=>p.ItemGroupName).ToList();
            #region 绑定数据      
            //疑似职业病
            if (OccCustomerReglis == null)
            {
                OccCustomerReglis = new List<OccCustomerRegHazardSumDto>();
            }

            //总人数
            #region MyRegion
            // var Allyscuslist = OccCustomerReglis.Where(o => o.Conclusion != null).GroupBy(o =>
            //new {
            //    Age = o.CustomerRegBM.Customer.Age.ToString(),
            //     // Conlutions = o.Conclusion,
            //     Name = o.CustomerRegBM.Customer.Name,
            //     //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
            //     // Opton = o.Advise,
            //     //Description = o.Description,
            //     CustomerBM = o.CustomerRegBM.CustomerBM,
            //    RiskNames = o.CustomerRegBM.RiskS,
            //    Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //    CheckType = o.CustomerRegBM.PostState,
            //    TypeWork = o.CustomerRegBM.TypeWork,
            //     // MedicalAdvice = o.MedicalAdvice,
            //     InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
            //    OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
            //    OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
            //    SumAdvice = o.CustomerSummarize?.Advice,
            //    OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
            //    Address = o.CustomerRegBM.Customer.Address,
            //    BookingDate = o.CustomerRegBM.BookingDate.ToString(),
            //    IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
            //    LoginDate = o.CustomerRegBM.LoginDate.ToString(),
            //    Mobile = o.CustomerRegBM.Customer.Mobile,
            //    Nation = o.CustomerRegBM.Customer.Nation,
            //}).Select(o => new OccDissCuslis
            //{
            //    Age = o.Key.Age.ToString(),
            //    Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count()==1
            //    ?o.FirstOrDefault().Conclusion:
            //    string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

            //    // Conlutions = string.Join(";", o.Where(n => n.Conclusion !="" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),
            //    Name = o.Key.Name,
            //     OccDiss = o.SelectMany(p => p.OccCustomerOccDiseases) == null ? "" : string.Join(",", o.SelectMany(p => p.OccCustomerOccDiseases)?.Select(r => r.Text).ToList()),
            //     Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
            //    ? o.FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise +"\r\n")),

            //    Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),
            //     CustomerBM = o.Key.CustomerBM,
            //     RiskNames = o.Key.RiskNames,
            //     Sex = o.Key.Sex,
            //     CheckType = o.Key.CheckType,
            //     TypeWork = o.Key.TypeWork,
            //     MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),
            //     InjuryAge = o.Key.InjuryAge,
            //     OccIll = o.Key.OccIll,
            //     OtherIll = o.Key.OtherIll,
            //     SumAdvice = o.Key.SumAdvice,
            //     OccIllSummarize = o.Key.OccIllSummarize,
            //     Address = o.Key.Address,
            //     BookingDate = o.Key.BookingDate.ToString(),
            //     IDCardNo = o.Key.IDCardNo,
            //     LoginDate = o.Key.LoginDate.ToString(),
            //     Mobile = o.Key.Mobile,
            //     Nation = o.Key.Nation,
            // }).Distinct().ToList(); 
            #endregion
            var Allyscuslist = getSumRS(CusItemResult,"");
            //疑似职业病
            var yscuslist = getSumRS(CusItemResult, "职业病");
            #region MyRegion
            //var yscuslist = OccCustomerReglis.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).GroupBy(o =>
            //    new
            //    {
            //        Age = o.CustomerRegBM.Customer.Age.ToString(),
            //        // Conlutions = o.Conclusion,
            //        Name = o.CustomerRegBM.Customer.Name,
            //        //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
            //        // Opton = o.Advise,
            //        //Description = o.Description,
            //        CustomerBM = o.CustomerRegBM.CustomerBM,
            //        RiskNames = o.CustomerRegBM.RiskS,
            //        Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //        CheckType = o.CustomerRegBM.PostState,
            //        TypeWork = o.CustomerRegBM.TypeWork,
            //        // MedicalAdvice = o.MedicalAdvice,
            //        InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
            //        OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
            //        OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
            //        SumAdvice = o.CustomerSummarize?.Advice,
            //        OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
            //        Address = o.CustomerRegBM.Customer.Address,
            //        BookingDate = o.CustomerRegBM.BookingDate.ToString(),
            //        IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
            //        LoginDate = o.CustomerRegBM.LoginDate.ToString(),
            //        Mobile = o.CustomerRegBM.Customer.Mobile,
            //        Nation = o.CustomerRegBM.Customer.Nation,
            //    }).Select(o => new OccDissCuslis
            //    {
            //        Age = o.Key.Age.ToString(),
            //        // Conlutions = string.Join(";", o.Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),
            //        Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count() == 1
            //       ? o.FirstOrDefault().Conclusion :
            //       string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

            //        Name = o.Key.Name,
            //        OccDiss = o.SelectMany(p => p.OccCustomerOccDiseases) == null ? "" : string.Join(",", o.SelectMany(p => p.OccCustomerOccDiseases)?.Select(r => r.Text).ToList()),
            //        //Opton = string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),
            //        Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
            //       ? o.FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),

            //        Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),
            //        CustomerBM = o.Key.CustomerBM,
            //        RiskNames = o.Key.RiskNames,
            //        Sex = o.Key.Sex,
            //        CheckType = o.Key.CheckType,
            //        TypeWork = o.Key.TypeWork,
            //        MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),
            //        InjuryAge = o.Key.InjuryAge,
            //        OccIll = o.Key.OccIll,
            //        OtherIll = o.Key.OtherIll,
            //        SumAdvice = o.Key.SumAdvice,
            //        OccIllSummarize = o.Key.OccIllSummarize,
            //        Address = o.Key.Address,
            //        BookingDate = o.Key.BookingDate.ToString(),
            //        IDCardNo = o.Key.IDCardNo,
            //        LoginDate = o.Key.LoginDate.ToString(),
            //        Mobile = o.Key.Mobile,
            //        Nation = o.Key.Nation,
            //    }).Distinct().ToList();
            #endregion
            if (yscuslist.Count() == 0)
            {
                // no4 = "(无)";
                yscuslist.Add(new OccDissCuslis
                {

                    Conlutions = "无",
                    Name = "无",
                    OccDiss = "无",
                    Opton = "无",
                    Description = "无",
                    CustomerBM = "无",
                    RiskNames = "无",
                    Sex = "无",
                    CheckType = "无",
                    TypeWork = "无",
                    MedicalAdvice = "无",
                    InjuryAge = "无",
                    OccIll = "无",
                    OtherIll = "无",
                    OccIllSummarize = "无",
                    SumAdvice = "无",
                    Address = "无",
                    IDCardNo = "无",
                    Mobile = "无",
                    Nation = "无",
                    Age = "无",
                    BookingDate = "无",
                    LoginDate = "无",

                });
            }
            else
            {
                yscuslist[0].AllCount = yscuslist.Count();
                yscuslist[yscuslist.Count()-1].AllCount = yscuslist.Count();
            }
            //疑似禁忌证
            var jjzcuslist = getSumRS(CusItemResult, "禁忌证");
            #region MyRegion
            //var jjzcuslist = OccCustomerReglis.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).GroupBy(o=>
            //new {
            //    Age = o.CustomerRegBM.Customer.Age.ToString(),
            //   // Conlutions = o.Conclusion,
            //    Name = o.CustomerRegBM.Customer.Name,
            //    //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
            //   // Opton = o.Advise,
            //    //Description = o.Description,
            //    CustomerBM = o.CustomerRegBM.CustomerBM,
            //    RiskNames = o.CustomerRegBM.RiskS,
            //    Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //    CheckType = o.CustomerRegBM.PostState,
            //    TypeWork = o.CustomerRegBM.TypeWork,
            //   // MedicalAdvice = o.MedicalAdvice,
            //    InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
            //    OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
            //    OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
            //    SumAdvice = o.CustomerSummarize?.Advice,
            //    OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
            //    Address = o.CustomerRegBM.Customer.Address,
            //    BookingDate = o.CustomerRegBM.BookingDate.ToString(),
            //    IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
            //    LoginDate = o.CustomerRegBM.LoginDate.ToString(),
            //    Mobile = o.CustomerRegBM.Customer.Mobile,
            //    Nation = o.CustomerRegBM.Customer.Nation,
            //}).Select(o => new OccDissCuslis
            //{
            //    Age = o.Key.Age.ToString(),
            //    // Conlutions = string.Join(";", o.Select(p=>p.OccHazardFactors.Text +":" + p.Conclusion )),
            //    Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count() == 1
            //   ? o.FirstOrDefault().Conclusion :
            //   string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

            //    Name = o.Key.Name,
            //    OccDiss = o.SelectMany(p=>p.OccDictionarys)==null?"": string.Join(",", o.SelectMany(p => p.OccDictionarys)?.Select(r => r.Text).ToList()),

            //    Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
            //   ? o.FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),

            //    // Opton =string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p=>p.OccHazardFactors.Text +":" + p.Advise + "\r\n")), 
            //    Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),             
            //    CustomerBM = o.Key.CustomerBM,
            //    RiskNames = o.Key.RiskNames,
            //    Sex = o.Key.Sex,
            //    CheckType = o.Key.CheckType,
            //    TypeWork = o.Key.TypeWork,
            //    MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),                           
            //    InjuryAge = o.Key.InjuryAge,
            //    OccIll = o.Key.OccIll,
            //    OtherIll = o.Key.OtherIll,
            //    SumAdvice = o.Key.SumAdvice,
            //    OccIllSummarize = o.Key.OccIllSummarize,
            //    Address = o.Key.Address,
            //    BookingDate = o.Key.BookingDate.ToString(),
            //    IDCardNo = o.Key.IDCardNo,
            //    LoginDate = o.Key.LoginDate.ToString(),
            //    Mobile = o.Key.Mobile,
            //    Nation = o.Key.Nation,
            //}).Distinct().ToList(); 
            #endregion
            if (jjzcuslist.Count > 0)
            {

                foreach (var j in jjzcuslist)
                {
                    if (j.MedicalAdvice == null || j.MedicalAdvice == "")
                    {
                        j.MedicalAdvice = "无";
                    }
                    if (j.OccIll == null || j.OccIll == "")
                    {
                        j.OccIll = "无";
                    }
                    if (j.Conlutions == null || j.Conlutions == "")
                    {
                        j.Conlutions = "无";
                    }
                    if (j.Description == null || j.Description == "")
                    {
                        j.Description = "无";
                    }
                }
                jjzcuslist[0].AllCount = jjzcuslist.Count();
                jjzcuslist[jjzcuslist.Count() - 1].AllCount = jjzcuslist.Count();
            }
            if (jjzcuslist.Count() == 0)
            {
                //no5 = "(无)";
                jjzcuslist.Add(new OccDissCuslis {
                   
                    Conlutions = "无",
                    Name = "无",
                    OccDiss = "无",
                    Opton = "无",
                    Description = "无",
                    CustomerBM = "无",
                    RiskNames = "无",
                    Sex = "无",
                    CheckType = "无",
                    TypeWork = "无",
                    MedicalAdvice = "无",
                    InjuryAge = "无",
                    OccIll = "无",
                    OtherIll = "无",
                    OccIllSummarize = "无",
                    SumAdvice = "无",
                    Address = "无",
                    IDCardNo = "无",
                    Mobile = "无",
                    Nation = "无",
                    Age = "无",
                    BookingDate = "无",
                    LoginDate = "无",
                });
            }
            var fccuslist = getSumRS(CusItemResult, "复查");
            //复查
            #region MyRegion
            //var fccuslist = OccCustomerReglis.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).GroupBy(o =>
            //   new
            //   {
            //       Age = o.CustomerRegBM.Customer.Age.ToString(),
            //    // Conlutions = o.Conclusion,
            //    Name = o.CustomerRegBM.Customer.Name,
            //    //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
            //    // Opton = o.Advise,
            //    //Description = o.Description,
            //    CustomerBM = o.CustomerRegBM.CustomerBM,
            //       RiskNames = o.CustomerRegBM.RiskS,
            //       Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //       RevGroups = o.CustomerSummarize?.ReviewContent,
            //       CheckType = o.CustomerRegBM.PostState,
            //       TypeWork = o.CustomerRegBM.TypeWork,
            //    // MedicalAdvice = o.MedicalAdvice,
            //    InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
            //       OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
            //       OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
            //       SumAdvice = o.CustomerSummarize?.Advice,
            //       OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
            //       Address = o.CustomerRegBM.Customer.Address,
            //       BookingDate = o.CustomerRegBM.BookingDate.ToString(),
            //       IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
            //       LoginDate = o.CustomerRegBM.LoginDate.ToString(),
            //       Mobile = o.CustomerRegBM.Customer.Mobile,
            //       Nation = o.CustomerRegBM.Customer.Nation,
            //   }).Select(o => new OccDissCuslis
            //   {
            //       Age = o.Key.Age.ToString(),
            //       Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count() == 1
            //      ? o.FirstOrDefault().Conclusion :
            //      string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

            //    // Conlutions = string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),
            //    Name = o.Key.Name,
            //       Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
            //      ? o.FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),

            //    //Opton = string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),
            //    Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),
            //       CustomerBM = o.Key.CustomerBM,
            //       RiskNames = o.Key.RiskNames,
            //       Sex = o.Key.Sex,
            //       RevGroups = o.Key.RevGroups,
            //       CheckType = o.Key.CheckType,
            //       TypeWork = o.Key.TypeWork,
            //       MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),
            //       InjuryAge = o.Key.InjuryAge,
            //       OccIll = o.Key.OccIll,
            //       OtherIll = o.Key.OtherIll,
            //       SumAdvice = o.Key.SumAdvice,
            //       OccIllSummarize = o.Key.OccIllSummarize,
            //       Address = o.Key.Address,
            //       BookingDate = o.Key.BookingDate.ToString(),
            //       IDCardNo = o.Key.IDCardNo,
            //       LoginDate = o.Key.LoginDate.ToString(),
            //       Mobile = o.Key.Mobile,
            //       Nation = o.Key.Nation,
            //   }).Distinct().ToList();

            #endregion
            if (fccuslist.Count() == 0)
            {
                // no6 = "(无)";
                fccuslist.Add(new OccDissCuslis
                {
                    Conlutions = "无",
                    Name = "无",
                    Opton = "无",
                    Description = "无",
                    CustomerBM = "无",
                    RiskNames = "无",
                    Sex = "无",
                    RevGroups = "无",
                    CheckType = "无",
                    TypeWork = "无",
                    MedicalAdvice = "无",
                    InjuryAge = "无",
                    OccIll = "无",
                    OtherIll = "无",
                    OccIllSummarize = "无",
                    SumAdvice = "无",
                    Address = "无",
                    IDCardNo = "无",
                    Mobile = "无",
                    Nation = "无",
                    Age = "无",
                    BookingDate = "无",
                    LoginDate = "无",
                });
            }
            else
            {
                fccuslist[0].AllCount = fccuslist.Count();
                fccuslist[fccuslist.Count() - 1].AllCount = fccuslist.Count();
            }

            var qtyccuslist = getSumRS(CusItemResult, "其他异常");
            // //其他异常
            #region MyRegion
            //var qtyccuslist = OccCustomerReglis.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他异常") || o.Conclusion.Contains("其他疾病"))).GroupBy(o =>
            //new {
            //    Age = o.CustomerRegBM.Customer.Age.ToString(),
            //    // Conlutions = o.Conclusion,
            //    Name = o.CustomerRegBM.Customer.Name,
            //    //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
            //    // Opton = o.Advise,
            //    //Description = o.Description,
            //    CustomerBM = o.CustomerRegBM.CustomerBM,
            //    RiskNames = o.CustomerRegBM.RiskS,
            //    Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //    RevGroups = o.CustomerSummarize?.ReviewContent,
            //    CheckType = o.CustomerRegBM.PostState,
            //    TypeWork = o.CustomerRegBM.TypeWork,
            //    // MedicalAdvice = o.MedicalAdvice,
            //    InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
            //    OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
            //    OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
            //    SumAdvice = o.CustomerSummarize?.Advice,
            //    OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
            //    Address = o.CustomerRegBM.Customer.Address,
            //    BookingDate = o.CustomerRegBM.BookingDate.ToString(),
            //    IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
            //    LoginDate = o.CustomerRegBM.LoginDate.ToString(),
            //    Mobile = o.CustomerRegBM.Customer.Mobile,
            //    Nation = o.CustomerRegBM.Customer.Nation,
            //}).Select(o => new OccDissCuslis
            //{
            //    Age = o.Key.Age.ToString(),
            //    Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count() == 1
            //   ? o.FirstOrDefault().Conclusion :
            //   string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

            //    //Conlutions = string.Join(";", o.Where(n=>n.Conclusion !=null && n.Conclusion!="").Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),
            //    Name = o.Key.Name,


            //    Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
            //   ? o.FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),

            //    //Opton = string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),
            //    Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),
            //    CustomerBM = o.Key.CustomerBM,
            //    RiskNames = o.Key.RiskNames,
            //    Sex = o.Key.Sex,
            //    RevGroups = o.Key.RevGroups,
            //    CheckType = o.Key.CheckType,
            //    TypeWork = o.Key.TypeWork,
            //    MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),
            //    InjuryAge = o.Key.InjuryAge,
            //    OccIll = o.Key.OccIll,
            //    OtherIll = o.Key.OtherIll,
            //    SumAdvice = o.Key.SumAdvice,
            //    OccIllSummarize = o.Key.OccIllSummarize,
            //    Address = o.Key.Address,
            //    BookingDate = o.Key.BookingDate.ToString(),
            //    IDCardNo = o.Key.IDCardNo,
            //    LoginDate = o.Key.LoginDate.ToString(),
            //    Mobile = o.Key.Mobile,
            //    Nation = o.Key.Nation,
            //}).Distinct().ToList(); 
            #endregion

            if (qtyccuslist.Count > 0)
            {
                foreach (var q in qtyccuslist)
                {
                    if (q.MedicalAdvice == null || q.MedicalAdvice == "")
                    {
                        q.MedicalAdvice = "无";
                    }
                    if (q.OccIll == null || q.OccIll == "")
                    {
                        q.OccIll = "无";
                    }
                    if (q.Conlutions == null || q.Conlutions == "")
                    {
                        q.Conlutions = "无";
                    }
                    if (q.Description == null || q.Description == "")
                    {
                        q.Description = "无";
                    }
                }

                qtyccuslist[0].AllCount = qtyccuslist.Count();
                qtyccuslist[qtyccuslist.Count() - 1].AllCount = qtyccuslist.Count();
                
            }
            if (qtyccuslist.Count() == 0)
            {
                //  no7 = "(无)";
                qtyccuslist.Add(new OccDissCuslis
                {
                   
                    Name = "无",
                    Sex = "无",
                    Conlutions = "无",
                    Opton = "无",
                    Description = "无",
                    CustomerBM = "无",
                    RiskNames = "无",
                    RevGroups = "无",
                    CheckType = "无",
                    TypeWork = "无",
                    MedicalAdvice = "无",
                    InjuryAge = "无",
                    OccIllSummarize = "无",
                    SumAdvice = "无",
                    Address = "无",
                    IDCardNo = "无",
                    Mobile = "无",
                    Nation = "无",
                    Age = "无",
                    BookingDate = "无",
                    LoginDate = "无",
                });
            }

            //未见异常人员名单
            var zccuslist = getSumRS(CusItemResult, "未见异常");
            #region MyRegion
            //var zccuslist = OccCustomerReglis.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).GroupBy(o =>
            //new {
            //    Age = o.CustomerRegBM.Customer.Age.ToString(),
            //    // Conlutions = o.Conclusion,
            //    Name = o.CustomerRegBM.Customer.Name,
            //    //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
            //    // Opton = o.Advise,
            //    //Description = o.Description,
            //    CustomerBM = o.CustomerRegBM.CustomerBM,
            //    RiskNames = o.CustomerRegBM.RiskS,
            //    Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
            //    RevGroups = o.CustomerSummarize?.ReviewContent,
            //    CheckType = o.CustomerRegBM.PostState,
            //    TypeWork = o.CustomerRegBM.TypeWork,
            //    // MedicalAdvice = o.MedicalAdvice,
            //    InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
            //    OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
            //    OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
            //    SumAdvice = o.CustomerSummarize?.Advice,
            //    OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
            //    Address = o.CustomerRegBM.Customer.Address,
            //    BookingDate = o.CustomerRegBM.BookingDate.ToString(),
            //    IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
            //    LoginDate = o.CustomerRegBM.LoginDate.ToString(),
            //    Mobile = o.CustomerRegBM.Customer.Mobile,
            //    Nation = o.CustomerRegBM.Customer.Nation,
            //}).Select(o => new OccDissCuslis
            //{
            //    Age = o.Key.Age.ToString(),
            //    Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count() == 1
            //   ? o.FirstOrDefault().Conclusion :
            //   string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

            //    //Conlutions = string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),
            //    Name = o.Key.Name,
            //    Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
            //   ? o.FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),
            //    // Opton = string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),
            //    Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),
            //    CustomerBM = o.Key.CustomerBM,
            //    RiskNames = o.Key.RiskNames,
            //    Sex = o.Key.Sex,
            //    RevGroups = o.Key.RevGroups,
            //    CheckType = o.Key.CheckType,
            //    TypeWork = o.Key.TypeWork,
            //    MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),
            //    InjuryAge = o.Key.InjuryAge,
            //    OccIll = o.Key.OccIll,
            //    OtherIll = o.Key.OtherIll,
            //    SumAdvice = o.Key.SumAdvice,
            //    OccIllSummarize = o.Key.OccIllSummarize,
            //    Address = o.Key.Address,
            //    BookingDate = o.Key.BookingDate.ToString(),
            //    IDCardNo = o.Key.IDCardNo,
            //    LoginDate = o.Key.LoginDate.ToString(),
            //    Mobile = o.Key.Mobile,
            //    Nation = o.Key.Nation,
            //}).Distinct().ToList(); 
            #endregion
            if (zccuslist.Count() == 0)
            {
                //no8 = "(无)";
                zccuslist.Add(new OccDissCuslis
                {

                    Name = "无",
                    Sex = "无",
                    Conlutions = "无",

                    Opton = "无",
                    Description = "无",
                    CustomerBM = "无",
                    RiskNames = "无",

                    RevGroups = "无",
                    CheckType = "无",
                    TypeWork = "无",
                    MedicalAdvice = "无",
                    InjuryAge = "无",
                    OccIllSummarize = "无",
                    SumAdvice = "无",
                    Address = "无",
                    IDCardNo = "无",
                    Mobile = "无",
                    Nation = "无",
                    Age = "无",
                    BookingDate = "无",
                    LoginDate = "无",
                });
            }
            else
            {
                zccuslist[0].AllCount = zccuslist.Count();
                zccuslist[zccuslist.Count() - 1].AllCount = zccuslist.Count();
            }
            //未检名单
            int notcheck = (int)ProjectIState.Not;
            var cusnocheckgroup = groupClientCusDtos.Where(o => o.CustomerItemGroup.Any(n => n.CheckState == notcheck &&
            n.DepartmentBM?.Category != "耗材" && n.IsAddMinus != (int)AddMinusType.Minus)).ToList();
            var nochels = cusnocheckgroup.Select(o => new OccDissCuslis
            {
                CustomerBM = o.CustomerBM,
                Age = o.Customer.Age.ToString(),
                Name = o.Customer.Name,
                Sex = SexHelper.CustomSexFormatter(o.Customer.Sex),
                NoCheckGroups = string.Join(",", o.CustomerItemGroup.Where(a => a.CheckState == notcheck && 
                a.DepartmentBM?.Category != "耗材" && a.IsAddMinus != (int)AddMinusType.Minus).Select(r => r.ItemGroupName).ToList()),

            }).Distinct().ToList();
            
            if (groupClientSumDtos.Count == 0)
            { no10 = "(无)"; }
            //体检类型
            var PostState = OccCustomerReglis.Select(o => o.CustomerRegBM.PostState).ToList();
            //单位体检组合
            var groups = groupClientCusDtos.SelectMany(o => o.CustomerItemGroup).Where
                (n => n.CheckState != notcheck && n.DepartmentBM?.Category != "耗材" && n.IsAddMinus != (int)AddMinusType.Minus).OrderBy(o=>o.DepartmentBM?.Name).Select(o=>o.ItemGroupName).Distinct().ToList();
            //危害因素
            var whys = OccCustomerReglis.SelectMany(o => o.CustomerRegBM.OccHazardFactors).Select(o=>o.Text).Distinct().ToList();

            //首页
            SY(no4,no5,no6,no7,no8,no9,no10,PostState,star,end);
            //职业病检查基本情况
            SYBise(nochels.Count(), PostState, whys, groups, star, end, Reference, fccuslist);
            if (nochels.Count() == 0)
            {
                // no9 = "(无)";
                nochels.Add(new OccDissCuslis
                {
                    CustomerBM = "无",
                    Name = "无",
                    Sex = "无",
                    NoCheckGroups = "无",
                });
            }
            // 目标疾病
            Target();
            // 目标疾病危害因素
            Targetrisk();
            //复查项目
            ReGroup();
            // 体检结果分析与结论统计
            ReSultCount();
            //人员总检
            AllOccDissCus(Allyscuslist);
            // 疑似职业病人员名单
            OccDissCus(yscuslist);
            //疑似禁忌证人员名单
            OccConCus(jjzcuslist);
            // 复查人员名单
            OccReviewCus(fccuslist);
            // 与职业危害因素无关的其它疾病异常人员名单
            OccOtherCus(qtyccuslist);
            // 体检目前未见异常人员名单
            OccNomalCus(zccuslist);
            // 有未检项目名单
            OccNoCheckCus(nochels);
            // 主要异常分析
            OtherIllCount();
            #endregion
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 70)?.Remarks;
    
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

        private List<OccDissCuslis> getSumRS(List<OccCustomerItemDto> CusItemResult, string sumName)
        {
            var que = OccCustomerReglis.Where(o => o.Conclusion != null);
            if (!string.IsNullOrEmpty(sumName))
            {
                if (sumName == "其他异常")
                {
                    que = que.Where(o => o.Conclusion.Contains("其他异常") || o.Conclusion.Contains("其他疾病"));

                }
                else if (sumName == "未见异常")
                {
                    que = que.Where(o => o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"));
                }
                else
                {
                    que = que.Where(p => p.Conclusion.Contains(sumName));
                }
            }
            var Allyscuslist = que.GroupBy(o =>
           new {
               Age = o.CustomerRegBM.Customer.Age.ToString(),
               // Conlutions = o.Conclusion,
               Name = o.CustomerRegBM.Customer.Name,
               //OccDiss = o.OccCustomerOccDiseases == null ? "" : string.Join(",", o.OccCustomerOccDiseases?.Select(r => r.Text).ToList()),
               // Opton = o.Advise,
               //Description = o.Description,
               CustomerBM = o.CustomerRegBM.CustomerBM,
               RiskNames = o.CustomerRegBM.RiskS,
               Sex = o.CustomerRegBM.Customer.Sex == 1 ? "男" : "女",
               CheckType = o.CustomerRegBM.PostState,
               TypeWork = o.CustomerRegBM.TypeWork,
               // MedicalAdvice = o.MedicalAdvice,
               InjuryAge = o.CustomerRegBM.InjuryAge ?? "" + o.CustomerRegBM.InjuryAgeUnit ?? "",
               OccIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 1)?.Sum,
               OtherIll = CusItemResult.FirstOrDefault(r => r.CustomerRegId == o.CustomerRegBM.Id && r.SumTypeBM == 2)?.Sum,
               SumAdvice = o.CustomerSummarize?.Advice,
               OccIllSummarize = o.CustomerSummarize?.CharacterSummary,
               Address = o.CustomerRegBM.Customer.Address,
               BookingDate = o.CustomerRegBM.BookingDate.ToString(),
               IDCardNo = o.CustomerRegBM.Customer.IDCardNo,
               LoginDate = o.CustomerRegBM.LoginDate.ToString(),
               Mobile = o.CustomerRegBM.Customer.Mobile,
               Nation = o.CustomerRegBM.Customer.Nation,
           }).Select(o => new OccDissCuslis
           {
               Age = o.Key.Age.ToString(),
               Conlutions = o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.Conclusion).Distinct().Count() == 1
               ? o.Where(n => n.Conclusion != "" && n.Conclusion != null).FirstOrDefault().Conclusion :
               string.Join(";", o.Where(n => n.Conclusion != "" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),

               // Conlutions = string.Join(";", o.Where(n => n.Conclusion !="" && n.Conclusion != null).Select(p => p.OccHazardFactors.Text + ":" + p.Conclusion)),
               Name = o.Key.Name,
               OccDiss = o.SelectMany(p => p.OccCustomerOccDiseases) == null ? "" : string.Join(",", o.SelectMany(p => p.OccCustomerOccDiseases)?.Select(r => r.Text).ToList()),
               Opton = o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.Advise).Distinct().Count() == 1
               ? o.Where(n => n.Advise != "" && n.Advise != null).FirstOrDefault().Advise : string.Join(";", o.Where(n => n.Advise != "" && n.Advise != null).Select(p => p.OccHazardFactors.Text + ":" + p.Advise + "\r\n")),
                 IllName=string.Join("\r\n", groupClientSumDtos.Where(p=>p.CustomerReg.CustomerBM == o.Key.CustomerBM).Select(p=>p.SummarizeName).ToList())??"未见明显异常",
               Description = string.Join(";", o.Where(n => n.Description != "" && n.Description != null).Select(p => p.Description)),
               CustomerBM = o.Key.CustomerBM,
               RiskNames = o.Key.RiskNames,
               Sex = o.Key.Sex,
               CheckType = o.Key.CheckType,
               TypeWork = o.Key.TypeWork,
               MedicalAdvice = string.Join(";", o.Where(n => n.MedicalAdvice != "" 
               && n.MedicalAdvice != null).Select(p => p.MedicalAdvice)),
               InjuryAge = o.Key.InjuryAge,
               OccIll = o.Key.OccIll,
               OtherIll = o.Key.OtherIll,
               SumAdvice = o.Key.SumAdvice,
               OccIllSummarize = o.Key.OccIllSummarize,
               Address = o.Key.Address,
               BookingDate = o.Key.BookingDate.ToString(),
               IDCardNo = o.Key.IDCardNo,
               LoginDate = o.Key.LoginDate.ToString(),
               Mobile = o.Key.Mobile,
               Nation = o.Key.Nation,
               InspectionCycle = occTargetCountDtos.FirstOrDefault(p=> o.Key.RiskNames.Contains(p.RiskNames)
                && p.ChckType==  o.Key.CheckType)?.InspectionCycle
           }).Distinct().ToList();
            return Allyscuslist;
        }     
        /// <summary>
        /// 主报表及主页
        /// </summary>
        private void SY(string n4,string n5, string n6, string n7, string n8, string n9, string n10,List<string>post,string star ,string end)
        {
          
                var reportJson = new ReportJson();
            reportJson.Detail = new List<CusInfo>();
           
            var cusInfo = new CusInfo();
            cusInfo.ClientRegBM = clientRegDto.ClientRegBM;
            cusInfo.ClientBM = clientRegDto.ClientInfo.ClientBM;
            cusInfo.ClientName = clientRegDto.ClientInfo.ClientName;
            cusInfo.ClientAbbreviation = clientRegDto.ClientInfo.ClientAbbreviation;
            cusInfo.EndCheckDate = clientRegDto.EndCheckDate.ToString("yyyy-MM-dd");
            cusInfo.StartCheckDate = clientRegDto.StartCheckDate.ToString("yyyy-MM-dd");
            cusInfo.SearchStartDate = star;
            cusInfo.SearchEndDate = end;
            cusInfo.linkMan = clientRegDto.linkMan;
            cusInfo.ClientlinkMan = clientRegDto.ClientInfo.LinkMan;
            cusInfo.Address = clientRegDto.ClientInfo.Address;
            cusInfo.ClientEmail = clientRegDto.ClientInfo.ClientEmail;
            cusInfo.Telephone = clientRegDto.ClientInfo.Telephone;
            cusInfo.RegInfo = clientRegDto.RegInfo;
            cusInfo.Remark = clientRegDto.Remark;
            //新增加字段
            //行业

            if (!string.IsNullOrEmpty(clientRegDto.ClientInfo.Clientlndutry))
            {
                var clientlndutry = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientlndutry);
                var Clientlndutry = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientlndutry);
                cusInfo.Clientlndutry = Clientlndutry.FirstOrDefault(p => p.Value == Convert.ToInt32(clientRegDto.ClientInfo.Clientlndutry))?.Text;
            }
            //合同性质
            var clientcontract = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.Clientcontract);
            var Clientcontract = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == clientcontract);
            cusInfo.Clientcontract = Clientcontract.FirstOrDefault(p => p.Value == Convert.ToInt32(clientRegDto.ClientInfo.Clientcontract))?.Text;
            //企业规模
            var ScaleType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.ScaleType);
            var ScaleTypes = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == ScaleType);
            cusInfo.Scale = ScaleTypes.FirstOrDefault(p => p.Value == clientRegDto.ClientInfo.Scale)?.Text;
         
            

            //经济类型
            var EconomicType = Enum.GetName(typeof(BasicDictionaryType), BasicDictionaryType.EconomicsType);
            var EconomicTypes = DefinedCacheHelper.GetBasicDictionary().Where(o => o.Type == EconomicType);
            cusInfo.EconomicType= EconomicTypes.FirstOrDefault(p => p.Value == clientRegDto.ClientInfo.EconomicType)?.Text;
           

            cusInfo.SocialCredit = clientRegDto.ClientInfo.SocialCredit;
            cusInfo.RegInfo = clientRegDto.RegInfo;
            foreach (var p in post)
            {
                if (p == null)
                {
                    continue;
                }
                if (p.Contains("上岗"))
                {
                    cusInfo.sgq = "√";

                }
               else  if (p.Contains("在岗"))
                {
                    cusInfo.zgs = "√";

                }
                else if (p.Contains("离岗时"))
                {
                    cusInfo.lgs = "√";

                }
                else if (p.Contains("离岗后"))
                {
                    cusInfo.lgh = "√";

                }
                else if (p.Contains("应急"))
                {
                    cusInfo.yj  = "√";

                }
            }
            //总人数
            cusInfo.AllCount = allCount.ToString();
            //已检人数
            cusInfo.CheckCount = CheckCount.ToString();
            //未检人数
            cusInfo.NoCheckCount = (allCount- CheckCount).ToString();

            cusInfo.ReCountCount = ReCount.ToString();
            cusInfo.InjuryCount = ClientCusDtos.Where(p => p.RiskS != "" && p.RiskS != null).Count().ToString();
            //根据目标疾病获取危害因素并去重
            var risklist= occTargetCountDtos.Select(p => p.RiskNames).ToList();
            string risks = string.Join("、", risklist);
            var risklists = risks.Split('、').ToList();
            cusInfo.RiskName = string.Join("、", risklists);

            reportJson.Detail.Add(cusInfo);
            cusInfos = reportJson.Detail.ToList();

            //主报表
            var MreportJson = new MainReportJson();
            MreportJson.Detail = new List<MCusInfo>();
            var cusInfoM = new MCusInfo();
            cusInfoM.ClientRegBM1 = clientRegDto.ClientRegBM;
            cusInfoM.ClientBM1 = clientRegDto.ClientInfo.ClientBM;
            cusInfoM.ClientName1 = clientRegDto.ClientInfo.ClientName;
            cusInfoM.ClientAbbreviation1 = clientRegDto.ClientInfo.ClientAbbreviation;
            cusInfoM.EndCheckDate1 = clientRegDto.EndCheckDate.ToString("yy-MM-dd");
            cusInfoM.StartCheckDate1 = clientRegDto.StartCheckDate.ToString("yy-MM-dd");
            //cusInfo.SearchStartDate = star;
            //cusInfo.SearchEndDate = end;
            cusInfoM.linkMan1 = clientRegDto.linkMan;
            cusInfoM.Address1 = clientRegDto.ClientInfo.Address;
            cusInfoM.ClientEmail1 = clientRegDto.ClientInfo.ClientEmail;
            cusInfoM.Telephone1 = clientRegDto.ClientInfo.Telephone;
            cusInfoM.No4 = n4;
            cusInfoM.No5 = n5;
            cusInfoM.No6 = n6;
            cusInfoM.No7 = n7;
            cusInfoM.No8 = n8;
            cusInfoM.No9 = n9;
            cusInfoM.No10 = n10;
            foreach (var p in post)
            {if (p == null)
                {
                    continue;
                }
                if (p.Contains("上岗"))
                {
                    cusInfo.sgq = "√";

                }
                else if (p.Contains("在岗"))
                {
                    cusInfo.zgs = "√";

                }
                else if (p.Contains("离岗时"))
                {
                    cusInfo.lgs = "√";

                }
                else if (p.Contains("离岗后"))
                {
                    cusInfo.lgh = "√";

                }
                else if (p.Contains("应急"))
                {
                    cusInfo.yj = "√";

                }
            }
            MreportJson.Detail.Add(cusInfoM);          


            var reportJsonStringsy = JsonConvert.SerializeObject(MreportJson);
            ReportMain.LoadDataFromXML(reportJsonStringsy);

            var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
            if (ReportMain.ControlByName("首页") != null)
            {
                var subReport = ReportMain.ControlByName("首页").AsSubReport.Report;
                subReport.LoadDataFromXML(reportJsonString1);
            }
           

            
        }
        /// <summary>
        /// 职业病检查基本情况
        /// </summary>
        private void SYBise(int NoCount, List<string> post, List<string> wh, List<string> group, string Star, string End, string Reference,
            List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("职业健康检查基本情况") == null)
            {
                return;
            }
            int notcheck = (int)ProjectIState.Not;
            var reportJson = new ReportBiseJson();

            //车间           
            var bmls = OccCustomerReglis.GroupBy(o => o.CustomerRegBM.WorkName).Select(o => new
            {
                workname = o.Key,
                worknamecount = o.Select(p=>p.CustomerRegBMId).Distinct().Count()
            });
            string worknameDetail = "";
            foreach (var bm in bmls)
            {
                var wordkname = bm.workname ?? "";
                if (wordkname == "")
                {
                    wordkname = "无部门";
                }

                worknameDetail += bm.workname + bm.worknamecount ?? "";
                worknameDetail += "人" + "、";

            }
            //工种
            var gzlsall = OccCustomerReglis.Select(p => new { p.CustomerRegBMId, TypeWork = p.CustomerRegBM.TypeWork }).Distinct().ToList();
            var gzls = gzlsall.GroupBy(o => o.TypeWork).Select(o => new
            {
                workname = o.Key,
                worknamecount = o.Count()
            });
            string TypeWorkDetail = "";
            foreach (var bm in gzls)
            {
                var wordkname = bm.workname ?? "";
                if (wordkname == "")
                {
                    wordkname = "无工种";
                }

                TypeWorkDetail += bm.workname + bm.worknamecount ?? "";
                TypeWorkDetail += "人" + "、";

            }
            //岗位类别上岗前应检人数18人，实检人数18人；
            var Postls = ClientCusDtos.GroupBy(o => o.PostState).Select(o => new
            {
                PostState = o.Key,
                postShoudcount = o.Where(p => p.SummSate == (int)SummSate.Audited).Count(),
                Postcount = o.Count()
            });
            //岗位类别危害因素人数
            string PostDetail = "";
            string PostRiskDetail = "";
            string PostSumDetail = "";
            foreach (var bm in Postls)
            {
                //岗位类型人数统计
                var PostState = bm.PostState ?? "";
                if (PostState == "")
                {
                    PostState = "无";
                }
                var rs = bm.PostState + "应检人数" + bm.Postcount ?? "";
                rs += "人，实检人数" + bm.postShoudcount ?? "";
                rs += "人；\r\n";
                PostDetail += rs;
                PostRiskDetail += PostState + "\r\n";
                //岗位类别危害因素统计
                var riskcuslist = ClientCusDtos.Where(p => p.PostState == PostState).
                    GroupBy(o => o.RiskS).Select(o => new
                    {
                        RiskS = o.Key,
                        RiskSShoudcount = o.Where(p => p.SummSate == (int)SummSate.Audited).Count(),
                        RiskScount = o.Count()
                    });
                foreach (var RiskInfo in riskcuslist)
                {

                    //接触粉尘、噪声作业人员：应检   人，实检    人；
                    var Riskrs = "接触" + RiskInfo.RiskS + "应检人数" + RiskInfo.RiskScount ?? "";
                    Riskrs += "人，实检人数" + RiskInfo.RiskSShoudcount ?? "";
                    Riskrs += "人；\r\n";
                    PostRiskDetail += Riskrs + "\r\n";
                }
                //岗位类别总检结论统计
                //上岗前：职业禁忌症、复查 的统计人数
                //在岗期间：疑似职业病、职业禁忌症、复查 的统计人数
                //离岗后：疑似职业病 的统计人数
                //离岗时：疑似职业病、复查 的统计人数

                if (bm.PostState!=null &&  bm.PostState.Contains("上岗"))
                {
                    var gq = "上岗前：" + "职业禁忌症:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("禁忌证")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人、";
                    gq += "复查:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("复查")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人；";
                    PostSumDetail += gq + "\r\n";
                }
                else if (bm.PostState != null && bm.PostState.Contains("在岗"))
                {
                    var gq = "在岗期间：" + "疑似职业病:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion!=null && p.Conclusion.Contains("职业病")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人、";

                    gq += "职业禁忌症:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("禁忌证")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人、";
                    gq += "复查:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("复查")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人；";
                    PostSumDetail += gq + "\r\n";
                }
                else if (bm.PostState != null && bm.PostState.Contains("离岗后"))
                {
                    var gq = "离岗后：" + "疑似职业病:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("职业病")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人；";
                    PostSumDetail += gq + "\r\n";
                }
                else if (bm.PostState != null && bm.PostState.Contains("离岗时"))
                {
                    var gq = "离岗时：" + "疑似职业病:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("职业病")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人、";
                    gq += "复查:" + OccCustomerReglis.Where(p => p.CustomerRegBM.PostState == bm.PostState && p.Conclusion != null && p.Conclusion.Contains("复查")).Select(p => p.CustomerRegBMId).Distinct().Count() + "人、";
                    PostSumDetail += gq + "\r\n";
                }
            }

            //            噪声：疑似职业疾病：*人，职业禁忌证：*人，复查：*人，目前未见异常：*人，其他疾病异常：*人，有未检项目：*人。
            //苯：疑似职业疾病：*人，职业禁忌证：*人，复查：*人，目前未见异常：*人，其他疾病异常：*人，有未检项目：*人。
            //var riskSum = OccCustomerReglis.GroupBy(p => p.OccHazardFactorsId).Select(p => new
            //{
            //    riskname = p.FirstOrDefault().OccHazardFactors.Text,
            //    allcont=p.Count(),
            //    zybrs = p.Where(o =>  o.Conclusion != null &&  o.Conclusion.Contains("职业病")).Select(n => n.CustomerRegBMId).Distinct().Count(),
            //    jjzrs = p.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).Select(n => n.CustomerRegBMId).Distinct().Count(),
            //    fczrs = p.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).Select(n => n.CustomerRegBMId).Distinct().Count(),
            //    wjycrs = p.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(n => n.CustomerRegBMId).Distinct().Count(),
            //    qtycrs = p.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常"))).Select(n => n.CustomerRegBMId).Distinct().Count()
            //});
            //string risksumstr = string.Join("\r\n", riskSum.Select(p => p.riskname + ":" + "疑似职业病:" + p.zybrs + "人,"
            //+ "职业禁忌证:" + p.jjzrs + "人," + "复查:" + p.fczrs + "人," + "目前未见异常:" + p.wjycrs + "人,"
            //+ "其他疾病异常:" + p.qtycrs + "人。"));
            //危害因素岗位类别人数
            string riskpostsumstr = "";
           
            var riskpostSumlist = OccCustomerReglis.GroupBy(p => new { p.OccHazardFactorsId ,p.OccHazardFactors.Text}).
                Select(p=> p.Key).Distinct().ToList();
            foreach (var riskpost in riskpostSumlist)
            {
                
                var riskcount = OccCustomerReglis.Where(p=>p.OccHazardFactorsId== riskpost.OccHazardFactorsId).Select(p=>p.CustomerRegBMId).Count();
               


                var sgriskpostlsit = OccCustomerReglis.Where(p => p.OccHazardFactorsId == riskpost.OccHazardFactorsId &&
                p.CustomerRegBM.PostState !=null &&
                p.CustomerRegBM.PostState.Contains("上岗")).ToList();
                var riskpostcount = sgriskpostlsit.Select(p => p.CustomerRegBMId).Distinct().Count();
                string riskstr = "接触\"" + riskpost.Text + "\"共" + riskcount + "人：上岗前共" + riskpostcount+"人。";
                if (riskpostcount > 0)
                {
                    var zybrs = sgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var jjzrs = sgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var fczrs = sgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var wjycrs = sgriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var qtycrs = sgriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    riskstr += "疑似职业病:" + zybrs + "人," + "职业禁忌证:" + jjzrs + "人," + "复查:" + fczrs + "人," + "目前未见异常:" + wjycrs + "人,"
          + "其他疾病异常:" + qtycrs + "人。";
                }

                var zgriskpostlsit = OccCustomerReglis.Where(p => p.OccHazardFactorsId == riskpost.OccHazardFactorsId  &&
                  p.CustomerRegBM.PostState != null && 
                p.CustomerRegBM.PostState.Contains("在岗")).ToList();
                var zgriskpostcount = zgriskpostlsit.Select(p => p.CustomerRegBMId).Distinct().Count();
                  riskstr  += "\r\n在岗期间共" + zgriskpostcount + "人。";
                if (zgriskpostcount > 0)
                {
                    var zybrs = zgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var jjzrs = zgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var fczrs = zgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var wjycrs = zgriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var qtycrs = zgriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    riskstr += "疑似职业病:" + zybrs + "人," + "职业禁忌证:" + jjzrs + "人," + "复查:" + fczrs + "人," + "目前未见异常:" + wjycrs + "人,"
          + "其他疾病异常:" + qtycrs + "人。";
                }

                var lgriskpostlsit = OccCustomerReglis.Where(p => p.OccHazardFactorsId == riskpost.OccHazardFactorsId &&
                  p.CustomerRegBM.PostState != null && 
             p.CustomerRegBM.PostState.Contains("离岗时")).ToList();
                var lgriskpostcount = lgriskpostlsit.Select(p => p.CustomerRegBMId).Distinct().Count();
                riskstr += "\r\n离岗时共" + lgriskpostcount + "人。";
                if (lgriskpostcount > 0)
                {
                    var zybrs = lgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var jjzrs = lgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var fczrs = lgriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var wjycrs = lgriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var qtycrs = lgriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    riskstr += "疑似职业病:" + zybrs + "人," + "职业禁忌证:" + jjzrs + "人," + "复查:" + fczrs + "人," + "目前未见异常:" + wjycrs + "人,"
          + "其他疾病异常:" + qtycrs + "人。";
                }
                var lghriskpostlsit = OccCustomerReglis.Where(p => p.OccHazardFactorsId == riskpost.OccHazardFactorsId  &&
                  p.CustomerRegBM.PostState != null && 
           p.CustomerRegBM.PostState.Contains("离岗后")).ToList();
                var lghriskpostcount = lghriskpostlsit.Select(p => p.CustomerRegBMId).Distinct().Count();
                riskstr += "\r\n离岗后共" + lghriskpostcount + "人。";
                if (lgriskpostcount > 0)
                {
                    var zybrs = lghriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var jjzrs = lghriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var fczrs = lghriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var wjycrs = lghriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var qtycrs = lghriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    riskstr += "疑似职业病:" + zybrs + "人," + "职业禁忌证:" + jjzrs + "人," + "复查:" + fczrs + "人," + "目前未见异常:" + wjycrs + "人,"
          + "其他疾病异常:" + qtycrs + "人。";
                }
                var yjriskpostlsit = OccCustomerReglis.Where(p => p.OccHazardFactorsId == riskpost.OccHazardFactorsId &&
                  p.CustomerRegBM.PostState != null && 
       p.CustomerRegBM.PostState.Contains("应急")).ToList();
                var yjriskpostcount = yjriskpostlsit.Select(p => p.CustomerRegBMId).Distinct().Count();
                riskstr += "\r\n应急共" + yjriskpostcount + "人。";
                if (yjriskpostcount > 0)
                {
                    var zybrs = yjriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var jjzrs = yjriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var fczrs = yjriskpostlsit.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var wjycrs = yjriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    var qtycrs = yjriskpostlsit.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常"))).Select(n => n.CustomerRegBMId).Distinct().Count();
                    riskstr += "疑似职业病:" + zybrs + "人," + "职业禁忌证:" + jjzrs + "人," + "复查:" + fczrs + "人," + "目前未见异常:" + wjycrs + "人,"
          + "其他疾病异常:" + qtycrs + "人。";
                }
                riskpostsumstr += riskstr + "\r\n";
            }
            //危害因素结论
            var riskSumlist = OccCustomerReglis.GroupBy(p => new { p.OccHazardFactorsId, p.Conclusion }).
                Select(p => new { p.Key.OccHazardFactorsId, p.Key.Conclusion }).OrderBy(p=>p.OccHazardFactorsId).ToList();
            var riskSumstr = "";
            foreach (var riskSum in riskSumlist)
            {
                var sumstr = "";
                var occsumlist= OccCustomerReglis.Where(p => p.OccHazardFactorsId == riskSum.OccHazardFactorsId && p.Conclusion == riskSum.Conclusion).ToList();
                var Count= occsumlist.Select(p => p.CustomerRegBMId).Count();
                sumstr = "发现" + occsumlist.FirstOrDefault()?.OccHazardFactors.Text + riskSum.Conclusion;
                if (!string.IsNullOrEmpty( riskSum.Conclusion) && riskSum.Conclusion.Contains("禁忌证"))
                {
                    var occnamelist = occsumlist.SelectMany(p => p.OccDictionarys).Select(p => p.Text).ToList();

                    var occnamestr = string.Join(";", occnamelist);
                    sumstr += "(" + occnamestr + ")";
                }
               else  if (!string.IsNullOrEmpty(riskSum.Conclusion) && riskSum.Conclusion.Contains("职业病"))
                {

                    var occnamelist = occsumlist.SelectMany(p => p.OccCustomerOccDiseases).Select(p => p.Text).ToList();
                    var occnamestr = string.Join(";", occnamelist);
                    sumstr += "(" + occnamestr + ")";
                }
                sumstr += Count + "人," + occsumlist.FirstOrDefault().Advise +"。";
                riskSumstr += sumstr;
            }

            var relist = CusReviewGroups.GroupBy(p => p.ItemGroupName).Select
                     (p => new { GroupName = p.Key, Cout = p.Count() });
            var fczh = ""; 
            int xuh = 0;
            foreach (var recus in relist)
            {
                xuh = xuh + 1;
                fczh += xuh + "、" + recus.GroupName + recus.Cout + "人次";
            }
            
            //接害工龄
            //var jhgls = OccCustomerReglis.Select(o=>new { o.CustomerRegBM.InjuryAge, InjuryAgeUnit= o.CustomerRegBM.InjuryAgeUnit==""?"年": o.CustomerRegBM.InjuryAgeUnit }).
            //    GroupBy(o =>new { o.InjuryAge, o.InjuryAgeUnit }).Select(o => new
            //{
            //    workname = o.Key.InjuryAge,
            //    o.Key.InjuryAgeUnit,
            //    worknamecount = o.Count()
            //});
            var jhgls = OccCustomerReglis.Select(o => new { o.CustomerRegBMId, o.CustomerRegBM.InjuryAge, InjuryAgeUnit = o.CustomerRegBM.InjuryAgeUnit == "" ? "年" : o.CustomerRegBM.InjuryAgeUnit == null ?"年": o.CustomerRegBM.InjuryAgeUnit }).Distinct().ToList();

            
            var  years5 = jhgls.Where(o => o.InjuryAge != "" && o.InjuryAge != null && Convert.ToDecimal(o.InjuryAge) <= 5
             && o.InjuryAgeUnit !=null &&  !o.InjuryAgeUnit.Contains("月") && !o.InjuryAgeUnit.Contains("天")).Count();

            var years5M = jhgls.Where(o => o.InjuryAge != "" && o.InjuryAge != null
            && (Convert.ToDecimal(o.InjuryAge)/12) <= 5
             && o.InjuryAgeUnit != null && o.InjuryAgeUnit.Contains("月")).Count();
            years5 = years5 + years5M;

            var years10 = jhgls.Where(o => o.InjuryAge != "" && o.InjuryAge != null &&
            Convert.ToDecimal(o.InjuryAge) > 5 && Convert.ToDecimal(o.InjuryAge) <= 10
            && o.InjuryAgeUnit != null && !o.InjuryAgeUnit.Contains("月") && !o.InjuryAgeUnit.Contains("天")).Count();

            var years10M = jhgls.Where(o => o.InjuryAge != "" && o.InjuryAge != null &&
     ( Convert.ToDecimal(o.InjuryAge) / 12) > 5 && (Convert.ToDecimal(o.InjuryAge)/12) <= 10 && o.InjuryAgeUnit != null
      && o.InjuryAgeUnit.Contains("月")).Count();
            years10 = years10 + years10M;

            var yearsM = jhgls.Where(o => o.InjuryAge != "" && o.InjuryAge != null && Convert.ToDecimal(o.InjuryAge) > 10
            && o.InjuryAgeUnit != null && !o.InjuryAgeUnit.Contains("月") && !o.InjuryAgeUnit.Contains("天")).Count();
            var yearsMM = jhgls.Where(o => o.InjuryAge != "" && o.InjuryAge != null &&
            (Convert.ToDecimal(o.InjuryAge)/12) > 10
           && o.InjuryAgeUnit != null && o.InjuryAgeUnit.Contains("月")).Count();
            yearsM = yearsM + yearsMM;


            string InjuryAgeDetail = string.Format("0~5年：{0}人、5~9年：{1}人、10年以上：{2}人", years5, years10, yearsM);
     
           
            CusCount cusCount = new CusCount();
            cusCount.ReGroupCount = fczh;
            cusCount.AllReGroupCount= CusReviewGroups.Select(p => p.CustomerBM).Distinct().Count();
            cusCount.Address = cusInfos[0].Address;
            cusCount.ClientEmail = cusInfos[0].ClientEmail;
            cusCount.ClientName = cusInfos[0].ClientName;
            cusCount.EndCheckDate = cusInfos[0].EndCheckDate;
            cusCount.StartCheckDate = cusInfos[0].StartCheckDate;
            cusCount.Clientcontract = cusInfos[0].Clientcontract;
            cusCount.SocialCredit = cusInfos[0].SocialCredit;
            cusCount.Scale = cusInfos[0].Scale;
            cusCount.EconomicType= cusInfos[0].EconomicType;
            cusCount.ClientBM = cusInfos[0].ClientBM;

            cusCount.ClientRegBM = cusInfos[0].ClientRegBM;

            cusCount.Clientlndutry = cusInfos[0].Clientlndutry;
            cusCount.RegInfo = cusInfos[0].RegInfo;
            cusCount.SearchStartDate = Star;
            cusCount.SearchEndDate = End;

            cusCount.Telephone = cusInfos[0].Telephone;
            cusCount.linkMan = cusInfos[0].linkMan;
            cusCount.ClientlinkMan = cusInfos[0].ClientlinkMan;
            cusCount.AllCount = allCount;
            //已总检
            cusCount.CheckCount = CheckCount;           
            cusCount.CheckPer = ((decimal)CheckCount * 100 / (decimal)allCount).ToString("0.00").TrimEnd('0').TrimEnd('.') + "%";
            //已检查
            var checkNum = groupClientCusDtos.Count;
            cusCount.HasCheckPer=((decimal)checkNum * 100 / (decimal)allCount).ToString("0.00").TrimEnd('0').TrimEnd('.') + "%";

            cusCount.WorNameCount = worknameDetail.TrimEnd('、');
            cusCount.WorkTypeCount = TypeWorkDetail.TrimEnd('、');
            cusCount.PoststateCount = PostDetail;
            cusCount.PoststateRiskCount = PostRiskDetail;
            cusCount.PoststateSumCount = PostSumDetail;
            cusCount.riskSumCount = riskpostsumstr;
            cusCount.riskSum = riskSumstr;
            cusCount.YearsCount = InjuryAgeDetail.TrimEnd('、');
            var checkcusls = OccCustomerReglis.Where(o => o.CustomerRegBM.CheckSate != 1).ToList();
            cusCount.Age1 = checkcusls.Where(o => o.CustomerRegBM.Customer.Age <= 24).Select(p=>p.CustomerRegBMId).Distinct().Count();

            cusCount.Age2 = checkcusls.Where(o => o.CustomerRegBM.Customer.Age >=25 
            && o.CustomerRegBM.Customer.Age <=34).Select(p => p.CustomerRegBMId).Distinct().Count();

            cusCount.Age3 = checkcusls.Where(o => o.CustomerRegBM.Customer.Age >= 35
            && o.CustomerRegBM.Customer.Age <= 44).Select(p => p.CustomerRegBMId).Distinct().Count();

            cusCount.Age4 = checkcusls.Where(o => o.CustomerRegBM.Customer.Age >= 45
            && o.CustomerRegBM.Customer.Age <= 54).Select(p => p.CustomerRegBMId).Distinct().Count();

            cusCount.Age5 = checkcusls.Where(o => o.CustomerRegBM.Customer.Age >= 55
        && o.CustomerRegBM.Customer.Age <= 64).Select(p => p.CustomerRegBMId).Distinct().Count();         

            cusCount.Age6 = checkcusls.Where(o => o.CustomerRegBM.Customer.Age >= 64).Select(p => p.CustomerRegBMId).Distinct().Count();

           
                var subReport = ReportMain.ControlByName("职业健康检查基本情况").AsSubReport.Report;
            if (subReport.ControlByName("distributionchar") != null)
            {
                IGRChart pChartBar = subReport.ControlByName("distributionchar").AsChart;
                FillNormalValuesForNUM(pChartBar, cusCount);
            }
            
            
            reportJson.Detail = new List<CusCount>();
            //胸片、肺功能

            var cusFC = cuslist.Where(p=> p.RevGroups!=null && (p.RevGroups.Contains("胸") || p.RevGroups.Contains("肺功能"))).ToList();
            if (cusFC.Count>0)
            {
                var cusname = cusFC.Select(p => p.Name).ToList() ;
                cusCount.XFReCheckCount = cusFC.Count;
                cusCount.XFReCheckCusName = string.Join("、",cusname);

            }

            //cusCount.NoCheckCont = checkcusls.Where(o => o.Conclusion!=null &&  o.Conclusion.Contains("未检项目")).Count();
            cusCount.sgCount = ClientCusDtos.Where(p => p.PostState!=null && p.PostState.Contains("上岗")).Count();
            cusCount.zgCount = ClientCusDtos.Where(p => p.PostState != null && p.PostState.Contains("在岗")).Count();
            cusCount.lgsCount = ClientCusDtos.Where(p => p.PostState != null && p.PostState.Contains("离岗时")).Count();
            cusCount.lghCount = ClientCusDtos.Where(p => p.PostState != null && p.PostState.Contains("离岗后")).Count();
            cusCount.yjCount = ClientCusDtos.Where(p => p.PostState != null && p.PostState.Contains("应急")).Count();

            cusCount.NoCheckCont = NoCount;
            var NoIllCuslist= checkcusls.Where(o => o.Conclusion != null && (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常")));
 
            cusCount.sgUnnormalCount = NoIllCuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("上岗")).Select(p=>p.CustomerRegBMId).Distinct().Count();
            cusCount.zgUnnormalCount = NoIllCuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("在岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lgsUnnormalCount = NoIllCuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗时")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lghUnnormalCount = NoIllCuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗后")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.yjUnnormalCount = NoIllCuslist.Where(p =>  p.CustomerRegBM.PostState != null &&p.CustomerRegBM.PostState.Contains("应急")).Select(p => p.CustomerRegBMId).Distinct().Count();


            cusCount.NoIllCount = NoIllCuslist.Select(p => p.CustomerRegBMId).Distinct().Count();

            var jjzcuslist = checkcusls.Where(o => o.Conclusion != null && o.Conclusion.Contains("禁忌证")).ToList();
            cusCount.OccConCount= jjzcuslist.Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.sgjjzCount = jjzcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("上岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.zgjjzCount = jjzcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("在岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lgjjzCount = jjzcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗时")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lghjjzCount = jjzcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗后")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.yjjjzCount = jjzcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("应急")).Select(p => p.CustomerRegBMId).Distinct().Count();

            var zybcuslist = checkcusls.Where(o => o.Conclusion != null && o.Conclusion.Contains("职业病")).ToList();
            cusCount.OccDisCount= zybcuslist.Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.sgzybCount = zybcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("上岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.zgzybCount = zybcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("在岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lgzybCount = zybcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗时")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lghzybCount = zybcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗后")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.yjzybCount = zybcuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("应急")).Select(p => p.CustomerRegBMId).Distinct().Count();

            var OtherCus = checkcusls.Where(o => o.Conclusion != null && (o.Conclusion.Contains("其他疾病") || o.Conclusion.Contains("其他异常")));
            cusCount.OtherCount = OtherCus.Select(p => p.CustomerRegBMId).Distinct().Count();

            cusCount.sgnormalCount = OtherCus.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("上岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.zgnormalCount = OtherCus.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("在岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lgsnormalCount = OtherCus.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗时")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lghnormalCount = OtherCus.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗后")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.yjnormalCount = OtherCus.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("应急")).Select(p => p.CustomerRegBMId).Distinct().Count();

            var fccuslist = checkcusls.Where(o => o.Conclusion != null && o.Conclusion.Contains("复查")).ToList();
            cusCount.ReCheckCount = fccuslist.Select(p => p.CustomerRegBMId).Distinct().Count();

            cusCount.sgfcCount = fccuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("上岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.zgfcCount = fccuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("在岗")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lgfcCount = fccuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗时")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.lghfcCount = fccuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("离岗后")).Select(p => p.CustomerRegBMId).Distinct().Count();
            cusCount.yjfcCount = fccuslist.Where(p => p.CustomerRegBM.PostState != null && p.CustomerRegBM.PostState.Contains("应急")).Select(p => p.CustomerRegBMId).Distinct().Count();

            foreach (var p in post)
            {
                if (p == null)
                {
                    continue;
                }
                if (    p.Contains("上岗"))
                {
                    cusCount.sgq = "√";

                }
                else if (p.Contains("在岗"))
                {
                    cusCount.zgs = "√";

                }
                else if (p.Contains("离岗时"))
                {
                    cusCount.lgs = "√";

                }
                else if (p.Contains("离岗后"))
                {
                    cusCount.lgh = "√";

                }
                else if (p.Contains("应急"))
                {
                    cusCount.yj = "√";

                }
            }
            cusCount.whys = string.Join("、", wh).TrimEnd(',');
            cusCount.group= string.Join(",", group).TrimEnd(',');
            //目标疾病
            string risk = "";
            var risks = occTargetCountDtos.GroupBy(p=>p.RiskNames);
            int BM = 1;
            foreach (var riskname in risks )
            {
                // 1、煤尘
                //职业病：煤工尘肺
                //职业禁忌证：活动性肺结核病、慢性阻塞性肺病、慢性间质性肺病、伴肺功能损害得疾病
                string riskName = BM + "、" + riskname.FirstOrDefault()?.RiskNames + "\r\n";
                riskName += riskname.FirstOrDefault()?.Target ;
                risk += riskName + "\r\n";
                BM = BM + 1;
            }
            cusCount.RiskTarget = risk  ;
            
            var hsgroups= occTargetCountDtos.Select(p=>p.Groups);
            List<String> hsgroupName = new List<string>();
            foreach (var groupname in hsgroups)
            {
                var groupnames = groupname.Split('、').ToList();
                foreach (var gp in groupnames)
                {
                    if (!hsgroupName.Contains(gp))
                    {
                        hsgroupName.Add(gp);
                    }

                }

            }
           //必检项目汇总
            cusCount.Hasgroup= string.Join("、", hsgroupName).TrimEnd(',');
            cusCount.Reference = Reference;
            //检查类别汇总字段
            cusCount.PostState= string.Join("、", post).TrimEnd(',');

            //单位加项组合
           
            var addgroups = groupClientCusDtos.SelectMany(o => o.CustomerItemGroup).Where
                (n => n.CheckState != notcheck && n.DepartmentBM?.Category != "耗材" && n.IsAddMinus != (int)AddMinusType.Add).OrderBy(o => o.DepartmentBM?.Name).Select(o => o.ItemGroupName).Distinct().ToList();
            cusCount.Addgroup = string.Join("、", addgroups).TrimEnd(','); ;

            reportJson.Detail.Add(cusCount);           
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            subReport.LoadDataFromXML(reportJsonString);

        }
        /// <summary>
        /// 目标疾病
        /// </summary>
        private void Target()
        {
            if (ReportMain.ControlByName("危害因素及体检实施情况") == null)
            { return; }

            TargetJson targetJson = new TargetJson();
            targetJson.OccTargets = new List<OccTargetCountDto>();
            targetJson.OccTargets = occTargetCountDtos;
            
                var subReport = ReportMain.ControlByName("危害因素及体检实施情况").AsSubReport.Report;
                var reportJsonString = JsonConvert.SerializeObject(targetJson);
                subReport.LoadDataFromXML(reportJsonString);
           
        }
        /// <summary>
        /// 目标疾病
        /// </summary>
        private void Targetrisk()
        {
            if (ReportMain.ControlByName("危害因素检查周期") == null)
            { return; }

            TargetJson targetJson = new TargetJson();
            targetJson.OccTargets = new List<OccTargetCountDto>();
            targetJson.OccTargets = occTargetCountDtos.OrderBy(p=>p.ChckType).ToList();

            var subReport = ReportMain.ControlByName("危害因素检查周期").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);

        }
        /// <summary>
        /// 复查项目
        /// </summary>
        private void ReGroup()
        {
            if (ReportMain.ControlByName("复查项目") == null)
            { return; }

            ReGroupJson targetJson = new ReGroupJson();
            targetJson.CusReviewGroups = new List<CusReviewGroupDto>();
            targetJson.CusReviewGroups = CusReviewGroups;

            var subReport = ReportMain.ControlByName("复查项目").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);

        }
        /// <summary>
        /// 体检结果分析与结论统计
        /// </summary>
        private void ReSultCount()
        {
            if (ReportMain.ControlByName("体检结果分析与结论统计") == null)
            {
                return;
            }
            ReSultCountJson targetJson = new ReSultCountJson();
            targetJson.RSultCountls = new List<ReSultCounts>();
            var ReSult = new ReSultCounts();
            //检查人数
            ReSult.AllCount = allCount;
            ReSult.CheckCount = CheckCount;
            ReSult.ManCount = OccCustomerReglis.Where(o => o.CustomerRegBM.Customer.Sex == (int)Sex.Man).Select(p=>p.CustomerRegBMId).Distinct().Count();
            ReSult.WomenCount = OccCustomerReglis.Where(o => o.CustomerRegBM.Customer.Sex == (int)Sex.Woman).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.CheckPercent = ((CheckCount  * 100) / allCount).ToString("0.00").TrimEnd('0').TrimEnd('.'); 
            //检查正常人数
            var normalcount = OccCustomerReglis.Where(o => o.Conclusion != null && 
            (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.NormalCount = normalcount;
            ReSult.NormaPercent= ((normalcount * 100) / CheckCount).ToString("0.00").TrimEnd('0').TrimEnd('.');
            //检查正常男性人数
            var normalMancount = OccCustomerReglis.Where(o => o.Conclusion != null &&
            (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))
            && o.CustomerRegBM.Customer.Sex==(int)Sex.Man).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.NormalManCount = normalMancount;
            ReSult.NormaManPercent = ((normalMancount * 100) / CheckCount).ToString("0.00").TrimEnd('0').TrimEnd('.');

            //检查正常女性人数
            var normalWomencount = OccCustomerReglis.Where(o => o.Conclusion != null &&
            (o.Conclusion.Contains("未见异常") || o.Conclusion.Contains("未见明显异常"))
            && o.CustomerRegBM.Customer.Sex == (int)Sex.Woman).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.NormalWomenCount = normalWomencount;
            ReSult.NormaWomenPercent = ((normalWomencount * 100) / CheckCount).ToString("0.00").TrimEnd('0').TrimEnd('.');


            //检查异常人数
            var unnormalcount = OccCustomerReglis.Where(o => o.Conclusion != null && 
            !o.Conclusion.Contains("未见异常") && !o.Conclusion.Contains("未见明显异常")).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.UnNormalCount = unnormalcount;
            ReSult.UnNormaPercent = ((unnormalcount * 100) / CheckCount).ToString("0.00").TrimEnd('0').TrimEnd('.');

            //检查异常男性人数
            var unnormalMancount = OccCustomerReglis.Where(o => o.Conclusion != null && 
            !o.Conclusion.Contains("未见异常") && !o.Conclusion.Contains("未见明显异常")
            && o.CustomerRegBM.Customer.Sex == (int)Sex.Man).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.UnNormalManCount = unnormalMancount;
            ReSult.UnNormaManPercent = ((unnormalMancount * 100) / CheckCount).ToString("0.00").TrimEnd('0').TrimEnd('.');


            //检查异常女性人数
            var unnormalwomencount = OccCustomerReglis.Where(o => o.Conclusion != null &&
            !o.Conclusion.Contains("未见异常") && !o.Conclusion.Contains("未见明显异常")
            && o.CustomerRegBM.Customer.Sex == (int)Sex.Woman).Select(p => p.CustomerRegBMId).Distinct().Count();
            ReSult.UnNormalWomenCount = unnormalwomencount;
            ReSult.UnNormaWomenPercent = ((unnormalwomencount * 100) / CheckCount).ToString("0.00").TrimEnd('0').TrimEnd('.');

            var subReport = ReportMain.ControlByName("体检结果分析与结论统计").AsSubReport.Report;

            targetJson.RSultCountls.Add(ReSult);

            IGRChart pChartBar0 = subReport.ControlByName("Chart1").AsChart;
            NormalValuesForNUM(pChartBar0, ReSult);
            IGRChart pChartBar2 = subReport.ControlByName("Chart2").AsChart;
            UNNormalValuesForNUM(pChartBar2, ReSult);


            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }

        /// <summary>
        /// 总人员名单
        /// </summary>
        private void AllOccDissCus(List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("人员总检名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
            // var ReSult = new ReSultCounts();
            targetJson.OccDissCusliss = cuslist.ToList();
            var subReport = ReportMain.ControlByName("人员总检名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }
        /// <summary>
        /// 疑似职业病人员名单
        /// </summary>
        private void OccDissCus(List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("疑似职业病人员名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
           // var ReSult = new ReSultCounts();
            targetJson.OccDissCusliss = cuslist.ToList();
            var subReport = ReportMain.ControlByName("疑似职业病人员名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }
        /// <summary>
        /// 疑似禁忌证人员名单
        /// </summary>
        private void OccConCus(List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("职业禁忌证人员名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
            //var ReSult = new ReSultCounts();
         
            targetJson.OccDissCusliss = cuslist.ToList();
            var subReport = ReportMain.ControlByName("职业禁忌证人员名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }

        /// <summary>
        /// 复查人员名单
        /// </summary>
        private void OccReviewCus(List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("复查人员名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
            var ReSult = new ReSultCounts();
          
            targetJson.OccDissCusliss = cuslist.ToList();
            var subReport = ReportMain.ControlByName("复查人员名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }
        /// <summary>
        /// 与职业危害因素无关的其它疾病异常人员名单
        /// </summary>
        private void OccOtherCus(List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("与职业危害因素无关的其它疾病异常人员名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
            var ReSult = new ReSultCounts();
           
            targetJson.OccDissCusliss = cuslist.ToList();
            var subReport = ReportMain.ControlByName("与职业危害因素无关的其它疾病异常人员名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }

        /// <summary>
        /// 体检目前未见异常人员名单
        /// </summary>
        private void OccNomalCus(List<OccDissCuslis> cuslist)
        {
            if (ReportMain.ControlByName("体检目前未见异常人员名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
            var ReSult = new ReSultCounts();
           
            targetJson.OccDissCusliss = cuslist.ToList();
            var subReport = ReportMain.ControlByName("体检目前未见异常人员名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }
        /// <summary>
        /// 有未检项目名单
        /// </summary>
        private void OccNoCheckCus(List<OccDissCuslis> nochels)
        {
            if (ReportMain.ControlByName("有未检项目名单") == null)
            {
                return;
            }
            OccDissCusJson targetJson = new OccDissCusJson();
            targetJson.OccDissCusliss = new List<OccDissCuslis>();
            

           
            
            targetJson.OccDissCusliss = nochels.ToList();
            var subReport = ReportMain.ControlByName("有未检项目名单").AsSubReport.Report;
            var reportJsonString = JsonConvert.SerializeObject(targetJson);
            subReport.LoadDataFromXML(reportJsonString);
        }
        /// <summary>
        /// 主要异常分析
        /// </summary>
        private void OtherIllCount()
        {
            if (ReportMain.ControlByName("主要异常分析") == null)
            {
                return;
            }
            OtherIllCuslisJson otherIllCuslisJson = new OtherIllCuslisJson();
            otherIllCuslisJson.OccDissCusliss = new List<OtherIllCuslis>();

               var sumlis = groupClientSumDtos.GroupBy(o => o.SummarizeName).Select(g => (new
            {
                Name = g.Key,
                cout = g.Count(),
            })).OrderByDescending(o => o.cout).ToList();
            for (int i = 0; i < sumlis.Count(); i++)
            {

                var culis = groupClientSumDtos.Where(o => o.SummarizeName == sumlis[i].Name).Distinct().ToList();
                var workls = culis.GroupBy(o => o.CustomerReg.WorkName).ToList().Select(g => new
                {
                    workname = g.Key,
                    worlcount = g.Count()
                });
                string workcon = "共有" + sumlis[i].Name + sumlis[i].cout +"人，其中";
                foreach (var cus in workls)
                {
                    string workname = cus.workname;
                    if (cus.workname == "")
                    {
                        workname = "无部门";
                    }
                    //部门人数
                  var WorkNamecount =  ClientCusDtos.Where(o => o.WorkName == cus.workname).Count();
                    //部门疾病数占疾病数
                    var WorkNamepesent = ((cus.worlcount * 100) / sumlis[i].cout).ToString("0.00").TrimEnd('0').TrimEnd('.');
                    //部门疾病数占部门数
                    var WorkNameP = "0%";
                    if (WorkNamecount != 0)
                    {
                         WorkNameP = ((cus.worlcount * 100) / WorkNamecount).ToString("0.00").TrimEnd('0').TrimEnd('.');
                    }                    
                    workcon += workname + cus.worlcount + "人,占" + sumlis[i].Name + "人数的" + WorkNamepesent + "%";
                    workcon += ",占部门体检人数的" + WorkNameP + "%";
                }
                var age1 = culis.Where(o => o.CustomerReg.TotalWorkAge!=null && o.CustomerReg.TotalWorkAge !="" && Convert.ToDecimal(o.CustomerReg.TotalWorkAge??"0") <= 5).Count();
                var age2 = culis.Where(o => o.CustomerReg.TotalWorkAge != null && o.CustomerReg.TotalWorkAge != "" && Convert.ToDecimal(o.CustomerReg.TotalWorkAge ?? "0") > 5 &&
               decimal.Parse(o.CustomerReg.TotalWorkAge ?? "0") <=10).Count();
                var age3 = culis.Where(o => o.CustomerReg.TotalWorkAge != null && o.CustomerReg.TotalWorkAge != "" && Convert.ToDecimal(o.CustomerReg.TotalWorkAge ?? "0") > 10 &&
           decimal.Parse(o.CustomerReg.TotalWorkAge ?? "0") <= 15).Count();
                var age4 = culis.Where(o => o.CustomerReg.TotalWorkAge != null && o.CustomerReg.TotalWorkAge != "" && Convert.ToDecimal(o.CustomerReg.TotalWorkAge ?? "0") > 15 &&
           decimal.Parse(o.CustomerReg.TotalWorkAge ?? "0") <= 20).Count();
                var age5 = culis.Where(o => o.CustomerReg.TotalWorkAge != null && o.CustomerReg.TotalWorkAge != "" && Convert.ToDecimal(o.CustomerReg.TotalWorkAge ?? "0") > 20 ).Count();
                var agecon = string.Format(@"其中1-5年工龄{0}人，6-10年工龄{1}人，11-15年工龄{2}人，15-20年工龄{3}人，20年以上工龄 {4}人。",
                    age1,age2,age3,age4,age5);



                for (int m = 0; m < culis.Count(); m++)
                {
                    OtherIllCuslis otherIllCuslis = new OtherIllCuslis();

                    otherIllCuslis.WorkNameCon = workcon;
                    otherIllCuslis.AgeCon = agecon;
                    otherIllCuslis.Remark = clientRegDto.Remark;

                    otherIllCuslis.Name = culis[m].CustomerReg.Customer.Name;
                    otherIllCuslis.Sex = SexHelper.CustomSexFormatter(culis[m].CustomerReg.Customer.Sex);
                    otherIllCuslis.Age = culis[m].CustomerReg.Customer.Age;
                    otherIllCuslis.CustomerBM = culis[m].CustomerReg.CustomerBM;                   
                    otherIllCuslis.AdviceName = sumlis[i].Name;
                    otherIllCuslis.AdviceDis = culis[m].SummarizeAdvice?.SummAdvice ?? culis[m].Advice;
                    otherIllCuslisJson.OccDissCusliss.Add(otherIllCuslis);
                }
                var subReport = ReportMain.ControlByName("主要异常分析").AsSubReport.Report;
                var reportJsonString = JsonConvert.SerializeObject(otherIllCuslisJson);
                subReport.LoadDataFromXML(reportJsonString);



            }

        }

        #region  人数图表
        protected void FillNormalValuesForNUM(IGRChart pChart, CusCount ages)
        {
           
            pChart.SeriesCount = 6;
            pChart.GroupCount = 1;

            pChart.set_SeriesLabel(0, "<=24岁");
            pChart.set_SeriesLabel(1, "25-34岁");
            pChart.set_SeriesLabel(2, "35-44岁");
            pChart.set_SeriesLabel(3, "45-54岁");
            pChart.set_SeriesLabel(4, "55-64岁");
            pChart.set_SeriesLabel(5, ">=65岁");


            pChart.set_GroupLabel(0, " ");


            pChart.set_Value(0, 0, ages.Age1.Value);
            pChart.set_Value(1, 0, ages.Age2.Value);
            pChart.set_Value(2, 0, ages.Age3.Value);
            pChart.set_Value(3, 0, ages.Age4.Value);
            pChart.set_Value(4, 0, ages.Age5.Value);
            pChart.set_Value(5, 0, ages.Age6.Value);
        }
        #endregion
        #region  正常人数图表
        protected void NormalValuesForNUM(IGRChart pChart, ReSultCounts reSultCount)
        {
            pChart.SeriesCount = 4;
            pChart.GroupCount = 1;

            pChart.set_SeriesLabel(0, "男性正常人数");
            pChart.set_SeriesLabel(1, "男性总人数");
            pChart.set_SeriesLabel(2, "女性正常人数");
            pChart.set_SeriesLabel(3, "女性总人数");


            pChart.set_GroupLabel(0, " ");


            pChart.set_Value(0, 0, reSultCount.NormalManCount.Value);
            pChart.set_Value(1, 0, reSultCount.ManCount.Value);
            pChart.set_Value(2, 0, reSultCount.NormalWomenCount.Value);
            pChart.set_Value(3, 0, reSultCount.WomenCount.Value);

        }
        #endregion
        #region  异常人数图表
        protected void UNNormalValuesForNUM(IGRChart pChart, ReSultCounts reSultCount)
        {
            pChart.SeriesCount = 4;
            pChart.GroupCount = 1;

            pChart.set_SeriesLabel(0, "男性异常人数");
            pChart.set_SeriesLabel(1, "男性总人数");
            pChart.set_SeriesLabel(2, "女性异常人数");
            pChart.set_SeriesLabel(3, "女性总人数");


            pChart.set_GroupLabel(0, " ");


            pChart.set_Value(0, 0, reSultCount.UnNormalManCount.Value);
            pChart.set_Value(1, 0, reSultCount.ManCount.Value);
            pChart.set_Value(2, 0, reSultCount.UnNormalWomenCount.Value);
            pChart.set_Value(3, 0, reSultCount.WomenCount.Value);

        }


        #endregion
        /// <summary>
        /// 主报表
        /// </summary>
        public class MainReportJson
        {
            /// <summary>
            /// 明细网格
            /// </summary>
            public List<MCusInfo> Detail { get; set; }
        }
        public class MCusInfo
        {
            /// <summary>
            /// 单位编码
            /// </summary>
            public virtual string ClientBM1 { get; set; }
            /// <summary>
            /// 单位预约编码
            /// </summary>
            public virtual string ClientRegBM1 { get; set; }

            /// <summary>
            /// 单位名称
            /// </summary>
            public virtual string ClientName1 { get; set; }
            /// <summary>
            /// 单位简称
            /// </summary>
            public virtual string ClientAbbreviation1 { get; set; }

            /// <summary>
            /// 预约描述
            /// </summary>
            [StringLength(128)]
            public virtual string RegInfo { get; set; }
            /// <summary>
            /// 说明
            /// </summary>
            [StringLength(128)]
            public virtual string Remark { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string StartCheckDate1 { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public virtual string EndCheckDate1 { get; set; }

            /// <summary>
            /// 单位负责人 默认单位负责人
            /// </summary>
            [StringLength(64)]
            public virtual string linkMan1 { get; set; }
            /// <summary>
            /// 企业邮箱
            /// </summary>
            [StringLength(32)]
            public virtual string ClientEmail1 { get; set; }


            /// <summary>
            /// 联系电话
            /// </summary>
            [StringLength(32)]
            public virtual string Telephone1 { get; set; }

            /// <summary>
            /// 详细地址
            /// </summary>
            [StringLength(256)]
            public virtual string Address1 { get; set; }
            public virtual string No1 { get; set; }
            public virtual string No2 { get; set; }
            public virtual string No3 { get; set; }
            public virtual string No4 { get; set; }
            public virtual string No5 { get; set; }
            public virtual string No6 { get; set; }
            public virtual string No7 { get; set; }
            public virtual string No8 { get; set; }
            public virtual string No9 { get; set; }
            public virtual string No10 { get; set; }

        }
        /// <summary>
        /// 首页
        /// </summary>
        public class ReportJson
        { 
            /// <summary>
            /// 明细网格
            /// </summary>
            public List<CusInfo> Detail { get; set; }
        }
        /// <summary>
        /// 职业病检查基本情况
        /// </summary>
        public class ReportBiseJson
        {           

            /// <summary>
            /// 明细网格
            /// </summary>
            public List<CusCount> Detail { get; set; }
        }
        public class CusCount
        {

            /// <summary>
            /// 单位编码
            /// </summary>
            public virtual string ClientBM { get; set; }

            /// 单位预约编码
            /// </summary>
            public virtual string ClientRegBM { get; set; }

            /// <summary>
            /// 单位名称
            /// </summary>
            public virtual string ClientName { get; set; }


            /// <summary>
            /// 单位简称
            /// </summary>
            public virtual string ClientAbbreviation { get; set; }

            /// <summary>
            /// 预约描述
            /// </summary>           
            public virtual string RegInfo { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string StartCheckDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public virtual string EndCheckDate { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string SearchStartDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public virtual string SearchEndDate { get; set; }

            /// <summary>
            /// 单位负责人 默认单位负责人
            /// </summary>
            [StringLength(64)]
            public virtual string linkMan { get; set; }

            /// <summary>
            /// 单位负责人 默认单位负责人
            /// </summary>
            [StringLength(64)]
            public virtual string ClientlinkMan { get; set; }
          
            /// <summary>
            /// 企业邮箱
            /// </summary>
            [StringLength(32)]
            public virtual string ClientEmail { get; set; }


            /// <summary>
            /// 联系电话
            /// </summary>
            [StringLength(32)]
            public virtual string Telephone { get; set; }

            /// <summary>
            /// 详细地址
            /// </summary>
            [StringLength(256)]
            public virtual string Address { get; set; }
            /// <summary>
            /// 结论名称
            /// </summary>
            public virtual string  SumName { get; set; }

            /// <summary>
            /// 结论数量
            /// </summary>
            public virtual string SumCount { get; set; }
            /// <summary>
            /// 有未检项目
            /// </summary>
            public virtual int? NoCheckCont { get; set; }

            /// <summary>
            /// 未见异常
            /// </summary>
            public virtual int? NoIllCount { get; set; }
            /// <summary>
            /// 其他疾病
            /// </summary>
            public virtual int? OtherCount { get; set; }
            /// <summary>
            /// 复查
            /// </summary>
            public virtual int? ReCheckCount { get; set; }
            /// <summary>
            /// 胸片、肺功能复查人数
            /// </summary>
            public virtual int? XFReCheckCount { get; set; }
            /// <summary>
            /// 胸片、肺功能复查人员
            /// </summary>
            public virtual string XFReCheckCusName { get; set; }
            /// <summary>
            /// 职业禁忌证
            /// </summary>
            public virtual int? OccConCount { get; set; }

            /// <summary>
            /// 疑似职业病
            /// </summary>
            public virtual int? OccDisCount { get; set; }
            /// <summary>
            /// 应检查人数
            /// </summary>
            public virtual int? AllCount { get; set; }
            /// <summary>
            /// 检查人数
            /// </summary>
            public virtual int? CheckCount { get; set; }
            /// <summary>
            /// 参检率
            /// </summary>
            public virtual string CheckPer { get; set; }
            /// <summary>
            /// 到检率
            /// </summary>
            public virtual string HasCheckPer { get; set; }
            /// <summary>
            /// 车间人数
            /// </summary>     
            public virtual string WorNameCount { get; set; }
            /// <summary>
            /// 工种人数
            /// </summary>     
            public virtual string WorkTypeCount { get; set; }
            /// <summary>
            /// 岗位类型人数
            /// </summary>     
            public virtual string PoststateCount { get; set; }
            /// <summary>
            /// 岗位类型危害因素人数
            /// </summary>     
            public virtual string PoststateRiskCount { get; set; }
            /// <summary>
            /// 岗位类型结论人数
            /// </summary>     
            public virtual string PoststateSumCount { get; set; }
            /// <summary>
            /// 危害因素结论人数
            /// </summary>     
            public virtual string riskSumCount { get; set; }
            /// <summary>
            /// 危害因素结论人数
            /// </summary>     
            public virtual string riskSum  { get; set; }
            /// <summary>
            /// 接害工龄人数
            /// </summary>     
            public virtual string YearsCount { get; set; }
            /// <summary>
            /// <=24岁
            /// </summary>
            public virtual int? Age1 { get; set; }
            /// <summary>
            /// 25-34岁
            /// </summary>
            public virtual int? Age2 { get; set; }
            /// <summary>
            // 34-44岁
            /// </summary>
            public virtual int? Age3 { get; set; }
            /// <summary>
            //44-54岁
            /// </summary>
            public virtual int? Age4 { get; set; }
            /// <summary>
            //54-64岁
            /// </summary>
            public virtual int? Age5 { get; set; }
            /// <summary>
            //>=64岁
            /// </summary>
            public virtual int? Age6 { get; set; }
            /// <summary>
            /// 上岗前
            /// </summary>
            [StringLength(256)]
            public virtual string sgq { get; set; }
            /// <summary>
            /// 在岗期间
            /// </summary>
            [StringLength(256)]
            public virtual string zgs { get; set; }
            /// <summary>
            /// 离岗时
            /// </summary>
            [StringLength(256)]
            public virtual string lgs { get; set; }
            /// <summary>
            /// 离岗后
            /// </summary>
            [StringLength(256)]
            public virtual string lgh { get; set; }
            /// <summary>
            /// 应急
            /// </summary>
            [StringLength(256)]
            public virtual string yj { get; set; }
            /// <summary>
            /// 上岗前
            /// </summary>
            [StringLength(256)]
            public virtual int? sgCount { get; set; }
            /// <summary>
            /// 上岗前异常人数
            /// </summary>          
            public virtual int? sgUnnormalCount { get; set; }
            /// <summary>
            /// 上岗前正常人数
            /// </summary>         
            public virtual int? sgnormalCount { get; set; }
            /// <summary>
            /// 上岗zyb
            /// </summary>         
            public virtual int? sgzybCount { get; set; }
            /// <summary>
            /// 上岗前正常人数
            /// </summary>         
            public virtual int? sgjjzCount { get; set; }
            /// <summary>
            /// 上岗前正常人数
            /// </summary>         
            public virtual int? sgfcCount { get; set; }
            /// <summary>
            /// 在岗期间
            /// </summary>           
            public virtual int? zgCount { get; set; }
            /// <summary>
            /// 在岗期间异常人数
            /// </summary>         
            public virtual int? zgUnnormalCount { get; set; }
            /// <summary>
            /// 在岗期间正常人数
            /// </summary>          
            public virtual int? zgnormalCount { get; set; }
            /// <summary>
            /// 在岗期间职业病
            /// </summary>         
            public virtual int? zgzybCount { get; set; }
            /// <summary>
            /// 在岗期间禁忌证
            /// </summary>         
            public virtual int? zgjjzCount { get; set; }
            /// <summary>
            /// 在岗期间复查
            /// </summary>         
            public virtual int? zgfcCount { get; set; }
            /// <summary>
            /// 离岗时
            /// </summary>       
            public virtual int? lgsCount { get; set; }
            /// <summary>
            /// 离岗时异常人数
            /// </summary>          
            public virtual int? lgsUnnormalCount { get; set; }
            /// <summary>
            /// 离岗时正常人数
            /// </summary>          
            public virtual int? lgsnormalCount { get; set; }
            /// <summary>
            /// 离岗时职业病
            /// </summary>         
            public virtual int? lgzybCount { get; set; }
            /// <summary>
            /// 离岗时禁忌证
            /// </summary>         
            public virtual int? lgjjzCount { get; set; }
            /// <summary>
            /// 离岗时间复查
            /// </summary>         
            public virtual int? lgfcCount { get; set; }
            /// <summary>
            /// 离岗后
            /// </summary>          
            public virtual int? lghCount { get; set; }
            /// <summary>
            /// 离岗后异常人数
            /// </summary>          
            public virtual int? lghUnnormalCount { get; set; }
            /// <summary>
            /// 离岗后正常人数
            /// </summary>        
            public virtual int? lghnormalCount { get; set; }
            /// <summary>
            ///离岗后职业病
            /// </summary>         
            public virtual int? lghzybCount { get; set; }
            /// <summary>
            /// 离岗后禁忌证
            /// </summary>         
            public virtual int? lghjjzCount { get; set; }
            /// <summary>
            /// 离岗后间复查
            /// </summary>         
            public virtual int? lghfcCount { get; set; }
            /// <summary>
            /// 应急
            /// </summary>           
            public virtual int? yjCount { get; set; }
            /// <summary>
            /// 应急异常人数
            /// </summary>        
            public virtual int? yjUnnormalCount { get; set; }
            /// <summary>
            /// 应急正常人数
            /// </summary>         
            public virtual int? yjnormalCount { get; set; }
            /// <summary>
            ///应急职业病
            /// </summary>         
            public virtual int? yjzybCount { get; set; }
            /// <summary>
            /// 应急禁忌证
            /// </summary>         
            public virtual int? yjjjzCount { get; set; }
            /// <summary>
            /// 应急复查
            /// </summary>         
            public virtual int? yjfcCount { get; set; }


            /// <summary>
            /// 危害因素
            /// </summary>
            [StringLength(256)]
            public virtual string whys { get; set; }
            /// <summary>
            /// 组合
            /// </summary>
            [StringLength(256)]
            public virtual string group { get; set; }
            /// <summary>
            /// 必检组合
            /// </summary>
            [StringLength(256)]
            public virtual string Hasgroup { get; set; }
            /// <summary>
            /// 目标疾病
            /// </summary>
            [StringLength(256)]
            public virtual string RiskTarget { get; set; }
            
            /// <summary>
            /// 加项组合
            /// </summary>
            [StringLength(256)]
            public virtual string Addgroup { get; set; }
            /// <summary>
            /// 参考标准
            /// </summary>
            public virtual string Reference { get; set; }

            /// <summary>
            /// 合同性质
            /// </summary>
            [StringLength(64)]
            public virtual string Clientcontract { get; set; }
            /// <summary>
            /// 企业规模
            /// </summary>
            public virtual string Scale { get; set; }
            /// <summary>
            /// 行业
            /// </summary>
            [StringLength(64)]
            public virtual string Clientlndutry { get; set; }

            /// <summary>
            /// 18位社会信用代码
            /// </summary>
            /// [StringLength(100)]
            public string SocialCredit { get; set; }

            /// <summary>
            /// 经济类型
            /// </summary>
            public string EconomicType { get; set; }

            /// <summary>
            /// 岗位类别
            /// </summary>
            public string PostState { get; set; }
            /// <summary>
            /// 结论数量
            /// </summary>
            public virtual string ReGroupCount { get; set; }
            /// <summary>
            /// 结论数量
            /// </summary>
            public virtual int? AllReGroupCount { get; set; }

        }
        public class CusInfo
        {
            /// <summary>
            /// 单位编码
            /// </summary>
            public virtual string ClientBM { get; set; }
            /// <summary>
            /// 单位预约编码
            /// </summary>
            public virtual string ClientRegBM { get; set; }

            /// <summary>
            /// 单位名称
            /// </summary>
            public virtual string ClientName { get; set; }
            /// <summary>
            /// 单位简称
            /// </summary>
            public virtual string ClientAbbreviation { get; set; }

            /// <summary>
            /// 开始时间
            /// </summary>
            public string StartCheckDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public virtual string EndCheckDate { get; set; }


            /// <summary>
            /// 开始时间
            /// </summary>
            public string SearchStartDate { get; set; }
            /// <summary>
            /// 结束时间
            /// </summary>
            public virtual string SearchEndDate { get; set; }

            /// <summary>
            /// 总人数
            /// </summary>           
            public virtual string AllCount { get; set; }

            /// <summary>
            /// 已检人数数
            /// </summary>           
            public virtual string CheckCount { get; set; }
            /// <summary>
            /// 未检人数数
            /// </summary>           
            public virtual string NoCheckCount { get; set; }
            /// <summary>
            /// 复查人数数
            /// </summary>           
            public virtual string ReCountCount { get; set; }
            
            /// <summary>
            /// 接害人数
            /// </summary>
            [StringLength(64)]
            public virtual string InjuryCount { get; set; }
            /// <summary>
            /// 危害因素
            /// </summary>
            [StringLength(64)]
            public virtual string RiskName { get; set; }
            /// <summary>
            /// 单位负责人 默认单位负责人
            /// </summary>
            [StringLength(64)]
            public virtual string linkMan { get; set; }
            /// <summary>
            /// 单位负责人 默认单位负责人
            /// </summary>
            [StringLength(64)]
            public virtual string ClientlinkMan { get; set; }

          
            /// <summary>
            /// 企业邮箱
            /// </summary>
            [StringLength(32)]
            public virtual string ClientEmail { get; set; }




            /// <summary>
            /// 联系电话
            /// </summary>
            [StringLength(32)]
            public virtual string Telephone { get; set; }

            /// <summary>
            /// 详细地址
            /// </summary>
            [StringLength(256)]
            public virtual string Address { get; set; }
            /// <summary>
            /// 上岗前
            /// </summary>
            [StringLength(256)]
            public virtual string sgq { get; set; }
            /// <summary>
            /// 在岗期间
            /// </summary>
            [StringLength(256)]
            public virtual string zgs { get; set; }
            /// <summary>
            /// 离岗时
            /// </summary>
            [StringLength(256)]
            public virtual string lgs { get; set; }
            /// <summary>
            /// 离岗后
            /// </summary>
            [StringLength(256)]
            public virtual string lgh { get; set; }
            /// <summary>
            /// 应急
            /// </summary>
            [StringLength(256)]
            public virtual string yj { get; set; }

            /// <summary>
            /// 合同性质
            /// </summary>
            [StringLength(64)]
            public virtual string Clientcontract { get; set; }
            /// <summary>
            /// 企业规模
            /// </summary>
            public virtual string Scale { get; set; }
            /// <summary>
            /// 行业
            /// </summary>
            [StringLength(64)]
            public virtual string Clientlndutry { get; set; }
            /// <summary>
            /// 预约描述
            /// </summary>           
            public virtual string RegInfo { get; set; }

            /// <summary>
            /// 说明
            /// </summary>
            [StringLength(128)]
            public virtual string Remark { get; set; }

            /// <summary>
            /// 18位社会信用代码
            /// </summary>
            /// [StringLength(100)]
            public string SocialCredit { get; set; }
            /// <summary>
            /// 经济类型
            /// </summary>
            public string EconomicType { get; set; }

        }
        public class TargetJson
        {
            /// <summary>
            /// 目标疾病
            /// </summary>
            public List<OccTargetCountDto> OccTargets { get; set; }
        }
        public class ReGroupJson
        {
            /// <summary>
            /// 目标疾病
            /// </summary>
            public List<CusReviewGroupDto> CusReviewGroups { get; set; }
        }
        public class ReSultCountJson
        {/// <summary>
         /// 疾病人数统计
         /// </summary>
            public List<ReSultCounts> RSultCountls { get; set; }

        }
            public class ReSultCounts
        {
            /// <summary>
            /// 应检查人数
            /// </summary>
            public virtual int? AllCount { get; set; }

            /// <summary>
            /// 男性人数
            /// </summary>
            public virtual int? ManCount { get; set; }
            /// <summary>
            /// 男性人数
            /// </summary>
            public virtual int? WomenCount { get; set; }
            /// <summary>
            /// 检查人数
            /// </summary>
            public virtual int? CheckCount { get; set; }

            /// <summary>
            /// 体检率
            /// </summary>
            public virtual string CheckPercent { get; set; }

            // <summary>
            /// 正常人数
            /// </summary>
            public virtual int? NormalCount { get; set; }
            // <summary>
            /// 正常占比
            /// </summary>
            public virtual string NormaPercent { get; set; }

            // <summary>
            /// 正常男性人数
            /// </summary>
            public virtual int? NormalManCount { get; set; }
            // <summary>
            /// 正常男性人数占比
            /// </summary>
            public virtual string NormaManPercent { get; set; }
            // <summary>
            /// 正常女性人数
            /// </summary>
            public virtual int? NormalWomenCount { get; set; }
            // <summary>
            /// 正常女性人数占比
            /// </summary>
            public virtual string NormaWomenPercent { get; set; }


            // <summary>
            /// 异常人数
            /// </summary>
            public virtual int? UnNormalCount { get; set; }
            // <summary>
            ///异常占比
            /// </summary>
            public virtual string UnNormaPercent { get; set; }

            // <summary>
            /// 异常男性人数
            /// </summary>
            public virtual int? UnNormalManCount { get; set; }
            // <summary>
            /// 异常男性人数占比
            /// </summary>
            public virtual string UnNormaManPercent { get; set; }
            // <summary>
            /// 异常女性人数
            /// </summary>
            public virtual int? UnNormalWomenCount { get; set; }
            // <summary>
            /// 异常女性人数占比
            /// </summary>
            public virtual string UnNormaWomenPercent { get; set; }


        }

        /// <summary>
        /// 目标疾病人数
        /// </summary>
        public class OccDissCusJson
        {
            /// <summary>
            /// 目标疾病人数
            /// </summary>
            public List<OccDissCuslis> OccDissCusliss { get; set; }
        }

        public class OccDissCuslis
        {


            /// <summary>
            /// 总人数
            /// </summary>
            public virtual int? AllCount { get; set; }
            /// <summary>
            /// 危害因素名称
            /// </summary>
            public virtual string RiskNames { get; set; }
            /// <summary>
            /// 体检号
            /// </summary>
            public virtual string CustomerBM { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public virtual string Name { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public virtual string Sex { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public virtual string Age { get; set; }
            /// <summary>
            /// 检查类型
            /// </summary>
            public virtual string CheckType { get; set; }
            /// <summary>
            /// 检查结论
            /// </summary>
            public virtual string Conlutions { get; set; }
            /// <summary>
            /// 结论描述
            /// </summary>
            public virtual string Description { get; set; }

            /// <summary>
            /// 目标疾病
            /// </summary>
            public virtual string OccDiss { get; set; }

            /// <summary>
            /// 处理意见
            /// </summary>
            public virtual string Opton { get; set; }

            /// <summary>
            /// 复查项目
            /// </summary>
            public virtual string RevGroups { get; set; }

            /// <summary>
            /// 未检项目
            /// </summary>
            public virtual string NoCheckGroups { get; set; }

            /// <summary>
            /// 工种
            /// </summary>
            [StringLength(16)]
            public virtual string TypeWork { get; set; }
            /// <summary>
            /// 医学建议
            /// </summary>
            [StringLength(1000)]
            public virtual string MedicalAdvice { get; set; }

            /// <summary>
            /// 接害工龄
            /// </summary>
            [StringLength(1000)]
            public virtual string InjuryAge { get; set; }
            /// <summary>
            /// 职业病异常
            /// </summary>
            [StringLength(1000)]
            public virtual string OccIll { get; set; }
            /// <summary>
            /// 其他异常
            /// </summary>
            [StringLength(1000)]
            public virtual string OtherIll { get; set; }

            public virtual string OccIllSummarize { get; set; }
            /// <summary>
            /// 建议名称
            /// </summary>

            public virtual string IllName { get; set; }
            /// <summary>
            /// 总检建议
            /// </summary>

            public virtual string SumAdvice { get; set; }


            /// <summary>
            ///体检日期
            /// </summary>
            public virtual string BookingDate { get; set; }

            /// <summary>
            /// 登记日期 第一次登记日期
            /// </summary>
            public virtual string LoginDate { get; set; }
            /// <summary>
            /// 身份证号
            /// </summary>
            [StringLength(24)]
            public virtual string IDCardNo { get; set; }

            /// <summary>
            /// 移动电话
            /// </summary>
            [StringLength(16)]
            public virtual string Mobile { get; set; }


            /// <summary>
            /// 民族
            /// </summary>
            [StringLength(16)]
            public virtual string Nation { get; set; }

            /// <summary>
            /// 通讯地址
            /// </summary>
            [StringLength(128)]
            public virtual string Address { get; set; }

            /// <summary>
            /// 所有检查周期
            /// </summary>
            [StringLength(5000)]
            public virtual string InspectionCycle { get; set; }
        }

        public class OtherIllCuslisJson
        {
            /// <summary>
            /// 其他疾病统计
            /// </summary>
            public List<OtherIllCuslis> OccDissCusliss { get; set; }
        }
        public class OtherIllCuslis
        {
            
            /// <summary>
            /// 体检号
            /// </summary>
            public virtual string CustomerBM { get; set; }
            /// <summary>
            /// 姓名
            /// </summary>
            public virtual string Name { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public virtual string Sex { get; set; }
            /// <summary>
            /// 年龄
            /// </summary>
            public virtual int? Age { get; set; }
            /// <summary>
            /// 部门对比
            /// </summary>
            public virtual string WorkNameCon { get; set; }
            /// <summary>
            /// 工龄对比
            /// </summary>
            public virtual string AgeCon { get; set; }

            /// <summary>
            /// 疾病名称
            /// </summary>
            public virtual string AdviceName { get; set; }
            /// <summary>
            /// 备注
            /// </summary>
            public virtual string Remark { get; set; }
            

            /// <summary>
            /// 建议
            /// </summary>
            public virtual string AdviceDis { get; set; }
           
        }
    }
}
