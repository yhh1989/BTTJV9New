using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;

namespace Sw.Hospital.HealthExaminationSystem.ApiProxy.Users
{
    public class UserAppService : AppServiceApiProxyBase, IUserAppService
    {
        public UserViewDto GetUser(EntityDto<long> input)
        {
            return GetResult<EntityDto<long>, UserViewDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public List<UserFormDto> GetUsers()
        {
            return GetResult<List<UserFormDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<UserClientZKDto> GetClientZKUsers()
        {
            return GetResult<List<UserClientZKDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public List<UserFormDto> GetUsersByCondition(SearchUserDto input)
        {
            return GetResult<SearchUserDto, List<UserFormDto>>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public List<UserFormDto> GetUsersByName(SearchUserDto input)
        {
            return GetResult<SearchUserDto, List<UserFormDto>>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public void CreateUser(CreateUserFormDto input)
        {
            GetResult<CreateUserFormDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void CreateUserHasTeandID(CreateUserFormTeandIDDto input)
        {
            GetResult<CreateUserFormTeandIDDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void UpdateUser(UpdateUserFormDto input)
        {
            GetResult<UpdateUserFormDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void DeleteUser(EntityDto<long> input)
        {
            GetResult<EntityDto<long>>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void UpdatePassword(UpdatePwdDto input)
        {
            GetResult<UpdatePwdDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }

        public void ResetPassword(UpdatePwdDto input)
        {
            GetResult<UpdatePwdDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public LoginlistDto SaveLogin(LoginlistDto input)
        {
            return GetResult<LoginlistDto, LoginlistDto>(input, DynamicUriBuilder.GetAppSettingValue());
        }
        public void DelLogin(LoginlistDto input)
        {
            GetResult<LoginlistDto>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public bool QueLogin(LoginlistDto input)
        {
            return GetResult<LoginlistDto,bool>(input, DynamicUriBuilder.GetAppSettingValue());

        }
        public bool QueOnlinLogin(LoginlistDto input)
        {
            return GetResult<LoginlistDto, bool>(input, DynamicUriBuilder.GetAppSettingValue());

        }

        public List<UserForComboDto> GetComboUsers()
        {
            return GetResult<List<UserForComboDto>>(DynamicUriBuilder.GetAppSettingValue());
        }
        public bool VerificationUser(VerificationUserDto input)
        {
          return   GetResult<VerificationUserDto,bool>(input, DynamicUriBuilder.GetAppSettingValue());
          
        }
    }
}