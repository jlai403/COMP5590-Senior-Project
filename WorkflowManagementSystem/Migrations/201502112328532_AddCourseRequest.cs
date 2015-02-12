namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCourseRequest : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Semester_Id = c.Int(),
                        Code = c.String(),
                        Credits = c.String(),
                        CalendarEntry = c.String(),
                        CrossImpact = c.String(),
                        StudentImpact = c.String(),
                        LibraryImpact = c.String(),
                        ITSImpact = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkflowItems", t => t.Id)
                .ForeignKey("dbo.Semesters", t => t.Semester_Id)
                .Index(t => t.Id)
                .Index(t => t.Semester_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.Courses", "Id", "dbo.WorkflowItems");
            DropIndex("dbo.Courses", new[] { "Semester_Id" });
            DropIndex("dbo.Courses", new[] { "Id" });
            DropTable("dbo.Courses");
        }
    }
}
