namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApprovalChain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovalChains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApprovalChainSteps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sequence = c.Int(nullable: false),
                        Role_Id = c.Int(),
                        ApprovalChain_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.ApprovalChains", t => t.ApprovalChain_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.ApprovalChain_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApprovalChainSteps", "ApprovalChain_Id", "dbo.ApprovalChains");
            DropForeignKey("dbo.ApprovalChainSteps", "Role_Id", "dbo.Roles");
            DropIndex("dbo.ApprovalChainSteps", new[] { "ApprovalChain_Id" });
            DropIndex("dbo.ApprovalChainSteps", new[] { "Role_Id" });
            DropTable("dbo.ApprovalChainSteps");
            DropTable("dbo.ApprovalChains");
        }
    }
}
