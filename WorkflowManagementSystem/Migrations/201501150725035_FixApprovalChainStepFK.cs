namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixApprovalChainStepFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApprovalChainSteps", "ApprovalChain_Id", "dbo.ApprovalChains");
            DropIndex("dbo.ApprovalChainSteps", new[] { "ApprovalChain_Id" });
            AlterColumn("dbo.ApprovalChainSteps", "ApprovalChain_Id", c => c.Int());
            CreateIndex("dbo.ApprovalChainSteps", "ApprovalChain_Id");
            AddForeignKey("dbo.ApprovalChainSteps", "ApprovalChain_Id", "dbo.ApprovalChains", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalChainSteps", "ApprovalChain_Id", "dbo.ApprovalChains");
            DropIndex("dbo.ApprovalChainSteps", new[] { "ApprovalChain_Id" });
            AlterColumn("dbo.ApprovalChainSteps", "ApprovalChain_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ApprovalChainSteps", "ApprovalChain_Id");
            AddForeignKey("dbo.ApprovalChainSteps", "ApprovalChain_Id", "dbo.ApprovalChains", "Id", cascadeDelete: true);
        }
    }
}
