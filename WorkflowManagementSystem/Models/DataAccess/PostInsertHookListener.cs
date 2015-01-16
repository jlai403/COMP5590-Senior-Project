using EFHooks;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class PostInsertHookListener : PostInsertHook<IEntity>
    {
        public override void Hook(IEntity entity, HookEntityMetadata metadata)
        {
            DatabaseManager.Instance._testCleanUpListener.EntityCreated(entity);
        }
    }
}