namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingActiveToPackage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Packages", "ActiveDate", c => c.DateTime());
            AddColumn("dbo.Packages", "DeactiveDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Packages", "DeactiveDate");
            DropColumn("dbo.Packages", "ActiveDate");
            DropColumn("dbo.Packages", "Active");
        }
    }
}
