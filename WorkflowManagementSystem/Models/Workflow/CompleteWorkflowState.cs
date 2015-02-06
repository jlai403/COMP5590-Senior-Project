using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CompleteWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, WorkflowItem request)
        {
            AssertUserHasSufficientPermissions(user, request);
            AssertWorkflowIsNotRejected(request);
        }

        private void AssertUserHasSufficientPermissions(User user, WorkflowItem request)
        {
            if (!user.Roles.Contains(request.CurrentWorkflowData.ApprovalChainStep.Role))
                throw new WMSException("User '{0}' does not have sufficient permissions to complete request", user.GetDisplayName());
        }

        private void AssertWorkflowIsNotRejected(WorkflowItem request)
        {
            if (request.CurrentWorkflowData.State == WorkflowStates.REJECTED)
                throw new WMSException("Request has already been rejected");
        }

        protected override void Update(User user, WorkflowItem request)
        {
            request.CurrentWorkflowData.State = WorkflowStates.COMPLETED;
            request.CurrentWorkflowData.User = user;
        }
    }
}