using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowRepository : Repository
    {
        public static WorkflowData CreateWorkflowData(ApprovalChainStep approvalChainStep)
        {
            var workflowData = new WorkflowData();
            AddEntity(workflowData);
            workflowData.Update(approvalChainStep, WorkflowStates.PENDING_APPROVAL, null);
            
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

        public static List<WorkflowItem> FindWorkflowItemsAwaitingForAction(string email)
        {
            var user = UserRepository.FindUser(email);
            var userRoles = user.Roles.Select(x => x.Id);
            return Queryable<WorkflowItem>().Where(x => userRoles.Contains(x.CurrentWorkflowData.ApprovalChainStep.Role.Id)).ToList();
        }

        public static List<WorkflowItem> FindWorkflowItemsRequestedByUser(string email)
        {
            return Queryable<WorkflowItem>().Where(x => x.Requester.Email.Equals(email)).ToList();
        }
    }
}