namespace FoodDelivery.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStaff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Call_Id", "dbo.Staffs");
            DropForeignKey("dbo.Orders", "Cook_Id", "dbo.Staffs");
            RenameColumn(table: "dbo.Orders", name: "Call_Id", newName: "Call_UserId");
            RenameColumn(table: "dbo.Orders", name: "Cook_Id", newName: "Cook_UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_Call_Id", newName: "IX_Call_UserId");
            RenameIndex(table: "dbo.Orders", name: "IX_Cook_Id", newName: "IX_Cook_UserId");
            DropPrimaryKey("dbo.Staffs");
            AddColumn("dbo.Staffs", "UserId", c => c.Int(nullable: false, identity: false));
            AddPrimaryKey("dbo.Staffs", "UserId");
            AddForeignKey("dbo.Orders", "Call_UserId", "dbo.Staffs", "UserId");
            AddForeignKey("dbo.Orders", "Cook_UserId", "dbo.Staffs", "UserId");
            DropColumn("dbo.Staffs", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Staffs", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Orders", "Cook_UserId", "dbo.Staffs");
            DropForeignKey("dbo.Orders", "Call_UserId", "dbo.Staffs");
            DropPrimaryKey("dbo.Staffs");
            DropColumn("dbo.Staffs", "UserId");
            AddPrimaryKey("dbo.Staffs", "Id");
            RenameIndex(table: "dbo.Orders", name: "IX_Cook_UserId", newName: "IX_Cook_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_Call_UserId", newName: "IX_Call_Id");
            RenameColumn(table: "dbo.Orders", name: "Cook_UserId", newName: "Cook_Id");
            RenameColumn(table: "dbo.Orders", name: "Call_UserId", newName: "Call_Id");
            AddForeignKey("dbo.Orders", "Cook_Id", "dbo.Staffs", "Id");
            AddForeignKey("dbo.Orders", "Call_Id", "dbo.Staffs", "Id");
        }
    }
}
