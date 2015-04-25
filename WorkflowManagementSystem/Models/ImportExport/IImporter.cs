using SpreadsheetLight;

namespace WorkflowManagementSystem.Models.ImportExport
{
    public interface IImporter
    {
        void Import(SLDocument importDocument);
    }
}