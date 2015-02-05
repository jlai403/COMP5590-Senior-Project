using System.Collections.Generic;
using System.Linq;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CommentAssembler
    {
        private Comment Comment { get; set; }

        private CommentAssembler(Comment comment)
        {
            Comment = comment;
        }

        public static List<CommentViewModel> AssembleAll(List<Comment> comments)
        {
            return comments.Select(comment => new CommentAssembler(comment).Assemble()).ToList();
        }

        private CommentViewModel Assemble()
        {
            var commentViewModel = new CommentViewModel();
            commentViewModel.User = Comment.User.GetDisplayName();
            commentViewModel.Text = Comment.Text;
            commentViewModel.DateTimeUtc = Comment.DateTimeUtc;
            return commentViewModel;
        }
    }
}