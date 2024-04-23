using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.MultiTenancy;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.Editions;

namespace Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy
{
    /// <inheritdoc />
    public class TenantManager : AbpTenantManager<Tenant, User>
    {
        /// <inheritdoc />
        public TenantManager(
            IRepository<Tenant> tenantRepository,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            EditionManager editionManager,
            IAbpZeroFeatureValueStore featureValueStore
        )
            : base(
                tenantRepository,
                tenantFeatureRepository,
                editionManager,
                featureValueStore
            )
        {
        }
    }
}