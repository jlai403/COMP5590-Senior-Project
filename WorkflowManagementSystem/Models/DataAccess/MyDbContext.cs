using System.Data.Entity;
using System.Linq;
using MyEntityFramework.Entity;

namespace MyEntityFramework.Transaction
{
    public class MyDbContext : DbContext
    {
        protected MyDbContext() : base()
        {
        }

        protected MyDbContext(string connectionString) : base(connectionString)
        {
        }

        public void AddEntity(IEntity entity)
        {
            Set(entity.GetType()).Add(entity);
        }

        public IQueryable<T> Queryable<T>() where T : class
        {
            return Set<T>();
        }
    }
}