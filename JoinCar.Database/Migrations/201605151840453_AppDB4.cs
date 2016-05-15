namespace JoinCar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AppDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "JsonDirections", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Trips", "JsonDirections");
        }
    }
}
