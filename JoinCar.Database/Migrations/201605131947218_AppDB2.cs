namespace JoinCar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDB2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Opinions", name: "User_Id", newName: "UserIssuingOpinion_Id");
            RenameIndex(table: "dbo.Opinions", name: "IX_User_Id", newName: "IX_UserIssuingOpinion_Id");
            AddColumn("dbo.Opinions", "Trip_Id", c => c.Int());
            CreateIndex("dbo.Opinions", "Trip_Id");
            AddForeignKey("dbo.Opinions", "Trip_Id", "dbo.Trips", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinions", "Trip_Id", "dbo.Trips");
            DropIndex("dbo.Opinions", new[] { "Trip_Id" });
            DropColumn("dbo.Opinions", "Trip_Id");
            RenameIndex(table: "dbo.Opinions", name: "IX_UserIssuingOpinion_Id", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Opinions", name: "UserIssuingOpinion_Id", newName: "User_Id");
        }
    }
}
