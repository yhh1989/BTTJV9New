using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Company.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Market.Dto;
#if Application
using Abp.Application.Services;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Company
{
    /// <summary>
    /// 团体及团体相关的应用服务
    /// </summary>
    public interface ICompanyRelatedAppService
#if Application
        : IApplicationService
#endif
    {
        /// <summary>
        /// 通过单位预约标识获取最后一条打印记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CompanyReportPrintRecordDto GetLastRecordByCompanyRegisterId(EntityDto<Guid> input);

        /// <summary>
        /// 创建团体报告打印记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CompanyReportPrintRecordDto CreateCompanyReportPrintRecord(CreateCompanyReportPrintRecordDto input);

        /// <summary>
        /// 通过标识获取单位预约信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        CompanyRegisterForPrintDto GetCompanyRegisterById(EntityDto<Guid> input);

        /// <summary>
        /// 使用公司标识为下拉框类查询公司预约记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CompanyRegisterDtoForComboBox>> QueryCompanyRegisterForComboBoxByCompanyId(EntityDto<Guid> input);

        /// <summary>
        /// 使用公司预约标识为下拉框类查询公司预约分组信息记录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CompanyRegisterTeamInformationDtoForComboBox>>
            QueryCompanyRegisterTeamInformationForComboBoxByCompanyRegisterId(EntityDto<Guid> input);

        /// <summary>
        /// 使用标识获取公司信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<CompanyInformationDto> CompanyInformationById(EntityDto<Guid> input);

        /// <summary>
        /// 使用条件检索公司信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CompanyInformationDtoNo2>> CompanyInformationListByCondition(CompanyFilterInput input);

        /// <summary>
        /// 使用父类标识获取单位信息列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CompanyInformationDtoNo3>> CompanyInformationListByParentId(EntityDto<Guid> input);
    }
}