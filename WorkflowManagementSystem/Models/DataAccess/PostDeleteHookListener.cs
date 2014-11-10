using EFHooks;
using MyEntityFramework.Entity;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class PostDeleteHookListener : PostDeleteHook<IEntity>
    {
        public override void Hook(IEntity entity, HookEntityMetadata metadata)
        {
            DatabaseManager.Instance._testCleanUpListener.EntityRemoved(entity);
        }
    }
}