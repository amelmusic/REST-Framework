using Castle.DynamicProxy;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    public class LogInterceptorProxy : BaseAttributeBasedInterceptorProxy<LogAttribute>
    {
        protected static JsonSerializerSettings JsonSerializerSettings { get; set; }
        static Dictionary<MethodInfo, bool> _cachedDictAccess = new Dictionary<MethodInfo, bool>();
        protected override Dictionary<MethodInfo, bool> CachedDictAccess => _cachedDictAccess;

        static LogInterceptorProxy()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        protected override void InterceptSync(IInvocation invocation)
        {
            var logger = GetLogger(invocation);
            LogMethodParameters(invocation, logger, logger.Logger.Name, false);
            base.InterceptSync(invocation);
        }

        protected override void InterceptAsync(IInvocation invocation, object context = null)
        {
            var logger = GetLogger(invocation);
            LogMethodParameters(invocation, logger, logger.Logger.Name, false);
            base.InterceptAsync(invocation);
        }

        //NOTE: This isn't needed anymore
        //protected override async Task InterceptInternalAsyncAfter(Task task, IInvocation invocation, object context = null)
        //{
        //    await task.ConfigureAwait(false);
        //}

        //protected override async Task<T> InterceptInternalAsyncAfter<T>(Task<T> task, IInvocation invocation, object context = null)
        //{
        //    var result = await base.InterceptInternalAsyncAfter(task, invocation);
            
        //    return result;
        //}

        protected override bool OnException(IInvocation invocation, Exception ex)
        {
            var logger = GetLogger(invocation);
            LogMethodParameters(invocation, logger, logger.Logger.Name, true, ex);
            
            return base.OnException(invocation, ex);
        }

        protected virtual void LogMethodParameters(IInvocation input, ILog log, string loggerName, bool isException = false, Exception ex = null)
        {
            if (!isException)
            {
                if (log.IsDebugEnabled)
                {
                    string output = JsonConvert.SerializeObject(input.Arguments, JsonSerializerSettings);
                    log4net.LogicalThreadContext.Properties["Request"] = output;
                    log.Debug(string.Format("Entered: {0}({1})", input.Method.Name, output));
                }

            }
            else
            {
                string output = JsonConvert.SerializeObject(input.Arguments, JsonSerializerSettings);
                log4net.LogicalThreadContext.Properties["Request"] = output;
                log.Error(string.Format("Entered: {0}({1}) and FAILED!", input.Method.Name, output), ex);
            }

        }
    }
}
