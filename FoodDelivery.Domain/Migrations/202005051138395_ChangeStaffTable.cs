namespace FoodDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStaffTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Call_UserId", "dbo.Staffs");
            DropForeignKey("dbo.Orders", "Cook_UserId", "dbo.Staffs");
            DropIndex("dbo.Orders", new[] { "Call_UserId" });
            DropIndex("dbo.Orders", new[] { "Cook_UserId" });
            DropPrimaryKey("dbo.Staffs");
            AlterColumn("dbo.Orders", "Call_UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Orders", "Cook_UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Staffs", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Staffs", "UserId");
            CreateIndex("dbo.Orders", "Call_UserId");
            CreateIndex("dbo.Orders", "Cook_UserId");
            AddForeignKey("dbo.Orders", "Call_UserId", "dbo.Staffs", "UserId");
            AddForeignKey("dbo.Orders", "Cook_UserId", "dbo.Staffs", "UserId");
            DropColumn("dbo.Staffs", "AuthUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staffs", "AuthUserId", c => c.String());
            DropForeignKey("dbo.Orders", "Cook_UserId", "dbo.Staffs");
            DropForeignKey("dbo.Orders", "Call_UserId", "dbo.Staffs");
            DropIndex("dbo.Orders", new[] { "Cook_UserId" });
            DropIndex("dbo.Orders", new[] { "Call_UserId" });
            DropPrimaryKey("dbo.Staffs");
            AlterColumn("dbo.Staffs", "UserId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Orders", "Cook_UserId", c => c.Int());
            AlterColumn("dbo.Orders", "Call_UserId", c => c.Int());
            AddPrimaryKey("dbo.Staffs", "UserId");
            CreateIndex("dbo.Orders", "Cook_UserId");
            CreateIndex("dbo.Orders", "Call_UserId");
            AddForeignKey("dbo.Orders", "Cook_UserId", "dbo.Staffs", "UserId");
            AddForeignKey("dbo.Orders", "Call_UserId", "dbo.Staffs", "UserId");
        }
    }
}
