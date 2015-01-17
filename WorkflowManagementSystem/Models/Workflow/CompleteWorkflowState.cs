using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CompleteWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request)
        {
            
        }

        protected override void Update(User user, IHaveWorkflow request)
        {
            request.CurrentWorkflowData.Status = WorkflowStatus.COMPLETED;
            request.CurrentWorkflowData.User = user;
        }
    }
}