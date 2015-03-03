using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Tests
{
    public class WorkflowItemTest : WorkflowManagementSystemTest
    {
        [Test]
        public void FindWorkflowItemsAwaitingForAction_Program()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            var actionableWorkflowItems = FacadeFactory.GetDomainFacade().FindWorkflowItemsAwaitingForAction(approver.Email);

            // assert
            actionableWorkflowItems.Count.ShouldBeEquivalentTo(1);
            var actionableWorkflowItem = actionableWorkflowItems.First();
            actionableWorkflowItem.Type.ShouldBeEquivalentTo(WorkflowItemTypes.Program);
            actionableWorkflowItem.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            actionableWorkflowItem.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            actionableWorkflowItem.RequestedDateUTC.ShouldBeEquivalentTo(programRequestInputViewModel.RequestedDateUTC);
            actionableWorkflowItem.CurrentState.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
        }

        [Test]
        public void FindWorkflowItemsAwaitingForAction_Course()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // act
            var actionableWorkflowItems = FacadeFactory.GetDomainFacade().FindWorkflowItemsAwaitingForAction(approver.Email);

            // assert
            actionableWorkflowItems.Count.ShouldBeEquivalentTo(2);
            var actionableWorkflowItem = actionableWorkflowItems.FirstOrDefault(x => x.Name.Equals(courseRequestInputViewModel.Name));
            actionableWorkflowItems.Should().NotBeNull();
            actionableWorkflowItem.Type.ShouldBeEquivalentTo(WorkflowItemTypes.Course);
            actionableWorkflowItem.Name.ShouldBeEquivalentTo(courseRequestInputViewModel.Name);
            actionableWorkflowItem.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            actionableWorkflowItem.RequestedDateUTC.ShouldBeEquivalentTo(courseRequestInputViewModel.RequestedDateUTC);
            actionableWorkflowItem.CurrentState.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
        }

        [Test]
        public void FindWorkflowItemsAwaitingForAction_Program_NoneForUser()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            var actionableWorkflowItems = FacadeFactory.GetDomainFacade().FindWorkflowItemsAwaitingForAction(approver.Email);

            // assert
            actionableWorkflowItems.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void FindWorkflowItemsAwaitingForAction_Course_NoneForUser()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // act
            var actionableWorkflowItems = FacadeFactory.GetDomainFacade().FindWorkflowItemsAwaitingForAction(approver.Email);

            // assert
            actionableWorkflowItems.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void FindWorkflowItemsRequestedByUser_Program()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Spring"));
            discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel2 = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel2);

            // act
            var workflowItemsRequestedByUser = FacadeFactory.GetDomainFacade().FindWorkflowItemsRequestedByUser(requester.Email);

            // assert
            workflowItemsRequestedByUser.Count.ShouldBeEquivalentTo(2);

            var workflowItemViewModel = workflowItemsRequestedByUser.First();
            workflowItemViewModel.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            workflowItemViewModel.Type.ShouldBeEquivalentTo(WorkflowItemTypes.Program);
            workflowItemViewModel.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            workflowItemViewModel.RequestedDateUTC.ShouldBeEquivalentTo(programRequestInputViewModel.RequestedDateUTC);
            workflowItemViewModel.CurrentState.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowItemViewModel.CurrentResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);

            workflowItemViewModel = workflowItemsRequestedByUser.Last();
            workflowItemViewModel.Name.ShouldBeEquivalentTo(programRequestInputViewModel2.Name);
            workflowItemViewModel.Type.ShouldBeEquivalentTo(WorkflowItemTypes.Program);
            workflowItemViewModel.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            workflowItemViewModel.RequestedDateUTC.ShouldBeEquivalentTo(programRequestInputViewModel2.RequestedDateUTC);
            workflowItemViewModel.CurrentState.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowItemViewModel.CurrentResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
        }

        [Test]
        public void FindWorkflowItemsRequestedByUser_Course()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Spring"));
            discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));
            var programRequestInputViewModel2 = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(requester, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel2);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            var courseRequestInputViewModel2 = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel2.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel2);

            // act
            var workflowItemsRequestedByUser = FacadeFactory.GetDomainFacade().FindWorkflowItemsRequestedByUser(requester.Email);

            // assert
            workflowItemsRequestedByUser.Count.ShouldBeEquivalentTo(4);

            var workflowItemViewModel = workflowItemsRequestedByUser.FirstOrDefault(x => x.Name.Equals(courseRequestInputViewModel.Name));
            workflowItemViewModel.Should().NotBeNull();
            workflowItemViewModel.Name.ShouldBeEquivalentTo(courseRequestInputViewModel.Name);
            workflowItemViewModel.Type.ShouldBeEquivalentTo(WorkflowItemTypes.Course);
            workflowItemViewModel.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            workflowItemViewModel.RequestedDateUTC.ShouldBeEquivalentTo(courseRequestInputViewModel.RequestedDateUTC);
            workflowItemViewModel.CurrentState.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowItemViewModel.CurrentResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);

            workflowItemViewModel = workflowItemsRequestedByUser.FirstOrDefault(x => x.Name.Equals(courseRequestInputViewModel2.Name));
            workflowItemViewModel.Should().NotBeNull();
            workflowItemViewModel.Name.ShouldBeEquivalentTo(courseRequestInputViewModel2.Name);
            workflowItemViewModel.Type.ShouldBeEquivalentTo(WorkflowItemTypes.Course);
            workflowItemViewModel.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            workflowItemViewModel.RequestedDateUTC.ShouldBeEquivalentTo(courseRequestInputViewModel2.RequestedDateUTC);
            workflowItemViewModel.CurrentState.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowItemViewModel.CurrentResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
        }

        [Test]
        public void FindWorkflowItemsRequestedByUser_Program_None()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            // act
            var workflowItemsRequestedByUser = FacadeFactory.GetDomainFacade().FindWorkflowItemsRequestedByUser(requester.Email);

            // assert
            workflowItemsRequestedByUser.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void FindWorkflowItemsRequestedByUser_Course_None()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            // act
            var workflowItemsRequestedByUser = FacadeFactory.GetDomainFacade().FindWorkflowItemsRequestedByUser(requester.Email);

            // assert
            workflowItemsRequestedByUser.Count.ShouldBeEquivalentTo(0);
        }
    }
}