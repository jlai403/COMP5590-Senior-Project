namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentsToWorkflowItem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateTimeUtc = c.DateTime(nullable: false),
                        Text = c.String(),
                        User_Id = c.Int(),
                        WorkflowItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.WorkflowItems", t => t.WorkflowItem_Id)
                .Index(t => t.User_Id)
                .Index(t => t.WorkflowItem_Id);
            
            DropColumn("dbo.Programs", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "Comment", c => c.String());
            DropForeignKey("dbo.Comments", "WorkflowItem_Id", "dbo.WorkflowItems");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropIndex("dbo.Comments", new[] { "WorkflowItem_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropTable("dbo.Comments");
        }
    }
}
