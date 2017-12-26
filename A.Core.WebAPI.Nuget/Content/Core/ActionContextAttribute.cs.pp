using A.Core.Interface;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http.Filters;


namespace $rootnamespace$.Core //REPLACED
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

            ILifetimeScope resolver = (container as Autofac.Integration.WebApi.AutofacWebApiDependencyScope).LifetimeScope as ILifetimeScope;

            var coreActionContext = container.GetService(typeof(IActionContext)) as IActionContext;
            coreActionContext.CurrentContainer = resolver;
            
            var requestId = Guid.NewGuid().ToString();
            PopulateFromClaimsPrincipal(actionContext, coreActionContext);
            log4net.LogicalThreadContext.Properties["RequestId"] = requestId;

            actionContext.Request.Properties.Add(new KeyValuePair<string, object>("RequestId", requestId));
            
            //coreActionContext.CurrentContainer = container.
            base.OnActionExecuting(actionContext);
        }

        private void PopulateFromClaimsPrincipal(System.Web.Http.Controllers.HttpActionContext actionContext, IActionContext coreActionContext)
        {
            
            List<string> roleList = new List<string>();
            var userFromClaim = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (userFromClaim != null)
            {
                var idClaimName = userFromClaim.Identity.Name;
                var idClaim = userFromClaim.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == "sub");
                if (idClaim != null)
                {
                    coreActionContext.Data["UserId"] = idClaim.Value.ToString();
                    log4net.LogicalThreadContext.Properties["UserName"] = idClaim.Value.ToString();  //get this from OAuth token
                    actionContext.Request.Properties.Add(new KeyValuePair<string, object>("UserName", idClaim.Value.ToString()));
                }
                else
                {
#if DEBUG
                    coreActionContext.Data.Add("UserId", "8E023B32-C436-495D-B81E-2E942CFA1F18"); //NOTE: this is for test only...
                    log4net.LogicalThreadContext.Properties["UserName"] = "-1"; //get this from OAuth token
#endif
                }
                var roleClaims = userFromClaim.Claims.Where(x => x.Type == ClaimTypes.Role || x.Type == "role");
                foreach (var claim in roleClaims)
                {
                    roleList.Add(claim.Value);
                }
                coreActionContext.Data.Add("RoleList", roleList.ToArray());
            }

        }

    }
}
