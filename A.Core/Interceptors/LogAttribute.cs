using log4net;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
     [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true,
 AllowMultiple = false)]
    public class LogAttribute : HandlerAttribute
    {
         static LogBehavior Behaviour = new LogBehavior();
        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return Behaviour;
        }
    }

    public class LogBehavior : ICallHandler
    {
        Dictionary<string, ILog> mLoggerList = new Dictionary<string, ILog>();
        static JsonSerializerSettings JsonSerializerSettings { get; set; }

        static LogBehavior()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }
        
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            ILog log = null;
            string loggerName = input.Target.GetType().FullName;
            if(mLoggerList.ContainsKey(loggerName))
            {
                log = mLoggerList[loggerName];
            }
            else
            {
                log = log4net.LogManager.GetLogger(loggerName);
                mLoggerList.Add(loggerName, log);
            }

            LogMethodParameters(input, log, loggerName);

            var result = getNext()(input, getNext);
            
            if(result.Exception != null)
            {
                LogMethodParameters(input, log, loggerName, true);
                log.Error(result.Exception);
            }
            return result;
        }

        private static void LogMethodParameters(IMethodInvocation input, ILog log, string loggerName, bool isException = false)
        {
            if(!isException)
            {
                if (log.IsDebugEnabled)
                {
                    string output = JsonConvert.SerializeObject(input.Arguments, JsonSerializerSettings);
                    log4net.LogicalThreadContext.Properties["Request"] = output;
                    log.Debug(string.Format("Entered: {0}_{1}", loggerName, input.MethodBase.Name));
                }
                
            }
            else
            {
                string output = JsonConvert.SerializeObject(input.Arguments, JsonSerializerSettings);
                log4net.LogicalThreadContext.Properties["Request"] = output;
                log.Error(string.Format("Entered: {0}_{1} and failed!", loggerName, input.MethodBase.Name));
            }
            
        }

        public int Order { get; set; }
    }


}
