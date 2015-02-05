namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttachmentsToWorkflowItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileId = c.Guid(nullable: false),
                        FileName = c.String(),
                        Content = c.Binary(),
                        ContentType = c.String(),
                        User_Id = c.Int(),
                        WorkflowItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.WorkflowItems", t => t.WorkflowItem_Id)
                .Index(t => t.User_Id)
                .Index(t => t.WorkflowItem_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "WorkflowItem_Id", "dbo.WorkflowItems");
            DropForeignKey("dbo.Attachments", "User_Id", "dbo.Users");
            DropIndex("dbo.Attachments", new[] { "WorkflowItem_Id" });
            DropIndex("dbo.Attachments", new[] { "User_Id" });
            DropTable("dbo.Attachments");
        }
    }
}
