namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProgram : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Programs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CrossImpact = c.String(),
                        StudentImpact = c.String(),
                        LibraryImpact = c.String(),
                        ITSImpact = c.String(),
                        Comment = c.String(),
                        Discipline_Id = c.Int(),
                        Requester_Id = c.Int(),
                        Semester_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Disciplines", t => t.Discipline_Id)
                .ForeignKey("dbo.Users", t => t.Requester_Id)
                .ForeignKey("dbo.Semesters", t => t.Semester_Id)
                .Index(t => t.Discipline_Id)
                .Index(t => t.Requester_Id)
                .Index(t => t.Semester_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programs", "Semester_Id", "dbo.Semesters");
            DropForeignKey("dbo.Programs", "Requester_Id", "dbo.Users");
            DropForeignKey("dbo.Programs", "Discipline_Id", "dbo.Disciplines");
            DropIndex("dbo.Programs", new[] { "Semester_Id" });
            DropIndex("dbo.Programs", new[] { "Requester_Id" });
            DropIndex("dbo.Programs", new[] { "Discipline_Id" });
            DropTable("dbo.Programs");
        }
    }
}
