using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public interface IWorkflowState
    {
        void UpdateWorkflowState(User user, IHaveWorkflow request);
    }
}