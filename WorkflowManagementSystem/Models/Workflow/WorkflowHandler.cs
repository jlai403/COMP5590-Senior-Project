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
            GetCurrentWorkflowState().Approve(user, Request);
        }

        private IWorkflowState GetCurrentWorkflowState()
        {
            return WorkflowStateFactory.GetState(Request.CurrentWorkflowData.Status);
        }
    }
}