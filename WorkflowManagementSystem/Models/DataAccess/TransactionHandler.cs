using System;
using System.Data.Entity;
using System.Threading;

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

        //public dynamic Execute(Func<object> transaction)
        //{
        //    dynamic result = null;
        //    using (var beginTransaction = DatabaseManager.Instance.BeginTransaction())
        //    {
        //        try
        //        {
        //            result = transaction.Invoke();
        //            DatabaseManager.Instance.DbContext.SaveChanges();
        //            beginTransaction.Commit();
        //        }
        //        catch (Exception e)
        //        {
        //            DatabaseManager.Instance.DbContext.SaveChanges();
        //            beginTransaction.Rollback();
        //            throw;
        //        }
        //    }
        //    return result;
        //}

        public dynamic Execute(Func<object> transaction)
        {
            dynamic result = null;
            try
            {
                using (var context = DatabaseManager.Instance.CreateNewUnitOfWork())
                {
                    using (var beginTransaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            SetCurrentDbContextOnThread(context);
                            result = transaction.Invoke();
                            context.SaveChanges();
                            beginTransaction.Commit();
                        }
                        catch (Exception e)
                        {
                            beginTransaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            finally
            {
                ClearCurrentDbContextOnThread();
            }
            
            return result;
        }

        /*
         * NOTE: 
         * The following code allocates the db context for the started transaction to a memory slot. 
         * This way, the Repository will have access to the current db context.
         */

        private static readonly Object _setContextLock = new Object();
        private void SetCurrentDbContextOnThread(DbContext context)
        {
            lock (_setContextLock)
            {
                var localDataStore = Thread.AllocateNamedDataSlot(Thread.CurrentThread.ManagedThreadId.ToString());
                Thread.SetData(localDataStore, context);
            }   
        }

        private static readonly Object _clearContextLock = new Object();
        private void ClearCurrentDbContextOnThread()
        {
            lock (_clearContextLock)
            {
                Thread.FreeNamedDataSlot(Thread.CurrentThread.ManagedThreadId.ToString());
            }
        }

        private static readonly Object _getContextLock = new Object();
        public MyDbContext CurrentDbContext()
        {
            lock (_getContextLock)
            {
                var localDataStore = Thread.GetNamedDataSlot(Thread.CurrentThread.ManagedThreadId.ToString());
                var dbContext = (MyDbContext) Thread.GetData(localDataStore);
                return dbContext;
            }
        }
    }
}
