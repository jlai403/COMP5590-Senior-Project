using MyEntityFramework.Entity;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public interface ICleanUpData
    {
        void CleanUp();
        void EntityCreated(IEntity entity);
        void EntityRemoved(IEntity entity);
    }
}