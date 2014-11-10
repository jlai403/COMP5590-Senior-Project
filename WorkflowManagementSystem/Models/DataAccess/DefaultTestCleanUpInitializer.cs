using MyEntityFramework.Entity;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class DefaultTestCleanUpInitializer : ICleanUpData
    {
        public void CleanUp()
        {
            // do nothing
        }

        public void EntityCreated(IEntity entity)
        {
            // do nothing
        }

        public void EntityRemoved(IEntity entity)
        {
            // do nothing
        }
    }
}