using Microsoft.EntityFrameworkCore;
using FoodForWeek.Tests.Tools.DataTools;
using Microsoft.Data.Sqlite;

namespace FoodForWeek.Tests.RepositoryTests
{
    public abstract class BaseRepositoryTests<T> where T: class
    {
        protected  DAL.AppContext _mryContext;
        protected  DbSet<T> _mrySet;
        private SqliteConnection _connection;
        public BaseRepositoryTests()
        {
            _mryContext = new DAL.AppContext(SetupOptionsForSqlite<DAL.AppContext>());
            _mryContext.Database.EnsureCreated();
            _mryContext.SeedData();
            _mrySet = _mryContext.Set<T>();
        }
        protected virtual DbContextOptions<U> SetupOptionsForSqlite<U>() where U: DbContext
        {
            _connection = new SqliteConnection("Datasource=:memory:");
            var optBuilder = new DbContextOptionsBuilder<U>();
            optBuilder.UseSqlite(_connection);
            _connection.Open();
            return optBuilder.Options;
        }
        protected void AbortConnection()
        {
            _connection.Close();
        }
    }
}
