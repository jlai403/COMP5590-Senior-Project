using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SpreadsheetLight;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ImportExport;

namespace WorkflowManagementSystem.Tests
{
    public class FacultyImportTest: WorkflowManagementSystemTest
    {
        [Test]
        public void ImportFaculty()
        {
            // arrange
            var excelWorkbook = new SLDocument();

            CreateFacultyImportSheet(excelWorkbook);
            
            AddFacultyRow(excelWorkbook, 1, "Science and Technology");
            AddFacultyRow(excelWorkbook, 2, "Bissett School of Business");

            var ms = new MemoryStream();
            excelWorkbook.SaveAs(ms);

            // act
            FacadeFactory.GetImportExportFacade().Import(ms);

            // assert
            var faculties = FacadeFactory.GetDomainFacade().FindAllFaculties();
            faculties.Count.ShouldBeEquivalentTo(2);
            faculties.Should().Contain(x => x.Name.Equals("Science and Technology"));
            faculties.Should().Contain(x => x.Name.Equals("Bissett School of Business"));

        }

        private void AddFacultyRow(SLDocument excelWorkbook, int row, string facultyName)
        {
            row += FacultyImporter.HEADER_ROW;
            excelWorkbook.SetCellValue(row, FacultyImporter.NAME_COLUMN_INDEX, facultyName);
        }

        private void CreateFacultyImportSheet(SLDocument excelWorkbook)
        {
            excelWorkbook.AddWorksheet(FacultyImporter.IMPORT_NAME);
            excelWorkbook.SelectWorksheet(FacultyImporter.IMPORT_NAME);
            excelWorkbook.SetCellValue(FacultyImporter.HEADER_ROW, FacultyImporter.NAME_COLUMN_INDEX, FacultyImporter.NAME_COLUMN_NAME);
        }
    }
}