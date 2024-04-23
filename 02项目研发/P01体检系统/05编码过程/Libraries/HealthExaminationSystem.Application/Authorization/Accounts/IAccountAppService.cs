using System.Threading.Tasks;
using Abp.Application.Services;
using Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts.Dto;

namespace Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts
{
    /// <summary>
    /// 账户应用服务
    /// </summary>
    public interface IAccountAppService : IApplicationService
    {
        /// <summary>
        /// 租户是否可用
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns></returns>
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        /// <summary>
        /// 注册器
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<RegisterOutput> Register(RegisterInput input);
    }
}
