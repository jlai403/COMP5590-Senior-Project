using System.IO;

namespace WorkflowManagementSystem.Models.Files
{
    public class FileInputViewModel
    {
        public FileInputViewModel(string workflowItemName, string fileName, Stream stream, string contentType)
        {
            WorkflowItemName = workflowItemName;
            FileName = fileName;
            Content = stream;
            ContentType = contentType;
        }

        public string WorkflowItemName { get; set; }
        public string FileName { get; set; }
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}