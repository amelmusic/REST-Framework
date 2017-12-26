using A.Core.Interface;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using AutoMapper;
//using System.Web.Http.Cors;
using Owin;
using A.Core;

namespace A.Core.PermissionModule.WebAPI.Core
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
            

            Container.AddNewExtension<Interception>();
        }
        private static List<IServicesRegistration> servicesRegistrationList = new List<IServicesRegistration>();
        private static List<Profile> profileList = new List<Profile>();

        public static void LoadAllAssembliesFromBin()
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
                    Assembly loadedAssembly = Assembly.LoadFrom(dll);
                    var asm = AppDomain.CurrentDomain.Load(loadedAssembly.FullName);
                    foreach (var type in asm.GetTypes())
                    {
                        if (type.GetInterfaces().Contains(typeof(IServicesRegistration)))
                        {
                            var registration = Activator.CreateInstance(type) as IServicesRegistration;
                            servicesRegistrationList.Add(registration);

                        }
                        if (!type.IsAbstract && type.GetInterfaces().Contains(typeof(IProfileConfiguration)) && type.GetConstructor(Type.EmptyTypes) != null)
                        {
                            var profile = Activator.CreateInstance(type) as Profile;
                            profileList.Add(profile);
                        }
                    }
                }
                catch (FileLoadException)
                { } // The Assembly has already been loaded.
                catch (BadImageFormatException)
                { } // If a BadImageFormatException exception is thrown, the file is not an assembly.
                catch (FileNotFoundException)
                { }

            }
            var container = Container;
            // foreach dll
            servicesRegistrationList.OrderBy(x => x.Priority).ToList()
            .ForEach(x =>
            {
                x.Register(ref container);
            });
            GlobalMapper.Init(profileList);
        }
        
        //public static void LoadAllBinDirectoryAssemblies()
        //{
        //    //NOTE: This should be loaded by reflection. For now use dummy
        //    ServicesRegistration registration = new ServicesRegistration();
        //    registration.Register(Container);
        //}

        public static void Register(HttpConfiguration config)
        {
            //config.EnableCors();


            
            var formatters = GlobalConfiguration.Configuration.Formatters;
            
            formatters.Remove(formatters.XmlFormatter);

            log4net.Config.XmlConfigurator.Configure();   
            config.DependencyResolver = Resolver;

            config.Filters.Add(new ActionContextAttribute());
            config.Filters.Add(new LogFilterAttribute());
            config.Filters.Add(new CommonExceptionFilterAttribute());
            //config.Filters.Add(new AuthorizeAttribute());

            LoadAllAssembliesFromBin();

            //We don't need missing data ond client and we should set camel case style. JS fans. :)
            config.Formatters.JsonFormatter.SerializerSettings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, ContractResolver = new CamelCasePropertyNamesContractResolver() };

            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.None;
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            Container.RegisterType<A.Core.Interface.IActionContext, A.Core.ActionContext>(new HierarchicalLifetimeManager());
            Container.RegisterInstance<IUnityContainer>(Container);
        }
    }
}