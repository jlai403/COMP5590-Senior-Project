using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class ApproveWorkflowState : IWorkflowState
    {
        protected override void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request)
        {
            AssertUserHasSufficientPermissions(user, request);
            AssertWorkflowIsNotRejected(request);
            AssertWorkflowIsNotCompleted(request);
            AssertWorkflowIsNotOnLastStep(request);
        }

        private void AssertWorkflowIsNotOnLastStep(IHaveWorkflow request)
        {
            if (request.CurrentWorkflowData.IsLastWorkflowStep())
                throw new WMSException("Request is currently on the last workflow and should be completed.");
        }

        private void AssertUserHasSufficientPermissions(User user, IHaveWorkflow request)
        {
            if (!user.Roles.Contains(request.CurrentWorkflowData.ApprovalChainStep.Role))
                throw new WMSException("User '{0}' does not have sufficient permissions to approve request", user.GetDisplayName());
        }

        private void AssertWorkflowIsNotRejected(IHaveWorkflow request)
        {
            if (request.CurrentWorkflowData.Status == WorkflowStatus.REJECTED)
                throw new WMSException("Request has already been rejected");
        }

        private void AssertWorkflowIsNotCompleted(IHaveWorkflow request)
        {
            if (request.CurrentWorkflowData.Status == WorkflowStatus.COMPLETED)
                throw new WMSException("Request has already been completed");
        }

        protected override void Update(User user, IHaveWorkflow request)
        {
            var currentWorkflowData = request.CurrentWorkflowData;
            currentWorkflowData.UpdateStatus(user, WorkflowStatus.APPROVED);

            var approvalChain = ApprovalChainRepository.FindApprovalChain(request.APPROVAL_CHAIN_NAME);
            int nextApprovalChainStepSequence = currentWorkflowData.ApprovalChainStep.Sequence + 1;
            var nextApprovalChainStep = approvalChain.ApprovalChainSteps.First(x => x.Sequence == nextApprovalChainStepSequence);

            var nextWorkflowData = WorkflowRepository.CreateWorkflowData(nextApprovalChainStep);
            nextWorkflowData.PreviousWorkflowData = currentWorkflowData;

            request.CurrentWorkflowData = nextWorkflowData;
        }
    }
}