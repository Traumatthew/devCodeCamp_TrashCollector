namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedgeolocationstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeoLocations",
                c => new
                    {
                        GeoID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Lat = c.String(),
                        Long = c.String(),
                    })
                .PrimaryKey(t => t.GeoID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GeoLocations", "CustomerId", "dbo.Customers");
            DropIndex("dbo.GeoLocations", new[] { "CustomerId" });
            DropTable("dbo.GeoLocations");
        }
    }
}
