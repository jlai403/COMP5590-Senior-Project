using System;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Files;
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

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
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
            programRequest.RequestedDateUTC.ShouldBeEquivalentTo(programRequestInputViewModel.RequestedDateUTC);
            programRequest.Requester.ShouldBeEquivalentTo(user.DisplayName);
            programRequest.Faculty.ShouldBeEquivalentTo(programRequestInputViewModel.Faculty);
            programRequest.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            programRequest.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            programRequest.CrossImpact.ShouldBeEquivalentTo(programRequestInputViewModel.CrossImpact);
            programRequest.StudentImpact.ShouldBeEquivalentTo(programRequestInputViewModel.StudentImpact);
            programRequest.LibraryImpact.ShouldBeEquivalentTo(programRequestInputViewModel.LibraryImpact);
            programRequest.ITSImpact.ShouldBeEquivalentTo(programRequestInputViewModel.ITSImpact);
            
            programRequest.Comments.Count.ShouldBeEquivalentTo(1);
            var comment = programRequest.Comments.First();
            comment.User.ShouldBeEquivalentTo(user.DisplayName);
            comment.Text.ShouldBeEquivalentTo(programRequestInputViewModel.Comment);
            comment.DateTimeUtc.ShouldBeEquivalentTo(programRequestInputViewModel.RequestedDateUTC);
            
            programRequest.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = programRequest.WorkflowSteps.First();
            workflowDataViewModel.State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void CreateProgramRequest_NameExists()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Program with the name 'Program Name' already exists.");
        }

        [Test]
        public void CreateProgramRequest_NoFaculty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";
            programRequestInputViewModel.Comment = "Comment";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Faculty is required.");
        }

        [Test]
        public void CreateProgramRequest_NoName()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            programRequestInputViewModel.Name = "";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Program name is required.");
        }

        [Test]
        public void CreateProgramRequest_StudentImpact()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            programRequestInputViewModel.StudentImpact = "";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Student impact is required.");
        }

        [Test]
        public void CreateProgramRequest_CrossImpact()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            programRequestInputViewModel.CrossImpact = "";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Cross impact is required.");
        }

        [Test]
        public void CreateProgramRequest_LibraryImpact()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            programRequestInputViewModel.LibraryImpact = "";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Library impact is required.");
        }

        [Test]
        public void CreateProgramRequest_ITSImpact()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            programRequestInputViewModel.ITSImpact = "";

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("ITS impact is required.");
        }

        [Test]
        public void FindProgram_NotFound()
        {
            // assemble
            // act
            Action action = ()=> FacadeFactory.GetDomainFacade().FindProgram("unknown");

            // assert
            action.ShouldThrow<WMSException>().WithMessage("Program 'unknown' not found.");
        }

        [Test]
        public void UploadAttachment_Program()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();
            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            var attachmentFileName = "some pdf";
            var expectedContentBytes = new byte[] { 0xFF, 0xFF, 0x00, 0xAA };
            var content = new MemoryStream(expectedContentBytes);
            var expectedContentType = "text/pdf";

            var fileInputViewModel = new FileInputViewModel(programRequestInputViewModel.Name, attachmentFileName, content, expectedContentType);

            // act
            FacadeFactory.GetDomainFacade().UploadFile(user.Email, fileInputViewModel, WorkflowItemTypes.Program);

            // assert
            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.Attachments.Count.ShouldBeEquivalentTo(1);
            programViewModel.Attachments.First().Key.ShouldBeEquivalentTo(attachmentFileName);
            programViewModel.Attachments.First().Value.Should().NotBeEmpty();
        }

        [Test]
        public void UploadAttachment_ProgramNotFound()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            var user = new UserTestHelper().CreateUserWithTestRoles();

            var attachmentFileName = "some pdf";
            var expectedContentBytes = new byte[] { 0xFF, 0xFF, 0x00, 0xAA };
            var content = new MemoryStream(expectedContentBytes);
            var expectedContentType = "text/pdf";

            var fileInputViewModel = new FileInputViewModel("such bogus", attachmentFileName, content, expectedContentType);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().UploadFile(user.Email, fileInputViewModel, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Cannot find Request of type 'Program' with the name 'such bogus'.");
        }

        [Test]
        public void CreateProgramRequest_NoComment()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramRequestInputViewModel();
            programRequestInputViewModel.RequestedDateUTC = new DateTime(2015, 1, 19);
            programRequestInputViewModel.Name = "Program Name";
            programRequestInputViewModel.Faculty = FacultyTestHelper.SCIENCE_AND_TECHNOLOGY;
            programRequestInputViewModel.Semester = semester.Id;
            programRequestInputViewModel.CrossImpact = "Cross Impact";
            programRequestInputViewModel.StudentImpact = "Student Impact";
            programRequestInputViewModel.LibraryImpact = "Library Impact";
            programRequestInputViewModel.ITSImpact = "ITS Impact";

            // act
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            var programRequests = FacadeFactory.GetDomainFacade().FindAllProgramRequests();
            programRequests.Count.ShouldBeEquivalentTo(1);

            var programRequest = programRequests.First();
            programRequest.RequestedDateUTC.ShouldBeEquivalentTo(programRequestInputViewModel.RequestedDateUTC);
            programRequest.Requester.ShouldBeEquivalentTo(user.DisplayName);
            programRequest.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            programRequest.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            programRequest.CrossImpact.ShouldBeEquivalentTo(programRequestInputViewModel.CrossImpact);
            programRequest.StudentImpact.ShouldBeEquivalentTo(programRequestInputViewModel.StudentImpact);
            programRequest.LibraryImpact.ShouldBeEquivalentTo(programRequestInputViewModel.LibraryImpact);
            programRequest.ITSImpact.ShouldBeEquivalentTo(programRequestInputViewModel.ITSImpact);

            programRequest.Comments.Count.ShouldBeEquivalentTo(0);

            programRequest.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = programRequest.WorkflowSteps.First();
            workflowDataViewModel.State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void CreateProgram_NoApprovalChain()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            
            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Unable to find Approval Chain for 'Program'");
            FacadeFactory.GetDomainFacade().FindAllProgramRequests().Count.ShouldBeEquivalentTo(0);
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

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            // act
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);

            // assert
            program.Requester.ShouldBeEquivalentTo(user.DisplayName);
            program.Name.ShouldBeEquivalentTo(programRequestInputViewModel.Name);
            program.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            program.CrossImpact.ShouldBeEquivalentTo(programRequestInputViewModel.CrossImpact);
            program.StudentImpact.ShouldBeEquivalentTo(programRequestInputViewModel.StudentImpact);
            program.LibraryImpact.ShouldBeEquivalentTo(programRequestInputViewModel.LibraryImpact);
            program.ITSImpact.ShouldBeEquivalentTo(programRequestInputViewModel.ITSImpact);

            program.Comments.Count.ShouldBeEquivalentTo(1);
            var comment = program.Comments.First();
            comment.User.ShouldBeEquivalentTo(user.DisplayName);
            comment.Text.ShouldBeEquivalentTo(programRequestInputViewModel.Comment);
            comment.DateTimeUtc.ShouldBeEquivalentTo(programRequestInputViewModel.RequestedDateUTC);

            program.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowDataViewModel = program.WorkflowSteps.First();
            workflowDataViewModel.State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            workflowDataViewModel.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowDataViewModel.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void ApproveWorkflowItem()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(2);

            var firstWorkflowStep = program.WorkflowSteps.First();
            firstWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.APPROVED);
            firstWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var secondWorkflowStep = program.WorkflowSteps.Last();
            secondWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            secondWorkflowStep.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void RejectWorkflowItem()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var rejector = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            FacadeFactory.GetDomainFacade().RejectWorkflowItem(rejector.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(1);

            var workflowStep = program.WorkflowSteps.First();
            workflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            workflowStep.State.ShouldBeEquivalentTo(WorkflowStates.REJECTED);
            workflowStep.User.ShouldBeEquivalentTo(rejector.DisplayName);
        }

        [Test]
        public void ApproveWorkflowItem_SecondApproval()
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

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            
            // act
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(3);

            var firstWorkflowStep = program.WorkflowSteps[0];
            firstWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.APPROVED);
            firstWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            firstWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var secondWorkflowStep = program.WorkflowSteps[1];
            secondWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.APPROVED);
            secondWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            secondWorkflowStep.User.ShouldBeEquivalentTo(approverTwo.DisplayName);

            var thirdWorkflowStep = program.WorkflowSteps[2];
            thirdWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
            thirdWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.APPC_MEMBER);
            thirdWorkflowStep.User.Should().BeNullOrEmpty();
        }

        [Test]
        public void ApproveWorkflowItem_WorkflowOnLastStep()
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

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request is currently on the last workflow and should be completed.");
            FacadeFactory.GetDomainFacade().FindAllProgramRequests().Count.ShouldBeEquivalentTo(1);

            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(4);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
        }

        [Test]
        public void IsWorkflowItemCurrentlyOnLastWorkflowStep()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            var result = FacadeFactory.GetDomainFacade().IsWorkflowItemCurrentlyOnLastWorkflowStep(programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsWorkflowItemCurrentlyOnLastWorkflowStep_CurrentlyOnLastWorkflowStep()
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

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            var result = FacadeFactory.GetDomainFacade().IsWorkflowItemCurrentlyOnLastWorkflowStep(programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            result.Should().BeTrue();
        }

        [Test]
        public void CompleteWorkflowItem()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var program = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            program.WorkflowSteps.Count.Should().Be(4);

            var firstWorkflowStep = program.WorkflowSteps[0];
            firstWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.APPROVED);
            firstWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER);
            firstWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var secondWorkflowStep = program.WorkflowSteps[1];
            secondWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.APPROVED);
            secondWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.FACULTY_COUNCIL_MEMBER);
            secondWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var thirdWorkflowStep = program.WorkflowSteps[2];
            thirdWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.APPROVED);
            thirdWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.APPC_MEMBER);
            thirdWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);

            var fourWorkflowStep = program.WorkflowSteps[3];
            fourWorkflowStep.State.ShouldBeEquivalentTo(WorkflowStates.COMPLETED);
            fourWorkflowStep.ResponsibleParty.ShouldBeEquivalentTo(RoleTestHelper.GFC_MEMBER);
            fourWorkflowStep.User.ShouldBeEquivalentTo(approver.DisplayName);
        }

        [Test]
        public void CompleteWorkflowItem_RejectedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been rejected");
            FacadeFactory.GetDomainFacade().FindAllProgramRequests().Count.ShouldBeEquivalentTo(1);

            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.REJECTED);
        }

        [Test]
        public void ApproveWorkflowItem_CompletedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been completed");
            FacadeFactory.GetDomainFacade().FindAllProgramRequests().Count.ShouldBeEquivalentTo(1);

            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(4);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.COMPLETED);
        }

        [Test]
        public void ApproveWorkflowItem_ApproverNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUser(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUser(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to approve request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);

            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
        }

        [Test]
        public void RejectWorkflowItem_RejectorNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUser(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUser(RoleTestHelper.FACULTY_MEMBER, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to complete request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);

            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(4);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
        }

        [Test]
        public void CompleteWorkflowItem_RejectorNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUser(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUser(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to reject request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);

            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.PENDING_APPROVAL);
        }

        [Test]
        public void ApproveWorkflowItem_Rejected()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);

            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been rejected");
            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.REJECTED);
        }

        [Test]
        public void RejectWorkflowItem_CompletedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been completed");
            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.WorkflowSteps.Count.ShouldBeEquivalentTo(4);
            programViewModel.WorkflowSteps.Last().State.ShouldBeEquivalentTo(WorkflowStates.COMPLETED);
        }

        [Test]
        public void AddComment_Program()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var commenter = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, FacultyTestHelper.SCIENCE_AND_TECHNOLOGY);

            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var commentInputViewModel = new CommentInputViewModel();
            commentInputViewModel.WorkflowItemName = programRequestInputViewModel.Name;
            commentInputViewModel.Text = "Comment Two";
            commentInputViewModel.DateTimeUtc = new DateTime(2015, 1, 20);

            // act
            var comment = FacadeFactory.GetDomainFacade().AddComment(commenter.Email, commentInputViewModel, WorkflowItemTypes.Program);

            // assert
            comment.User.ShouldBeEquivalentTo(commenter.DisplayName);
            comment.Text.ShouldBeEquivalentTo(commentInputViewModel.Text);
            comment.DateTimeUtc.ShouldBeEquivalentTo(commentInputViewModel.DateTimeUtc);
            
            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            programViewModel.Comments.Count.ShouldBeEquivalentTo(2);
            comment = programViewModel.Comments.Last();
            comment.User.ShouldBeEquivalentTo(commenter.DisplayName);
            comment.Text.ShouldBeEquivalentTo(commentInputViewModel.Text);
            comment.DateTimeUtc.ShouldBeEquivalentTo(commentInputViewModel.DateTimeUtc);
        }
    }
}