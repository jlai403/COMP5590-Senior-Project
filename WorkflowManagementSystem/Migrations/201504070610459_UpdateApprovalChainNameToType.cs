namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApprovalChainNameToType : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ApprovalChains", "Name", "Type");
        }
        
        public override void Down()
        {
            RenameColumn("dbo.ApprovalChains", "Type", "Name");
        }
    }
}
