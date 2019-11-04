using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace X.Core.PermissionModule.Services.Database
{
    public partial class X.Core.PermissionModuleContext : DbContext
    {
        public X.Core.PermissionModuleContext()
        {
        }

        public X.Core.PermissionModuleContext(DbContextOptions<X.Core.PermissionModuleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<XCoreHello> XCoreHello { get; set; }
    }
}
