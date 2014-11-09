using System;
using System.Collections.Generic;
using System.Linq;
using MyEntityFramework.Entity;
using MyEntityFramework.Transaction;
using NUnit.Framework;

namespace MyEntityFramework.Repository
{
    public abstract class Repository
    {
        protected static void AddEntity(IEntity entity)
        {
            DatabaseManager.Instance.DbContext.AddEntity(entity);
        }

        protected static void FindEntity<T>(int id)
        {
            throw new NotImplementedException();
        }

        protected static List<T> FindAll<T>() where T : class
        {
            return DatabaseManager.Instance.DbContext.Set<T>().ToList();
        }

        protected static IQueryable<T> Queryable<T>() where T : class
        {
            return DatabaseManager.Instance.DbContext.Queryable<T>();
        }
    }
}