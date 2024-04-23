namespace Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts.Dto
{
    /// <summary>
    /// 租户有效性验证输出数据传输
    /// </summary>
    public class IsTenantAvailableOutput
    {
        /// <summary>
        /// 有效性状态
        /// </summary>
        public TenantAvailabilityState State { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public int? TenantId { get; set; }

        /// <summary>
        /// 租户有效性验证输出数据传输
        /// </summary>
        public IsTenantAvailableOutput()
        {

        }

        /// <summary>
        /// 租户有效性验证输出数据传输
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="tenantId">标识</param>
        public IsTenantAvailableOutput(TenantAvailabilityState state, int? tenantId = null)
        {
            State = state;
            TenantId = tenantId;
        }
    }
}