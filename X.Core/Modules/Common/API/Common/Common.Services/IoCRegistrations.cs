using Autofac;
using Autofac.Extras.DynamicProxy;
using Common.Services.DocumentGenerators;
using Common.Services.StorageProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Services
{
    public static class IoCRegistrations
    {
        public static void Register(ref ContainerBuilder container)
        {
            container.RegisterType<HTMLGenerator>()
                .Named<IDocumentGenerator>("Common.Services.DocumentGenerators.TemplateType.1")
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new X.Core.Interceptors.ForceVirtualMethodsHook() })
                .InterceptedBy(typeof(X.Core.Interceptors.LogInterceptorProxy))
                .InterceptedBy(typeof(X.Core.Interceptors.CacheInterceptorProxy))
                .InterceptedBy(typeof(X.Core.Interceptors.TransactionInterceptorProxy));

            container.RegisterType<HTMLGenerator>()
                .Named<IDocumentGenerator>("Common.Services.DocumentGenerators.TemplateType.2")
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new X.Core.Interceptors.ForceVirtualMethodsHook() })
                .InterceptedBy(typeof(X.Core.Interceptors.LogInterceptorProxy))
                .InterceptedBy(typeof(X.Core.Interceptors.CacheInterceptorProxy))
                .InterceptedBy(typeof(X.Core.Interceptors.TransactionInterceptorProxy));


            container.RegisterType<DBStorageProvider>()
                .Named<IStorageProvider>("Common.Services.StorageProviders.DB")
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new X.Core.Interceptors.ForceVirtualMethodsHook() })
                .InterceptedBy(typeof(X.Core.Interceptors.LogInterceptorProxy))
                .InterceptedBy(typeof(X.Core.Interceptors.CacheInterceptorProxy))
                .InterceptedBy(typeof(X.Core.Interceptors.TransactionInterceptorProxy));
        }
    }
}
