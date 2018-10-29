namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedcitytocustomertable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "City", c => c.String());
            AddColumn("dbo.AspNetUsers", "city", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "city");
            DropColumn("dbo.Employees", "City");
        }
    }
}
