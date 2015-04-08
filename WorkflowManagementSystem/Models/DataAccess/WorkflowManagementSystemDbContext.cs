using System.Data.Entity;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Course;
using WorkflowManagementSystem.Models.Faculties;
using WorkflowManagementSystem.Models.Files;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Roles;
using WorkflowManagementSystem.Models.Search;
using WorkflowManagementSystem.Models.Semesters;
using WorkflowManagementSystem.Models.Users;
using WorkflowManagementSystem.Models.Workflow;

namespace WorkflowManagementSystem.Models.DataAccess
{
    public class WorkflowManagementSystemDbContext : MyDbContext
    {
        public WorkflowManagementSystemDbContext() : base()
        {
        }

        public WorkflowManagementSystemDbContext(string connectionString) : base(connectionString)
        {
        }

        protected DbSet<ApprovalChain> ApprovalChain { get; set; }
        protected DbSet<File> File { get; set; }
        protected DbSet<ApprovalChainStep> ApprovalChainStep { get; set; }
        protected DbSet<Comment> Comment { get; set; }
        protected DbSet<Course.Course> Course { get; set; }
        protected DbSet<PrerequisiteCourse> PrerequisiteCourse { get; set; }
        protected DbSet<Discipline> Discipline { get; set; }
        protected DbSet<Faculty> Faculty { get; set; }
        protected DbSet<IndexKey> IndexKey { get; set; }
        protected DbSet<Role> Role { get; set; }
        protected DbSet<Semester> Semester { get; set; }
        protected DbSet<User> User { get; set; }
        protected DbSet<WorkflowData> WorkflowData { get; set; }
        protected DbSet<WorkflowItem> WorkflowItem { get; set; }
        protected DbSet<WorkflowItemIndex> WorkflowItemIndices { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<WorkflowItem>().HasRequired(x => x.Requester);
            modelBuilder.Entity<WorkflowItem>().HasRequired(x => x.CurrentWorkflowData);
            modelBuilder.Entity<Program>().ToTable("Programs");
            modelBuilder.Entity<Course.Course>().ToTable("Courses");

            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
                m.ToTable("UserRoles");
            });

            modelBuilder.Entity<Discipline>().HasRequired(d => d.Faculty).WithMany(f => f.Disciplines).WillCascadeOnDelete(true);

            modelBuilder.Entity<ApprovalChain>().HasMany(ac => ac.ApprovalChainSteps);
            modelBuilder.Entity<Course.Course>().HasMany(x => x.PrerequisiteCourses).WithRequired(x => x.Course).WillCascadeOnDelete(false);
        }

    }
}