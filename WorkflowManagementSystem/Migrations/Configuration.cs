using WorkflowManagementSystem.Models.DataAccess;

namespace WorkflowManagementSystem.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<WorkflowManagementSystemDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "WorkflowManagementSystem.WorkflowManagementSystemDbContext";
        }

        protected override void Seed(WorkflowManagementSystemDbContext context)
        {
            //  This method will be called after migrating to the latest version.
        }
    }
}
