using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.Common.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.PhysicalExamination
{
    public class PhysicalExaminationAppService : AppServiceApiProxyBase, IPhysicalExaminationAppService
    {
        public List<ClientRegPhysicalDto> QueryCompany()
        {
            return GetResult<List<ClientRegPhysicalDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public PageResultDto<CustomerRegPhysicalDto> PersonalInformationQuery(PageInputDto<CustomerRegPhysicalDto> input)
        {
            return GetResult<PageInputDto<CustomerRegPhysicalDto>, PageResultDto<CustomerRegPhysicalDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}