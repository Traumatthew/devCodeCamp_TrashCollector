namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedaccounttype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "accountType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "accountType");
        }
    }
}
