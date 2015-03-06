using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.Search;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public abstract class Repository
    {
        protected static void AddEntity(IEntity entity)
        {
            TransactionHandler.Instance.CurrentDbContext().AddEntity(entity);
        }

        protected static void FindEntity<T>(int id)
        {
            throw new NotImplementedException();
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