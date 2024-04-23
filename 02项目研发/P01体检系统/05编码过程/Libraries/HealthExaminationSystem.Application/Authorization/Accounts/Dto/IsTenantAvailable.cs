using System.ComponentModel.DataAnnotations;
using Abp.MultiTenancy;

namespace Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts.Dto
{
    /// <summary>
    /// 租户有效性验证输入数据传输
    /// </summary>
    public class IsTenantAvailableInput
    {
        /// <summary>
        /// 租户名称
        /// </summary>
        [Required]
        [MaxLength(AbpTenantBase.MaxTenancyNameLength)]
        public string TenancyName { get; set; }
    }
}
