using System;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CommentInputViewModel
    {
        public string WorkflowItemName { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeUtc { get; set; }

        public CommentInputViewModel()
        {
            DateTimeUtc = DateTime.UtcNow;
        }
    }
}
