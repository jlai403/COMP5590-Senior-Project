namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactorProgramToExtendWorkflowItem : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Programs", "CurrentWorkflowData_Id", "dbo.WorkflowDatas");
            DropIndex("dbo.Programs", new[] { "CurrentWorkflowData_Id" });
            DropPrimaryKey("dbo.Programs");
            CreateTable(
                "dbo.WorkflowItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CurrentWorkflowData_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkflowDatas", t => t.CurrentWorkflowData_Id, cascadeDelete: true)
                .Index(t => t.CurrentWorkflowData_Id);
            
            DropColumn("dbo.Programs", "Id");
            AddColumn("dbo.Programs", "Id", c => c.Int(nullable: false, identity: false));

            AddPrimaryKey("dbo.Programs", "Id");
            CreateIndex("dbo.Programs", "Id");
            AddForeignKey("dbo.Programs", "Id", "dbo.WorkflowItems", "Id");
            DropColumn("dbo.Programs", "CurrentWorkflowData_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Programs", "CurrentWorkflowData_Id", c => c.Int());
            DropForeignKey("dbo.WorkflowItems", "CurrentWorkflowData_Id", "dbo.WorkflowDatas");
            DropForeignKey("dbo.Programs", "Id", "dbo.WorkflowItems");
            DropIndex("dbo.Programs", new[] { "Id" });
            DropIndex("dbo.WorkflowItems", new[] { "CurrentWorkflowData_Id" });
            DropPrimaryKey("dbo.Programs");

            DropColumn("dbo.Programs", "Id");
            AddColumn("dbo.Programs", "Id", c => c.Int(nullable: false, identity: true));

            DropTable("dbo.WorkflowItems");
            AddPrimaryKey("dbo.Programs", "Id");
            CreateIndex("dbo.Programs", "CurrentWorkflowData_Id");
            AddForeignKey("dbo.Programs", "CurrentWorkflowData_Id", "dbo.WorkflowDatas", "Id");
        }
    }
}
