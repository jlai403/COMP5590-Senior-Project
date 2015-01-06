using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Programs;

namespace WorkflowManagementSystem.Tests
{
    public class ProgramTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateProgramRequest()
        {
            // assemble
            var user = new UserTestHelper().CreateUserWithTestRoles();

            new SemesterTestHelper().LoadTestSemesters();
            new DisciplineTestHelper().LoadTestDisciplines();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = user.Email;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";

            // act
            FacadeFactory.GetDomainFacade().CreateProgramRequest(programRequestInputViewModel);

            // assert
            var programRequests = FacadeFactory.GetDomainFacade().FindAllProgramRequests();
            programRequests.Count.ShouldBeEquivalentTo(1);

            var programRequest = programRequests.First();
            programRequest.Requester.ShouldBeEquivalentTo(user.DisplayName);
            programRequest.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            programRequest.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            programRequest.Discipline.ShouldBeEquivalentTo(discipline.DisplayName);
            programRequest.CrossImpact.ShouldBeEquivalentTo(programRequestInputViewModel.CrossImpact);
            programRequest.StudentImpact.ShouldBeEquivalentTo(programRequestInputViewModel.StudentImpact);
            programRequest.LibraryImpact.ShouldBeEquivalentTo(programRequestInputViewModel.LibraryImpact);
            programRequest.ITSImpact.ShouldBeEquivalentTo(programRequestInputViewModel.ITSImpact);
            programRequest.Comment.ShouldBeEquivalentTo(programRequestInputViewModel.Comment);
        }
    }
}