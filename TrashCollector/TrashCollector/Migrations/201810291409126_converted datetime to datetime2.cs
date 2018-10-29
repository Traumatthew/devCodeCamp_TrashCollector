namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class converteddatetimetodatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PickUpRequests", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickUpRequests", "Date", c => c.DateTime(nullable: false));
        }
    }
}
