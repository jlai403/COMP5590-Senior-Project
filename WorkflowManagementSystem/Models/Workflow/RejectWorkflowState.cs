using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class RejectWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, WorkflowItem request)
        {
            AssertUserHasSufficientPermissions(user, request);
            AssertWorkflowIsNotCompleted(request);
        }

        private void AssertUserHasSufficientPermissions(User user, WorkflowItem request)
        {
            if (!user.Roles.Contains(request.CurrentWorkflowData.ApprovalChainStep.Role))
                throw new WMSException("User '{0}' does not have sufficient permissions to reject request", user.GetDisplayName());
        }


        private void AssertWorkflowIsNotCompleted(WorkflowItem request)
        {
            if (request.CurrentWorkflowData.State == WorkflowStates.COMPLETED)
                throw new WMSException("Request has already been completed");
        }
        protected override void Update(User user, WorkflowItem request)
        {
            request.CurrentWorkflowData.State = WorkflowStates.REJECTED;
            request.CurrentWorkflowData.User = user;
        }
    }
}