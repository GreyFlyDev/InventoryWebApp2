namespace InventoryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRestockModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restocks",
                c => new
                    {
                        RestockId = c.Int(nullable: false, identity: true),
                        QuantityPurchased = c.Int(nullable: false),
                        PurchasePricePerUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.String(),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestockId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restocks");
        }
    }
}
