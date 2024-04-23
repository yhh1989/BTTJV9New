using Abp.MultiTenancy;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;

namespace Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy
{
    /// <inheritdoc />
    public class Tenant : AbpTenant<User>
    {
        /// <inheritdoc />
        public Tenant()
        {
        }

        /// <inheritdoc />
        public Tenant(string tenancyName, string name)
            : base(tenancyName, name)
        {
        }
    }
}