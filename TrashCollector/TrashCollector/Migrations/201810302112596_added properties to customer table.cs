namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedpropertiestocustomertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "lat", c => c.String());
            AddColumn("dbo.Customers", "lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "lng");
            DropColumn("dbo.Customers", "lat");
        }
    }
}
