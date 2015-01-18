using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CompleteWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request)
        {
            AssertUserHasSufficientPermissions(user, request);
            AssertWorkflowIsNotRejected(request);
        }

        private void AssertUserHasSufficientPermissions(User user, IHaveWorkflow request)
        {
            if (!user.Roles.Contains(request.CurrentWorkflowData.ApprovalChainStep.Role))
                throw new WMSException("User '{0}' does not have sufficient permissions to complete request", user.GetDisplayName());
        }

        private void AssertWorkflowIsNotRejected(IHaveWorkflow request)
        {
            if (request.CurrentWorkflowData.Status == WorkflowStatus.REJECTED)
                throw new WMSException("Request has already been rejected");
        }

        protected override void Update(User user, IHaveWorkflow request)
        {
            request.CurrentWorkflowData.Status = WorkflowStatus.COMPLETED;
            request.CurrentWorkflowData.User = user;
        }
    }
}