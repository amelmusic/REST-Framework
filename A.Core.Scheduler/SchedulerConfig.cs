using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using A.Core.Interface;
using A.Core.Scheduler.Jobs;
using A.Core.Scheduler.Services;
using A.Core.Scheduler.TestServices;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Autofac.Extras.Quartz;
using AutoMapper;
using AutoMapper.Configuration;
using A.Core;

namespace A.Core.Scheduler
{
    public static class SchedulerConfig
    {
        public static ContainerBuilder ContainerBuilder { get; set; }
        public static IContainer Container { get; set; }

        static SchedulerConfig()
        {
            ContainerBuilder = new ContainerBuilder();
            //ContainerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
        }
        private static List<IServicesRegistration> servicesRegistrationList = new List<IServicesRegistration>();
        private static List<Profile> profileList = new List<Profile>();

        public static void LoadAllAssembliesFromBin()
        {
            string binPath = System.AppDomain.CurrentDomain.BaseDirectory; // note: don't use CurrentEntryAssembly or anything like that.
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
            var container = ContainerBuilder;
            // foreach dll
            servicesRegistrationList.OrderBy(x => x.Priority).ToList()
            .ForEach(x =>
            {
                x.Register(ref container);
            });
            GlobalMapper.Init(profileList);
        }

        public static void Configure()
        {
            //NOTE: CHANGE WINDOWS SERVICE LANG HERE OR LOAD IT FROM CONFIG...
            var lang = "bs"; //Settings.Default.DefaultCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

            log4net.Config.XmlConfigurator.Configure();
            ConfigureContainer();
            Container = ContainerBuilder.Build();
        }

        private static void ConfigureContainer()
        {
            LoadAllAssembliesFromBin();
            ContainerBuilder.RegisterType<A.Core.ActionContext>()
                .As<A.Core.Interface.IActionContext>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .EnableClassInterceptors();

            ContainerBuilder.RegisterModule(new QuartzAutofacFactoryModule());
            ContainerBuilder.RegisterModule(new QuartzAutofacJobsModule(typeof(HeartbeatJob).Assembly));

            // register Service instance
            ContainerBuilder.RegisterType<ServiceCore>().AsSelf();
            ContainerBuilder.RegisterType<AuthenticatorService>().AsSelf().SingleInstance();
            // register dependencies
            //cb.RegisterType<HeartbeatService>().As<IHeartbeatService>();

            ContainerBuilder.RegisterType<HeartbeatService>()
                .As<IHeartbeatService>()
                .InstancePerLifetimeScope()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
