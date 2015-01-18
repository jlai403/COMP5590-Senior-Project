using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class RejectWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request)
        {
            AssertUserHasSufficientPermissions(user, request);
            AssertWorkflowIsNotCompleted(request);
        }

        private void AssertUserHasSufficientPermissions(User user, IHaveWorkflow request)
        {
            if (!user.Roles.Contains(request.CurrentWorkflowData.ApprovalChainStep.Role))
                throw new WMSException("User '{0}' does not have sufficient permissions to reject request", user.GetDisplayName());
        }


        private void AssertWorkflowIsNotCompleted(IHaveWorkflow request)
        {
            if (request.CurrentWorkflowData.Status == WorkflowStatus.COMPLETED)
                throw new WMSException("Request has already been completed");
        }
        protected override void Update(User user, IHaveWorkflow request)
        {
            request.CurrentWorkflowData.Status = WorkflowStatus.REJECTED;
            request.CurrentWorkflowData.User = user;
        }
    }
}