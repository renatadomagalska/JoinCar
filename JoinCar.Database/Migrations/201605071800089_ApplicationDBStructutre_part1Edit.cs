namespace JoinCar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDBStructutre_part1Edit : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interests", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Trips", "Description", c => c.String());
            CreateIndex("dbo.Interests", "User_Id");
            AddForeignKey("dbo.Interests", "User_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Interests", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Interests", "Description", c => c.String());
            DropForeignKey("dbo.Interests", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Interests", new[] { "User_Id" });
            DropColumn("dbo.Trips", "Description");
            DropColumn("dbo.Interests", "User_Id");
        }
    }
}
