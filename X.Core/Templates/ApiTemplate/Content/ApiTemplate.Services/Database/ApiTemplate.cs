using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ApiTemplate.Services.Database
{
    public partial class ApiTemplate : DbContext
    {
        public ApiTemplate()
        {
        }

        public ApiTemplate(DbContextOptions<ApiTemplate> options)
            : base(options)
        {
        }

        public virtual DbSet<XCoreHello> XCoreHello { get; set; }
    }
}
