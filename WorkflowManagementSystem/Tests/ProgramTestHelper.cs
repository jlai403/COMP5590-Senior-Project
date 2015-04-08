using System;
using System.Linq;
using FluentAssertions;
using WorkflowManagementSystem.Models.Faculties;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Tests
{
    public class ProgramTestHelper
    {
        private static Random Random = new Random();

        public ProgramRequestInputViewModel CreateNewValidProgramRequestInputViewModel(SemesterViewModel semester, string facultyName)
        {
            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Name = string.Format("Program Name - {0}", Random.Next(1, 9999));
            programRequestInputViewModel.Faculty = facultyName;
            programRequestInputViewModel.Semester = semester.Id;
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
            actual.CrossImpact.ShouldBeEquivalentTo(expected.CrossImpact);
            actual.StudentImpact.ShouldBeEquivalentTo(expected.StudentImpact);
            actual.LibraryImpact.ShouldBeEquivalentTo(expected.LibraryImpact);
            actual.ITSImpact.ShouldBeEquivalentTo(expected.ITSImpact);

            actual.Comments.Count.ShouldBeEquivalentTo(1);
            var comment = actual.Comments.First();
            comment.Text.ShouldBeEquivalentTo(expected.Comment);
            comment.DateTimeUtc.ShouldBeEquivalentTo(expected.RequestedDateUTC);

            actual.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = actual.WorkflowSteps.First();
            workflowDataViewModel.State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }
    }
}