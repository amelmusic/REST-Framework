using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using System.Diagnostics;
using IdentityServer3.AccessTokenValidation;


[assembly: OwinStartupAttribute(typeof(A.Core.PermissionModule.WebAPI.Startup))]
namespace A.Core.PermissionModule.WebAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            //{
            //    Authority = "http://localhost/IdentityManager.Identity/oauth"//Settings.Default.IdentityServerUrl
            //});
        }
    }
}