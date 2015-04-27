using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using SpreadsheetLight;
using WorkflowManagementSystem.Models.Faculties;

namespace WorkflowManagementSystem.Models.ImportExport
{
    internal class DisciplineImporter: IImporter
    {
        public static int HEADER_ROW = 1;
        public const string IMPORT_NAME = "Discipline";

        public static int FACULTY_COLUMN_INDEX = 1;
        public static string FACULTY_COLUMN_NAME = "Faculty";
        public static int NAME_COLUMN_INDEX = 2;
        public static string NAME_COLUMN_NAME = "Name";
        public static int CODE_COLUMN_INDEX = 3;
        public static string CODE_COLUMN_NAME = "Code";

        public void Import(SLDocument importDocument)
        {
            var data = ExtractData(importDocument);
            LoadData(data);
        }

        private List<DisciplineInputViewModel> ExtractData(SLDocument importDocument)
        {
            var numberOfRows = importDocument.GetWorksheetStatistics().NumberOfRows;

            var disciplineInputViewModels = new List<DisciplineInputViewModel>();
            for (int row = HEADER_ROW + 1; row <= numberOfRows; row++)
            {
                var facultyName = importDocument.GetCellValueAsString(row, FACULTY_COLUMN_INDEX);
                if (facultyName.IsNullOrWhiteSpace()) continue;

                var disciplineInputViewModel = new DisciplineInputViewModel();
                disciplineInputViewModel.Faculty = facultyName;
                disciplineInputViewModel.Name = importDocument.GetCellValueAsString(row, NAME_COLUMN_INDEX);
                disciplineInputViewModel.Code = importDocument.GetCellValueAsString(row, CODE_COLUMN_INDEX);

                disciplineInputViewModels.Add(disciplineInputViewModel);
            }
            return disciplineInputViewModels;
        }

        private void LoadData(List<DisciplineInputViewModel> data)
        {
            foreach (var disciplineInputViewModel in data)
            {
                var role = FacadeFactory.GetDomainFacade().FindRole(disciplineInputViewModel.Name);
                if (role != null) continue;

                FacadeFactory.GetDomainFacade().CreateDiscipline(disciplineInputViewModel);
            }
        }
    }
}