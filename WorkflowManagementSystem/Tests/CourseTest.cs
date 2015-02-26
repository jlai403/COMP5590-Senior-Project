using System;
using System.Collections.Generic;
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
            courseInputViewModel.Name = "Such course";
            courseInputViewModel.CourseNumber = "6590";
            courseInputViewModel.Discipline = discipline.Id;
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
            course.CourseNumber.ShouldBeEquivalentTo(courseInputViewModel.CourseNumber);
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
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
            course.ProgramName.ShouldBeEquivalentTo(courseInputViewModel.ProgramName);
        }

        [Test]
        public void CreateCourseRequest_GeneratedCourseNumber_NoOtherCoursesOfSameLevel()
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
            courseInputViewModel.Name = "Such senior course";
            courseInputViewModel.CourseNumber = "5xxx";
            courseInputViewModel.Discipline = discipline.Id;
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
            course.CourseNumber.Should().BeEquivalentTo("5000");
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
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
            course.ProgramName.ShouldBeEquivalentTo(courseInputViewModel.ProgramName);
        }

        [Test]
        public void CreateCourseRequest_GeneratedCourseNumber()
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

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            courseRequestInputViewModel.CourseNumber = "5100";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            courseRequestInputViewModel.CourseNumber = "51xx";

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            course.Should().NotBeNull();
            course.CourseNumber.Should().BeEquivalentTo("5101");
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
            course.Name.ShouldBeEquivalentTo(courseRequestInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseRequestInputViewModel.Credits);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseRequestInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.ITSImpact);
            course.RequestedDateUtc.ShouldBeEquivalentTo(courseRequestInputViewModel.RequestedDateUtc);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseRequestInputViewModel.ProgramName);
        }

        [Test]
        public void CreateCourseRequest_NonGeneratedCourseNumberGreaterThanGeneratedCourseNumber()
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

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            courseRequestInputViewModel.CourseNumber = "5500";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            courseRequestInputViewModel.CourseNumber = "5xxx";

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            course.Should().NotBeNull();
            course.CourseNumber.Should().BeEquivalentTo("5000");
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
            course.Name.ShouldBeEquivalentTo(courseRequestInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseRequestInputViewModel.Credits);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseRequestInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.ITSImpact);
            course.RequestedDateUtc.ShouldBeEquivalentTo(courseRequestInputViewModel.RequestedDateUtc);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseRequestInputViewModel.ProgramName);
        }

        [Test]
        public void GetNewValidCourseNumber()
        {
            // assemble
            var providedCourseNumber = "1xxx";
            var takenCourseNumbers = new List<int>() { 1000 };

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1001);
        }

        [Test]
        public void GetNewValidCourseNumber_TwoTakenCourseNumbers()
        {
            // assemble
            var providedCourseNumber = "1xxx";
            var takenCourseNumbers = new List<int>() { 1000, 1001 };

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1002);
        }

        [Test]
        public void GetNewValidCourseNumber_GapInCourseNumersGreaterThanOne()
        {
            // assemble
            var providedCourseNumber = "1xxx";
            var takenCourseNumbers = new List<int>() { 1000, 1500 };

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1001);
        }

        [Test]
        public void GetNewValidCourseNumber_ThreeTakenNumbersWithGapGreaterThanOne()
        {
            // assemble
            var providedCourseNumber = "1xxx";
            var takenCourseNumbers = new List<int>() { 1000, 1200, 1300 };

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1001);
        }

        [Test]
        public void GetNewValidCourseNumber_ThreeTakenNumbersInSequence()
        {
            // assemble
            var providedCourseNumber = "1xxx";
            var takenCourseNumbers = new List<int>() { 1000, 1001, 1002 };

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1003);
        }

        [Test]
        public void GetNewValidCourseNumber_NoTakenCourseNumbers_ThreeGeneratedDigits()
        {
            // assemble
            var providedCourseNumber = "1xxx";
            var takenCourseNumbers = new List<int>();

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1000);
        }

        [Test]
        public void GetNewValidCourseNumber_NoTakenCourseNumbers_TwoGeneratedDigits()
        {
            // assemble
            var providedCourseNumber = "17xx";
            var takenCourseNumbers = new List<int>();

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1700);
        }

        [Test]
        public void GetNewValidCourseNumber_NoTakenCourseNumbers_OneGeneratedDigit()
        {
            // assemble
            var providedCourseNumber = "177x";
            var takenCourseNumbers = new List<int>();

            // act
            var actual = new CourseNumberGenerator(providedCourseNumber, takenCourseNumbers).GetNextValidCourseNumber();

            // assert
            actual.ShouldBeEquivalentTo(1770);
        }
    }
}