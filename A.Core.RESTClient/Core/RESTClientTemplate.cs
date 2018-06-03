









#region A_Core generated code

using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using A.Core.Extensions;
using A.Core.Interface;
using Flurl.Http;
using A.Core.RESTClient.Core;
using A.Core.Model;
using Autofac;
using System.Reflection;
namespace A.Core.RESTClient 
{ 
}

namespace A.Core.RESTClient.RESTClient
{
public class RESTClientRegistration : IServicesRegistration
    {
        public void Register(ref ContainerBuilder container)
        {
            var assembly = Assembly.GetExecutingAssembly();

            container.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("RESTClient"))
                .AsImplementedInterfaces().PropertiesAutowired();
        }

        public int Priority { get; set; } = 0;
    }
}

#endregion
