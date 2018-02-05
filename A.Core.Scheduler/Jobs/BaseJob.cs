using System;
using System.Threading;
using System.Threading.Tasks;
using A.Core.Interface;
using A.Core.Scheduler.Services;
using log4net;
using Quartz;

namespace A.Core.Scheduler.Jobs
{
    //NOTE: BY DEFAULT, CONCURRENT EXECUTION IS DISABLED!!!
    [DisallowConcurrentExecution]
    public abstract class BaseJob : IJob
    {
        protected static readonly ILog s_log = LogManager.GetLogger(typeof(BaseJob));
        public IActionContext ActionContext { get; set; }

        public AuthenticatorService AuthenticatorService { get; set; } //Single instance. Will handle refresh token..

        protected BaseJob(IActionContext actionContext, AuthenticatorService authenticatorService)
        {
            ActionContext = actionContext;
            AuthenticatorService = authenticatorService;
        }

        public virtual async Task Execute(IJobExecutionContext context)
        {
            try
            {
                await Init();
                await ExecuteInternal(context);
            }
            catch (Exception ex)
            {
                s_log.Error(ex);
                throw new JobExecutionException(ex);
            }
        }

        public abstract Task ExecuteInternal(IJobExecutionContext context);

        protected virtual async Task Init()
        {
            await InitAuth();
            this.ActionContext.Data["RequestId"] = Guid.NewGuid().ToString();
            this.ActionContext.Data["Language"] = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        }

        protected virtual async Task InitAuth()
        {
            await AuthenticatorService.PopulateActionContextWithUserInfo(this.ActionContext);
        }
    }
}
