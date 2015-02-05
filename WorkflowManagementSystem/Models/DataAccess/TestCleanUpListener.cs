using System;
using System.Collections.Generic;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class TestCleanUpListener : ICleanUpData
    {
        private List<TypePair> TypePair { get; set; }

        public TestCleanUpListener()
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
                TypePair.Reverse();
                foreach (var typePair in TypePair)
                {
                    var dbSet = TransactionHandler.Instance.CurrentDbContext().Set(typePair.Type);
                    var entity = (IEntity)dbSet.Find(typePair.Entity.Id); // query for entity first so EF updates EntityState from Detached
                    if (entity != null)
                    {
                        dbSet.Remove(entity);
                    }
                }
                return null;
            });

            TypePair.Clear();
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