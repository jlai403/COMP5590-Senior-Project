namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateWorkflowData : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WorkflowDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ApprovalChainStep_Id = c.Int(),
                        PreviousWorkflowData_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApprovalChainSteps", t => t.ApprovalChainStep_Id)
                .ForeignKey("dbo.WorkflowDatas", t => t.PreviousWorkflowData_Id)
                .Index(t => t.ApprovalChainStep_Id)
                .Index(t => t.PreviousWorkflowData_Id);
            
            AddColumn("dbo.Programs", "CurrentWorkflowData_Id", c => c.Int());
            CreateIndex("dbo.Programs", "CurrentWorkflowData_Id");
            AddForeignKey("dbo.Programs", "CurrentWorkflowData_Id", "dbo.WorkflowDatas", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Programs", "CurrentWorkflowData_Id", "dbo.WorkflowDatas");
            DropForeignKey("dbo.WorkflowDatas", "PreviousWorkflowData_Id", "dbo.WorkflowDatas");
            DropForeignKey("dbo.WorkflowDatas", "ApprovalChainStep_Id", "dbo.ApprovalChainSteps");
            DropIndex("dbo.WorkflowDatas", new[] { "PreviousWorkflowData_Id" });
            DropIndex("dbo.WorkflowDatas", new[] { "ApprovalChainStep_Id" });
            DropIndex("dbo.Programs", new[] { "CurrentWorkflowData_Id" });
            DropColumn("dbo.Programs", "CurrentWorkflowData_Id");
            DropTable("dbo.WorkflowDatas");
        }
    }
}
