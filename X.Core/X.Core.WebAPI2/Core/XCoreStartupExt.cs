using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using X.Core.Interface;


namespace X.Core.WebAPI.Core
{
    public class FromDTO : Profile
    {
        public FromDTO()
        {
            this.AddConditionalObjectMapper();
        }
    }

    public static class XCoreStartupExt
    {
        /// <summary>
        /// Configures autofac, core classes etc
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddXCore(this IServiceCollection services, ref IContainer applicationContainer)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var builder = new ContainerBuilder();

            // Register dependencies, populate the services from
            // the collection, and build the container.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection. Mix and match as needed.
            builder.Populate(services);


            //UserService s;
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired();

            builder.RegisterType<ActionContext>()
                .As<IActionContext>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors();

            applicationContainer = builder.Build();
            return services;
        }
    }
}
