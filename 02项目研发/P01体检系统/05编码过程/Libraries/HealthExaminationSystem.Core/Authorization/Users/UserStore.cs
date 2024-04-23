using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users
{
    /// <summary>
    /// 用户存储
    /// </summary>
    public class UserStore : AbpUserStore<Role, User>
    {
        /// <summary>
        /// 用户存储
        /// </summary>
        /// <param name="userRepository"></param>
        /// <param name="userLoginRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="userPermissionSettingRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="userClaimStore"></param>
        /// <param name="userOrganizationUnitRepository"></param>
        /// <param name="organizationUnitRoleRepository"></param>
        public UserStore(
            IRepository<User, long> userRepository,
            IRepository<UserLogin, long> userLoginRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<Role> roleRepository,
            IRepository<UserPermissionSetting, long> userPermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserClaim, long> userClaimStore,
            IRepository<UserOrganizationUnit, long> userOrganizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(
                userRepository,
                userLoginRepository,
                userRoleRepository,
                roleRepository,
                userPermissionSettingRepository,
                unitOfWorkManager,
                userClaimStore,
                userOrganizationUnitRepository,
                organizationUnitRoleRepository)
        {
        }
    }
}