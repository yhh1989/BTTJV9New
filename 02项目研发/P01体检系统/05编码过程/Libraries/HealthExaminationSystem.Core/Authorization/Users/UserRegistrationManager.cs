using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Authorization.Users;
using Abp.Domain.Services;
using Abp.IdentityFramework;
using Abp.Runtime.Session;
using Abp.UI;
using Microsoft.AspNet.Identity;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Roles;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;

namespace Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users
{
    /// <summary>
    /// 用户注册管理
    /// </summary>
    public class UserRegistrationManager : DomainService
    {
        private readonly RoleManager _roleManager;

        private readonly TenantManager _tenantManager;

        private readonly UserManager _userManager;

        /// <summary>
        /// 用户注册管理
        /// </summary>
        /// <param name="tenantManager"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public UserRegistrationManager(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;

            AbpSession = NullAbpSession.Instance;
        }

        /// <summary>
        /// ABP 会话
        /// </summary>
        public IAbpSession AbpSession { get; set; }

        /// <summary>
        /// 注册（异步）
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="emailAddress"></param>
        /// <param name="userName"></param>
        /// <param name="plainPassword"></param>
        /// <param name="isEmailConfirmed"></param>
        /// <returns></returns>
        public async Task<User> RegisterAsync(string name, string surname, string emailAddress, string userName,
            string plainPassword, bool isEmailConfirmed)
        {
            CheckForTenant();

            var tenant = await GetActiveTenantAsync();

            var user = new User
            {
                TenantId = tenant.Id,
                Name = name,
                Surname = surname,
                EmailAddress = emailAddress,
                IsActive = true,
                UserName = userName,
                IsEmailConfirmed = true,
                Roles = new List<UserRole>()
            };

            user.Password = new PasswordHasher().HashPassword(plainPassword);

            foreach (var defaultRole in _roleManager.Roles.Where(r => r.IsDefault).ToList())
                user.Roles.Add(new UserRole(tenant.Id, user.Id, defaultRole.Id));

            CheckErrors(await _userManager.CreateAsync(user));
            await CurrentUnitOfWork.SaveChangesAsync();

            return user;
        }

        private void CheckForTenant()
        {
            if (!AbpSession.TenantId.HasValue) throw new InvalidOperationException("Can not register host users!");
        }

        private async Task<Tenant> GetActiveTenantAsync()
        {
            if (!AbpSession.TenantId.HasValue) return null;

            return await GetActiveTenantAsync(AbpSession.TenantId.Value);
        }

        private async Task<Tenant> GetActiveTenantAsync(int tenantId)
        {
            var tenant = await _tenantManager.FindByIdAsync(tenantId);
            if (tenant == null) throw new UserFriendlyException(L("UnknownTenantId{0}", tenantId));

            if (!tenant.IsActive) throw new UserFriendlyException(L("TenantIdIsNotActive{0}", tenantId));

            return tenant;
        }

        /// <summary>
        /// 检查错误
        /// </summary>
        /// <param name="identityResult">一致性结果</param>
        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}