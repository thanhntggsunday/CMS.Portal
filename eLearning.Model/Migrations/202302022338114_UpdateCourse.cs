namespace eLearning.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "Overview", c => c.String());
            AddColumn("dbo.Courses", "Requirements", c => c.String());
            AlterColumn("dbo.Courses", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Courses", "Description", c => c.String(maxLength: 250));
            DropColumn("dbo.Courses", "Requirements");
            DropColumn("dbo.Courses", "Overview");
        }
    }
}
