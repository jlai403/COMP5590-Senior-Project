using System.IO;
using FluentAssertions;
using NUnit.Framework;
using SpreadsheetLight;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.Models.ImportExport;

namespace WorkflowManagementSystem.Tests
{
    public class SemesterImportTest : WorkflowManagementSystemTest
    {
        [Test]
        public void ImportSemester()
        {
            // arrange
            var excelWorkbook = new SLDocument();

            CreateSemesterImportSheet(excelWorkbook);

            AddSemesterRow(excelWorkbook, 1, "Fall", "2014");
            AddSemesterRow(excelWorkbook, 2, "Winter", "2015");

            var ms = new MemoryStream();
            excelWorkbook.SaveAs(ms);

            // act
            FacadeFactory.GetImportExportFacade().Import(ms);

            // assert
            var semesters = FacadeFactory.GetDomainFacade().FindAllSemesters();
            semesters.Count.ShouldBeEquivalentTo(2);
            semesters.Should().Contain(x => x.Term.Equals("Fall") && x.Year.Equals("2014"));
            semesters.Should().Contain(x => x.Term.Equals("Winter") && x.Year.Equals("2015"));

        }

        private void AddSemesterRow(SLDocument excelWorkbook, int row, string term, string year)
        {
            row += SemesterImporter.HEADER_ROW;
            excelWorkbook.SetCellValue(row, SemesterImporter.TERM_COLUMN_INDEX, term);
            excelWorkbook.SetCellValue(row, SemesterImporter.YEAR_COLUMN_INDEX, year);
        }

        private void CreateSemesterImportSheet(SLDocument excelWorkbook)
        {
            excelWorkbook.AddWorksheet(SemesterImporter.IMPORT_NAME);
            excelWorkbook.SelectWorksheet(SemesterImporter.IMPORT_NAME);
            excelWorkbook.SetCellValue(SemesterImporter.HEADER_ROW, SemesterImporter.TERM_COLUMN_INDEX, SemesterImporter.TERM_COLUMN_NAME);
            excelWorkbook.SetCellValue(SemesterImporter.HEADER_ROW, SemesterImporter.YEAR_COLUMN_INDEX, SemesterImporter.YEAR_COLUMN_NAME);
        }
    }
}