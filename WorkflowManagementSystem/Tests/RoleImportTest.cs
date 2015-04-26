using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SpreadsheetLight;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ImportExport;

namespace WorkflowManagementSystem.Tests
{
    public class RoleImportTest: WorkflowManagementSystemTest
    {
        [Test]
        public void ImportRole()
        {
            // arrange
            var excelWorkbook = new SLDocument();

            CreateRoleImportSheet(excelWorkbook);

            AddRoleRow(excelWorkbook, 1, "Role One");
            AddRoleRow(excelWorkbook, 2, "Role Two");

            var ms = new MemoryStream();
            excelWorkbook.SaveAs(ms);

            // act
            FacadeFactory.GetImportExportFacade().Import(ms);

            // assert
            var roles = FacadeFactory.GetDomainFacade().FindAllRoles();
            roles.Count.ShouldBeEquivalentTo(2);
            roles.Should().Contain(x => x.Name.Equals("Role One"));
            roles.Should().Contain(x => x.Name.Equals("Role Two"));

        }

        private void AddRoleRow(SLDocument excelWorkbook, int row, string facultyName)
        {
            row += RoleImporter.HEADER_ROW;
            excelWorkbook.SetCellValue(row, RoleImporter.NAME_COLUMN_INDEX, facultyName);
        }

        private void CreateRoleImportSheet(SLDocument excelWorkbook)
        {
            excelWorkbook.AddWorksheet(RoleImporter.IMPORT_NAME);
            excelWorkbook.SelectWorksheet(RoleImporter.IMPORT_NAME);
            excelWorkbook.SetCellValue(RoleImporter.HEADER_ROW, RoleImporter.NAME_COLUMN_INDEX, RoleImporter.NAME_COLUMN_NAME);
        }
    }
}