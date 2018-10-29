namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedsuspensionstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suspensions",
                c => new
                    {
                        SupsensionId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.SupsensionId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Suspensions", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Suspensions", new[] { "CustomerId" });
            DropTable("dbo.Suspensions");
        }
    }
}
