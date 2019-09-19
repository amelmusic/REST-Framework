using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using X.Core.Interface;
using X.Core.Validation;

namespace X.Core.WebAPI.Filters
{
    public class ActionContextFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var coreActionContext = actionContext.HttpContext.RequestServices.GetService(typeof(IActionContext)) as IActionContext;
            

            var requestId = Guid.NewGuid().ToString();
            PopulateFromClaimsPrincipal(actionContext, coreActionContext);
            if (actionContext.HttpContext.Request.Headers.ContainsKey("RequestId"))
            {
                requestId = actionContext.HttpContext.Request.Headers["RequestId"];
                log4net.LogicalThreadContext.Properties["RequestId"] = requestId;
            }
            else
            {
                log4net.LogicalThreadContext.Properties["RequestId"] = requestId;
            }

            actionContext.HttpContext.Request.Headers.Add("RequestId", requestId);

            //coreActionContext.CurrentContainer = container.
            base.OnActionExecuting(actionContext);
        }

        private void PopulateFromClaimsPrincipal(ActionExecutingContext actionContext, IActionContext coreActionContext)
        {

            List<string> roleList = new List<string>();
            var userFromClaim = actionContext.HttpContext.User;

            actionContext.HttpContext.Request.Headers.TryGetValue("Authorization", out var authorizationHeader);
            var languageHeader = actionContext.HttpContext.Request.Headers["Accept-Language"];

            String firstLanguage = languageHeader.FirstOrDefault();
            coreActionContext.Data["AuthorizationToken"] = authorizationHeader;
            coreActionContext.Data["Language"] = firstLanguage;

            if (userFromClaim != null)
            {
                var idClaimName = userFromClaim.Identity.Name;
                var idClaim = userFromClaim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == "sub");
                if (idClaim != null)
                {
                    coreActionContext.Data["UserName"] = idClaim.Value.ToString();
                    log4net.LogicalThreadContext.Properties["UserName"] = idClaim.Value.ToString();
                } 
                else
                {
#if DEBUG
                    coreActionContext.Data.Add("UserName", "8E023B32-C436-495D-B81E-2E942CFA1F18"); //NOTE: this is for test only...
                    log4net.LogicalThreadContext.Properties["UserName"] = "-1"; //get this from OAuth token
#endif
                }
                var roleClaims = userFromClaim.Claims.Where(x => x.Type == ClaimTypes.Role || x.Type == "role");
                foreach (var claim in roleClaims)
                {
                    roleList.Add(claim.Value);
                }
                coreActionContext.Data.Add("RoleList", roleList.ToArray());


                coreActionContext.Data.Add("SYS_CLAIMS", userFromClaim.Claims.ToArray());
            }

        }
    }
}
