using System.Collections.Generic;
using System.Linq;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowAssembler
    {
        private WorkflowData WorkflowData { get; set; }

        private WorkflowAssembler(WorkflowData workflowData)
        {
            WorkflowData = workflowData;
        }

        public static List<WorkflowDataViewModel> AssembleAll(List<WorkflowData> workflowHistory)
        {
            return workflowHistory.Select(workflowData => new WorkflowAssembler(workflowData).Assemble()).ToList();
        }

        private WorkflowDataViewModel Assemble()
        {
            var workflowDataViewModel = new WorkflowDataViewModel();
            workflowDataViewModel.ResponsibleParty = WorkflowData.ApprovalChainStep.Role.Name;
            workflowDataViewModel.Status = WorkflowData.Status;
            return workflowDataViewModel;
        }
    }
}