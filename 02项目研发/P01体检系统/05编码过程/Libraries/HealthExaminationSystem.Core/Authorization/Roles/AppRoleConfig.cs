using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles
{
    /// <summary>
    /// 应用角色配置
    /// </summary>
    public static class AppRoleConfig
    {
        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="roleManagementConfig">角色管理器配置</param>
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host)
            );

            //Static tenant roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant)
            );
        }
    }
}