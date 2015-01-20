using System;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowDataViewModel
    {
        public WorkflowStatus Status { get; set; }
        public string ResponsibleParty { get; set; }
        public string User { get; set; }

        public bool IsApproved()
        {
            return Status == WorkflowStatus.APPROVED;
        }

        public bool IsRejected()
        {
            return Status == WorkflowStatus.REJECTED;
        }

        public string GetStatusDisplay()
        {
            if (IsApproved()) 
                return "Approved";
            else if (IsRejected())
                return "Rejected:";
            else if (Status == WorkflowStatus.COMPLETED)
                return "Completed";
            else
                return "Pending Approval";
        }
    }
}