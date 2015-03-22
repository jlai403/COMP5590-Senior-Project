namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VersioningApprovalChain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApprovalChains", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.ApprovalChains", "Version", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApprovalChains", "Version");
            DropColumn("dbo.ApprovalChains", "Active");
        }
    }
}
