namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcustomercolumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "firstName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "lastName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "address", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "phoneNumber", c => c.String(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "phoneNumber", c => c.String());
            DropColumn("dbo.AspNetUsers", "address");
            DropColumn("dbo.AspNetUsers", "lastName");
            DropColumn("dbo.AspNetUsers", "firstName");
        }
    }
}
