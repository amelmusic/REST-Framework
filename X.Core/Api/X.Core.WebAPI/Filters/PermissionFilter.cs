using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace X.Core.WebAPI.Filters
{
    public class PermissionFilter : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //context.ActionDescriptor.
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
