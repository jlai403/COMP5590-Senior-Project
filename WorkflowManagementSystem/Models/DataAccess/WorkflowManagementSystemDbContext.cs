using System.Data.Entity;
using WorkflowManagementSystem.Models.Roles;
using WorkflowManagementSystem.Models.Users;

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

        protected DbSet<User> User { get; set; }
        protected DbSet<Role> Role { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
                m.ToTable("UserRoles");
            });
        }
    }
}