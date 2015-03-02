namespace WorkflowManagementSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGradingToCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Grading", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "Grading");
        }
    }
}
