using System.Data.Entity;
using WorkflowManagementSystem.Models.Faculty;
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

        protected DbSet<Discipline> Discipline { get; set; }
        protected DbSet<Faculty.Faculty> Faculty { get; set; }
        protected DbSet<Role> Role { get; set; }
        protected DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
                m.ToTable("UserRoles");
            });

            modelBuilder.Entity<Discipline>().HasRequired(d => d.Faculty).WithMany(f => f.Disciplines);
        }
    }
}