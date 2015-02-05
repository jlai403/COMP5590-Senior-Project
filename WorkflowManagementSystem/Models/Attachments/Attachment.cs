using System;
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
            Content = attachmentInputViewModel.Content.ToArray();
            ContentType = attachmentInputViewModel.ContentType;
            FileId = Guid.NewGuid();
        }

    }
}
