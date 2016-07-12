namespace InventoryWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTotalInvestmentToProductAndRestockModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "TotalInvestment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Restocks", "TotalInvestment", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Restocks", "TotalInvestment");
            DropColumn("dbo.Products", "TotalInvestment");
        }
    }
}
