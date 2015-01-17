using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public interface IWorkflowState
    {
        void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request);
        void UpdateWorkflowState(User user, IHaveWorkflow request);
    }
}