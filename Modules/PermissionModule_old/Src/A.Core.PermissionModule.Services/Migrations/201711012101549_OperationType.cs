namespace A.Core.PermissionModule.Services.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OperationType : DbMigration
    {
        public override void Up()
        {
            AddColumn("permission.Permission", "OperationType", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("permission.Permission", "OperationType");
        }
    }
}
