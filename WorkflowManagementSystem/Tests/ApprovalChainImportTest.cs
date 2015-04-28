using System.IO;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SpreadsheetLight;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.ImportExport;

namespace WorkflowManagementSystem.Tests
{
    public class ApprovalChainImportTest: WorkflowManagementSystemTest
    {
        [Test]
        public void ImportApprovalChain()
        {
            // arrange
            new RoleTestHelper().CreateTestRoles();

            var excelWorkbook = new SLDocument();

            CreateApprovalChainImportSheet(excelWorkbook);

            AddApprovalChainRow(excelWorkbook, 1, "Program", "yes", RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER, RoleTestHelper.GFC_MEMBER);
            AddApprovalChainRow(excelWorkbook, 2, "Course", "yes", RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER, RoleTestHelper.GFC_MEMBER);
            AddApprovalChainRow(excelWorkbook, 3, "Course", "yes", RoleTestHelper.FACULTY_CURRICULUMN_MEMBER, RoleTestHelper.FACULTY_COUNCIL_MEMBER, RoleTestHelper.APPC_MEMBER, RoleTestHelper.GFC_MEMBER);

            var ms = new MemoryStream();
            excelWorkbook.SaveAs(ms);

            // act
            FacadeFactory.GetImportExportFacade().Import(ms);

            // assert
            FacadeFactory.GetDomainFacade().FindAllApprovalChains(ApprovalChainTypes.PROGRAM).Count.ShouldBeEquivalentTo(1);

            var programApprovalChainSteps = FacadeFactory.GetDomainFacade().FindActiveApprovalChainSteps(ApprovalChainTypes.PROGRAM);
            programApprovalChainSteps.Count.ShouldBeEquivalentTo(4);
            programApprovalChainSteps.Should().Contain(x => x.Sequence == 1 && x.RoleName.Equals(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER));
            programApprovalChainSteps.Should().Contain(x => x.Sequence == 2 && x.RoleName.Equals(RoleTestHelper.FACULTY_COUNCIL_MEMBER));
            programApprovalChainSteps.Should().Contain(x => x.Sequence == 3 && x.RoleName.Equals(RoleTestHelper.APPC_MEMBER));
            programApprovalChainSteps.Should().Contain(x => x.Sequence == 4 && x.RoleName.Equals(RoleTestHelper.GFC_MEMBER));

            FacadeFactory.GetDomainFacade().FindAllApprovalChains(ApprovalChainTypes.COURSE).Count.ShouldBeEquivalentTo(1);

            var courseApprovalChainSteps = FacadeFactory.GetDomainFacade().FindActiveApprovalChainSteps(ApprovalChainTypes.COURSE);
            courseApprovalChainSteps.Count.ShouldBeEquivalentTo(4);
            courseApprovalChainSteps.Should().Contain(x => x.Sequence == 1 && x.RoleName.Equals(RoleTestHelper.FACULTY_CURRICULUMN_MEMBER));
            courseApprovalChainSteps.Should().Contain(x => x.Sequence == 2 && x.RoleName.Equals(RoleTestHelper.FACULTY_COUNCIL_MEMBER));
            courseApprovalChainSteps.Should().Contain(x => x.Sequence == 3 && x.RoleName.Equals(RoleTestHelper.APPC_MEMBER));
            courseApprovalChainSteps.Should().Contain(x => x.Sequence == 4 && x.RoleName.Equals(RoleTestHelper.GFC_MEMBER));
        }

        private void AddApprovalChainRow(SLDocument excelWorkbook, int row, string type, string active, params string[] roles)
        {
            row += ApprovalChainImporter.HEADER_ROW;
            excelWorkbook.SetCellValue(row, ApprovalChainImporter.TYPE_COLUMN_INDEX, type);
            excelWorkbook.SetCellValue(row, ApprovalChainImporter.ACTIVE_COLUMN_INDEX, active);

            var roleColumnIndex = ApprovalChainImporter.ROLES_START_COLUMN_INDEX;
            foreach (var role in roles)
            {
                excelWorkbook.SetCellValue(row, roleColumnIndex++, role);
            }
        }

        private void CreateApprovalChainImportSheet(SLDocument excelWorkbook)
        {
            excelWorkbook.AddWorksheet(ApprovalChainImporter.IMPORT_NAME);
            excelWorkbook.SelectWorksheet(ApprovalChainImporter.IMPORT_NAME);
            excelWorkbook.SetCellValue(ApprovalChainImporter.HEADER_ROW, ApprovalChainImporter.TYPE_COLUMN_INDEX, ApprovalChainImporter.TYPE_COLUMN_NAME);
            excelWorkbook.SetCellValue(ApprovalChainImporter.HEADER_ROW, ApprovalChainImporter.ACTIVE_COLUMN_INDEX, ApprovalChainImporter.ACTIVE_COLUMN_NAME);
            excelWorkbook.SetCellValue(ApprovalChainImporter.HEADER_ROW, ApprovalChainImporter.ROLES_START_COLUMN_INDEX, ApprovalChainImporter.ROLES_COLUMN_NAME);
        }
    }
}