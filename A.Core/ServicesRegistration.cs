using A.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Foundatio.Caching;
using System.Diagnostics;
using Autofac;
using Autofac.Extras.DynamicProxy;
using A.Core.Interceptors;

namespace A.Core
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

            container.RegisterType<A.Core.ActionContext>()
                    .As<A.Core.Interface.IActionContext>()
                    .InstancePerLifetimeScope()
                    .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                    .EnableClassInterceptors();

            InMemoryCacheClient client = new InMemoryCacheClient()
            {
                MaxItems = 250
            };
            container.RegisterInstance<ICacheClient>(client);

            container.Register(c => new LogInterceptorProxy()).PropertiesAutowired();
            container.Register(c => new TransactionInterceptorProxy()).PropertiesAutowired();
            container.Register(c => new CacheInterceptorProxy(c.Resolve<ICacheClient>(), c.Resolve<IActionContext>()));
            
        }
    }
}
