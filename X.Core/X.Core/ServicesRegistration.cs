using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Extras.DynamicProxy;
using X.Core.Interface;

namespace X.Core
{
    public class ServicesRegistration : IServicesRegistration
    {
        public ServicesRegistration()
        {
            Priority = -1; //dummy initialization of core components
        }

        public int Priority { get; set; }

        public void Register(ref ContainerBuilder container)
        {
            //Debugger.Launch();
            container.RegisterType<DummyPermissionChecker>()
                .As<IPermissionChecker>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors();

            container.RegisterType<X.Core.ActionContext>()
                .As<X.Core.Interface.IActionContext>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors();

            //InMemoryCacheClient client = new InMemoryCacheClient()
            //{
            //    MaxItems = 250
            //};
            //container.RegisterInstance<ICacheClient>(client);

            //container.Register(c => new LogInterceptorProxy()).PropertiesAutowired();
            //container.Register(c => new TransactionInterceptorProxy()).PropertiesAutowired();
            //container.Register(c => new CacheInterceptorProxy(c.Resolve<ICacheClient>(), c.Resolve<IActionContext>()));
        }
    }
}
