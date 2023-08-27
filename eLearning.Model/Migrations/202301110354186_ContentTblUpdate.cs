namespace eLearning.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentTblUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Content", "MetaCode", c => c.String(maxLength: 250));
            DropColumn("dbo.Content", "ItemType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Content", "ItemType", c => c.Int(nullable: false));
            DropColumn("dbo.Content", "MetaCode");
        }
    }
}
