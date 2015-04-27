using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SpreadsheetLight;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ImportExport;

namespace WorkflowManagementSystem.Tests
{
    public class DisciplineImportTest: WorkflowManagementSystemTest
    {
        [Test]
        public void ImportDiscipline()
        {
            // arrange
            new FacultyTestHelper().CreateScienceAndTechnologyFaculty();

            var excelWorkbook = new SLDocument();

            CreateDisciplineImportSheet(excelWorkbook);

            AddDisciplineRow(excelWorkbook, 1, "Science and Technology", "Computer Science", "COMP");
            AddDisciplineRow(excelWorkbook, 2, "Science and Technology", "Biology", "BIOL");

            var ms = new MemoryStream();
            excelWorkbook.SaveAs(ms);

            // act
            FacadeFactory.GetImportExportFacade().Import(ms);

            // assert
            var roles = FacadeFactory.GetDomainFacade().FindAllDisciplines();
            roles.Count.ShouldBeEquivalentTo(2);
            roles.Should().Contain(x => x.Name.Equals("Computer Science") && x.Faculty.Equals("Science and Technology") && x.Code.Equals("COMP"));
            roles.Should().Contain(x => x.Name.Equals("Biology") && x.Faculty.Equals("Science and Technology") && x.Code.Equals("BIOL"));

        }

        private void AddDisciplineRow(SLDocument excelWorkbook, int row, string facultyName, string name, string code)
        {
            row += DisciplineImporter.HEADER_ROW;
            excelWorkbook.SetCellValue(row, DisciplineImporter.FACULTY_COLUMN_INDEX, facultyName);
            excelWorkbook.SetCellValue(row, DisciplineImporter.NAME_COLUMN_INDEX, name);
            excelWorkbook.SetCellValue(row, DisciplineImporter.CODE_COLUMN_INDEX, code);
        }

        private void CreateDisciplineImportSheet(SLDocument excelWorkbook)
        {
            excelWorkbook.AddWorksheet(DisciplineImporter.IMPORT_NAME);
            excelWorkbook.SelectWorksheet(DisciplineImporter.IMPORT_NAME);
            excelWorkbook.SetCellValue(DisciplineImporter.HEADER_ROW, DisciplineImporter.FACULTY_COLUMN_INDEX, DisciplineImporter.FACULTY_COLUMN_NAME);
            excelWorkbook.SetCellValue(DisciplineImporter.HEADER_ROW, DisciplineImporter.NAME_COLUMN_INDEX, DisciplineImporter.NAME_COLUMN_NAME);
            excelWorkbook.SetCellValue(DisciplineImporter.HEADER_ROW, DisciplineImporter.CODE_COLUMN_INDEX, DisciplineImporter.CODE_COLUMN_NAME);
        }
    }
}