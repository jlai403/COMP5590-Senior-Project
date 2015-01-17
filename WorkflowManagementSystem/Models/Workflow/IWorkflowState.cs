using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public interface IWorkflowState
    {
        void Approve(User user, IHaveWorkflow request);
        void Reject();
    }
}