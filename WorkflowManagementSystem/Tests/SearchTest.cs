using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Programs;

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
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var randomProgram = new ProgramRequestInputViewModel();
            randomProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            randomProgram.Requester = user.Email;
            randomProgram.Name = "random";
            randomProgram.Semester = semester.Id;
            randomProgram.Discipline = discipline.Id;
            randomProgram.CrossImpact = "random";
            randomProgram.StudentImpact = "random";
            randomProgram.LibraryImpact = "random";
            randomProgram.ITSImpact = "random";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramRequestInputViewModel();
            robotsAndUnicornsProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            robotsAndUnicornsProgram.Requester = user.Email;
            robotsAndUnicornsProgram.Name = "Robots and unicorns";
            robotsAndUnicornsProgram.Semester = semester.Id;
            robotsAndUnicornsProgram.Discipline = discipline.Id;
            robotsAndUnicornsProgram.CrossImpact = "Cross Impact";
            robotsAndUnicornsProgram.StudentImpact = "Student Impact";
            robotsAndUnicornsProgram.LibraryImpact = "Library Impact";
            robotsAndUnicornsProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            var dogeProgram = new ProgramRequestInputViewModel();
            dogeProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            dogeProgram.Requester = user.Email;
            dogeProgram.Name = "Such program";
            dogeProgram.Semester = semester.Id;
            dogeProgram.Discipline = discipline.Id;
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
        public void SearchWorkflowItems_Programs_MultipleKeywords()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var randomProgram = new ProgramRequestInputViewModel();
            randomProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            randomProgram.Requester = user.Email;
            randomProgram.Name = "random";
            randomProgram.Semester = semester.Id;
            randomProgram.Discipline = discipline.Id;
            randomProgram.CrossImpact = "random";
            randomProgram.StudentImpact = "random";
            randomProgram.LibraryImpact = "random";
            randomProgram.ITSImpact = "random";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramRequestInputViewModel();
            robotsAndUnicornsProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            robotsAndUnicornsProgram.Requester = user.Email;
            robotsAndUnicornsProgram.Name = "Robots and unicorns";
            robotsAndUnicornsProgram.Semester = semester.Id;
            robotsAndUnicornsProgram.Discipline = discipline.Id;
            robotsAndUnicornsProgram.CrossImpact = "Cross Impact";
            robotsAndUnicornsProgram.StudentImpact = "Student Impact";
            robotsAndUnicornsProgram.LibraryImpact = "Library Impact";
            robotsAndUnicornsProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            var suchProgram = new ProgramRequestInputViewModel();
            suchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            suchProgram.Requester = user.Email;
            suchProgram.Name = "Such program";
            suchProgram.Semester = semester.Id;
            suchProgram.Discipline = discipline.Id;
            suchProgram.CrossImpact = "Cross Impact";
            suchProgram.StudentImpact = "Student Impact";
            suchProgram.LibraryImpact = "Library Impact";
            suchProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, suchProgram);

            var muchProgram = new ProgramRequestInputViewModel();
            muchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            muchProgram.Requester = user.Email;
            muchProgram.Name = "Much program";
            muchProgram.Semester = semester.Id;
            muchProgram.Discipline = discipline.Id;
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
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var randomProgram = new ProgramRequestInputViewModel();
            randomProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            randomProgram.Requester = user.Email;
            randomProgram.Name = "random";
            randomProgram.Semester = semester.Id;
            randomProgram.Discipline = discipline.Id;
            randomProgram.CrossImpact = "random";
            randomProgram.StudentImpact = "random";
            randomProgram.LibraryImpact = "random";
            randomProgram.ITSImpact = "random";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, randomProgram);

            var robotsAndUnicornsProgram = new ProgramRequestInputViewModel();
            robotsAndUnicornsProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            robotsAndUnicornsProgram.Requester = user.Email;
            robotsAndUnicornsProgram.Name = "Robots and unicorns";
            robotsAndUnicornsProgram.Semester = semester.Id;
            robotsAndUnicornsProgram.Discipline = discipline.Id;
            robotsAndUnicornsProgram.CrossImpact = "Cross Impact";
            robotsAndUnicornsProgram.StudentImpact = "Student Impact";
            robotsAndUnicornsProgram.LibraryImpact = "Library Impact";
            robotsAndUnicornsProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, robotsAndUnicornsProgram);

            var suchProgram = new ProgramRequestInputViewModel();
            suchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            suchProgram.Requester = user.Email;
            suchProgram.Name = "Such program";
            suchProgram.Semester = semester.Id;
            suchProgram.Discipline = discipline.Id;
            suchProgram.CrossImpact = "Cross Impact";
            suchProgram.StudentImpact = "Student Impact";
            suchProgram.LibraryImpact = "Library Impact";
            suchProgram.ITSImpact = "ITS Impact";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, suchProgram);

            var muchProgram = new ProgramRequestInputViewModel();
            muchProgram.RequestedDateUTC = new DateTime(2015, 1, 19);
            muchProgram.Requester = user.Email;
            muchProgram.Name = "Much program";
            muchProgram.Semester = semester.Id;
            muchProgram.Discipline = discipline.Id;
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
    }
}