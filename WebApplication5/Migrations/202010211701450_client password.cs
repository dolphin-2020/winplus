namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientpassword : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "Password");
        }
    }
}
