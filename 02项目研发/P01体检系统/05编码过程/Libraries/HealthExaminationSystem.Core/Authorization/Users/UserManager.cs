using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.IdentityFramework;
using Abp.Localization;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users
{
    /// <summary>
    /// 用户管理
    /// </summary>
    public class UserManager : AbpUserManager<Role, User>
    {
        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="userStore"></param>
        /// <param name="roleManager"></param>
        /// <param name="permissionManager"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="cacheManager"></param>
        /// <param name="organizationUnitRepository"></param>
        /// <param name="userOrganizationUnitRepository"></param>
        /// <param name="organizationUnitSettings"></param>
        /// <param name="localizationManager"></param>
        /// <param name="settingManager"></param>
        /// <param name="emailService"></param>
        /// <param name="userTokenProviderAccessor"></param>
        public UserManager(
            UserStore userStore,
            RoleManager roleManager,
            IPermissionManager permissionManager,
            IUnitOfWorkManager unitOfWorkManager,
            ICacheManager cacheManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IOrganizationUnitSettings organizationUnitSettings,
            ILocalizationManager localizationManager,
            ISettingManager settingManager,
            IdentityEmailMessageService emailService,
            IUserTokenProviderAccessor userTokenProviderAccessor)
            : base(
                userStore,
                roleManager,
                permissionManager,
                unitOfWorkManager,
                cacheManager,
                organizationUnitRepository,
                userOrganizationUnitRepository,
                organizationUnitSettings,
                localizationManager,
                emailService,
                settingManager,
                userTokenProviderAccessor)
        {
        }
    }
}