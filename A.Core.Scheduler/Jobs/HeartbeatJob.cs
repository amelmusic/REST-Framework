using System;
using System.Threading;
using System.Threading.Tasks;
using A.Core.Interface;
using A.Core.Scheduler.Services;
using A.Core.Scheduler.TestServices;
using Quartz;

namespace A.Core.Scheduler.Jobs
{
    public class HeartbeatJob : BaseJob
    {
        private readonly IHeartbeatService _hearbeat;

        public HeartbeatJob(IHeartbeatService hearbeat, IActionContext actionContext, AuthenticatorService authenticatorService) 
            : base(actionContext, authenticatorService)
        {
            if (hearbeat == null) throw new ArgumentNullException(nameof(hearbeat));
            _hearbeat = hearbeat;
        }

        //async Task IJob.Execute(IJobExecutionContext context)
        //{
        //    _hearbeat.UpdateState("alive");
        //}


        public override async Task ExecuteInternal(IJobExecutionContext context)
        {
            var culture = Thread.CurrentThread.CurrentUICulture;
            _hearbeat.UpdateState($"Service alive, RequestId: {ActionContext.Data["RequestId"]}, culture: {culture.TwoLetterISOLanguageName}");
        }
    }
}
