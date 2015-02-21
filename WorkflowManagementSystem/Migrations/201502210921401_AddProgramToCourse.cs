namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProgramToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Program_Id", c => c.Int());
            CreateIndex("dbo.Courses", "Program_Id");
            AddForeignKey("dbo.Courses", "Program_Id", "dbo.Programs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Program_Id", "dbo.Programs");
            DropIndex("dbo.Courses", new[] { "Program_Id" });
            DropColumn("dbo.Courses", "Program_Id");
        }
    }
}
