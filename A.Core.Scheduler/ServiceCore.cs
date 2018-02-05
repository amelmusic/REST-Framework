using System;
using System.Threading.Tasks;
using A.Core.Scheduler.Services;
using log4net;
using Quartz;

namespace A.Core.Scheduler
{
    public class ServiceCore
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(ServiceCore));

        private readonly IScheduler _scheduler;
        private readonly AuthenticatorService _authenticatorService;

        public ServiceCore(IScheduler scheduler, AuthenticatorService authenticatorService)
        {
            if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

            _scheduler = scheduler;
            _authenticatorService = authenticatorService;
        }

        public async Task<bool> Start()
        {
            s_log.Info("Service started");

            if (!_scheduler.IsStarted)
            {
                await _authenticatorService.Init();
                s_log.Info("Starting Scheduler");
                await _scheduler.Start();
            }
            
            return true;
        }

        public async Task<bool> Stop()
        {
            s_log.Info("Stopping Scheduler...");
            await _scheduler.Shutdown(true);

            s_log.Info("Service stopped");
            return true;
        }
    }
}
