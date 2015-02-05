using System;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Programs;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowRepository : Repository
    {
        public static WorkflowData CreateWorkflowData(ApprovalChainStep approvalChainStep)
        {
            var workflowData = new WorkflowData();
            AddEntity(workflowData);
            workflowData.Update(approvalChainStep, WorkflowStatus.PENDING_APPROVAL, null);
            
            return workflowData;
        }

        public static WorkflowItem FindWorkflowItemForType(WorkflowItemTypes workflowItemType, string workflowItemName)
        {
            switch (workflowItemType)
            {
                case WorkflowItemTypes.Program:
                    return ProgramRepository.FindProgram(workflowItemName);
                default:
                    throw new NotImplementedException(string.Format("Unknown WorkflowItemType: '{0}'", workflowItemType));
            }
        }
    }
}