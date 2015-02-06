using System;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Programs
{
    public class Program : WorkflowItem
    {
        public virtual Semester Semester { get; set; }
        public virtual Discipline Discipline { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        
        public override string APPROVAL_CHAIN_NAME { get { return "Program"; } }

        public void Update(User user, ProgramRequestInputViewModel programRequestInputViewModel)
        {
            UpdateWorkflowItem(user, programRequestInputViewModel.Name, programRequestInputViewModel.RequestedDateUTC, WorkflowItemTypes.Program);
            
            Semester = SemesterRepository.FindSemester(programRequestInputViewModel.Semester);
            Discipline = DisciplineRepository.FindDiscipline(programRequestInputViewModel.Discipline);
            CrossImpact = programRequestInputViewModel.CrossImpact;
            StudentImpact = programRequestInputViewModel.StudentImpact;
            LibraryImpact = programRequestInputViewModel.LibraryImpact;
            ITSImpact = programRequestInputViewModel.ITSImpact;

            if (!programRequestInputViewModel.Comment.IsNullOrWhiteSpace())
            {
                AddComment(user, programRequestInputViewModel.RequestedDateUTC, programRequestInputViewModel.Comment);
            }
        }

        public List<WorkflowData> GetWorkflowHistory(WorkflowData workflowData = null)
        {
            var workflowDataHistory = new List<WorkflowData>();
            workflowData = workflowData ?? CurrentWorkflowData;

            if (workflowData.PreviousWorkflowData != null)
            {
                workflowDataHistory.AddRange(GetWorkflowHistory(workflowData.PreviousWorkflowData));
            }

            workflowDataHistory.Add(workflowData);
            return workflowDataHistory;
        }
    }
}