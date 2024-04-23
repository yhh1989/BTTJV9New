using System.Data.Entity.Migrations;
using Abp.MultiTenancy;
using Abp.Zero.EntityFramework;
using EntityFramework.DynamicFilters;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations.SeedData;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<MyProjectDbContext>, IMultiTenantSeed
    {
        public AbpTenantBase Tenant { get; set; }

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "HealthExaminationSystem";
            CommandTimeout = 60 * 60;
        }

        protected override void Seed(MyProjectDbContext context)
        {
            context.DisableAllFilters();

            if (Tenant == null)
            {
                //Host seed
                new InitialHostDbBuilder(context).Create();

                //Default tenant seed (in host database).
                new DefaultTenantCreator(context).Create();
                new TenantRoleAndUserBuilder(context, 1).Create();

                new InitialPermissionModule(context).Create();
                new InitialAdministrativeDivision(context).Create();
                new InitializeStartupMenuBar(context).Create();
                //new InitialTestData(_context).Create();
                //new InitialFormRoleCreator(context).Create();
            }
            else
            {
                //You can add seed for tenant databases and use Tenant property...
            }

            context.SaveChanges();
        }
    }
}

// Ö´ÐÐÇ¨ÒÆÓï¾ä
// /*Update-Database -StartUpProjectName HealthExaminationSystem.Web -ProjectName HealthExaminationSystem.EntityFramework -Force*/