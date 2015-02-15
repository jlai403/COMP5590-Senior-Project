namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWorkflowInvertedIndex : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IndexKeys",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WorkflowItemIndexes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Entity_Id = c.Int(),
                        Key_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WorkflowItems", t => t.Entity_Id)
                .ForeignKey("dbo.IndexKeys", t => t.Key_Id)
                .Index(t => t.Entity_Id)
                .Index(t => t.Key_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WorkflowItemIndexes", "Key_Id", "dbo.IndexKeys");
            DropForeignKey("dbo.WorkflowItemIndexes", "Entity_Id", "dbo.WorkflowItems");
            DropIndex("dbo.WorkflowItemIndexes", new[] { "Key_Id" });
            DropIndex("dbo.WorkflowItemIndexes", new[] { "Entity_Id" });
            DropTable("dbo.WorkflowItemIndexes");
            DropTable("dbo.IndexKeys");
        }
    }
}
