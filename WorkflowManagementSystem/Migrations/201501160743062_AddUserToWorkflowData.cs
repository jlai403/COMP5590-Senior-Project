namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserToWorkflowData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WorkflowDatas", "User_Id", c => c.Int());
            CreateIndex("dbo.WorkflowDatas", "User_Id");
            AddForeignKey("dbo.WorkflowDatas", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkflowDatas", "User_Id", "dbo.Users");
            DropIndex("dbo.WorkflowDatas", new[] { "User_Id" });
            DropColumn("dbo.WorkflowDatas", "User_Id");
        }
    }
}
