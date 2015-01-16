using System;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class WorkflowDataViewModel
    {
        public WorkflowStatus Status { get; set; }
        public string ResponsibleParty { get; set; }
        public string User { get; set; }
    }
}