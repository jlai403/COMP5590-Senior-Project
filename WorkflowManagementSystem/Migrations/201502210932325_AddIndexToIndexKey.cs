namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToIndexKey : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IndexKeys", "Key", c => c.String(maxLength: 256));
            CreateIndex("dbo.IndexKeys", "Key", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.IndexKeys", new[] { "Key" });
            AlterColumn("dbo.IndexKeys", "Key", c => c.String());
        }
    }
}
