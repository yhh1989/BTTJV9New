using Abp.Application.Features;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Runtime.Caching;
using Sw.Hospital.HealthExaminationSystem.Core.Authorization.Users;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;

namespace Sw.Hospital.HealthExaminationSystem.Core.Features
{
    /// <summary>
    /// 特征值存储
    /// </summary>
    public class FeatureValueStore : AbpFeatureValueStore<Tenant, User>
    {
        /// <summary>
        /// 特征值存储
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="tenantFeatureRepository"></param>
        /// <param name="tenantRepository"></param>
        /// <param name="editionFeatureRepository"></param>
        /// <param name="featureManager"></param>
        /// <param name="unitOfWorkManager"></param>
        public FeatureValueStore(
            ICacheManager cacheManager,
            IRepository<TenantFeatureSetting, long> tenantFeatureRepository,
            IRepository<Tenant> tenantRepository,
            IRepository<EditionFeatureSetting, long> editionFeatureRepository,
            IFeatureManager featureManager,
            IUnitOfWorkManager unitOfWorkManager)
            : base(cacheManager,
                tenantFeatureRepository,
                tenantRepository,
                editionFeatureRepository,
                featureManager,
                unitOfWorkManager)
        {
        }
    }
}