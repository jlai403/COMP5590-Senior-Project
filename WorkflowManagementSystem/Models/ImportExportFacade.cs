using System.IO;
using WorkflowManagementSystem.Models.ImportExport;

namespace WorkflowManagementSystem.Models
{
    public class ImportExportFacade
    {
        public void Import(Stream stream)
        {
            new ExcelImporter().Import(stream);
        }
    }
}