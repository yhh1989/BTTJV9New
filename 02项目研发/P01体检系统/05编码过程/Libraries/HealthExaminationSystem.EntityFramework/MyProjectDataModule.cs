using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Reflection;
using Abp.Modules;
using Abp.Zero.EntityFramework;
using Sw.Hospital.HealthExaminationSystem.Core;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

//[assembly: DbMappingViewCacheType(typeof(MyProjectDbContext), typeof(MyMappingViewCache))]
namespace Sw.Hospital.HealthExaminationSystem.EntityFramework
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(MyProjectCoreModule))]
    public class MyProjectDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Database.SetInitializer(new CreateDatabaseIfNotExists<MyProjectDbContext>());
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyProjectDbContext, Migrations.Configuration>());
            //new DbMigrator(new Migrations.Configuration()).Update();
            Database.SetInitializer<MyProjectDbContext>(null);
            Configuration.DefaultNameOrConnectionString = "HealthExaminationSystem";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }

    //public class MyMappingViewCache : DbMappingViewCache
    //{
    //    private static readonly string ComputeMappingHashValue;

    //    private static readonly Dictionary<EntitySetBase, DbMappingView> MappingViews;

    //    static MyMappingViewCache()
    //    {
    //        using (var db = new MyProjectDbContext())
    //        {
    //            var objDb = ((IObjectContextAdapter)db).ObjectContext;
    //            var mapping =
    //                (StorageMappingItemCollection)objDb.MetadataWorkspace.GetItemCollection(DataSpace.CSSpace);
    //            MappingViews = mapping.GenerateViews(new List<EdmSchemaError>());
    //            ComputeMappingHashValue = mapping.ComputeMappingHashValue();
    //        }
    //    }

    //    public override DbMappingView GetView(EntitySetBase extent)
    //    {
    //        if (MappingViews.ContainsKey(extent))
    //            return MappingViews[extent];

    //        return null;
    //    }

    //    public override string MappingHashValue => ComputeMappingHashValue;
    //}
}