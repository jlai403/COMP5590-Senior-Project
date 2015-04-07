using System.Collections.Generic;
using System.Linq;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public abstract class Repository
    {
        protected static void AddEntity(IEntity entity)
        {
            TransactionHandler.Instance.CurrentDbContext().AddEntity(entity);
        }

        protected static T FindEntity<T>(int id) where T : class, IEntity
        {
            return TransactionHandler.Instance.CurrentDbContext().Set<T>().First(x => x.Id == id);
        }

        protected static List<T> FindAll<T>() where T : class
        {
            return TransactionHandler.Instance.CurrentDbContext().Set<T>().ToList();
        }

        protected static IQueryable<T> Queryable<T>() where T : class
        {
            return TransactionHandler.Instance.CurrentDbContext().Queryable<T>();
        }

        protected static void DeleteEntity(IEntity entity)
        {
            TransactionHandler.Instance.CurrentDbContext().DeleteEntity(entity);
        }
    }
}