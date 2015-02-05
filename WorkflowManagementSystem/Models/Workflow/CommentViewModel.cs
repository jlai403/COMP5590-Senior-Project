using System;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class CommentViewModel
    {
        public string User { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeUtc { get; set; }
    }
}