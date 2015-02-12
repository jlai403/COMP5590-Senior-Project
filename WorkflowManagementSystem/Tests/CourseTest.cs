using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Course;

namespace WorkflowManagementSystem.Tests
{
    public class CourseTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateCourseRequest()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCoursesApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseInputViewModel = new CourseRequestInputViewModel();
            courseInputViewModel.Name = "COMP 6590";
            courseInputViewModel.Code = "All about unicorns";
            courseInputViewModel.ProgramName = programRequestInputViewModel.Name;
            courseInputViewModel.Credits = "3";
            courseInputViewModel.Semester = semester.Id;
            courseInputViewModel.CalendarEntry = "Calendar Entry";
            courseInputViewModel.CrossImpact = "Cross Impact";
            courseInputViewModel.StudentImpact = "Student Impact";
            courseInputViewModel.LibraryImpact = "Library Impact";
            courseInputViewModel.ITSImpact = "ITS Impact";
            courseInputViewModel.RequestedDateUtc = new DateTime(2015, 2, 10);

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseInputViewModel.Name);
            course.Should().NotBeNull();
            course.Code.ShouldBeEquivalentTo(courseInputViewModel.Code);
            course.Name.ShouldBeEquivalentTo(courseInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseInputViewModel.Credits);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseInputViewModel.ITSImpact);
            course.RequestedDateUtc.ShouldBeEquivalentTo(courseInputViewModel.RequestedDateUtc);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
        }
    }
}