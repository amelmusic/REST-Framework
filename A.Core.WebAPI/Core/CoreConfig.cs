using A.Core.Interface;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace A.Core.WebAPI.Core
{
    /// <summary>
    /// Handles Core configuration. NOTE: Call this from GLOBAL.asax
    /// </summary>
    public class CoreConfig
    {
        public static UnityContainer Container { get; set; }
        public static UnityResolver Resolver { get; set; }

        static CoreConfig()
        {
            Container = new UnityContainer();
            Resolver = new UnityResolver(Container);
            //NOTE: We should add unity interception extension here if needed
        }
        private static List<IServicesRegistration> servicesRegistrationList = new List<IServicesRegistration>();
        public static void LoadAllBinDirectoryAssemblies()
        {
            string binPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin"); // note: don't use CurrentEntryAssembly or anything like that.
            var thisAssemblyName = Assembly.GetExecutingAssembly().ManifestModule.ToString();
            foreach (string dll in Directory.GetFiles(binPath, "*.dll", SearchOption.AllDirectories))
            {
                try
                {
                    if (dll.EndsWith(thisAssemblyName))
                    {
                        continue;
                    }
                    //if (dll.Contains(".Services") || dll.Contains(".Adapter"))
                    {
                        //AppDomain.CurrentDomain.GetAssemblies()
                        Assembly loadedAssembly = Assembly.LoadFrom(dll);
                        var asm = AppDomain.CurrentDomain.Load(loadedAssembly.FullName);
                        foreach (var type in asm.GetTypes())
                        {
                            if (type.GetInterfaces().Contains(typeof(IServicesRegistration)))
                            {
                                var registration = Activator.CreateInstance(type) as IServicesRegistration;
                                servicesRegistrationList.Add(registration);

                            }
                        }
                    }

                }
                catch (FileLoadException loadEx)
                { } // The Assembly has already been loaded.
                catch (BadImageFormatException imgEx)
                { } // If a BadImageFormatException exception is thrown, the file is not an assembly.
                catch (FileNotFoundException imgEx)
                { }

            } // foreach dll
            servicesRegistrationList.OrderBy(x => x.Priority).ToList()
            .ForEach(x =>
            {
                x.Register(Container);
            });
        }
        
        //public static void LoadAllBinDirectoryAssemblies()
        //{
        //    //NOTE: This should be loaded by reflection. For now use dummy
        //    ServicesRegistration registration = new ServicesRegistration();
        //    registration.Register(Container);
        //}

        public static void Register(HttpConfiguration config)
        {
            
            log4net.Config.XmlConfigurator.Configure();   
            config.DependencyResolver = Resolver;
            
            config.Filters.Add(new LogFilterAttribute());
            config.Filters.Add(new CommonExceptionFilterAttribute());

            LoadAllBinDirectoryAssemblies();

            //We don't need missing data ond client and we should set camel case style. JS fans. :)
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() };

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
        }
    }
}