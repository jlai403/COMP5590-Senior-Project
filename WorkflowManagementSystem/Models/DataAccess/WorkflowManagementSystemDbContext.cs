﻿using System.Data.Entity;
using WorkflowManagementSystem.Models.ApprovalChains;
using WorkflowManagementSystem.Models.Faculty;
using WorkflowManagementSystem.Models.Programs;
using WorkflowManagementSystem.Models.Roles;
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
        protected DbSet<ApprovalChainStep> ApprovalChainStep { get; set; }
        protected DbSet<Discipline> Discipline { get; set; }
        protected DbSet<Faculty.Faculty> Faculty { get; set; }
        protected DbSet<Program> Program { get; set; }
        protected DbSet<Role> Role { get; set; }
        protected DbSet<Semester> Semester { get; set; }
        protected DbSet<User> User { get; set; }
        protected DbSet<WorkflowData> WorkflowData { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users).Map(m =>
            {
                m.MapLeftKey("UserId");
                m.MapRightKey("RoleId");
                m.ToTable("UserRoles");
            });

            modelBuilder.Entity<Discipline>().HasRequired(d => d.Faculty).WithMany(f => f.Disciplines).WillCascadeOnDelete(true);

            modelBuilder.Entity<ApprovalChain>().HasMany(ac => ac.ApprovalChainSteps);
        }
    }
}