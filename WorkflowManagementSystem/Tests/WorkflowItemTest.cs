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
    }
}