using System.IO;

namespace WorkflowManagementSystem.Models.Attachments
{
    public class AttachmentInputViewModel
    {
        public string WorkflowItemName { get; set; }
        public string FileName { get; set; }
        public Stream Content { get; set; }
        public string ContentType { get; set; }
    }
}