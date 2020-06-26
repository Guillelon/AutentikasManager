namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingOrderDEtailProductIdToNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderDetails", "PackageId", "dbo.Packages");
            DropIndex("dbo.OrderDetails", new[] { "PackageId" });
            AlterColumn("dbo.OrderDetails", "PackageId", c => c.Int(nullable: true));
            CreateIndex("dbo.OrderDetails", "PackageId");
            AddForeignKey("dbo.OrderDetails", "PackageId", "dbo.Packages", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "PackageId", "dbo.Packages");
            DropIndex("dbo.OrderDetails", new[] { "PackageId" });
            AlterColumn("dbo.OrderDetails", "PackageId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderDetails", "PackageId");
            AddForeignKey("dbo.OrderDetails", "PackageId", "dbo.Packages", "Id", cascadeDelete: true);
        }
    }
}
