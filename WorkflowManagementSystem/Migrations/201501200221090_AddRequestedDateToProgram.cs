namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequestedDateToProgram : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Programs", "RequestedDateUTC", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Programs", "RequestedDateUTC");
        }
    }
}
