using System;
using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using SpreadsheetLight;
using WorkflowManagementSystem.Models.ApprovalChains;

namespace WorkflowManagementSystem.Models.ImportExport
{
    public class ApprovalChainImporter : IImporter
    {
        public static int HEADER_ROW = 1;
        public const string IMPORT_NAME = "ApprovalChain";
        
        public static int TYPE_COLUMN_INDEX = 1;
        public static string TYPE_COLUMN_NAME = "Name";

        public static int ACTIVE_COLUMN_INDEX = 2;
        public static string ACTIVE_COLUMN_NAME = "Active";

        public static int ROLES_START_COLUMN_INDEX = 3;
        public static string ROLES_COLUMN_NAME = "Roles (Ordered left to right)";

        public static string ACTIVE_APPROVAL_CHAIN = "yes";

        public void Import(SLDocument importDocument)
        {
            var data = ExtractData(importDocument);
            LoadData(data);
        }

        private List<ApprovalChainInputViewModel> ExtractData(SLDocument importDocument)
        {
            var numberOfRows = importDocument.GetWorksheetStatistics().NumberOfRows;

            var approvalChainInputViewModels = new List<ApprovalChainInputViewModel>();
            for (int row = HEADER_ROW + 1; row <= numberOfRows; row++)
            {
                var type = importDocument.GetCellValueAsString(row, TYPE_COLUMN_INDEX);
                if (type.IsNullOrWhiteSpace()) continue;

                var active = importDocument.GetCellValueAsString(row, ACTIVE_COLUMN_INDEX);

                var approvalChainInputViewModel = new ApprovalChainInputViewModel();
                approvalChainInputViewModel.Type = type;
                approvalChainInputViewModel.Active = ACTIVE_APPROVAL_CHAIN.Equals(active, StringComparison.InvariantCultureIgnoreCase);

                for (int textColumn = ROLES_START_COLUMN_INDEX; textColumn < 10; textColumn++)
                {
                    string role = importDocument.GetCellValueAsString(row, textColumn);
                    if (!role.IsNullOrWhiteSpace())
                    {
                        approvalChainInputViewModel.Roles.Add(role);
                    }
                }

                approvalChainInputViewModels.Add(approvalChainInputViewModel);
            }
            return approvalChainInputViewModels;
        }

        private void LoadData(List<ApprovalChainInputViewModel> data)
        {
            foreach (var approvalChainInputViewModel in data)
            {
                var approvalChain = FacadeFactory.GetDomainFacade().FindApprovalChain(approvalChainInputViewModel.Type, approvalChainInputViewModel.Roles);
                if (approvalChain != null) continue;

                FacadeFactory.GetDomainFacade().CreateApprovalChain(approvalChainInputViewModel);
            }
        }
    }
}