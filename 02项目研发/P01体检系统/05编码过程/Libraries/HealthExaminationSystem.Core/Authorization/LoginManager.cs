using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Zero.Configuration;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;
using System.Threading.Tasks;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization
{
    /// <summary>
    /// 登陆管理器
    /// </summary>
    public class LogInManager : AbpLogInManager<Tenant, Role, User>
    {
        /// <summary>
        /// 初始化登陆管理器
        /// </summary>
        /// <param name="userManager">用户管理器</param>
        /// <param name="multiTenancyConfig">多租户配置</param>
        /// <param name="tenantRepository">租户仓储</param>
        /// <param name="unitOfWorkManager">单元管理器</param>
        /// <param name="settingManager">设置管理器</param>
        /// <param name="userLoginAttemptRepository"></param>
        /// <param name="userManagementConfig"></param>
        /// <param name="iocResolver"></param>
        /// <param name="roleManager"></param>
        public LogInManager(
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            IRepository<Tenant> tenantRepository,
            IUnitOfWorkManager unitOfWorkManager,
            ISettingManager settingManager,
            IRepository<UserLoginAttempt, long> userLoginAttemptRepository,
            IUserManagementConfig userManagementConfig, IIocResolver iocResolver,
            RoleManager roleManager)
            : base(
                userManager,
                multiTenancyConfig,
                tenantRepository,
                unitOfWorkManager,
                settingManager,
                userLoginAttemptRepository,
                userManagementConfig,
                iocResolver,
                roleManager)
        {
        }

        /// <summary>
        /// 创建用户登录结果
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public Task<AbpLoginResult<Tenant, User>> CreateLoginResultPAsync(User user, Tenant tenant = null)
        {
            return CreateLoginResultAsync(user, tenant);
        }

        /// <summary>
        /// 使用用户名或邮箱查找用户
        /// </summary>
        /// <param name="userNameOrEmailAddress"></param>
        /// <returns></returns>
        public Task<User> FindByNameOrEmailAsync(string userNameOrEmailAddress)
        {
            return UserManager.FindByNameOrEmailAsync(userNameOrEmailAddress);
        }

        /// <summary>
        /// 使用名称查找租户
        /// </summary>
        /// <param name="tenant"></param>
        /// <returns></returns>
        public Task<Tenant> FindTenantByNameAsync(string tenant)
        {
            return TenantRepository.FirstOrDefaultAsync(r => r.TenancyName == tenant);
        }
    }
}