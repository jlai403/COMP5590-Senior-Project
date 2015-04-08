namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveDisciplineFromProgram : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Programs", "Discipline_Id", "dbo.Disciplines");
            DropIndex("dbo.Programs", new[] { "Discipline_Id" });
            AddColumn("dbo.Programs", "Faculty_Id", c => c.Int());
            CreateIndex("dbo.Programs", "Faculty_Id");
            AddForeignKey("dbo.Programs", "Faculty_Id", "dbo.Faculties", "Id");
            DropColumn("dbo.Programs", "Discipline_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "Discipline_Id", c => c.Int());
            DropForeignKey("dbo.Programs", "Faculty_Id", "dbo.Faculties");
            DropIndex("dbo.Programs", new[] { "Faculty_Id" });
            DropColumn("dbo.Programs", "Faculty_Id");
            CreateIndex("dbo.Programs", "Discipline_Id");
            AddForeignKey("dbo.Programs", "Discipline_Id", "dbo.Disciplines", "Id");
        }
    }
}
