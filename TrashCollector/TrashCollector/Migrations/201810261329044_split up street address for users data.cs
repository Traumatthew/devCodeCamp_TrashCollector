namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class splitupstreetaddressforusersdata : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "Street", c => c.String());
            AddColumn("dbo.Customers", "State", c => c.String());
            AddColumn("dbo.Customers", "Zip", c => c.String());
            AddColumn("dbo.Employees", "Street", c => c.String());
            AddColumn("dbo.Employees", "State", c => c.String());
            AddColumn("dbo.Employees", "Zip", c => c.String());
            AddColumn("dbo.AspNetUsers", "street", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "state", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "zip", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "Address");
            DropColumn("dbo.Employees", "Address");
            DropColumn("dbo.AspNetUsers", "address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "address", c => c.String(nullable: false));
            AddColumn("dbo.Employees", "Address", c => c.String());
            AddColumn("dbo.Customers", "Address", c => c.String());
            DropColumn("dbo.AspNetUsers", "zip");
            DropColumn("dbo.AspNetUsers", "state");
            DropColumn("dbo.AspNetUsers", "street");
            DropColumn("dbo.Employees", "Zip");
            DropColumn("dbo.Employees", "State");
            DropColumn("dbo.Employees", "Street");
            DropColumn("dbo.Customers", "Zip");
            DropColumn("dbo.Customers", "State");
            DropColumn("dbo.Customers", "Street");
        }
    }
}
