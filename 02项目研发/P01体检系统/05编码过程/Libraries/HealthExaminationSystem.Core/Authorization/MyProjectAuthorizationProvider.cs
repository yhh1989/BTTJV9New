using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization
{
    /// <summary>
    /// 授权的供应商
    /// </summary>
    public class MyProjectAuthorizationProvider : AuthorizationProvider
    {
        /// <summary>
        /// 设置权限
        /// </summary>
        /// <param name="context">权限定义上下文</param>
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"),
                multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, MyProjectConsts.LocalizationSourceName);
        }
    }
}