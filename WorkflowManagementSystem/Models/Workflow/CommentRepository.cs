using System;
using WorkflowManagementSystem.Models.DataAccess;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CommentRepository : Repository
    {
        public static Comment CreateComment(User user, DateTime commentDateTimeUtc, string text)
        {
            var comment = new Comment();
            AddEntity(comment);
            comment.Update(user, commentDateTimeUtc, text);
            return comment;
        }

        public static void AddComment(User user, CommentInputViewModel commentInputViewModel, WorkflowItemTypes workflowItemType)
        {
            var workflowItem = WorkflowRepository.FindWorkflowItemForType(workflowItemType, commentInputViewModel.WorkflowItemName);
            workflowItem.AddComment(user, commentInputViewModel.DateTimeUtc, commentInputViewModel.Text);
        }
    }
}