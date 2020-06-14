namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingPayedToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PreparedDate", c => c.DateTime());
            AddColumn("dbo.Orders", "PayedDate", c => c.DateTime());
            AddColumn("dbo.Orders", "Prepared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Prepared");
            DropColumn("dbo.Orders", "PayedDate");
            DropColumn("dbo.Orders", "PreparedDate");
        }
    }
}
