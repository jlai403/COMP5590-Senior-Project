using System;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class TransactionHandler
    {
        private static TransactionHandler _instance;

        public static TransactionHandler Instance
        {
            get { return _instance ?? (_instance = new TransactionHandler()); }
        }

        private TransactionHandler()
        {
        }

        public dynamic Execute(Func<object> transaction)
        {
            dynamic result = null;
            using (var beginTransaction = DatabaseManager.Instance.BeginTransaction())
            {
                try
                {
                    result = transaction.Invoke();
                    DatabaseManager.Instance.DbContext.SaveChanges();
                    beginTransaction.Commit();
                }
                catch (Exception e)
                {
                    beginTransaction.Rollback();
                    throw;
                }
                finally
                {
                    beginTransaction.Dispose();
                }
            }
            return result;
        }
    }
}
