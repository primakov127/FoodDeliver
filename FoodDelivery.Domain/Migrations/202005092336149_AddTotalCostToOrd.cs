﻿namespace FoodDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTotalCostToOrd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "TotalCost");
        }
    }
}
