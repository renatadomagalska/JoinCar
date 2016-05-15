namespace JoinCar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDB5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Opinions", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Opinions", new[] { "Trip_Id" });
            RenameColumn(table: "dbo.Opinions", name: "Trip_Id", newName: "TripId");
            RenameColumn(table: "dbo.Opinions", name: "UserIssuingOpinion_Id", newName: "UserIssuingOpinionId");
            RenameIndex(table: "dbo.Opinions", name: "IX_UserIssuingOpinion_Id", newName: "IX_UserIssuingOpinionId");
            AlterColumn("dbo.Opinions", "TripId", c => c.Int(nullable: false));
            CreateIndex("dbo.Opinions", "TripId");
            AddForeignKey("dbo.Opinions", "TripId", "dbo.Trips", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinions", "TripId", "dbo.Trips");
            DropIndex("dbo.Opinions", new[] { "TripId" });
            AlterColumn("dbo.Opinions", "TripId", c => c.Int());
            RenameIndex(table: "dbo.Opinions", name: "IX_UserIssuingOpinionId", newName: "IX_UserIssuingOpinion_Id");
            RenameColumn(table: "dbo.Opinions", name: "UserIssuingOpinionId", newName: "UserIssuingOpinion_Id");
            RenameColumn(table: "dbo.Opinions", name: "TripId", newName: "Trip_Id");
            CreateIndex("dbo.Opinions", "Trip_Id");
            AddForeignKey("dbo.Opinions", "Trip_Id", "dbo.Trips", "Id");
        }
    }
}
