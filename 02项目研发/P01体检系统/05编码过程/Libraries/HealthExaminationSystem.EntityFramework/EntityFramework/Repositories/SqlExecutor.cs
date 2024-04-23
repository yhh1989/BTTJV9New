using System.Data.Entity;
using System.Linq;
using Abp.Dependency;
using Abp.EntityFramework;
using Sw.Hospital.HealthExaminationSystem.Core;

namespace Sw.Hospital.HealthExaminationSystem.EntityFramework.EntityFramework.Repositories
{
    /// <inheritdoc cref="ISqlExecutor" />
    public class SqlExecutor : ISqlExecutor, ITransientDependency
    {
        private readonly IDbContextProvider<MyProjectDbContext> _myProjectDbContextProvider;

        public SqlExecutor(IDbContextProvider<MyProjectDbContext> myProjectDbContextProvider)
        {
            _myProjectDbContextProvider = myProjectDbContextProvider;
        }

        public DbContext DbContext => _myProjectDbContextProvider.GetDbContext();

        /// <inheritdoc />
        public int Execute(string sql, params object[] parameters)
        {
           
            return _myProjectDbContextProvider.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
            
        }

        /// <inheritdoc />
        public IQueryable<T> SqlQuery<T>(string sql, params object[] parameters)
        {
            return _myProjectDbContextProvider.GetDbContext().Database.SqlQuery<T>(sql, parameters).AsQueryable();
        }
    }
}