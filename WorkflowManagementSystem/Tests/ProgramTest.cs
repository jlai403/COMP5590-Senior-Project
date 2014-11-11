using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Program;

namespace WorkflowManagementSystem.Tests
{
    public class ProgramTest : WorkflowManagementSystemTest
    {
        //[Test]
        //public void CreateProgramRequest()
        //{
        //    // assemble
        //    var user = new UserTestHelper().CreateUserWithTestRoles();

        //    new SemesterTestHelper().LoadTestSemesters();
        //    new DisciplineTestHelper().LoadTestDisciplines();

        //    var programRequestInputViewModel = new ProgramRequestInputViewModel();
        //    programRequestInputViewModel.Requester = user.GetFullName();
        //    programRequestInputViewModel.Name = "Program Name";
        //    programRequestInputViewModel.Semester = "2015 - Winter";
        //    programRequestInputViewModel.Discipline = "COMP - Computer Science";
        //    programRequestInputViewModel.CrossImpact = "Cross Impact";
        //    programRequestInputViewModel.StudentImpact = "Student Impact";
        //    programRequestInputViewModel.LibraryImpact = "Library Impact";
        //    programRequestInputViewModel.ITSImpact = "ITS Impact";
        //    programRequestInputViewModel.Comment = "Comment";

        //    // act
        //    FacadeFactory.GetDomainFacade().CreateProgramRequest(programRequestInputViewModel);

        //    // assert
        //    var programRequests = FacadeFactory.GetDomainFacade().FindAllProgramRequests();
        //    programRequests.Count.ShouldBeEquivalentTo(1);

        //    var programRequest = programRequests.First();
        //    programRequest.Requester.ShouldBeEquivalentTo(programRequestInputViewModel.Requester);
        //    programRequest.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
        //    programRequest.Semester.ShouldBeEquivalentTo(programRequestInputViewModel.Semester);
        //    programRequest.Discipline.ShouldBeEquivalentTo(programRequestInputViewModel.Discipline);
        //    programRequest.CrossImpact.ShouldBeEquivalentTo(programRequestInputViewModel.CrossImpact);
        //    programRequest.StudentImpact.ShouldBeEquivalentTo(programRequestInputViewModel.StudentImpact);
        //    programRequest.LibraryImpact.ShouldBeEquivalentTo(programRequestInputViewModel.LibraryImpact);
        //    programRequest.ITSImpact.ShouldBeEquivalentTo(programRequestInputViewModel.ITSImpact);
        //    programRequest.Comment.ShouldBeEquivalentTo(programRequestInputViewModel.Comment);
        //}
    }
}