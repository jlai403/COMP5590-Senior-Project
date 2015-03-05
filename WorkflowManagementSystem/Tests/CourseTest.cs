using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Course;
using WorkflowManagementSystem.Models.ErrorHandling;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Workflow;

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
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseInputViewModel = new CourseRequestInputViewModel();
            courseInputViewModel.Name = "Such course";
            courseInputViewModel.CourseNumber = "6590";
            courseInputViewModel.Discipline = discipline.Id;
            courseInputViewModel.ProgramName = programRequestInputViewModel.Name;
            courseInputViewModel.Credits = CourseConstants.AVAILABLE_CREDITS.First();
            courseInputViewModel.Grading = CourseConstants.AVAILABLE_GRADINGS.First();
            courseInputViewModel.Semester = semester.Id;
            courseInputViewModel.CalendarEntry = "Calendar Entry";
            courseInputViewModel.CrossImpact = "Cross Impact";
            courseInputViewModel.StudentImpact = "Student Impact";
            courseInputViewModel.LibraryImpact = "Library Impact";
            courseInputViewModel.ITSImpact = "ITS Impact";
            courseInputViewModel.RequestedDateUTC = new DateTime(2015, 2, 10);

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseInputViewModel.Name);
            course.Should().NotBeNull();
            course.CourseNumber.ShouldBeEquivalentTo(courseInputViewModel.CourseNumber);
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
            course.Name.ShouldBeEquivalentTo(courseInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseInputViewModel.Credits);
            course.Grading.ShouldBeEquivalentTo(courseInputViewModel.Grading);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseInputViewModel.ITSImpact);
            course.RequestedDateUTC.ShouldBeEquivalentTo(courseInputViewModel.RequestedDateUTC);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseInputViewModel.ProgramName);

            course.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, string.Empty);
        }

        [Test]
        public void CreateCourseRequest_NoProgramProvided()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var courseInputViewModel = new CourseRequestInputViewModel();
            courseInputViewModel.Name = "Such course";
            courseInputViewModel.CourseNumber = "6590";
            courseInputViewModel.Discipline = discipline.Id;
            courseInputViewModel.ProgramName = "";
            courseInputViewModel.Credits = CourseConstants.AVAILABLE_CREDITS.First();
            courseInputViewModel.Grading = CourseConstants.AVAILABLE_GRADINGS.First();
            courseInputViewModel.Semester = semester.Id;
            courseInputViewModel.CalendarEntry = "Calendar Entry";
            courseInputViewModel.CrossImpact = "Cross Impact";
            courseInputViewModel.StudentImpact = "Student Impact";
            courseInputViewModel.LibraryImpact = "Library Impact";
            courseInputViewModel.ITSImpact = "ITS Impact";
            courseInputViewModel.RequestedDateUTC = new DateTime(2015, 2, 10);

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseInputViewModel.Name);
            course.Should().NotBeNull();
            course.CourseNumber.ShouldBeEquivalentTo(courseInputViewModel.CourseNumber);
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
            course.Name.ShouldBeEquivalentTo(courseInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseInputViewModel.Credits);
            course.Grading.ShouldBeEquivalentTo(courseInputViewModel.Grading);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseInputViewModel.ITSImpact);
            course.RequestedDateUTC.ShouldBeEquivalentTo(courseInputViewModel.RequestedDateUTC);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseInputViewModel.ProgramName);

            course.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, string.Empty);
        }

        [Test]
        public void CreateCourseRequest_InvalidProgram()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var courseInputViewModel = new CourseRequestInputViewModel();
            courseInputViewModel.Name = "Such course";
            courseInputViewModel.CourseNumber = "6590";
            courseInputViewModel.Discipline = discipline.Id;
            courseInputViewModel.ProgramName = "invalid program";
            courseInputViewModel.Credits = CourseConstants.AVAILABLE_CREDITS.First();
            courseInputViewModel.Grading = CourseConstants.AVAILABLE_GRADINGS.First();
            courseInputViewModel.Semester = semester.Id;
            courseInputViewModel.CalendarEntry = "Calendar Entry";
            courseInputViewModel.CrossImpact = "Cross Impact";
            courseInputViewModel.StudentImpact = "Student Impact";
            courseInputViewModel.LibraryImpact = "Library Impact";
            courseInputViewModel.ITSImpact = "ITS Impact";
            courseInputViewModel.RequestedDateUTC = new DateTime(2015, 2, 10);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseInputViewModel);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Could not find the program 'invalid program'.");
        }

        [Test]
        public void CreateCourseRequest_CommentIncluded()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseInputViewModel = new CourseRequestInputViewModel();
            courseInputViewModel.Name = "Such course";
            courseInputViewModel.CourseNumber = "6590";
            courseInputViewModel.Discipline = discipline.Id;
            courseInputViewModel.ProgramName = programRequestInputViewModel.Name;
            courseInputViewModel.Credits = CourseConstants.AVAILABLE_CREDITS.First();
            courseInputViewModel.Grading = CourseConstants.AVAILABLE_GRADINGS.First();
            courseInputViewModel.Semester = semester.Id;
            courseInputViewModel.CalendarEntry = "Calendar Entry";
            courseInputViewModel.CrossImpact = "Cross Impact";
            courseInputViewModel.StudentImpact = "Student Impact";
            courseInputViewModel.LibraryImpact = "Library Impact";
            courseInputViewModel.ITSImpact = "ITS Impact";
            courseInputViewModel.RequestedDateUTC = new DateTime(2015, 2, 10);
            courseInputViewModel.Comment = "Such comment";

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseInputViewModel.Name);
            course.Should().NotBeNull();
            course.CourseNumber.ShouldBeEquivalentTo(courseInputViewModel.CourseNumber);
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
            course.Name.ShouldBeEquivalentTo(courseInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseInputViewModel.Credits);
            course.Grading.ShouldBeEquivalentTo(courseInputViewModel.Grading);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseInputViewModel.ITSImpact);
            course.RequestedDateUTC.ShouldBeEquivalentTo(courseInputViewModel.RequestedDateUTC);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseInputViewModel.ProgramName);
            
            course.Comments.Count.ShouldBeEquivalentTo(1);
            course.Comments.First().DateTimeUtc.ShouldBeEquivalentTo(courseInputViewModel.RequestedDateUTC);
            course.Comments.First().User.ShouldBeEquivalentTo(requester.DisplayName);
            course.Comments.First().Text.ShouldBeEquivalentTo(courseInputViewModel.Comment);

            course.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, string.Empty);
        }

        [Test]
        public void UploadAttachment_Course()
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

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(user.Email, courseRequestInputViewModel);

            var attachmentFileName = "some pdf";
            var expectedContentBytes = new byte[] { 0xFF, 0xFF, 0x00, 0xAA };
            var content = new MemoryStream(expectedContentBytes);
            var expectedContentType = "text/pdf";

            var fileInputViewModel = new FileInputViewModel(courseRequestInputViewModel.Name, attachmentFileName, content, expectedContentType);

            // act
            FacadeFactory.GetDomainFacade().UploadFile(user.Email, fileInputViewModel, WorkflowItemTypes.Course);

            // assert
            var programViewModel = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            programViewModel.Attachments.Count.ShouldBeEquivalentTo(1);
            programViewModel.Attachments.First().Key.ShouldBeEquivalentTo(attachmentFileName);
            programViewModel.Attachments.First().Value.Should().NotBeEmpty();
        }

        [Test]
        public void UploadAttachment_CourseNotFound()
        {
            // assemble
            var user = new UserTestHelper().CreateUserWithTestRoles();

            var attachmentFileName = "some pdf";
            var expectedContentBytes = new byte[] { 0xFF, 0xFF, 0x00, 0xAA };
            var content = new MemoryStream(expectedContentBytes);
            var expectedContentType = "text/pdf";

            var fileInputViewModel = new FileInputViewModel("such bogus", attachmentFileName, content, expectedContentType);

            // act
            Action act = ()=> FacadeFactory.GetDomainFacade().UploadFile(user.Email, fileInputViewModel, WorkflowItemTypes.Course);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Cannot find Request of type 'Course' with the name 'such bogus'.");
        }

        [Test]
        public void CreateCourseRequest_GeneratedCourseNumber_NoOtherCoursesOfSameLevel()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseInputViewModel = new CourseRequestInputViewModel();
            courseInputViewModel.Name = "Such senior course";
            courseInputViewModel.CourseNumber = "5xxx";
            courseInputViewModel.Discipline = discipline.Id;
            courseInputViewModel.ProgramName = programRequestInputViewModel.Name;
            courseInputViewModel.Credits = CourseConstants.AVAILABLE_CREDITS.First();
            courseInputViewModel.Grading = CourseConstants.AVAILABLE_GRADINGS.First();
            courseInputViewModel.Semester = semester.Id;
            courseInputViewModel.CalendarEntry = "Calendar Entry";
            courseInputViewModel.CrossImpact = "Cross Impact";
            courseInputViewModel.StudentImpact = "Student Impact";
            courseInputViewModel.LibraryImpact = "Library Impact";
            courseInputViewModel.ITSImpact = "ITS Impact";
            courseInputViewModel.RequestedDateUTC = new DateTime(2015, 2, 10);

            // act
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseInputViewModel);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseInputViewModel.Name);
            course.Should().NotBeNull();
            course.CourseNumber.Should().BeEquivalentTo("5000");
            course.Discipline.ShouldBeEquivalentTo(discipline.Code);
            course.Name.ShouldBeEquivalentTo(courseInputViewModel.Name);
            course.Credits.ShouldBeEquivalentTo(courseInputViewModel.Credits);
            course.Grading.ShouldBeEquivalentTo(courseInputViewModel.Grading);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseInputViewModel.ITSImpact);
            course.RequestedDateUTC.ShouldBeEquivalentTo(courseInputViewModel.RequestedDateUTC);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseInputViewModel.ProgramName);

            course.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, string.Empty);
        }

        [Test]
        public void CreateCourseRequest_GeneratedCourseNumber()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
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
            course.Grading.ShouldBeEquivalentTo(courseRequestInputViewModel.Grading);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseRequestInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.ITSImpact);
            course.RequestedDateUTC.ShouldBeEquivalentTo(courseRequestInputViewModel.RequestedDateUTC);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseRequestInputViewModel.ProgramName);

            course.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, string.Empty);
        }

        [Test]
        public void CreateCourseRequest_NonGeneratedCourseNumberGreaterThanGeneratedCourseNumber()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().First(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().First(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
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
            course.Grading.ShouldBeEquivalentTo(courseRequestInputViewModel.Grading);
            course.Semester.ShouldBeEquivalentTo(semester.DisplayName);
            course.CalendarEntry.ShouldBeEquivalentTo(courseRequestInputViewModel.CalendarEntry);
            course.CrossImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.CrossImpact);
            course.StudentImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.StudentImpact);
            course.LibraryImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.LibraryImpact);
            course.ITSImpact.ShouldBeEquivalentTo(courseRequestInputViewModel.ITSImpact);
            course.RequestedDateUTC.ShouldBeEquivalentTo(courseRequestInputViewModel.RequestedDateUTC);
            course.Requester.ShouldBeEquivalentTo(requester.DisplayName);
            course.ProgramName.ShouldBeEquivalentTo(courseRequestInputViewModel.ProgramName);

            course.WorkflowSteps.Count.ShouldBeEquivalentTo(1);
            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, string.Empty);
        }

        [Test]
        public void AddComment_Course()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var commenter = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            var commentInputViewModel = new CommentInputViewModel();
            commentInputViewModel.WorkflowItemName = courseRequestInputViewModel.Name;
            commentInputViewModel.Text = "Comment Two";
            commentInputViewModel.DateTimeUtc = new DateTime(2015, 1, 20);

            // act
            var comment = FacadeFactory.GetDomainFacade().AddComment(commenter.Email, commentInputViewModel, WorkflowItemTypes.Course);

            // assert
            comment.User.ShouldBeEquivalentTo(commenter.DisplayName);
            comment.Text.ShouldBeEquivalentTo(commentInputViewModel.Text);
            comment.DateTimeUtc.ShouldBeEquivalentTo(commentInputViewModel.DateTimeUtc);

            var courseViewModel = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            courseViewModel.Comments.Count.ShouldBeEquivalentTo(1);
            comment = courseViewModel.Comments.First();
            comment.User.ShouldBeEquivalentTo(commenter.DisplayName);
            comment.Text.ShouldBeEquivalentTo(commentInputViewModel.Text);
            comment.DateTimeUtc.ShouldBeEquivalentTo(commentInputViewModel.DateTimeUtc);
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

        [Test]
        public void ApproveWorkflowItem()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // act
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            course.WorkflowSteps.Count.Should().Be(2);

            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.APPROVED, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, approver.DisplayName);
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[1], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.FACULTY_COUNCIL_MEMBER, string.Empty);
        }

        [Test]
        public void RejectWorkflowItem()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var rejector = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // act
            FacadeFactory.GetDomainFacade().RejectWorkflowItem(rejector.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            course.WorkflowSteps.Count.Should().Be(1);
            
            new WorkflowTestHelper().AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.REJECTED, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, rejector.DisplayName);
        }

        [Test]
        public void ApproveWorkflowItem_SecondApproval()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();
            var approverTwo = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            course.WorkflowSteps.Count.Should().Be(3);

            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.APPROVED, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, approver.DisplayName);
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[1], WorkflowStates.APPROVED, RoleTestHelper.FACULTY_COUNCIL_MEMBER, approver.DisplayName);
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[2], WorkflowStates.PENDING_APPROVAL, RoleTestHelper.APPC_MEMBER, string.Empty);
        }

        [Test]
        public void ApproveWorkflowItem_WorkflowOnLastStep()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();
            var approverTwo = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, programRequestInputViewModel.Name, WorkflowItemTypes.Program);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request is currently on the last workflow and should be completed.");
        }

        [Test]
        public void IsWorkflowItemCurrentlyOnLastWorkflowStep()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            var result = FacadeFactory.GetDomainFacade().IsWorkflowItemCurrentlyOnLastWorkflowStep(courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            result.Should().BeFalse();
        }

        [Test]
        public void IsWorkflowItemCurrentlyOnLastWorkflowStep_CurrentlyOnLastWorkflowStep()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();
            var approverTwo = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approverTwo.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            var result = FacadeFactory.GetDomainFacade().IsWorkflowItemCurrentlyOnLastWorkflowStep(courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            result.Should().BeTrue();
        }

        [Test]
        public void CompleteWorkflowItem()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var course = FacadeFactory.GetDomainFacade().FindCourse(courseRequestInputViewModel.Name);
            course.WorkflowSteps.Count.Should().Be(4);

            var workflowTestHelper = new WorkflowTestHelper();
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[0], WorkflowStates.APPROVED, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, approver.DisplayName);
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[1], WorkflowStates.APPROVED, RoleTestHelper.FACULTY_COUNCIL_MEMBER, approver.DisplayName);
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[2], WorkflowStates.APPROVED, RoleTestHelper.APPC_MEMBER, approver.DisplayName);
            workflowTestHelper.AssertWorkflowStep(course.WorkflowSteps[3], WorkflowStates.COMPLETED, RoleTestHelper.GFC_MEMBER, approver.DisplayName);
        }

        [Test]
        public void CompleteWorkflowItem_RejectedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been rejected");
        }

        [Test]
        public void ApproveWorkflowItem_CompletedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been completed");
        }

        [Test]
        public void ApproveWorkflowItem_ApproverNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to approve request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);
        }

        [Test]
        public void RejectWorkflowItem_RejectorNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER, RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to complete request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);
        }

        [Test]
        public void CompleteWorkflowItem_RejectorNotPartOfResponsibleParty()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);
            var approver = new UserTestHelper().CreateUserRoles(RoleTestHelper.FACULTY_MEMBER);

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            var errorMessage = string.Format("User '{0}' does not have sufficient permissions to reject request", approver.DisplayName);
            act.ShouldThrow<WMSException>().WithMessage(errorMessage);
        }

        [Test]
        public void ApproveWorkflowItem_Rejected()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));
            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);

            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been rejected");
        }

        [Test]
        public void RejectWorkflowItem_CompletedWorkflow()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new ApprovalChainTestHelper().CreateCourseApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var requester = new UserTestHelper().CreateUserWithTestRoles();
            var approver = new UserTestHelper().CreateUserWithTestRoles();

            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals(SemesterTestHelper.WINTER_2015));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals(DisciplineTestHelper.COMP_SCI));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(requester.Email, programRequestInputViewModel);

            var courseRequestInputViewModel = new CourseTestHelper().CreateNewValidCourseRequestInputViewModel(semester, discipline, programRequestInputViewModel.Name);
            FacadeFactory.GetDomainFacade().CreateCourseRequest(requester.Email, courseRequestInputViewModel);

            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().ApproveWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);
            FacadeFactory.GetDomainFacade().CompleteWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // act
            Action act = () => FacadeFactory.GetDomainFacade().RejectWorkflowItem(approver.Email, courseRequestInputViewModel.Name, WorkflowItemTypes.Course);

            // assert
            act.ShouldThrow<WMSException>().WithMessage("Request has already been completed");
        }
    }
}