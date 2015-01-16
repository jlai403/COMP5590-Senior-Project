using System.Collections.Generic;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.Programs
{
    public class Program : IEntity
    {
        public readonly string APPROVAL_CHAIN_NAME = "Program";

        public int Id { get; set; }
        public virtual User Requester { get; set; }
        public string Name { get; set; }
        public virtual Semester Semester { get; set; }
        public virtual Discipline Discipline { get; set; }
        public string CrossImpact { get; set; }
        public string StudentImpact { get; set; }
        public string LibraryImpact { get; set; }
        public string ITSImpact { get; set; }
        public string Comment { get; set; }
        public virtual WorkflowData CurrentWorkflowData { get; set; }

        public void Update(User user, ProgramRequestInputViewModel programRequestInputViewModel)
        {
            Requester = user;
            Name = programRequestInputViewModel.Name;
            Semester = SemesterRepository.FindSemester(programRequestInputViewModel.Semester);
            Discipline = DisciplineRepository.FindDiscipline(programRequestInputViewModel.Discipline);
            CrossImpact = programRequestInputViewModel.CrossImpact;
            StudentImpact = programRequestInputViewModel.StudentImpact;
            LibraryImpact = programRequestInputViewModel.LibraryImpact;
            ITSImpact = programRequestInputViewModel.ITSImpact;
            Comment = programRequestInputViewModel.Comment;
        }

        public List<WorkflowData> GetWorkflowHistory()
        {
            return TraverseWorkflowData(CurrentWorkflowData);
        }

        private List<WorkflowData> TraverseWorkflowData(WorkflowData workflowData)
        {
            var workflowDataHistory = new List<WorkflowData>();
            if (workflowData.PreviousWorkflowData == null)
            {
                workflowDataHistory.Add(workflowData);
            }
            else
            {
                workflowDataHistory.AddRange(TraverseWorkflowData(workflowData.PreviousWorkflowData));
            }
            return workflowDataHistory;
        }
    }
}