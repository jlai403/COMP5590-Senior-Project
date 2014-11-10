using System.Data.Entity;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class WorkflowManagementSystemDbContext : MyDbContext
    {
        public WorkflowManagementSystemDbContext()
        {
        }

        public WorkflowManagementSystemDbContext(string connectionString) : base(connectionString)
        {
        }

        protected DbSet<User.User> User { get; set; }
    }
}