namespace FoodDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthIdToStaff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Staffs", "AuthUserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Staffs", "AuthUserId");
        }
    }
}
