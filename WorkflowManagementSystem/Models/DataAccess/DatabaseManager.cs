using System.Configuration;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;

        public ICleanUpData _testCleanUpListener = new NullTestCleanUpInitializer();
        public ICreateUnitOfWork _newUnitOfWorkCreationStrategy = new DefaultUnitOfWorkCreationgStrategy();


        public static DatabaseManager Instance
        {
            get { return _instance ?? (_instance = new DatabaseManager()); }
        }

        public void Initialize(MyDbContext dbContext)
        {
            dbContext.Database.Initialize(true);
        }

        public void CleanUp()
        {
            _testCleanUpListener.CleanUp();
        }

        public MyDbContext CreateNewUnitOfWork()
        {
            return _newUnitOfWorkCreationStrategy.CreateNewUnitOfWork();
        }


        public void Delete(MyDbContext dbContext)
        {
            dbContext.Database.Delete();
        }
    }

    public interface ICreateUnitOfWork
    {
        MyDbContext CreateNewUnitOfWork();
    }

    public class DefaultUnitOfWorkCreationgStrategy : ICreateUnitOfWork
    {
        public MyDbContext CreateNewUnitOfWork()
        {
            return new WorkflowManagementSystemDbContext();
        }
    }

    public class TestUnitOfWorkCreationStategy : ICreateUnitOfWork
    {
        public MyDbContext CreateNewUnitOfWork()
        {
            return new WorkflowManagementSystemDbContext(ConfigurationManager.ConnectionStrings["TestDbContext"].ConnectionString);
        }
    }
}
