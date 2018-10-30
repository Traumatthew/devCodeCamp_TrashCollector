namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedlatandlongattributetoemployees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "lat", c => c.String());
            AddColumn("dbo.Employees", "lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "lng");
            DropColumn("dbo.Employees", "lat");
        }
    }
}
