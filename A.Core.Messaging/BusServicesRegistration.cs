using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interface;
using Autofac;
using Autofac.Extras.DynamicProxy;
using EasyNetQ;

namespace A.Core.Messaging
{
    public class BusServicesRegistration : IServicesRegistration
    {
        public void Register(ref ContainerBuilder container)
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["RabbitMQ"].ConnectionString;

            container.Register(c => RabbitHutch.CreateBus(connectionString))
                .As<EasyNetQ.IBus>().SingleInstance();


            container.RegisterType<Bus>()
                .As<IBus>()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new A.Core.Interceptors.ForceVirtualMethodsHook() })
                .InterceptedBy(typeof(A.Core.Interceptors.LogInterceptorProxy))
                .InterceptedBy(typeof(A.Core.Interceptors.CacheInterceptorProxy))
                .InterceptedBy(typeof(A.Core.Interceptors.TransactionInterceptorProxy));
        }

        public int Priority { get; set; } = 0;
    }
}
