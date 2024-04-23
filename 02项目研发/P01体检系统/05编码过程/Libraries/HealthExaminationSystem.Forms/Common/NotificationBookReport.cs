using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using gregn6Lib;
using Newtonsoft.Json;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.CusReg;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.ReVisitTime;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.ApiProxy.WorkType;
using Sw.Hospital.HealthExaminationSystem.Application.Common;
using Sw.Hospital.HealthExaminationSystem.Application.CusReg;
using Sw.Hospital.HealthExaminationSystem.Application.InspectionTotal.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.OccupationalDiseases.OPostState;
using Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime;
using Sw.Hospital.HealthExaminationSystem.Application.ReVisitTime.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.TargetDisease;
using Sw.Hospital.HealthExaminationSystem.Application.WorkType;
using Sw.Hospital.HealthExaminationSystem.Common.Helpers;
using Sw.Hospital.HealthExaminationSystem.Common.Models;

namespace Sw.Hospital.HealthExaminationSystem.Common
{
    /// <summary>
    /// 告知书打印类
    /// </summary>
    public class NotificationBookReport
    {
        /// <summary>
        /// 疑似职业健康告知书打印
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="preview"></param>
        /// <param name="printDialog"></param>
        public static void SuspectedOccupationalDiseasesPrint(TjlCustomerSummarizeDto dto, bool preview = false, bool printDialog = false)
        {
            #region 获取模板定义报表
            var gridppUrl = GridppHelper.GetTemplate("疑似职业健康告知书.grf");
            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            #endregion

            #region 定义应用服务
            ICustomerAppService customerAppService = new CustomerAppService();
            ITargetDiseaseAppService targetDiseaseAppService = new TargetDiseaseAppService();
            #endregion

            #region 查询患者登记信息
            //var customerReg = customerAppService.QueryCustomerReg(new SearchCustomerDto() {  CustomerBM = customRegId })?[0];
            #endregion

            #region 模板数据绑定
            var reportJson = new ReportDataObjectOnlyMaster<SuspectedOccupationalDiseasesDataObject>();
            reportJson.Master = new List<SuspectedOccupationalDiseasesDataObject>();
            var Master = new SuspectedOccupationalDiseasesDataObject();
            Master.CodeID = "";
            Master.CompanyName = dto.CustomerReg.ClientInfo.ClientName;
            Master.HospitalName = "北京市密云区中医医院";
            Master.PatientName = dto.CustomerReg.Customer.Name;
            Master.IDNumber = dto.CustomerReg.Customer.IDCardNo;
            if (!string.IsNullOrWhiteSpace(dto.ZYTabooIllness))
            {
                var DiseaseInfo = targetDiseaseAppService.GetDiseaseContraindicationExplainDtoForId(new Abp.Application.Services.Dto.EntityDto<Guid>() { Id = Guid.Parse(dto.ZYTabooIllness) });
                Master.Illness = DiseaseInfo?.Name;
            }
            reportJson.Master.Add(Master);

            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);

            #endregion

            if (preview)
            {
                report.PrintPreview(true);
                return;
            }

            if (printDialog)
            {
                report.Print(true);
                return;
            }
            report.Print(false);
        }

        /// <summary>
        /// 禁忌证告知书打印
        /// </summary>
        /// <param name="customRegId"></param>
        /// <param name="preview"></param>
        /// <param name="printDialog"></param>
        public static void ContraindicationNotificationPrint(TjlCustomerSummarizeDto dto, bool preview = false, bool printDialog = false)
        {
            #region 获取模板定义报表
            var gridppUrl = GridppHelper.GetTemplate("禁忌证告知书.grf");
            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            #endregion

            #region 定义应用服务
            ICustomerAppService customerAppService = new CustomerAppService();
            IWorkTypeAppService workTypeAppService = new WorkTypeAppService();
            IPostStateAppService postStateAppService = new PostStateAppService();
            #endregion

            #region 查询患者登记信息
            //var customerReg = customerAppService.QueryCustomerReg(new SearchCustomerDto() { CustomerBM = customRegId })?[0];
            #endregion

            #region 模板数据绑定
            var reportJson = new ReportDataObjectOnlyMaster<ContraindicationNotificationReport>();
            reportJson.Master = new List<ContraindicationNotificationReport>();
            var Master = new ContraindicationNotificationReport();
            Master.CodeID = "";
            Master.CompanyName = dto.CustomerReg.ClientInfo.ClientName;
            Master.HospitalName = "北京市密云区中医医院";
            Master.PatientName = dto.CustomerReg.Customer.Name;
            Master.IDNumber = dto.CustomerReg.Customer.IDCardNo;
            if (!string.IsNullOrWhiteSpace(dto.CustomerReg.TypeWork))
            {
                var WorkType = workTypeAppService.Get(new Application.WorkType.Dto.WorkTypeDto() { Id = Guid.Parse(dto.CustomerReg.TypeWork) });
                Master.WorkType = WorkType.Name;
            }
            if (!string.IsNullOrWhiteSpace(dto.CustomerReg.PostState))
            {
                var PostStatus = postStateAppService.Get(new Abp.Application.Services.Dto.EntityDto<Guid>() { Id = Guid.Parse(dto.CustomerReg.PostState) });
                Master.PostStatus = PostStatus.Name;
            }
            reportJson.Master.Add(Master);

            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);

            #endregion

            if (preview)
            {
                report.PrintPreview(true);
                return;
            }

            if (printDialog)
            {
                report.Print(true);
                return;
            }
            report.Print(false);
        }

        /// <summary>
        /// 复诊通知书单人打印
        /// </summary>
        /// <param name="ClientRegId">单位预约ID</param>
        public static void CheckReviewPrint(string ClientRegId, bool preview = false, bool printDialog = false)
        {
            #region 获取模板定义报表
            var gridppUrl = GridppHelper.GetTemplate("复查通知书.grf");
            var report = new GridppReport();
            report.LoadFromURL(gridppUrl);
            #endregion

            #region 定义应用服务
            IReVisitTimeAppService reVisitTimeAppService = new ReVisitTimeAppService();
            ICommonAppService _commonAppService = new CommonAppService();
            #endregion

            #region 查询患者复诊信息
            var ReVisitTimeDto = reVisitTimeAppService.GetReVisitTimeForClientRegId(new QueryCheckReview() { CompanyRegisterId = Guid.Parse(ClientRegId) });
            #endregion

            #region 模板数据绑定
            if (ReVisitTimeDto.Count == 0)
                return;
            var reportJson = new ReportDataObjectOnlyMaster<CheckReviewReport>();
            reportJson.Master = new List<CheckReviewReport>();
            var Master = new CheckReviewReport();
            Master.CodeID = ReVisitTimeDto[0].CustomerRegister.ClientInfo.ClientBM;
            Master.CompanyName = ReVisitTimeDto[0].CustomerRegister.ClientInfo.ClientName;
            Master.HospitalName = "北京市密云区中医医院";
            Master.DatetimeVal = _commonAppService.GetDateTimeNow().Now.ToString("yyyy-MM-dd");
            string ContentStr = "贵单位在我院进行职业健康检查，通过各项检查发现职工";
            var ItemGroupData = ReVisitTimeDto.GroupBy(n => n.ItemGroupId);
            foreach (var itemKey in ItemGroupData)
            {
                string Str = "";
                foreach (var itemValue in itemKey)
                {
                    Str += string.Format("{0}({1})、", itemValue.CustomerRegister.Customer.Name, itemValue.CustomerRegister.Customer.IDCardNo);
                }
                Str += string.Format("检查结果异常，需进一步检查以作出明确结论，请在领取通知单后复查 {0}{1}", itemKey.ToList()[0].ItemGroup.ItemGroupName, Environment.NewLine);
                ContentStr += Str;
            }
            Master.ContentText = ContentStr;
            reportJson.Master.Add(Master);
            var reportJsonString = JsonConvert.SerializeObject(reportJson);
            report.LoadDataFromXML(reportJsonString);

            #endregion

            if (preview)
            {
                report.PrintPreview(true);
                return;
            }

            if (printDialog)
            {
                report.Print(true);
                return;
            }
            report.Print(false);
        }
    }
}
