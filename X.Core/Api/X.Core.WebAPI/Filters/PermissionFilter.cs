using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.WebAPI.Filters
{
    public class PermissionFilter : ActionFilterAttribute
    {
        private ILogger _logger = null;
        private IConfiguration _configuration;
        public PermissionFilter(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<PermissionFilter>();
            _configuration = configuration;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if ((context.RouteData.Values["controller"]?.Equals("PermissionChecker") == true
                || context.RouteData.Values["controller"]?.Equals("PermissionCheck") == true)
                && context.RouteData.Values["action"]?.Equals("Check") == true)
            {
                await base.OnActionExecutionAsync(context, next);
            }
            else
            {
                IPermissionChecker checker = context.HttpContext.RequestServices.GetService(typeof(IPermissionChecker)) as IPermissionChecker;

                var operationType = "View";
                switch (context.HttpContext.Request.Method)
                {
                    case "POST":
                    case "PUT":
                    case "PATCH":
                        operationType = "Edit";
                        break;
                    case "DELETE":
                        operationType = "Delete";
                        break;
                }

                var operation = $"{_configuration["Application:Code"]}.{context.RouteData.Values["controller"]}.{context.RouteData.Values["action"]}";

                var isAllowed = await checker.IsAllowed(new PermissionCheckRequest()
                {
                    OperationType = operationType,
                    Permission = operation,
                });
                if (!isAllowed)
                {
                    IActionContext actionContext = context.HttpContext.RequestServices.GetService(typeof(IActionContext)) as IActionContext;

                    var isAuthorized = actionContext.Data.ContainsKey("UserId");
                    if (!isAuthorized)
                    {
                        _logger.Log(LogLevel.Warning, $"User not authorized");
                        context.Result = new UnauthorizedObjectResult(context.ModelState);
                    }
                    else
                    {
                        _logger.Log(LogLevel.Warning, $"Permission: {operation} with type {operationType} is not allowed!");
                        context.Result = new ObjectResult(context.ModelState) { StatusCode = 403 };
                    }
                }
                else
                {
                    //context.ActionDescriptor.
                    await base.OnActionExecutionAsync(context, next);
                }
            }

        }
    }
}
