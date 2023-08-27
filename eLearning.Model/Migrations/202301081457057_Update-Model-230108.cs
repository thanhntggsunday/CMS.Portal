namespace eLearning.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModel230108 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Category", newName: "ContentCategories");
            AddColumn("dbo.ContentCategories", "MetaCode", c => c.String(maxLength: 250));
            AddColumn("dbo.CourseCategories", "MetaCode", c => c.String(maxLength: 250));
            AlterColumn("dbo.Content", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Content", "Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.CourseCategories", "MetaCode");
            DropColumn("dbo.ContentCategories", "MetaCode");
            RenameTable(name: "dbo.ContentCategories", newName: "Category");
        }
    }
}
