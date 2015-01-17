using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class ApproveWorkflowState : IWorkflowState
    {
        public void AssertWorkflowCanBeUpdated(User user, IHaveWorkflow request)
        {
        }

        public void UpdateWorkflowState(User user, IHaveWorkflow request)
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