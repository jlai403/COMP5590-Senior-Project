﻿using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Tests
{
    public class SearchTest : WorkflowManagementSystemTest
    {
        [Test]
        public void SearchWorkflowItems_Programs()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var randomProgram = new ProgramRequestInputViewModel();
            randomProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            randomProgram.Name = "random";
            randomProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            randomProgram.Semester = semester.Id;
            randomProgram.CrossImpact = "random";
            randomProgram.StudentImpact = "random";
            randomProgram.LibraryImpact = "random";
            randomProgram.ITSImpact = "random";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramRequestInputViewModel();
            robotsAndUnicornsProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            robotsAndUnicornsProgram.Name = "Robots and unicorns";
            robotsAndUnicornsProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            robotsAndUnicornsProgram.Semester = semester.Id;
            robotsAndUnicornsProgram.CrossImpact = "Cross Impact";
            robotsAndUnicornsProgram.StudentImpact = "Student Impact";
            robotsAndUnicornsProgram.LibraryImpact = "Library Impact";
            robotsAndUnicornsProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            var dogeProgram = new ProgramRequestInputViewModel();
            dogeProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            dogeProgram.Name = "Such program";
            dogeProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            dogeProgram.Semester = semester.Id;
            dogeProgram.CrossImpact = "Cross Impact";
            dogeProgram.StudentImpact = "Student Impact";
            dogeProgram.LibraryImpact = "Library Impact";
            dogeProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, dogeProgram);

            // act
            var workflowItemResults = FacadeFactory.GetSearchFacade().SearchWorkflowItems("Cross");

            // assert
            workflowItemResults.Count().ShouldBeEquivalentTo(2);
            workflowItemResults.Should().Contain(x => x.Name.Equals(robotsAndUnicornsProgram.Name));
            workflowItemResults.Should().Contain(x => x.Name.Equals(dogeProgram.Name));
        }

        [Test]
        public void SearchWorkflowItems_Courses()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            var robotsAndUnicornsCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            robotsAndUnicornsCourse.Name = "robots and unicorns";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, robotsAndUnicornsCourse);

            var dogeCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            dogeCourse.Name = "doge";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, dogeCourse);

            // act
            var workflowItemResults = FacadeFactory.GetSearchFacade().SearchWorkflowItems("Cross");

            // assert
            workflowItemResults.Count().ShouldBeEquivalentTo(3);
            workflowItemResults.Should().Contain(x => x.Name.Equals(programRequestInputViewModel.Name));
            workflowItemResults.Should().Contain(x => x.Name.Equals(robotsAndUnicornsCourse.Name));
            workflowItemResults.Should().Contain(x => x.Name.Equals(dogeCourse.Name));
        }

        [Test]
        public void SearchWorkflowItems_Courses_MultipleKeywords()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var robotsAndUnicornsCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            robotsAndUnicornsCourse.Name = "robots and unicorns";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, robotsAndUnicornsCourse);

            var dogeCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            dogeCourse.Name = "doge";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, dogeCourse);

            var randomCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            randomCourse.Name = "random course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, randomCourse);

            var suchCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            suchCourse.Name = "such course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, suchCourse);

            // act
            var workflowItemResults = FacadeFactory.GetSearchFacade().SearchWorkflowItems("Such Random");

            // assert
            workflowItemResults.Count().ShouldBeEquivalentTo(2);
            workflowItemResults.Should().Contain(x => x.Name.Equals(randomCourse.Name));
            workflowItemResults.Should().Contain(x => x.Name.Equals(suchCourse.Name));
        }


        [Test]
        public void SearchWorkflowItems_Programs_MultipleKeywords()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var randomProgram = new ProgramRequestInputViewModel();
            randomProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            randomProgram.Name = "random";
            randomProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            randomProgram.Semester = semester.Id;
            randomProgram.CrossImpact = "random";
            randomProgram.StudentImpact = "random";
            randomProgram.LibraryImpact = "random";
            randomProgram.ITSImpact = "random";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramRequestInputViewModel();
            robotsAndUnicornsProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            robotsAndUnicornsProgram.Name = "Robots and unicorns";
            robotsAndUnicornsProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            robotsAndUnicornsProgram.Semester = semester.Id;
            robotsAndUnicornsProgram.CrossImpact = "Cross Impact";
            robotsAndUnicornsProgram.StudentImpact = "Student Impact";
            robotsAndUnicornsProgram.LibraryImpact = "Library Impact";
            robotsAndUnicornsProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            var suchProgram = new ProgramRequestInputViewModel();
            suchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            suchProgram.Name = "Such program";
            suchProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            suchProgram.Semester = semester.Id;
            suchProgram.CrossImpact = "Cross Impact";
            suchProgram.StudentImpact = "Student Impact";
            suchProgram.LibraryImpact = "Library Impact";
            suchProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, suchProgram);

            var muchProgram = new ProgramRequestInputViewModel();
            muchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            muchProgram.Name = "Much program";
            muchProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            muchProgram.Semester = semester.Id;
            muchProgram.CrossImpact = "Cross Impact";
            muchProgram.StudentImpact = "Student Impact";
            muchProgram.LibraryImpact = "Library Impact";
            muchProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, muchProgram);

            // act
            var workflowItemResults = FacadeFactory.GetSearchFacade().SearchWorkflowItems("random much");

            // assert
            workflowItemResults.Count().ShouldBeEquivalentTo(2);
            workflowItemResults.Should().Contain(x => x.Name.Equals(muchProgram.Name));
            workflowItemResults.Should().Contain(x => x.Name.Equals(randomProgram.Name));
        }

        [Test]
        public void SearchWorkflowItems_Programs_ProgramNameShouldOnlyReturnOneResult()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var randomProgram = new ProgramRequestInputViewModel();
            randomProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            randomProgram.Name = "random";
            randomProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            randomProgram.Semester = semester.Id;
            randomProgram.CrossImpact = "random";
            randomProgram.StudentImpact = "random";
            randomProgram.LibraryImpact = "random";
            randomProgram.ITSImpact = "random";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramRequestInputViewModel();
            robotsAndUnicornsProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            robotsAndUnicornsProgram.Name = "Robots and unicorns";
            robotsAndUnicornsProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            robotsAndUnicornsProgram.Semester = semester.Id;
            robotsAndUnicornsProgram.CrossImpact = "Cross Impact";
            robotsAndUnicornsProgram.StudentImpact = "Student Impact";
            robotsAndUnicornsProgram.LibraryImpact = "Library Impact";
            robotsAndUnicornsProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            var suchProgram = new ProgramRequestInputViewModel();
            suchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            suchProgram.Name = "Such program";
            suchProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            suchProgram.Semester = semester.Id;
            suchProgram.CrossImpact = "Cross Impact";
            suchProgram.StudentImpact = "Student Impact";
            suchProgram.LibraryImpact = "Library Impact";
            suchProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, suchProgram);

            var muchProgram = new ProgramRequestInputViewModel();
            muchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            muchProgram.Name = "Much program";
            muchProgram.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            muchProgram.Semester = semester.Id;
            muchProgram.CrossImpact = "Cross Impact";
            muchProgram.StudentImpact = "Student Impact";
            muchProgram.LibraryImpact = "Library Impact";
            muchProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, muchProgram);

            // act
            var workflowItemResults = FacadeFactory.GetSearchFacade().SearchWorkflowItems("Robots and unicorns");

            // assert
            workflowItemResults.Count().ShouldBeEquivalentTo(1);
            workflowItemResults.Should().Contain(x => x.Name.Equals(robotsAndUnicornsProgram.Name));
        }

        [Test]
        public void SearchForProgramNames()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var dogeCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            dogeCourse.Name = "doge course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, dogeCourse);

            var dogeProgram = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            dogeProgram.Name = "doge program";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, dogeProgram);

            var robotsAndUnicornsProgram = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            robotsAndUnicornsProgram.Name = "robots and unicorns program";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            // act
            var programNames = FacadeFactory.GetSearchFacade().SearchForProgramNames("doge");

            // assert
            programNames.Count().ShouldBeEquivalentTo(1);
            programNames.Should().Contain(dogeProgram.Name);
        }


        [Test]
        public void SearchForProgramNames_Multiple()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var randomProgram = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            randomProgram.Name = "random program";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            robotsAndUnicornsProgram.Name = "robots and unicorns program";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            // act
            var programNames = FacadeFactory.GetSearchFacade().SearchForProgramNames("r");

            // assert
            programNames.Count().ShouldBeEquivalentTo(2);
            programNames.Should().Contain(randomProgram.Name);
            programNames.Should().Contain(robotsAndUnicornsProgram.Name);
        }

        [Test]
        public void SearchForCourseNames()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var dogeProgram = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            dogeProgram.Name = "doge program";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, dogeProgram);

            var dogeCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            dogeCourse.Name = "doge course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, dogeCourse);

            var robotsAndUnicornsCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            robotsAndUnicornsCourse.Name = "robots and unicorns course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, robotsAndUnicornsCourse);

            // act
            var programNames = FacadeFactory.GetSearchFacade().SearchForCourseNames("ro");

            // assert
            programNames.Count().ShouldBeEquivalentTo(1);
            programNames.Should().Contain(robotsAndUnicornsCourse.Name);
        }

        [Test]
        public void SearchForCourseNames_Multiple()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var randomCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            randomCourse.Name = "random course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, randomCourse);

            var robotsAndUnicornsCourse = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, string.Empty);
            robotsAndUnicornsCourse.Name = "robots and unicorns course";
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, robotsAndUnicornsCourse);

            // act
            var programNames = FacadeFactory.GetSearchFacade().SearchForCourseNames("r");

            // assert
            programNames.Count().ShouldBeEquivalentTo(2);
            programNames.Should().Contain(randomCourse.Name);
            programNames.Should().Contain(robotsAndUnicornsCourse.Name);
        }

        [Test]
        public void SearchForUsers()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            var userOne = new UserSignUpViewModel();
            userOne.FirstName = "User";
            userOne.LastName = "One";
            userOne.Email = "user1@someawesomeemailprovider.com";
            userOne.Password = "123456";
            userOne.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            FacadeFactory.GetDomainFacade().CreateUser(userOne);

            var userTwo = new UserSignUpViewModel();
            userTwo.FirstName = "User";
            userTwo.LastName = "Two";
            userTwo.Email = "user2@someawesomeemailprovider.com";
            userTwo.Password = "123456";
            userTwo.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            FacadeFactory.GetDomainFacade().CreateUser(userTwo);

            var userThree = new UserSignUpViewModel();
            userThree.FirstName = "User";
            userThree.LastName = "Three";
            userThree.Email = "threeUser@someawesomeemailprovider.com";
            userThree.Password = "123456";
            userThree.Roles.Add(RoleTestHelper.FACULTY_MEMBER);
            FacadeFactory.GetDomainFacade().CreateUser(userThree);

            // act
            var users = FacadeFactory.GetSearchFacade().SearchForUsers("user");

            // assert
            users.Count().ShouldBeEquivalentTo(2);
            users.Should().Contain(x => x.Email.Equals(userOne.Email));
            users.Should().Contain(x => x.Email.Equals(userTwo.Email));
            users.Should().NotContain(x => x.Email.Equals(userThree.Email));
        }

        [Test]
        public void SearchForRoleNames()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();

            // act
            var roleNames = FacadeFactory.GetSearchFacade().SearchForRoleNames("Fac");

            // assert
            roleNames.Should().Contain(RoleTestHelper.FACULTY_MEMBER);
            roleNames.Should().Contain(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            roleNames.Should().Contain(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
        }
    }
}