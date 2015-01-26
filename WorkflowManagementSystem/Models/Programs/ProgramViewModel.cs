using System;
using System.Collections.Generic;
using System.Linq;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Programs
{
    public class ProgramViewModel
    {
        public string Requester { get; set; }
        public string Name { get; set; }
        public string Semester { get; set; }
        public string Discipline { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public string Comment { get; set; }
        public List<WorkflowDataViewModel> WorkflowSteps { get; set; }
        public DateTime RequestedDateUTC { get; set; }

        public ProgramViewModel()
        {
            WorkflowSteps = new List<WorkflowDataViewModel>();
        }

        public string CurrentResponsibleParty()
        {
            return WorkflowSteps.Last().ResponsibleParty;
        }

        public string CurrentStatus()
        {
            return WorkflowSteps.Last().GetStatusDisplay();
        }
    }
}