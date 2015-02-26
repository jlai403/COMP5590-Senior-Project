namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDisciplineToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Discipline_Id", c => c.Int());
            AddColumn("dbo.Courses", "CourseNumber", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "Discipline_Id");
            AddForeignKey("dbo.Courses", "Discipline_Id", "dbo.Disciplines", "Id");
            DropColumn("dbo.Courses", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Courses", "Code", c => c.String());
            DropForeignKey("dbo.Courses", "Discipline_Id", "dbo.Disciplines");
            DropIndex("dbo.Courses", new[] { "Discipline_Id" });
            DropColumn("dbo.Courses", "CourseNumber");
            DropColumn("dbo.Courses", "Discipline_Id");
        }
    }
}
