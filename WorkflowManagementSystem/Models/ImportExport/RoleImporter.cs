using System.Collections.Generic;
using SpreadsheetLight;
using WorkflowManagementSystem.Models.Roles;

namespace WorkflowManagementSystem.Models.ImportExport
{
    public class RoleImporter : IImporter
    {
        public const string IMPORT_NAME = "Role";
        public static int HEADER_ROW = 1;
       
        public static int NAME_COLUMN_INDEX = 1;
        public static string NAME_COLUMN_NAME = "Name";
        
        public void Import(SLDocument importDocument)
        {
            var data = ExtractData(importDocument);
            LoadData(data);
        }

        private List<RoleInputViewModel> ExtractData(SLDocument importDocument)
        {
            var numberOfRows = importDocument.GetWorksheetStatistics().NumberOfRows;

            var roleInputViewModels = new List<RoleInputViewModel>();
            for (int row = HEADER_ROW + 1; row <= numberOfRows; row++)
            {
                var roleInputViewModel = new RoleInputViewModel();
                roleInputViewModel.Name = importDocument.GetCellValueAsString(row, NAME_COLUMN_INDEX);

                roleInputViewModels.Add(roleInputViewModel);
            }
            return roleInputViewModels;
        }

        private void LoadData(List<RoleInputViewModel> data)
        {
            foreach (var roleInputViewModel in data)
            {
                var role = FacadeFactory.GetDomainFacade().FindRole(roleInputViewModel.Name);
                if (role != null) continue;

                FacadeFactory.GetDomainFacade().CreateRole(roleInputViewModel);
            }
        }
    }
}