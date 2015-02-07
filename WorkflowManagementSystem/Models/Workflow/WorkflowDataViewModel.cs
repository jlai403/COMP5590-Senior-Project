namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowDataViewModel
    {
        public WorkflowStates State { get; set; }
        public string ResponsibleParty { get; set; }
        public string User { get; set; }

        public bool IsApproved()
        {
            return State == WorkflowStates.APPROVED;
        }

        public bool IsRejected()
        {
            return State == WorkflowStates.REJECTED;
        }
    }
}