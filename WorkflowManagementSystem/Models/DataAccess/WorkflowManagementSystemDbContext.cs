using System.Data.Entity;
using MyEntityFramework.Transaction;
using WorkflowManagementSystem.Models.User;

namespace WorkflowManagementSystem
{
    public class WorkflowManagementSystemDbContext : MyDbContext
    {
        public WorkflowManagementSystemDbContext()
        {
        }

        public WorkflowManagementSystemDbContext(string connectionString) : base(connectionString)
        {
        }

        protected DbSet<User> User { get; set; }
    }
}