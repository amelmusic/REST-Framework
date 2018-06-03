using System;
using System.IO;
using log4net;
using Topshelf;
using Topshelf.Autofac;

namespace A.Core.Scheduler
{
    public class Scheduler
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(Scheduler));

        public static void Run()
        {
            Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
            SchedulerConfig.Configure();

            s_log.Info("Starting...");
            try
            {
                var rc = HostFactory.Run(conf =>
                {
                    conf.SetServiceName("A.CORE.SCHEDULER"); //CHANGE THIS FOR YOUR SCENARIO
                    conf.SetDisplayName("A.CORE.SCHEDULER"); //CHANGE THIS FOR YOUR SCENARIO
                    conf.UseLog4Net();
                    conf.UseAutofacContainer(SchedulerConfig.Container);

                    conf.Service<ServiceCore>(svc =>
                    {
                        svc.ConstructUsingAutofacContainer();

                        svc.WhenStarted(async o => await o.Start());
                        svc.WhenStopped(async o =>
                        {
                            await o.Stop();
                            SchedulerConfig.Container.Dispose();
                        });

                    });
                    conf.RunAsLocalSystem();
                });

                var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());  //11


                Environment.Exit(exitCode);
                //return 0;
            }
            catch (Exception ex)
            {
                s_log.Fatal("Unhandled exception", ex);
                log4net.LogManager.Shutdown();
                //return 1;
            }
        }
    }
}
