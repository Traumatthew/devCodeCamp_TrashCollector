namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedattributestothepickrequeststable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PickUpRequests", "Place", c => c.String(nullable: false));
            AlterColumn("dbo.PickUpRequests", "Time", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PickUpRequests", "Time", c => c.String());
            AlterColumn("dbo.PickUpRequests", "Place", c => c.String());
        }
    }
}
