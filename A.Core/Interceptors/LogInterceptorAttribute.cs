using log4net;
using Newtonsoft.Json;
using PostSharp.Aspects;
using PostSharp.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    /// <summary>
    ///     Aspect that, when applied to a method, appends a record to the <see cref="Logger" /> class whenever this method is
    ///     executed.
    /// </summary>
    [Serializable]
    public sealed class LogInterceptorAttribute : OnMethodBoundaryAspect
    {
        [NonSerialized]
        static JsonSerializerSettings jsonSerializerSettings;
        [NonSerialized]
        static ILog log = null;
        [NonSerialized]
        static string loggerName = null;

        public override void CompileTimeInitialize(System.Reflection.MethodBase method, AspectInfo aspectInfo)
        {
            base.CompileTimeInitialize(method, aspectInfo);
        }

        public override void RuntimeInitialize(System.Reflection.MethodBase method)
        {
            // Debugger.Launch();
            base.RuntimeInitialize(method);
            jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;


            loggerName = method.DeclaringType.FullName;
            log = log4net.LogManager.GetLogger(method.DeclaringType.FullName);

        }
        /// <summary>
        ///     Method invoked before the target method is executed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (log.IsDebugEnabled)
            {
                string output = JsonConvert.SerializeObject(args.Arguments, jsonSerializerSettings);
                log4net.LogicalThreadContext.Properties["Request"] = output;
                log.Debug(string.Format("Entered: {0}_{1}", loggerName, args.Method.Name));
            }
        }

        /// <summary>
        ///     Method invoked when the target method has failed.
        /// </summary>
        /// <param name="args">Method execution context.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            string output = JsonConvert.SerializeObject(args.Arguments, jsonSerializerSettings);
            log4net.LogicalThreadContext.Properties["Request"] = output;
            log.Error(string.Format("Entered: {0}_{1} and failed!", loggerName, args.Method.Name), args.Exception);

        }

    }
}
