using System.Linq;
using Sw.Hospital.HealthExaminationSystem.Core.MultiTenancy;
using Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly MyProjectDbContext _context;

        public DefaultTenantCreator(MyProjectDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
