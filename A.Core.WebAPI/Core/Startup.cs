using Owin;
using Microsoft.Owin;

//using IdentityServer3.AccessTokenValidation;

[assembly: OwinStartupAttribute(typeof(A.Core.WebAPI.StartupCore))]
namespace A.Core.WebAPI
{
    public partial class StartupCore
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //NOTE: Uncomment this for enabling IdentityServer etc
            //app.UseIdentityServerBearerTokenAuthentication(new IdentityServerBearerTokenAuthenticationOptions
            //{
            //    Authority = Settings.Default.IdentityServerBaseAddress,
            //    RequiredScopes = new[] { "openid", "roles", "profile", "email", "read", "write" }
            //});
        }
    }
}