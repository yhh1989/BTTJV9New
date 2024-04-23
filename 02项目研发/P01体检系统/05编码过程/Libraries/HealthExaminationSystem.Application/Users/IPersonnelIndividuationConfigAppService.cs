using System.Collections.Generic;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    public interface IPersonnelIndividuationConfigAppService
#if Application
        : IApplicationService
#endif
    {
        List<UserIndividuationConfigsDto> GetAllUsers();

        List<UserIndividuationConfigsDto> GetUsersByCondition(SearchUserIndividuationConfigDto input);

        UserIndividuationConfigsDto GetUserById(EntityDto<long> input);

        void CreatePersonnelIndividuationConfig(CreateOrUpdatePersonnelIndividuationConfigDto input);

        void UpdatePersonnelIndividuationConfig(CreateOrUpdatePersonnelIndividuationConfigDto input);

        List<StartupMenuBarDto> GetAllStartupMenuBars();
    }
}