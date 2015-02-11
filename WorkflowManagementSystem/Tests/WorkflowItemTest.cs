using System.Collections.Generic;
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
        public void FindWorkflowItemsAwaitingForAction()
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
        public void FindWorkflowItemsAwaitingForAction_NoneForUser()
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
        public void FindWorkflowItemsRequestedByUser()
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
        public void FindWorkflowItemsRequestedByUser_None()
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
    }
}