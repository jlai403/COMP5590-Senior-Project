using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class RejectWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request)
        {
        }

        protected override void Update(User user, IHaveWorkflow request)
        {
            request.CurrentWorkflowData.Status = WorkflowStatus.REJECTED;
            request.CurrentWorkflowData.User = user;
        }
    }
}