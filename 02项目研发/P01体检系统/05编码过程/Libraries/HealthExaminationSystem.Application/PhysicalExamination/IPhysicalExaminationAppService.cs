using System.Collections.Generic;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination
{
    public interface IPhysicalExaminationAppService
#if !Proxy
        : IApplicationService
#endif
    {
        /// <summary>
        /// 单位查询
        /// </summary>
        List<ClientRegPhysicalDto> QueryCompany();

        /// <summary>
        /// 个人信息查询
        /// </summary>
        PageResultDto<CustomerRegPhysicalDto> PersonalInformationQuery(PageInputDto<CustomerRegPhysicalDto> input);

    }
}