using System;
using System.IO;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Attachments
{
    public class Attachment : IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }

        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public string ContentType { get; set; }

        public void Update(User user, AttachmentInputViewModel attachmentInputViewModel)
        {
            User = user;
            FileName = attachmentInputViewModel.FileName;
            Content = GetBytes(attachmentInputViewModel.Content);
            ContentType = attachmentInputViewModel.ContentType;
            FileId = Guid.NewGuid();
        }

        private byte[] GetBytes(Stream stream)
        {
            byte[] byteArray;
            using(var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                byteArray = ms.ToArray();
            }
            return byteArray;
        }
    }
}
