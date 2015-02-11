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
            workflowDataViewModel.State = WorkflowData.State;
            workflowDataViewModel.User = WorkflowData.GetUserDisplayName();
            return workflowDataViewModel;
        }

        public static List<WorkflowItemViewModel> AssembleWorkflowItems(List<WorkflowItem> actionableWorkflowItems)
        {
            return actionableWorkflowItems.Select(workflowItem => new WorkflowAssembler(workflowItem).AssembleWorkflowItem()).ToList();
        }

        private WorkflowItemViewModel AssembleWorkflowItem()
        {
            var workflowItemViewModel = new WorkflowItemViewModel();
            workflowItemViewModel.Name = WorkflowItem.Name;
            workflowItemViewModel.Requester = WorkflowItem.Requester.GetDisplayName();
            workflowItemViewModel.RequestedDateUTC = WorkflowItem.RequestedDateUTC;
            workflowItemViewModel.CurrentState = WorkflowItem.CurrentWorkflowData.State;
            workflowItemViewModel.Type = WorkflowItem.Type;
            workflowItemViewModel.CurrentResponsibleParty = WorkflowItem.CurrentWorkflowData.ApprovalChainStep.Role.Name;
            return workflowItemViewModel;
        }
    }
}