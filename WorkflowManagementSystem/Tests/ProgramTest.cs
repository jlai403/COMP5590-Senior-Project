using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Tests
{
    public class ProgramTest : WorkflowManagementSystemTest
    {
        [Test]
        public void CreateProgramRequest()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();
            
            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = user.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";

            // act
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

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
            
            programRequest.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = programRequest.WorkflowSteps.First();
            workflowDataViewModel.Status.ShouldBeEquivalentTo(WorkflowStatus.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void FindProgram()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = user.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // act
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);

            // assert
            program.Requester.ShouldBeEquivalentTo(user.DisplayName);
            program.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            program.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            program.Discipline.ShouldBeEquivalentTo(discipline.DisplayName);
            program.CrossImpact.ShouldBeEquivalentTo(programRequestInputViewModel.CrossImpact);
            program.StudentImpact.ShouldBeEquivalentTo(programRequestInputViewModel.StudentImpact);
            program.LibraryImpact.ShouldBeEquivalentTo(programRequestInputViewModel.LibraryImpact);
            program.ITSImpact.ShouldBeEquivalentTo(programRequestInputViewModel.ITSImpact);
            program.Comment.ShouldBeEquivalentTo(programRequestInputViewModel.Comment);

            program.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = program.WorkflowSteps.First();
            workflowDataViewModel.Status.ShouldBeEquivalentTo(WorkflowStatus.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void ApproveProgramRequest()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(2);

            var firstWorkflowStep = program.WorkflowSteps.First();
            firstWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.APPROVED);
            firstWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var secondWorkflowStep = program.WorkflowSteps.Last();
            secondWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.PENDING_APPROVAL);
            secondWorkflowStep.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void RejectProgramRequest()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var rejector = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            FacadeFactory.GetDomainFacade().RejectProgramRequest(rejector.Email, programRequestInputViewModel.Name);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(1);

            var workflowStep = program.WorkflowSteps.First();
            workflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.REJECTED);
            workflowStep.User.ShouldBeEquivalentTo(rejector.DisplayName);
        }

        [Test]
        public void ApproveProgramRequest_SecondApproval()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();
            var approverTwo = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            
            // act
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approverTwo.Email, programRequestInputViewModel.Name);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(3);

            var firstWorkflowStep = program.WorkflowSteps[0];
            firstWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.APPROVED);
            firstWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            firstWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var secondWorkflowStep = program.WorkflowSteps[1];
            secondWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.APPROVED);
            secondWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            secondWorkflowStep.User.ShouldBeEquivalentTo(approverTwo.DisplayName);

            var thirdWorkflowStep = program.WorkflowSteps[2];
            thirdWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.PENDING_APPROVAL);
            thirdWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.APPC_MEMBER);
            thirdWorkflowStep.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void CompleteProgramRequest()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // act
            FacadeFactory.GetDomainFacade().CompleteProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(4);

            var firstWorkflowStep = program.WorkflowSteps[0];
            firstWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.APPROVED);
            firstWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            firstWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var secondWorkflowStep = program.WorkflowSteps[1];
            secondWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.APPROVED);
            secondWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            secondWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var thirdWorkflowStep = program.WorkflowSteps[2];
            thirdWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.APPROVED);
            thirdWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.APPC_MEMBER);
            thirdWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var fourWorkflowStep = program.WorkflowSteps[3];
            fourWorkflowStep.Status.ShouldBeEquivalentTo(WorkflowStatus.COMPLETED);
            fourWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.GFC_MEMBER);
            fourWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);
        }

        [Test]
        public void CompleteProgramRequest_RejectedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().RejectProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CompleteProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been rejected");
        }

        [Test]
        public void ApproveProgramRequest_CompletedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CompleteProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been completed");
        }

        [Test]
        public void ApproveProgramRequest_ApproverNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to approve request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);
        }

        [Test]
        public void RejectProgramRequest_RejectorNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CompleteProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to complete request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);
        }

        [Test]
        public void CompleteProgramRequest_RejectorNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().RejectProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to reject request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);
        }

        [Test]
        public void ApproveProgramRequest_Rejected()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().RejectProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been rejected");
        }

        [Test]
        public void RejectProgramRequest_CompletedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.Requester = requester.DisplayName;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.Discipline = discipline.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().ApproveProgramRequest(approver.Email, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CompleteProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().RejectProgramRequest(approver.Email, programRequestInputViewModel.Name);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been completed");
        }
    }
}