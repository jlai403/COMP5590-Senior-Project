using System;
using System.Collections.Generic;
using WorkflowManagementSystem.Models.Attachments;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public abstract class WorkflowItem : IEntity
    {
        public int Id { get; set; }
        public virtual WorkflowData CurrentWorkflowData { get; set; }
        public abstract string APPROVAL_CHAIN_NAME { get; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Attachment> Attachments { get; set; }

        public WorkflowItem()
        {
            Comments = new List<Comment>();
        }

        public Comment AddComment(User user, DateTime commentDateTimeUtc, string text)
        {
            var comment = CommentRepository.CreateComment(user, commentDateTimeUtc, text);
            Comments.Add(comment);
            return comment;
        }

        public void AddAttachment(User user, AttachmentInputViewModel attachmentInputViewModel)
        {
            var attachment = AttachmentRepository.CreateAttachment(user, attachmentInputViewModel);
            Attachments.Add(attachment);
        }
    }
}