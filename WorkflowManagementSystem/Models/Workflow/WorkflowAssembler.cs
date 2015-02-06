using System.Collections.Generic;
using System.Linq;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowAssembler
    {
        private WorkflowData WorkflowData { get; set; }
        private WorkflowItem WorkflowItem { get; set; }

        private WorkflowAssembler(WorkflowData workflowData)
        {
            WorkflowData = workflowData;
        }

        private WorkflowAssembler(WorkflowItem workflowItem)
        {
            WorkflowItem = workflowItem;
        }

        public static List<WorkflowDataViewModel> AssembleAll(List<WorkflowData> workflowHistory)
        {
            return workflowHistory.Select(workflowData => new WorkflowAssembler(workflowData).Assemble()).ToList();
        }

        private WorkflowDataViewModel Assemble()
        {
            var workflowDataViewModel = new WorkflowDataViewModel();
            workflowDataViewModel.ResponsibleParty = WorkflowData.ApprovalChainStep.Role.Name;
            workflowDataViewModel.States = WorkflowData.State;
            workflowDataViewModel.User = WorkflowData.GetUserDisplayName();
            return workflowDataViewModel;
        }

        public static List<ActionableWorkflowItemViewModel> AssembleWorkflowItemsAwaitingForAction(List<WorkflowItem> actionableWorkflowItems)
        {
            return actionableWorkflowItems.Select(workflowItem => new WorkflowAssembler(workflowItem).AssembleActionableWorkflowItem()).ToList();
        }

        private ActionableWorkflowItemViewModel AssembleActionableWorkflowItem()
        {
            var actionableWorkflowViewModel = new ActionableWorkflowItemViewModel();
            actionableWorkflowViewModel.Name = WorkflowItem.Name;
            actionableWorkflowViewModel.Requester = WorkflowItem.Requester.GetDisplayName();
            actionableWorkflowViewModel.RequestedDateUTC = WorkflowItem.RequestedDateUTC;
            actionableWorkflowViewModel.CurrentState = WorkflowItem.CurrentWorkflowData.State;
            actionableWorkflowViewModel.Type = WorkflowItem.Type;
            return actionableWorkflowViewModel;
        }
    }
}