using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.ErrorHandling;
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
            var workflowItem = Queryable<WorkflowItem>().FirstOrDefault(x => x.Name.Equals(workflowItemName) && x.Type == workflowItemType);
            if (workflowItem == null) throw new WMSException("Cannot find Request of type '{0}' with the name '{1}'.", workflowItemType.ToString(), workflowItemName);
            return workflowItem;
            //switch (workflowItemType)
            //{
            //    case WorkflowItemTypes.Program:
            //        return ProgramRepository.FindProgram(workflowItemName);
            //    case WorkflowItemTypes.Course:
            //        return CourseRepository.FindCourse(workflowItemName);
            //    default:
            //        throw new NotImplementedException(string.Format("Unknown WorkflowItemType: '{0}'", workflowItemType));
            //}
        }

        public static List<WorkflowItem> FindWorkflowItemsAwaitingForAction(string email)
        {
            var user = UserRepository.FindUser(email);
            var userRoles = user.Roles.Select(x => x.Id);
            return Queryable<WorkflowItem>()
                .Where(x => 
                    userRoles.Contains(x.CurrentWorkflowData.ApprovalChainStep.Role.Id) &&
                    x.CurrentWorkflowData.State == WorkflowStates.PENDING_APPROVAL).ToList();
        }

        public static List<WorkflowItem> FindWorkflowItemsRequestedByUser(string email)
        {
            return Queryable<WorkflowItem>().Where(x => x.Requester.Email.Equals(email)).ToList();
        }
    }
}