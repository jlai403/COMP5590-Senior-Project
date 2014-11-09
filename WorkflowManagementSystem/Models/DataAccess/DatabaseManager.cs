using System;
using System.Data.Entity;

namespace MyEntityFramework.Transaction
{
    public class DatabaseManager
    {
        private static DatabaseManager _instance;
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

        public void Delete()
        {
            DbContext.Database.Delete();
        }

        public DbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }
    }
}
