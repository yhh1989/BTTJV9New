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
    /// ��ɫ������
    /// </summary>
    public class RoleManager : AbpRoleManager<Role, User>
    {
        /// <summary>
        /// ��ɫ������
        /// </summary>
        /// <param name="store">��ɫ�ֿ�</param>
        /// <param name="permissionManager">Ȩ�޹�����</param>
        /// <param name="roleManagementConfig">��ɫ��������</param>
        /// <param name="cacheManager">���������</param>
        /// <param name="unitOfWorkManager">��Ԫ������</param>
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