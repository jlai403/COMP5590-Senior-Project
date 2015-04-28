using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using SpreadsheetLight;
using WorkflowManagementSystem.Models.Semesters;

namespace WorkflowManagementSystem.Models.ImportExport
{
    public class SemesterImporter : IImporter 
    {
        public static int HEADER_ROW = 1;
        public const string IMPORT_NAME = "Semester";

        public static int TERM_COLUMN_INDEX = 1;
        public static string TERM_COLUMN_NAME = "Term";

        public static int YEAR_COLUMN_INDEX = 2;
        public static string YEAR_COLUMN_NAME = "Year";

        public void Import(SLDocument importDocument)
        {
            var data = ExtractData(importDocument);
            LoadData(data);
        }

        private List<SemesterInputViewModel> ExtractData(SLDocument importDocument)
        {
            var numberOfRows = importDocument.GetWorksheetStatistics().NumberOfRows;

            var semesterInputViewModels = new List<SemesterInputViewModel>();
            for (int row = HEADER_ROW + 1; row <= numberOfRows; row++)
            {
                var semesterTerm = importDocument.GetCellValueAsString(row, TERM_COLUMN_INDEX);
                if (semesterTerm.IsNullOrWhiteSpace()) continue;

                var semesterInputViewModel = new SemesterInputViewModel();
                semesterInputViewModel.Term = semesterTerm;
                semesterInputViewModel.Year = importDocument.GetCellValueAsString(row, YEAR_COLUMN_INDEX);

                semesterInputViewModels.Add(semesterInputViewModel);
            }
            return semesterInputViewModels;
        }

        private void LoadData(List<SemesterInputViewModel> data)
        {
            foreach (var semesterInputViewModel in data)
            {
                var faculty = FacadeFactory.GetDomainFacade().FindSemester(semesterInputViewModel.Term, semesterInputViewModel.Year);
                if (faculty != null) continue;

                FacadeFactory.GetDomainFacade().CreateSemester(semesterInputViewModel);
            }
        }
    }
}