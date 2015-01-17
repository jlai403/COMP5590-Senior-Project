using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class RejectWorkflowState : IWorkflowState
    {
        public void UpdateWorkflowState(User user, IHaveWorkflow request)
        {
            request.CurrentWorkflowData.Status = WorkflowStatus.REJECTED;
            request.CurrentWorkflowData.User = user;
        }
    }
}