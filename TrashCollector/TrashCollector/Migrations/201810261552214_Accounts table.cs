namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Accountstable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerAccountDetails",
                c => new
                    {
                        CustomerAccountId = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        MoneyOwed = c.Double(nullable: false),
                        WeeklyPickUpDay = c.String(),
                        CurrentlySuspended = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomerAccountId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerAccountDetails", "CustomerId", "dbo.Customers");
            DropIndex("dbo.CustomerAccountDetails", new[] { "CustomerId" });
            DropTable("dbo.CustomerAccountDetails");
        }
    }
}
