using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Sw.Hospital.HealthExaminationSystem.Application.Users.Dto;
#if !Proxy
using Sw.Hospital.HealthExaminationSystem.Application.Roles.Dto;
#endif

namespace Sw.Hospital.HealthExaminationSystem.Application.Users
{
    /// <summary>
    /// 用户应用服务
    /// </summary>
    public interface IUserAppService
#if !Proxy
        : IAsyncCrudAppService<UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>
#endif
    {
#if !Proxy
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <returns></returns>
        Task<ListResultDto<RoleDto>> GetRoles();
#endif

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        UserViewDto GetUser(EntityDto<long> input);

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        List<UserFormDto> GetUsers();
        /// <summary>
        /// 单位审批人员列表
        /// </summary>
        /// <returns></returns>
        List<UserClientZKDto> GetClientZKUsers();
        /// <summary>
        /// 搜索用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        List<UserFormDto> GetUsersByCondition(SearchUserDto input);

        List<UserFormDto> GetUsersByName(SearchUserDto input);

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input"></param>
        void CreateUser(CreateUserFormDto input);
        /// <summary>
        /// 创建指定租户用户
        /// </summary>
        /// <param name="input"></param>
        void CreateUserHasTeandID(CreateUserFormTeandIDDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input"></param>
        void UpdateUser(UpdateUserFormDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="input"></param>
        void DeleteUser(EntityDto<long> input);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        void UpdatePassword(UpdatePwdDto input);

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="input"></param>
        void ResetPassword(UpdatePwdDto input);
        /// <summary>
        /// 保存登录信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        LoginlistDto SaveLogin(LoginlistDto input);
        /// <summary>
        /// 删除登录信息
        /// </summary>
        /// <param name="input"></param>
        void DelLogin(LoginlistDto input);

        bool QueLogin(LoginlistDto input);
        /// <summary>
        /// 判断是否有登录用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        bool QueOnlinLogin(LoginlistDto input);
        /// <summary>
        /// 获取下拉框绑定用户列表
        /// </summary>
        /// <returns></returns>

#if Application
        Task<List<UserForComboDto>> GetComboUsers();
#elif Proxy
        List<UserForComboDto> GetComboUsers();
#endif
        /// <summary>
        /// 判断用户名   密码是否一致
        /// </summary>
        /// <param name="input"></param>
        bool VerificationUser(VerificationUserDto input);
    }
}