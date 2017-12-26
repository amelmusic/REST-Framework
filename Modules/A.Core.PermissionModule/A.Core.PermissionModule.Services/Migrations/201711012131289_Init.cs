namespace A.Core.PermissionModule.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "permission.Permission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Description = c.String(nullable: false, maxLength: 250),
                        IsAllowed = c.Boolean(nullable: false),
                        OperationType = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "permission.RolePermission",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.Int(nullable: false),
                        PermissionId = c.Int(nullable: false),
                        IsAllowed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("permission.Permission", t => t.PermissionId, cascadeDelete: true)
                .ForeignKey("permission.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.PermissionId);
            
            CreateTable(
                "permission.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("permission.RolePermission", "RoleId", "permission.Role");
            DropForeignKey("permission.RolePermission", "PermissionId", "permission.Permission");
            DropIndex("permission.RolePermission", new[] { "PermissionId" });
            DropIndex("permission.RolePermission", new[] { "RoleId" });
            DropTable("permission.Role");
            DropTable("permission.RolePermission");
            DropTable("permission.Permission");
        }
    }
}
