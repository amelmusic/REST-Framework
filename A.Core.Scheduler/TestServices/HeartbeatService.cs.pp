using System;
using System.Diagnostics;
using System.Threading;
using log4net;

namespace $rootnamespace$.TestServices
{
    public class HeartbeatService : IHeartbeatService
    {
        public string Id { get; set; }
        private static readonly ILog s_log = LogManager.GetLogger(typeof(HeartbeatService));

        public HeartbeatService()
        {
            Id = Guid.NewGuid().ToString();
        }

        public void UpdateState(string state)
        {
            s_log.Info($"Service in process: {Process.GetCurrentProcess().ProcessName}, state: {state}, temp Id: {Id}");
            Thread.Sleep(5000);
            s_log.Info($"Service in process: {Process.GetCurrentProcess().ProcessName}, state: {state}, temp Id: {Id} ENDED");
        }
    }

    public interface IHeartbeatService
    {
        void UpdateState(string state);
    }
}
