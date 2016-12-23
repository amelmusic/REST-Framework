using A.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Foundatio.Caching;
using System.Diagnostics;

namespace A.Core
{
    public class ServicesRegistration : IServicesRegistration
    {
        public ServicesRegistration()
        {
            Priority = -1; //dummy initialization of core components
        }

        public int Priority { get; set; }

        public void Register(ref UnityContainer container)
        {
            //Debugger.Launch();
            container.RegisterType<IPermissionChecker, DummyPermissionChecker>(new HierarchicalLifetimeManager());
            InMemoryCacheClient client = new InMemoryCacheClient()
            {
                MaxItems = 250
            };
            container.RegisterInstance<ICacheClient>(client);
        }
    }
}
