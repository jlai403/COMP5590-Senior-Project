namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTypeToWorkflowItemAndPulledCommonWorkflowItemPropertiesFromProgramUpToParent : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Programs", "Requester_Id", "dbo.Users");
            DropIndex("dbo.Programs", new[] { "Requester_Id" });
            AddColumn("dbo.WorkflowDatas", "State", c => c.Int(nullable: false));
            AddColumn("dbo.WorkflowItems", "Type", c => c.Int(nullable: false));
            AddColumn("dbo.WorkflowItems", "RequestedDateUTC", c => c.DateTime(nullable: false));
            AddColumn("dbo.WorkflowItems", "Name", c => c.String());
            AddColumn("dbo.WorkflowItems", "Requester_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.WorkflowItems", "Requester_Id");
            AddForeignKey("dbo.WorkflowItems", "Requester_Id", "dbo.Users", "Id", cascadeDelete: true);
            DropColumn("dbo.WorkflowDatas", "Status");
            DropColumn("dbo.Programs", "Requester_Id");
            DropColumn("dbo.Programs", "RequestedDateUTC");
            DropColumn("dbo.Programs", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "Name", c => c.String());
            AddColumn("dbo.Programs", "RequestedDateUTC", c => c.DateTime(nullable: false));
            AddColumn("dbo.Programs", "Requester_Id", c => c.Int());
            AddColumn("dbo.WorkflowDatas", "Status", c => c.Int(nullable: false));
            DropForeignKey("dbo.WorkflowItems", "Requester_Id", "dbo.Users");
            DropIndex("dbo.WorkflowItems", new[] { "Requester_Id" });
            DropColumn("dbo.WorkflowItems", "Requester_Id");
            DropColumn("dbo.WorkflowItems", "Name");
            DropColumn("dbo.WorkflowItems", "RequestedDateUTC");
            DropColumn("dbo.WorkflowItems", "Type");
            DropColumn("dbo.WorkflowDatas", "State");
            CreateIndex("dbo.Programs", "Requester_Id");
            AddForeignKey("dbo.Programs", "Requester_Id", "dbo.Users", "Id");
        }
    }
}
