using System;
using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    public interface IFormRoleAppService
#if !Proxy
        : IApplicationService 
#endif
    {
        List<FormRoleDto> GetAll();

        List<FormRoleViewDto> GetAllForView();

        List<FormModuleViewDto> GetAllFormModules();

        FormRoleViewDto GetById(EntityDto<Guid> input);

        void Create(CreateFormRoleDto input);

        void Update(UpdateFormRoleDto input);

        void Delete(EntityDto<Guid> input);
    }
}