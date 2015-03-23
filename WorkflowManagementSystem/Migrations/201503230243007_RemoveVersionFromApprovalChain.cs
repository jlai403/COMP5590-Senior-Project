namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveVersionFromApprovalChain : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApprovalChains", "Version");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApprovalChains", "Version", c => c.Int(nullable: false));
        }
    }
}
