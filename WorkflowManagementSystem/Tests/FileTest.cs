using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Tests
{
    public class FileTest : WorkflowManagementSystemTest
    {
        [Test]
        public void FindFile()
        {
            // assemble
            new RoleTestHelper().CreateTestRoles();
            new ApprovalChainTestHelper().CreateProgramApprovalChain();
            new SemesterTestHelper().CreateTestSemesters();
            new DisciplineTestHelper().CreateTestDisciplines();

            var user = new UserTestHelper().CreateUserWithTestRoles();
            var semester = FacadeFactory.GetDomainFacade().FindAllSemesters().FirstOrDefault(x => x.DisplayName.Equals("2015 - Winter"));
            var discipline = FacadeFactory.GetDomainFacade().FindAllDisciplines().FirstOrDefault(x => x.Name.Equals("Computer Science"));

            var programRequestInputViewModel = new ProgramTestHelper().CreateNewValidProgramRequestInputViewModel(user, semester, discipline);
            FacadeFactory.GetDomainFacade().CreateProgramRequest(user.Email, programRequestInputViewModel);

            var expectedContentBytes = new byte[] { 0xFF, 0xFF, 0x00, 0xAA };
            var content = new MemoryStream(expectedContentBytes);

            var fileInputViewModel = new FileInputViewModel(programRequestInputViewModel.Name, "some pdf", content, "text/pdf");

            FacadeFactory.GetDomainFacade().UploadFile(user.Email, fileInputViewModel, WorkflowItemTypes.Program);
            var programViewModel = FacadeFactory.GetDomainFacade().FindProgram(programRequestInputViewModel.Name);
            
            var fileId = programViewModel.Attachments.First().Value;
            
            // act
            var fileViewModel = FacadeFactory.GetDomainFacade().FindFile(fileId);

            // assert
            fileViewModel.FileName.ShouldBeEquivalentTo(fileInputViewModel.FileName);
            fileViewModel.Content.ShouldBeEquivalentTo(expectedContentBytes);
            fileViewModel.ContentType.ShouldBeEquivalentTo(fileInputViewModel.ContentType);
        }
    }
}