using System;
using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Files
{
    public class FileAssembler
    {
        private File File { get; set; }

        public FileAssembler(File file)
        {
            File = file;
        }

        public static Dictionary<string, Guid> AssembleAll(List<File> attachments)
        {
            var attachmentsDictionary = new Dictionary<string, Guid>();
            foreach (var attachment in attachments)
            {
                attachmentsDictionary.Add(attachment.FileName, attachment.FileId);
            }
            return attachmentsDictionary;
        }

        public FileViewModel Assemble()
        {
            var fileViewModel = new FileViewModel();
            fileViewModel.FileName = File.FileName;
            fileViewModel.Content = File.Content;
            fileViewModel.ContentType = File.ContentType;
            return fileViewModel;
        }
    }
}