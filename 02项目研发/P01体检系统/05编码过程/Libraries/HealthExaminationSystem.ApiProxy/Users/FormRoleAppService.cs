using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Users
{
    public class FormRoleAppService : AppServiceApiProxyBase, IFormRoleAppService
    {
        public List<FormRoleDto> GetAll()
        {
            return GetResult<List<FormRoleDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FormRoleViewDto> GetAllForView()
        {
            return GetResult<List<FormRoleViewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<FormModuleViewDto> GetAllFormModules()
        {
            return GetResult<List<FormModuleViewDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public FormRoleViewDto GetById(EntityDto<Guid> input)
        {
            return GetResult<EntityDto<Guid>, FormRoleViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Create(CreateFormRoleDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Update(UpdateFormRoleDto input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void Delete(EntityDto<Guid> input)
        {
            GetResult(input, DynamicUriBuilder.GetAppSettingValue());
        }
    }
}