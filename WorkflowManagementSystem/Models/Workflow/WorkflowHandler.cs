using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowHandler
    {
        public IHaveWorkflow Request { get; set; }

        public WorkflowHandler(IHaveWorkflow request)
        {
            Request = request;
        }

        public void InitiateWorkflow()
        {
            var approvalChain = ApprovalChainRepository.FindApprovalChain(Request.APPROVAL_CHAIN_NAME);
            var approvalChainStep = approvalChain.ApprovalChainSteps.First();

            var workflowData = WorkflowRepository.CreateWorkflowData(approvalChainStep);
            Request.CurrentWorkflowData = workflowData;
        }

        public void Approve(User user)
        {
            WorkflowStateFactory.GetState(WorkflowStatus.APPROVED).UpdateWorkflowState(user, Request);
        }

        public void Reject(User user)
        {
            WorkflowStateFactory.GetState(WorkflowStatus.REJECTED).UpdateWorkflowState(user, Request);
        }
    }
}