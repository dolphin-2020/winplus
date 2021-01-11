namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientAttachments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Description = c.String(),
                        FilePath = c.String(),
                        Create = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ClientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        PhoneNum = c.String(nullable: false),
                        Address = c.String(),
                        Created = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Servers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        description = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsPaied = c.Boolean(nullable: false),
                        ServerStatu = c.String(),
                        IsRemoved = c.Boolean(nullable: false),
                        IsFinished = c.Boolean(nullable: false),
                        Create = c.DateTime(nullable: false),
                        DueTime = c.DateTime(nullable: false),
                        ClientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.ServerHistories",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ServerId = c.Guid(nullable: false),
                        description = c.String(),
                        Create = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servers", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.ServerPayments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Create = c.DateTime(nullable: false),
                        Payment = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServerId = c.Guid(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servers", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.ServerId);
            
            CreateTable(
                "dbo.UserClients",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        ClientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        JoinTime = c.DateTime(nullable: false),
                        IsRemoved = c.Boolean(nullable: false),
                        UserRoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserRoles", t => t.UserRoleId, cascadeDelete: true)
                .Index(t => t.UserRoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleName = c.String(),
                        IsRemoved = c.Boolean(nullable: false),
                        Create = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ClientAttachments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "UserRoleId", "dbo.UserRoles");
            DropForeignKey("dbo.UserClients", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClients", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ServerPayments", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.ServerHistories", "ServerId", "dbo.Servers");
            DropForeignKey("dbo.Servers", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientAttachments", "ClientId", "dbo.Clients");
            DropIndex("dbo.Users", new[] { "UserRoleId" });
            DropIndex("dbo.UserClients", new[] { "ClientId" });
            DropIndex("dbo.UserClients", new[] { "UserId" });
            DropIndex("dbo.ServerPayments", new[] { "ServerId" });
            DropIndex("dbo.ServerHistories", new[] { "ServerId" });
            DropIndex("dbo.Servers", new[] { "ClientId" });
            DropIndex("dbo.ClientAttachments", new[] { "ClientId" });
            DropIndex("dbo.ClientAttachments", new[] { "UserId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.Users");
            DropTable("dbo.UserClients");
            DropTable("dbo.ServerPayments");
            DropTable("dbo.ServerHistories");
            DropTable("dbo.Servers");
            DropTable("dbo.Clients");
            DropTable("dbo.ClientAttachments");
        }
    }
}
