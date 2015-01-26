using System;
using System.Linq;
using FluentAssertions;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Tests
{
    public class ProgramTestHelper
    {
        private static Random Random = new Random();

        public ProgramRequestInputViewModel CreateNewValidProgramRequestInputViewModel(UserViewModel user, SemesterViewModel semester, DisciplineViewModel discipline)
        {
            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Requester = user.DisplayName;
            programRequestInputViewModel.Name = string.Format("Program Name - {0}", Random.Next(1, 9999));
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            return programRequestInputViewModel;
        }

        public void AssertProgramRequest(ProgramRequestInputViewModel expected, ProgramViewModel actual, params string[] assertParameters)
        {
            actual.Requester.ShouldBeEquivalentTo(assertParameters[0]);
            actual.Name.ShouldBeEquivalentTo(expected.Name);
            actual.Semester.ShouldBeEquivalentTo(assertParameters[1]);
            actual.Discipline.ShouldBeEquivalentTo(assertParameters[2]);
            actual.CrossImpact.ShouldBeEquivalentTo(expected.CrossImpact);
            actual.StudentImpact.ShouldBeEquivalentTo(expected.StudentImpact);
            actual.LibraryImpact.ShouldBeEquivalentTo(expected.LibraryImpact);
            actual.ITSImpact.ShouldBeEquivalentTo(expected.ITSImpact);
            actual.Comment.ShouldBeEquivalentTo(expected.Comment);

            actual.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = actual.WorkflowSteps.First();
            workflowDataViewModel.Status.ShouldBeEquivalentTo(WorkflowStatus.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }
    }
}