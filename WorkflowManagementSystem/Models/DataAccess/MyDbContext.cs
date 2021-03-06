﻿using System.Linq;
using EFHooks;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class MyDbContext : HookedDbContext
    {
        protected MyDbContext() : base()
        {
            RegisterHooks();
        }

        protected MyDbContext(string connectionString) : base(connectionString)
        {
            RegisterHooks();
        }

        private void RegisterHooks()
        {
            RegisterHook(new PostInsertHookListener());
            RegisterHook(new PostDeleteHookListener());
        }

        public void AddEntity(IEntity entity)
        {
            Set(entity.GetType()).Add(entity);
        }

        public void DeleteEntity(IEntity entity)
        {
            Set(entity.GetType()).Remove(entity);
        }

        public IQueryable<T> Queryable<T>() where T : class
        {
            return Set<T>();
        }
    }
}