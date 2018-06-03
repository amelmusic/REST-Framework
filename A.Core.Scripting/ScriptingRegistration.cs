using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interface;
using A.Core.Scripting;
using Autofac;
using Autofac.Extras.DynamicProxy;


namespace Core360.Accounting.Services.Scripting
{
    public class ScriptingRegistration : IServicesRegistration
    {
        public void Register(ref ContainerBuilder container)
        {
            container.RegisterType<ScriptingAPI>()
                .As<IScriptingAPI>()
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
