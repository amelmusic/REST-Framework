using A.Core.Interface;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace A.Core.WebAPI.Core
{
    public class PermissionAttribute : ActionFilterAttribute
    {
        public string Permission { get; set; }
        public string OperationType { get; set; }

        public PermissionAttribute(string permission)
        {
            Permission = permission;
        }

        public PermissionAttribute(string permission, string operationType)
        {
            Permission = permission;
            OperationType = operationType;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if(!string.IsNullOrWhiteSpace(Permission))
            {
                var container = actionContext.Request.GetDependencyScope();
                if (container == null)
                {
                    throw new ApplicationException("Container must be initialized!");
                }

                var permissionChecker = container.GetService(typeof(IPermissionChecker)) as IPermissionChecker;

                bool isAllowed = permissionChecker.IsAllowed(Permission);
                if(!isAllowed)
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, "Action not allowed");
                }
                else
                {
                    base.OnActionExecuting(actionContext);
                }
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }

    }
}