using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services.Database
{
    public partial class ApiTemplateContext : DbContext
    {
        public ApiTemplateContext()
        {
        }

        public ApiTemplateContext(DbContextOptions<ApiTemplateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<XCoreHello> XCoreHello { get; set; }
    }
}
