using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations
{
    public class AbpZeroDbMigrator : AbpZeroDbMigrator<MyProjectDbContext, Configuration>
    {
        public AbpZeroDbMigrator(
            IUnitOfWorkManager unitOfWorkManager,
            IDbPerTenantConnectionStringResolver connectionStringResolver,
            IIocResolver iocResolver)
            : base(
                unitOfWorkManager,
                connectionStringResolver,
                iocResolver)
        {
        }
    }
}
