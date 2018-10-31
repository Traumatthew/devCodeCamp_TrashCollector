namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedweeklypickupdatetostring : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WeeklyPickups", "Date", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WeeklyPickups", "Date", c => c.DateTime(nullable: false));
        }
    }
}
