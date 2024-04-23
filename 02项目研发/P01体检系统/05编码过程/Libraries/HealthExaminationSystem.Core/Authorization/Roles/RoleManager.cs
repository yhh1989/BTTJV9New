using Abp.Authorization;
using Abp.Authorization.Roles;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Organizations;
using Abp.Runtime.Caching;
using Abp.Zero.Configuration;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles
{
    /// <summary>
    /// 角色管理器
    /// </summary>
    public class RoleManager : AbpRoleManager<Role, User>
    {
        /// <summary>
        /// 角色管理器
        /// </summary>
        /// <param name="store">角色仓库</param>
        /// <param name="permissionManager">权限管理器</param>
        /// <param name="roleManagementConfig">角色管理配置</param>
        /// <param name="cacheManager">缓存管理器</param>
        /// <param name="unitOfWorkManager">单元管理器</param>
        /// <param name="organizationUnitRepository"></param>
        /// <param name="organizationUnitRoleRepository"></param>
        public RoleManager(
            RoleStore store,
            IPermissionManager permissionManager,
            IRoleManagementConfig roleManagementConfig,
            ICacheManager cacheManager,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<OrganizationUnit, long> organizationUnitRepository,
            IRepository<OrganizationUnitRole, long> organizationUnitRoleRepository)
            : base(
                store,
                permissionManager,
                roleManagementConfig,
                cacheManager,
                unitOfWorkManager,
                organizationUnitRepository,
                organizationUnitRoleRepository)
        {
        }
    }
}