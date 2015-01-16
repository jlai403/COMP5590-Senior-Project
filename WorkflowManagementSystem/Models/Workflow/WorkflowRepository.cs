using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowRepository : Repository
    {
        public static WorkflowData CreateWorkflowData(string approvalChainName)
        {
            var approvalChain = ApprovalChainRepository.FindApprovalChain(approvalChainName);
            var approvalChainStep = approvalChain.ApprovalChainSteps.First();
            
            var workflowData = new WorkflowData();
            AddEntity(workflowData);
            workflowData.Update(approvalChainStep, WorkflowStatus.PENDING_APPROVAL, null);
            
            return workflowData;
        }
    }
}