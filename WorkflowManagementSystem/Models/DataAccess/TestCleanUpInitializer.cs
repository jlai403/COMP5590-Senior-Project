using System;
using System.Collections.Generic;
using MyEntityFramework.Entity;
using MyEntityFramework.Transaction;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class TestCleanUpInitializer : ICleanUpData
    {
        private List<TypePair> TypePair { get; set; }

        public TestCleanUpInitializer()
        {
            TypePair = new List<TypePair>();
        }

        public void EntityCreated(IEntity entity)
        {
            TypePair.Add(new TypePair(entity.GetType(), entity));
        }

        public void EntityRemoved(IEntity entity)
        {
            var entityToRemove = TypePair.Find(x => x.Entity.Equals(entity));
            TypePair.Remove(entityToRemove);
        }

        public void CleanUp()
        {
            TransactionHandler.Instance.Execute(() =>
            {
                foreach (var typePair in TypePair)
                {
                    var dbSet = DatabaseManager.Instance.DbContext.Set(typePair.Type);
                    dbSet.Remove(typePair.Entity);
                }
                return null;
            });
        }
    }

    internal class TypePair
    {
        public Type Type { get; set; }
        public IEntity Entity { get; set; }

        public TypePair(Type type, IEntity entity)
        {
            Type = type;
            Entity = entity;
        }
    }
}