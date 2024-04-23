using System;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Users
{
    public class PersonnelIndividuationConfigAppService : AppServiceApiProxyBase,
        IPersonnelIndividuationConfigAppService
    {
        public List<UserIndividuationConfigsDto> GetAllUsers()
        {
            return GetResult<List<UserIndividuationConfigsDto>>(DynamicUriBuilder.GetAppSettingValue());
        }

        public List<UserIndividuationConfigsDto> GetUsersByCondition(SearchUserIndividuationConfigDto input)
        {
            return GetResult<SearchUserIndividuationConfigDto, List<UserIndividuationConfigsDto>>(input,
                DynamicUriBuilder.GetAppSettingValue());
        }

        public UserIndividuationConfigsDto GetUserById(EntityDto<long> input)
        {
            return GetResult<EntityDto<long>, UserIndividuationConfigsDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void CreatePersonnelIndividuationConfig(CreateOrUpdatePersonnelIndividuationConfigDto input)
        {
            GetResult<CreateOrUpdatePersonnelIndividuationConfigDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void UpdatePersonnelIndividuationConfig(CreateOrUpdatePersonnelIndividuationConfigDto input)
        {
            GetResult<CreateOrUpdatePersonnelIndividuationConfigDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<StartupMenuBarDto> GetAllStartupMenuBars()
        {
            return GetResult<List<StartupMenuBarDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
    }
}