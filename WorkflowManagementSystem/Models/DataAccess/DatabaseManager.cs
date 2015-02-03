using System.Data;
using System.Data.Entity;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;

        public ICleanUpData _testCleanUpListener = new NullTestCleanUpInitializer();

        public static DatabaseManager Instance
        {
            get { return _instance ?? (_instance = new DatabaseManager()); }
        }

        public MyDbContext DbContext { get; set; }


        public void Initialize(MyDbContext dbContext)
        {
            DbContext = dbContext;
            DbContext.Database.Initialize(true);
        }

        public DbContextTransaction BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return DbContext.Database.BeginTransaction(isolationLevel);
        }

        public void CleanUp()
        {
            _testCleanUpListener.CleanUp();
        }

        public void Delete()
        {
            DbContext.Database.Delete();
        }
    }
}
