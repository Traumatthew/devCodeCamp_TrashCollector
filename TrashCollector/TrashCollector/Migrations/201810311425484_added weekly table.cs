namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedweeklytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.WeeklyPickups",
                c => new
                    {
                        WeeklyPickupID = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Complete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.WeeklyPickupID)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WeeklyPickups", "CustomerId", "dbo.Customers");
            DropIndex("dbo.WeeklyPickups", new[] { "CustomerId" });
            DropTable("dbo.WeeklyPickups");
        }
    }
}
