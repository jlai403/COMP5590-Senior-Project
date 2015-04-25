using System.Collections.Generic;
using SpreadsheetLight;
using WorkflowManagementSystem.Models.Faculties;

namespace WorkflowManagementSystem.Models.ImportExport
{
    public class FacultyImporter: IImporter
    {
        public static int HEADER_ROW = 1;
        public const string IMPORT_NAME = "Faculty";
        
        public static int NAME_COLUMN_INDEX = 1;
        public static string NAME_COLUMN_NAME = "Name";
        
        public void Import(SLDocument importDocument)
        {
            var data = ExtractData(importDocument);
            LoadData(data);
        }

        private List<FacultyInputViewModel> ExtractData(SLDocument importDocument)
        {
            var numberOfRows = importDocument.GetWorksheetStatistics().NumberOfRows;

            var facultyInputViewModels = new List<FacultyInputViewModel>();
            for (int row = HEADER_ROW + 1; row <= numberOfRows; row++)
            {
                var facultyInputViewModel = new FacultyInputViewModel();
                facultyInputViewModel.Name = importDocument.GetCellValueAsString(row, NAME_COLUMN_INDEX);

                facultyInputViewModels.Add(facultyInputViewModel);
            }
            return facultyInputViewModels;
        }

        private void LoadData(List<FacultyInputViewModel> data)
        {
            foreach (var facultyInputViewModel in data)
            {
                var faculty = FacadeFactory.GetDomainFacade().FindFaculty(facultyInputViewModel.Name);
                if (faculty != null) continue;

                FacadeFactory.GetDomainFacade().CreateFaculty(facultyInputViewModel);
            }
        }
    }
}