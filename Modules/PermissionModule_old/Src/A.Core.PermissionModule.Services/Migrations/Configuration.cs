using A.Core.PermissionModule.Model;

namespace A.Core.PermissionModule.Services.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<A.Core.PermissionModule.Services.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(A.Core.PermissionModule.Services.Context context)
        {
            //context.Permission.AddOrUpdate(p=> p.Name, new Permission() { Description = "Default rule", Name = "*", IsAllowed = true});
        }
    }
}
