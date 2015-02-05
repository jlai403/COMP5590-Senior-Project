using System;
using WorkflowManagementSystem.Models.Users;

namespace WorkflowManagementSystem.Models.Workflow
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public virtual User User { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string Text { get; set; }

        public void Update(User user, DateTime commentDateTimeUtc, string text)
        {
            User = user;
            DateTimeUtc = commentDateTimeUtc;
            Text = text;
        }
    }
}