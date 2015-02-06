using System;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class ActionableWorkflowItemViewModel
    {
        public WorkflowItemTypes Type { get; set; }
        public string Name { get; set; }
        public string Requester { get; set; }
        public DateTime RequestedDateUTC { get; set; }
        public WorkflowStates CurrentState { get; set; }
    }
}