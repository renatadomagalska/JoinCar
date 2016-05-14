namespace JoinCar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDB3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Interests", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Interests", new[] { "Trip_Id" });
            RenameColumn(table: "dbo.Interests", name: "Trip_Id", newName: "TripId");
            RenameColumn(table: "dbo.Interests", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Trips", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Interests", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.Trips", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.Interests", "TripId", c => c.Int(nullable: false));
            CreateIndex("dbo.Interests", "TripId");
            AddForeignKey("dbo.Interests", "TripId", "dbo.Trips", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interests", "TripId", "dbo.Trips");
            DropIndex("dbo.Interests", new[] { "TripId" });
            AlterColumn("dbo.Interests", "TripId", c => c.Int());
            RenameIndex(table: "dbo.Trips", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.Interests", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Trips", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Interests", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Interests", name: "TripId", newName: "Trip_Id");
            CreateIndex("dbo.Interests", "Trip_Id");
            AddForeignKey("dbo.Interests", "Trip_Id", "dbo.Trips", "Id");
        }
    }
}
