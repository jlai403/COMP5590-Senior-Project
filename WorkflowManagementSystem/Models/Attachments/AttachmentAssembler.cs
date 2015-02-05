using System;
using System.Collections.Generic;

namespace WorkflowManagementSystem.Models.Attachments
{
    public class AttachmentAssembler
    {
        public static Dictionary<string, Guid> AssembleAll(List<Attachment> attachments)
        {
            var attachmentsDictionary = new Dictionary<string, Guid>();
            foreach (var attachment in attachments)
            {
                attachmentsDictionary.Add(attachment.FileName, attachment.FileId);
            }
            return attachmentsDictionary;
        }
    }
}