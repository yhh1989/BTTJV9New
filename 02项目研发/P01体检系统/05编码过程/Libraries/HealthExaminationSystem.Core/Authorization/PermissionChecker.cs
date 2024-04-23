using Abp.Authorization;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization
{
    /// <summary>
    /// 权限检查
    /// </summary>
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        /// <summary>
        /// 权限检查
        /// </summary>
        /// <param name="userManager">用户管理器</param>
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}