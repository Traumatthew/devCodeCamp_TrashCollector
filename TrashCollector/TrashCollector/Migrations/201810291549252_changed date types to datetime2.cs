namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeddatetypestodatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Suspensions", "StartDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.Suspensions", "EndDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Suspensions", "EndDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Suspensions", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
