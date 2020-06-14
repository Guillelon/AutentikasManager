namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingNotesAndDeliveryMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Notes", c => c.String());
            AddColumn("dbo.Orders", "DeliveryMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "DeliveryMethod");
            DropColumn("dbo.Orders", "Notes");
        }
    }
}
