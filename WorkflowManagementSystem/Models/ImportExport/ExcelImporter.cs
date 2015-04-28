using System.Collections.Generic;
using System.IO;
using SpreadsheetLight;
using WorkflowManagementSystem.Models.ErrorHandling;

namespace WorkflowManagementSystem.Models.ImportExport
{
    public class ExcelImporter
    {
        private List<string> _importOrder = new List<string>
        {
            FacultyImporter.IMPORT_NAME,
            DisciplineImporter.IMPORT_NAME,
            RoleImporter.IMPORT_NAME,
            SemesterImporter.IMPORT_NAME,
            ApprovalChainImporter.IMPORT_NAME
        };

        public void Import(Stream inputStream)
        {
            using (var importDocument = new SLDocument(inputStream))
            {
                foreach (var importName in _importOrder)
                {
                    if (importDocument.SelectWorksheet(importName) == false) continue;
                    var importer = GetImporter(importName);
                    importer.Import(importDocument);
                }
            }
        }

        private IImporter GetImporter(string importName)
        {
            switch (importName)
            {
                case FacultyImporter.IMPORT_NAME:
                    return new FacultyImporter();
                case DisciplineImporter.IMPORT_NAME:
                    return new DisciplineImporter();
                case RoleImporter.IMPORT_NAME:
                    return new RoleImporter();
                case SemesterImporter.IMPORT_NAME:
                    return new SemesterImporter();
                case ApprovalChainImporter.IMPORT_NAME:
                    return new ApprovalChainImporter();
                default:
                    throw new WMSException("Could not find import for '{0}'", importName);
            }
        }
    }
}