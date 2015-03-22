using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowHandler
    {
        public WorkflowItem Request { get; set; }

        public WorkflowHandler(WorkflowItem request)
        {
            Request = request;
        }

        public void InitiateWorkflow()
        {
            var approvalChain = ApprovalChainRepository.FindActiveApprovalChain(Request.APPROVAL_CHAIN_NAME);

            if (approvalChain == null) 
                throw new WMSException("Unable to find Approval Chain for '{0}'", Request.APPROVAL_CHAIN_NAME);

            var approvalChainStep = approvalChain.ApprovalChainSteps.First();
            var workflowData = WorkflowRepository.CreateWorkflowData(approvalChainStep);
            Request.CurrentWorkflowData = workflowData;
        }

        public void Approve(User user)
        {
            WorkflowStateFactory.GetState(WorkflowStates.APPROVED).UpdateRequestToCurrentState(user, Request);
        }

        public void Reject(User user)
        {
            WorkflowStateFactory.GetState(WorkflowStates.REJECTED).UpdateRequestToCurrentState(user, Request);
        }

        public void Complete(User user)
        {
            WorkflowStateFactory.GetState(WorkflowStates.COMPLETED).UpdateRequestToCurrentState(user, Request);
        }
    }
}