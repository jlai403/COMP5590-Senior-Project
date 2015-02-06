using System;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowDataViewModel
    {
        public WorkflowStates States { get; set; }
        public string ResponsibleParty { get; set; }
        public string User { get; set; }

        public bool IsApproved()
        {
            return States == WorkflowStates.APPROVED;
        }

        public bool IsRejected()
        {
            return States == WorkflowStates.REJECTED;
        }

        public string GetStatusDisplay()
        {
            if (IsApproved()) 
                return "Approved";
            else if (IsRejected())
                return "Rejected:";
            else if (States == WorkflowStates.COMPLETED)
                return "Completed";
            else
                return "Pending Approval";
        }
    }
}