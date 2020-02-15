using Autofac;
using Autofac.Extras.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PermissionModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.Core.Interface;

namespace PermissionModule.API
{
    public static class XCorePermissionModuleExt
    {
        public static void AddPermissionModule(this IServiceCollection services, IConfiguration configuration, ContainerBuilder builder)
        {
            var connection = configuration.GetConnectionString("PermissionModule");
            if (string.IsNullOrWhiteSpace(connection))
            {
                throw new ApplicationException("Connection string PermissionModule not found in config!");
            }

            services.AddDbContext<Services.Database.PermissionModuleContext>(options => options.UseSqlServer(connection));

            builder.RegisterType<PermissionChecker>()
               .As<IPermissionChecker>()
               .InstancePerLifetimeScope()
               .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
               .EnableClassInterceptors();
        }
    }
}
