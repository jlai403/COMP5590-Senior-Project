namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPrerequisitesToCourse : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrerequisiteCourses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Courses", "PrerequisiteCourses_Id", c => c.Int());
            CreateIndex("dbo.Courses", "PrerequisiteCourses_Id");
            AddForeignKey("dbo.Courses", "PrerequisiteCourses_Id", "dbo.PrerequisiteCourses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "PrerequisiteCourses_Id", "dbo.PrerequisiteCourses");
            DropForeignKey("dbo.PrerequisiteCourses", "Id", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "PrerequisiteCourses_Id" });
            DropIndex("dbo.PrerequisiteCourses", new[] { "Id" });
            DropColumn("dbo.Courses", "PrerequisiteCourses_Id");
            DropTable("dbo.PrerequisiteCourses");
        }
    }
}
