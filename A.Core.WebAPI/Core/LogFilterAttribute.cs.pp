using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace $rootnamespace$.Core
{
    public class LogFilterAttribute : ActionFilterAttribute
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public LogFilterAttribute()
        {
            JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            JsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            JsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        private static ILog Log = log4net.LogManager.GetLogger(typeof(LogFilterAttribute));
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            actionContext.Request.Properties.Add(new KeyValuePair<string, object>("REQUEST_START_TIME", Stopwatch.StartNew()));

            log4net.LogicalThreadContext.Properties["Controller"] = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            log4net.LogicalThreadContext.Properties["Action"] = actionContext.ControllerContext.ControllerDescriptor.ControllerName + "_" + actionContext.ActionDescriptor.ActionName;
            

            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            if (Log.IsInfoEnabled)
            {

                log4net.LogicalThreadContext.Properties["Duration"] = ((Stopwatch)actionExecutedContext.Request.Properties["REQUEST_START_TIME"]).ElapsedMilliseconds;
                string output = JsonConvert.SerializeObject(actionExecutedContext.ActionContext.ActionArguments, JsonSerializerSettings);

                log4net.LogicalThreadContext.Properties["Request"] = output;
                log4net.LogicalThreadContext.Properties["Method"] = actionExecutedContext.Request.Method.Method;
                if (actionExecutedContext.Response != null)
                {
                    log4net.LogicalThreadContext.Properties["StatusCode"] = (int)actionExecutedContext.Response.StatusCode;
                }
                else if (actionExecutedContext.Exception != null)
                {
                    log4net.LogicalThreadContext.Properties["StatusCode"] = (int)System.Net.HttpStatusCode.BadRequest;
                }

                if (actionExecutedContext.Exception == null && actionExecutedContext.ActionContext.Response.Content != null)
                {
                    //NOTE: We need this in extra secure apps
                    //string response = JsonConvert.SerializeObject((actionExecutedContext.ActionContext.Response.Content as ObjectContent).Value, JsonSerializerSettings);
                    //log4net.LogicalThreadContext.Properties["Response"] = response;
                }

                if (actionExecutedContext.Exception == null)
                {
                    Log.Info(actionExecutedContext.Request.RequestUri);
                }
                else
                {
                    Log.Error(actionExecutedContext.Request.RequestUri);
                }
                
            }
        }

    }
}