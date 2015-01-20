using System;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Tests
{
    public class ProgramTestHelper
    {
        public ProgramRequestInputViewModel CreateNewValidProgramRequestInputViewModel(UserViewModel user, SemesterViewModel semester, DisciplineViewModel discipline)
        {
            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Requester = user.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            return programRequestInputViewModel;
        }

    }
}