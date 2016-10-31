using A.Core.Interface;

using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Microsoft.AspNet.Identity.Owin;

namespace A.Core.WebAPI.Core
{
    public class ActionContextAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var container = actionContext.Request.GetDependencyScope();
            if(container == null)
            {
                throw new ApplicationException("Container must be initialized!");
            }
            UnityResolver resolver = container as UnityResolver;
            if(resolver == null)
            {
                throw new ApplicationException("Resolver must be initialized!");
            }
            var coreActionContext = container.GetService(typeof(IActionContext)) as IActionContext;
            coreActionContext.CurrentContainer = resolver.Container;
            coreActionContext.Data.Add("UserId", "8E023B32-C436-495D-B81E-2E942CFA1F18");
            var requestId = Guid.NewGuid().ToString();
            var userName = "amel";
            log4net.LogicalThreadContext.Properties["RequestId"] = requestId;
            log4net.LogicalThreadContext.Properties["UserName"] = userName; //get this from OAuth token
            actionContext.Request.Properties.Add(new KeyValuePair<string, object>("RequestId", requestId));
            actionContext.Request.Properties.Add(new KeyValuePair<string, object>("UserName", userName));
            //coreActionContext.CurrentContainer = container.
            base.OnActionExecuting(actionContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            
            
        }

    }
}