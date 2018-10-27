namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpickuprequeststable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickUpRequests",
                c => new
                    {
                        PickUpId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        Place = c.String(),
                        Date = c.DateTime(nullable: false),
                        Time = c.String(),
                        Fee = c.Double(nullable: false),
                        complete = c.Boolean(nullable: false),
                        notes = c.String(),
                    })
                .PrimaryKey(t => t.PickUpId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PickUpRequests", "CustomerId", "dbo.Customers");
            DropIndex("dbo.PickUpRequests", new[] { "CustomerId" });
            DropTable("dbo.PickUpRequests");
        }
    }
}
