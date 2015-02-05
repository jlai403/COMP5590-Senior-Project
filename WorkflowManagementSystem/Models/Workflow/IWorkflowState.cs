using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public abstract class IWorkflowState
    {
        protected abstract void AssertWorkflowCanBeUpdated(User user, WorkflowItem request);
        protected abstract void Update(User user, WorkflowItem request);

        public void UpdateRequestToCurrentState(User user, WorkflowItem request)
        {
            AssertWorkflowCanBeUpdated(user, request);
            Update(user, request);
        }
    }
}