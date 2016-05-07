namespace JoinCar.Database.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ApplicationDBStructutre_part1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Interests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Trip_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trips", t => t.Trip_Id)
                .Index(t => t.Trip_Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        From = c.String(),
                        To = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        AvailableSeats = c.Int(nullable: false),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Opinions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Opinions", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Interests", "Trip_Id", "dbo.Trips");
            DropForeignKey("dbo.Trips", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Opinions", new[] { "User_Id" });
            DropIndex("dbo.Trips", new[] { "User_Id" });
            DropIndex("dbo.Interests", new[] { "Trip_Id" });
            DropTable("dbo.Opinions");
            DropTable("dbo.Trips");
            DropTable("dbo.Interests");
        }
    }
}
