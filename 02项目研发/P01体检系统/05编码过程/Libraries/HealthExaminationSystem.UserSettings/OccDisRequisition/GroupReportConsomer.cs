using Abp.Application.Services.Dto;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.CustomerReport;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition;
using Sw.Hospital.HealthExaminationSystem.Application.OccDisRequisition.Dto;
using Sw.Hospital.HealthExaminationSystem.Common.Enums;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.UserCache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sw.Hospital.HealthExaminationSystem.UserSettings.OccDisRequisition
{
  public class GroupReportConsomer
    {
        private GridppReport ReportMain;
        private IOccDisRequisitionAppService _OccDisRequisitionAppService;
        private List<OutOccCustomerSumDto> groupOutOccCustomerSumDto = new List<OutOccCustomerSumDto>();
        private readonly IPrintPreviewAppService _printPreviewAppService = new PrintPreviewAppService();
        OutOccCustomerSumDto dto = new OutOccCustomerSumDto();
        private readonly ICommonAppService _commonAppService = new CommonAppService();
        public GroupReportConsomer()
        {
            _OccDisRequisitionAppService = new OccDisRequisitionAppService();
            ReportMain = new GridppReport();

        }
        /// <summary>
        /// 打印 
        /// </summary>
        /// <param name="isPreview">是否预览，是则预览，否则打印</param>
        public void Print(bool isPreview, EntityDto<Guid> input, string path = "")
        {
            var cusNameInput = new OutOccCustomerSumDto();
            cusNameInput.Id = input.Id;
            var customersum = _OccDisRequisitionAppService.GetOccDisRequisition(cusNameInput);
            var zybSum = customersum.Where(p => p.Conclusion !=null && p.Conclusion.Contains("职业病")).ToList();
            var jjzSum= customersum.Where(p => p.Conclusion != null && p.Conclusion.Contains("禁忌证")).ToList();
            //打印机设置
            var printName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PrintNameSet, 70)?.Remarks;
           // var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            //职业健康
            if (zybSum.Count > 0)
            {
                

                var strBarPrintName = "职业健康检查疑似职业病.grf";
                var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
                ReportMain.LoadFromURL(GrdPath);
                var reportJson = new ReportJson();
                reportJson.Detail = new List<Detail>();
                var detail = new Detail();
                foreach (var sum in zybSum)
                {
                    detail.ClientName = sum.ClientName;
                    detail.Name = sum.Name;
                    detail.Sex = sum.Sex;
                    detail.Age = sum.Age;
                    detail.IDCardNo = sum.IDCardNo;
                    //工龄 detail. = ;
                    detail.TypeWork = sum.TypeWork;
                    detail.ZYRiskName = sum.ZYRiskName;
                    detail.ReviewContent = sum.ReviewContent;
                    detail.CheckDate = sum.CheckDate;
                    detail.ReviewContentDate = sum.ReviewContentDate;
                    detail.WorkName = sum.WorkName;
                    detail.CustomerRegNum = sum.CustomerRegNum;
                    detail.ZYTabooIllness =string.Join(",", sum.ZYTabooIllness.Select(p=>p.Text).ToList());
                    detail.Contraindications = string.Join(",", sum.OccCustomerContraindications.Select(p => p.Text).ToList());
                    //结果汇总 detail. = dto.;
                    detail.ZYTreatmentAdvice = sum.ZYTreatmentAdvice;
                    detail.PostState = sum.PostState;
                    detail.TotalWorkAge = sum.TotalWorkAge;
                    detail.InjuryAge = sum.InjuryAge;
                    detail.Conclusion = sum.Conclusion;
                    detail.Description = sum.Description;
                    reportJson.Detail.Add(detail);
                    var reportJsonString1 = JsonConvert.SerializeObject(reportJson);
                    ReportMain.LoadDataFromXML(reportJsonString1);
                    //打印后事件
                    ReportMain.PrintEnd += () =>
                    {
                        _printPreviewAppService.UpdateNoticePrintState(new EntityDto<Guid> { Id = sum.CustomerRegBMId.Value });
                        //日志
                        CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                        createOpLogDto.LogBM = sum.CustomerBM;
                        createOpLogDto.LogName = sum.Name;
                        createOpLogDto.LogText = "打印通知单";
                        createOpLogDto.LogDetail = "";
                        createOpLogDto.LogType = (int)LogsTypes.PrintId;
                        _commonAppService.SaveOpLog(createOpLogDto);

                    };
                }
              

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
            if (jjzSum.Count > 0)
            {

                //禁忌证

             var   strBarPrintName = "职业健康检查职业禁忌证.grf";
              var   GrdPath = GridppHelper.GetTemplate(strBarPrintName);
              ReportMain.LoadFromURL(GrdPath);
              var   reportJson = new ReportJson();
                reportJson.Detail = new List<Detail>();
              var    detail = new Detail();
                foreach (var sum in jjzSum)
                {


                    detail.ClientName = sum.ClientName;
                    detail.Name = sum.Name;
                    detail.Sex = sum.Sex;
                    detail.Age = sum.Age;
                    detail.IDCardNo = sum.IDCardNo;
                    //工龄 detail. = ;
                    detail.TypeWork = sum.TypeWork;
                    detail.ZYRiskName = sum.ZYRiskName;
                    detail.ReviewContent = sum.ReviewContent;
                    detail.CheckDate = sum.CheckDate;
                    detail.ReviewContentDate = sum.ReviewContentDate;
                    detail.WorkName = sum.WorkName;
                    detail.CustomerRegNum = sum.CustomerRegNum;
                    detail.ZYTabooIllness =string.Join(",", sum.ZYTabooIllness);
                    detail.Contraindications=string.Join(",", sum.OccCustomerContraindications);
                    //结果汇总 detail. = dto.;
                    detail.ZYTreatmentAdvice = sum.ZYTreatmentAdvice;
                    detail.PostState = sum.PostState;
                    detail.TotalWorkAge = sum.TotalWorkAge;
                    detail.CustomerRegNum = sum.CustomerRegNum;
                    detail.InjuryAge = sum.InjuryAge;
                    detail.Conclusion = sum.Conclusion;
                    reportJson.Detail.Add(detail);
                    var reportJsonString = JsonConvert.SerializeObject(reportJson);
                    ReportMain.LoadDataFromXML(reportJsonString);
                    //打印后事件
                    ReportMain.PrintEnd += () =>
                    {
                        _printPreviewAppService.UpdateNoticePrintState(new EntityDto<Guid> { Id = sum.CustomerRegBMId.Value });
                        //日志
                        CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                        createOpLogDto.LogBM = sum.CustomerBM;
                        createOpLogDto.LogName = sum.Name;
                        createOpLogDto.LogText = "打印通知单";
                        createOpLogDto.LogDetail = "";
                        createOpLogDto.LogType = (int)LogsTypes.PrintId;
                        _commonAppService.SaveOpLog(createOpLogDto);

                    };

                }           

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



        }

        public void Prints(bool isPreview, EntityDto<Guid> input, string path = "")
        {
            var strBarPrintName = DefinedCacheHelper.GetBasicDictionaryByValue(BasicDictionaryType.PresentationSet, 10).Remarks;
            strBarPrintName = "职业健康检查复查告知书.grf";
            var GrdPath = GridppHelper.GetTemplate(strBarPrintName);
            ReportMain.LoadFromURL(GrdPath);
            OutOccCustomerSumDto sumDto = new OutOccCustomerSumDto();
            sumDto.Id = input.Id;
            var dtos = _OccDisRequisitionAppService.GetOccDisRequisition(sumDto);
            //var cusNameInput = dto;
            var customersum = dtos;

            var reportJson = new ReportJson();
            reportJson.Detail = new List<Detail>();
            var detail = new Detail();
            foreach (var sum in customersum)
            {
                detail.ClientName = sum.ClientName;
                detail.Name = sum.Name;
                detail.Sex = sum.Sex;
                detail.Age = sum.Age;
                detail.IDCardNo = sum.IDCardNo;
                //工龄 detail. = ;
                detail.TypeWork = sum.TypeWork;
                detail.ZYRiskName = sum.ZYRiskName;
                detail.ReviewContent = sum.ReviewContent;

                detail.ReviewContent = sum.ReviewContent;
                detail.ReviewContentDate = sum.ReviewContentDate;
                detail.CustomerRegNum = sum.CustomerRegNum;
                detail.CheckDate = sum.CheckDate;
                //打印后事件
                ReportMain.PrintEnd += () =>
                {
                    _printPreviewAppService.UpdateNoticePrintState(new EntityDto<Guid> { Id = sum.CustomerRegBMId.Value });
                    //日志
                    CreateOpLogDto createOpLogDto = new CreateOpLogDto();
                    createOpLogDto.LogBM = sum.CustomerBM;
                    createOpLogDto.LogName = sum.Name;
                    createOpLogDto.LogText = "打印通知单";
                    createOpLogDto.LogDetail = "";
                    createOpLogDto.LogType = (int)LogsTypes.PrintId;
                    _commonAppService.SaveOpLog(createOpLogDto);

                };
            }
            reportJson.Detail.Add(detail);
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            ReportMain.LoadDataFromXML(reportJsonString);

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


    }
    /// <summary>
    /// 报表
    /// </summary>
    public class ReportJson
    {
        /// <summary>
        /// 明细网格
        /// </summary>
        public List<Detail> Detail { get; set; }
    }
    public class Detail
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string ClientName { get; set; }
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
        /// 工种
        /// </summary>
        public virtual string TypeWork { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public virtual string IDCardNo { get; set; }
        /// <summary>
        /// 危害因素 多个逗号拼接
        /// </summary>
        public virtual string ZYRiskName { get; set; }
        /// <summary>
        /// 复查日期
        /// </summary>
        public virtual DateTime? ReviewContentDate { get; set; }
        /// <summary>
        /// 体检日期
        /// </summary>
        public virtual DateTime? CheckDate { get; set; }
        /// <summary>
        /// 复查项目
        /// </summary>
        public virtual string ReviewContent { get; set; }
        /// <summary>
        /// 疑似职业健康
        /// </summary>
        public virtual string ZYTabooIllness { get; set; }
        /// <summary>
        /// 疑似禁忌症
        /// </summary>
        public virtual string Contraindications { get; set; }
        /// <summary>
        /// 处理意见
        /// </summary>
        public virtual string ZYTreatmentAdvice { get; set; }
        /// <summary>
        /// 职业健康结论
        /// </summary>
        public virtual string Conclusion { get; set; }
        /// <summary>
        /// 总工龄
        /// </summary>
        public virtual string TotalWorkAge { get; set; }
        /// <summary>
        /// 接害工龄
        /// </summary>
        public virtual string InjuryAge { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        public virtual string WorkName { get; set; }
        /// <summary>
        /// 岗位类别
        /// </summary>
        public virtual string PostState { get; set; }
        /// <summary>
        /// 职业病结论描述
        /// </summary>
        public virtual string Description { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public virtual int? CustomerRegNum { get; set; }
    }
}
