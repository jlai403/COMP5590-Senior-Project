using System;
using System.Collections.Generic;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public abstract class WorkflowItem : IEntity
    {
        public int Id { get; set; }
        public virtual WorkflowData CurrentWorkflowData { get; set; }
        public abstract string APPROVAL_CHAIN_NAME { get; }
        public virtual List<Comment> Comments { get; set; }

        public WorkflowItem()
        {
            Comments = new List<Comment>();
        }

        public void AddComment(User user, DateTime commentDateTimeUtc, string comment)
        {
            Comments.Add(CommentRepository.CreateComment(user, commentDateTimeUtc, comment));
        }
    }
}