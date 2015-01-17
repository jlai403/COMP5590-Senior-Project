using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public abstract class IWorkflowState
    {
        protected abstract void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request);
        protected abstract void Update(User user, IHaveWorkflow request);

        public void UpdateRequestToCurrentState(User user, IHaveWorkflow request)
        {
            AssertWorkflowCanBeUpdated(user, request);
            Update(user, request);
        }
    }
}