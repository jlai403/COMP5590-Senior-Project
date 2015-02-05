namespace WorkflowManagementSystem.Models.Files
{
    public class FileViewModel
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }
    }
}