using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles
{
    /// <summary>
    /// 角色仓库
    /// </summary>
    public class RoleStore : AbpRoleStore<Role, User>
    {
        /// <summary>
        /// 角色仓库
        /// </summary>
        /// <param name="roleRepository">角色仓储</param>
        /// <param name="userRoleRepository">用户角色仓储</param>
        /// <param name="rolePermissionSettingRepository">角色权限设置仓储</param>
        /// <param name="unitOfWorkManager"></param>
        public RoleStore(
            IRepository<Role> roleRepository,
            IRepository<UserRole, long> userRoleRepository,
            IRepository<RolePermissionSetting, long> rolePermissionSettingRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                roleRepository,
                userRoleRepository,
                rolePermissionSettingRepository,
                unitOfWorkManager)
        {
        }
    }
}