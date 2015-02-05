using System;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CommentRepository : Repository
    {
        public static Comment AddComment(User user, DateTime commentDateTimeUtc, string text)
        {
            var comment = new Comment();
            AddEntity(comment);
            comment.Update(user, commentDateTimeUtc, text);
            return comment;
        }
    }
}