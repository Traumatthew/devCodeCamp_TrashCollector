namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedphonenumbertophone : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "phone", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String(nullable: false));
            DropColumn("dbo.AspNetUsers", "phone");
        }
    }
}
