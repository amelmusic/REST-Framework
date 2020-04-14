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
using X.Core.Interceptors;
using X.Core.Interface;
using X.Core.Services.Core.StateMachine;
using Microsoft.Extensions.Logging;
using X.Core.WebAPI.Filters;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

namespace X.Core.WebAPI.Core
{
    public class FromDTO : Profile
    {
        public FromDTO()
        {
            this.AddConditionalObjectMapper();
            this.ValidateInlineMaps = false;
        }
    }

    public static class XCoreStartupExt
    {
        /// <summary>
        /// Configures autofac, core classes etc
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static void AddXCore(this IServiceCollection services, ContainerBuilder builder)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

            AddXCore(builder);
        }

        public static void AddXCore(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AssignableTo<IService>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new ForceVirtualMethodsHook() })
                .InterceptedBy(typeof(LogInterceptorProxy))
                .InterceptedBy(typeof(CacheInterceptorProxy))
                .InterceptedBy(typeof(TransactionInterceptorProxy));

            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .AsClosedTypesOf(typeof(StateMachine<,>))
                .InstancePerLifetimeScope()
                .PropertiesAutowired()
                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new ForceVirtualMethodsHook() })
                .InterceptedBy(typeof(LogInterceptorProxy))
                .InterceptedBy(typeof(CacheInterceptorProxy))
                .InterceptedBy(typeof(TransactionInterceptorProxy));

            builder.RegisterType<ActionContext>()
                .As<IActionContext>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors();

            new ServicesRegistration().Register(ref builder);
        }
    }
}
