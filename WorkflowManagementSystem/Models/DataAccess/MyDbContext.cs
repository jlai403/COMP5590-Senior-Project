using System.Data.Entity;
using System.Linq;
using MyEntityFramework.Entity;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class MyDbContext : DbContext
    {
        public ICleanUpData _testCleanUpInitializer = new DefaultTestCleanUpInitializer();

        protected MyDbContext() : base()
        {
        }

        protected MyDbContext(string connectionString) : base(connectionString)
        {
        }

        public void AddEntity(IEntity entity)
        {
            _testCleanUpInitializer.EntityCreated(entity);
            Set(entity.GetType()).Add(entity);
        }

        public void RemoveEntity(IEntity entity)
        {
            _testCleanUpInitializer.EntityRemoved(entity);
            Set(entity.GetType()).Remove(entity);
        }

        public IQueryable<T> Queryable<T>() where T : class
        {
            return Set<T>();
        }

        public void CleanUp()
        {
            _testCleanUpInitializer.CleanUp();
        }
    }
}