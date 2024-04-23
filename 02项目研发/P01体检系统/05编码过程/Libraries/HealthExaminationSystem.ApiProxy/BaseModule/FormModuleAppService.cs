using System.Collections.Generic;
using Sw.Hospital.HealthExaminationSystem.ApiProxy;
using Sw.Hospital.HealthExaminationSystem.Application.BaseModule.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.BaseModule
{
    public class FormModuleAppService : AppServiceApiProxyBase, IFormModuleAppService
    {
        public List<FormModuleDto> GetByNames(FindNameDto input)
        {
            return GetResult<FindNameDto, List<FormModuleDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public FormModuleDto GetByName(NameDto input)
        {
            return GetResult<NameDto, FormModuleDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FormModuleDto> GetAllList()
        {
            return GetResult<List<FormModuleDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}