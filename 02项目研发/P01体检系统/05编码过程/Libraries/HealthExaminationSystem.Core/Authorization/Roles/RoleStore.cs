using Abp.Authorization.Roles;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles
{
    /// <summary>
    /// ��ɫ�ֿ�
    /// </summary>
    public class RoleStore : AbpRoleStore<Role, User>
    {
        /// <summary>
        /// ��ɫ�ֿ�
        /// </summary>
        /// <param name="roleRepository">��ɫ�ִ�</param>
        /// <param name="userRoleRepository">�û���ɫ�ִ�</param>
        /// <param name="rolePermissionSettingRepository">��ɫȨ�����òִ�</param>
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