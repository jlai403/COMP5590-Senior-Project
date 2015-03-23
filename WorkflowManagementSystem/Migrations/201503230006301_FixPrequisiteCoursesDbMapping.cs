namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPrequisiteCoursesDbMapping : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "PrerequisiteCourses_Id", "dbo.PrerequisiteCourses");
            DropForeignKey("dbo.PrerequisiteCourses", "Id", "dbo.Courses");
            DropIndex("dbo.PrerequisiteCourses", new[] { "Id" });
            DropIndex("dbo.Courses", new[] { "PrerequisiteCourses_Id" });
            DropPrimaryKey("dbo.PrerequisiteCourses");
            AddColumn("dbo.PrerequisiteCourses", "Prerequisite_Id", c => c.Int());
            AddColumn("dbo.PrerequisiteCourses", "Course_Id", c => c.Int(nullable: false));
            DropColumn("dbo.PrerequisiteCourses", "Id");
            AddColumn("dbo.PrerequisiteCourses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.PrerequisiteCourses", "Id");
            CreateIndex("dbo.PrerequisiteCourses", "Prerequisite_Id");
            CreateIndex("dbo.PrerequisiteCourses", "Course_Id");
            AddForeignKey("dbo.PrerequisiteCourses", "Prerequisite_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.PrerequisiteCourses", "Course_Id", "dbo.Courses", "Id");
            DropColumn("dbo.Courses", "PrerequisiteCourses_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "PrerequisiteCourses_Id", c => c.Int());
            DropForeignKey("dbo.PrerequisiteCourses", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.PrerequisiteCourses", "Prerequisite_Id", "dbo.Courses");
            DropIndex("dbo.PrerequisiteCourses", new[] { "Course_Id" });
            DropIndex("dbo.PrerequisiteCourses", new[] { "Prerequisite_Id" });
            DropPrimaryKey("dbo.PrerequisiteCourses");
            AlterColumn("dbo.PrerequisiteCourses", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.PrerequisiteCourses", "Course_Id");
            DropColumn("dbo.PrerequisiteCourses", "Prerequisite_Id");
            AddPrimaryKey("dbo.PrerequisiteCourses", "Id");
            CreateIndex("dbo.Courses", "PrerequisiteCourses_Id");
            CreateIndex("dbo.PrerequisiteCourses", "Id");
            AddForeignKey("dbo.PrerequisiteCourses", "Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Courses", "PrerequisiteCourses_Id", "dbo.PrerequisiteCourses", "Id");
        }
    }
}
