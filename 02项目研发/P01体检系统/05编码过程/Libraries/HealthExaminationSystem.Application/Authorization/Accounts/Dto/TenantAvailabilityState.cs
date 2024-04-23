namespace Sw.Hospital.HealthExaminationSystem.Application.Authorization.Accounts.Dto
{
    /// <summary>
    /// 租户可用性状态
    /// </summary>
    public enum TenantAvailabilityState
    {
        /// <summary>
        /// 可用的
        /// </summary>
        Available = 1,
        /// <summary>
        /// 闲置的
        /// </summary>
        InActive,
        /// <summary>
        /// 没有发现
        /// </summary>
        NotFound
    }
}