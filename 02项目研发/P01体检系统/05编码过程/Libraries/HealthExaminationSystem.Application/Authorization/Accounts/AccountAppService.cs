using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts.Dto;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts
{
    /// <summary>
    /// 账户应用服务
    /// </summary>
    public class AccountAppService : MyProjectAppServiceBase, IAccountAppService
    {
        private readonly UserRegistrationManager _userRegistrationManager;


        /// <summary>
        /// 账户应用服务
        /// </summary>
        /// <param name="userRegistrationManager">用户注册管理器</param>
        public AccountAppService(
            UserRegistrationManager userRegistrationManager)
        {
            _userRegistrationManager = userRegistrationManager;
        }
         

        /// <summary>
        /// 租户有效性验证
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                false
            );

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
            };
        }
    }
}