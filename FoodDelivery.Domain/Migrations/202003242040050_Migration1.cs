namespace FoodDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orders", "ClientName", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "ClientAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Orders", "ClientPhone", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "ClientPhone", c => c.String());
            AlterColumn("dbo.Orders", "ClientAddress", c => c.String());
            AlterColumn("dbo.Orders", "ClientName", c => c.String());
        }
    }
}
