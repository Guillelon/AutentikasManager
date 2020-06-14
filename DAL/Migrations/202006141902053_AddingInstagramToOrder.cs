namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingInstagramToOrder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ClientInstagram", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ClientInstagram");
        }
    }
}
