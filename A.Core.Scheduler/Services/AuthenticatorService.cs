using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interface;
using log4net;
using Newtonsoft.Json.Linq;

/**************************************************************************************
//NOTE: THIS FILE SERVES AS AN EXAMPLE FOR AUTHENTICATING AN POPULATING IACTIONCONTEXT OBJECT.
//IF YOU HAVE OAUTH2 AUTH SERVER, SIMPLY UNCOMMENT THIS CODE
***************************************************************************************/
namespace A.Core.Scheduler.Services
{
    public class AuthenticatorService
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(AuthenticatorService));

        //public TokenResponse TokenResponse { get; set; }

        public virtual async Task Init()
        {
            //TokenResponse = await RequestToken();
        }

        public virtual async Task PopulateActionContextWithUserInfo(IActionContext context)
        {
            //if (TokenResponse == null)
            //{
            //    var token = await RequestToken();
            //    TokenResponse = token;
            //    if (token.IsError)
            //    {
            //        throw new UnauthorizedAccessException("Either user is disabled, or its credentials are no longer valid!");
            //    }
            //}
            
            //context.Data["AuthorizationToken"] = TokenResponse.AccessToken;

            //if (TokenResponse.AccessToken.Contains("."))
            //{
            //    var parts = TokenResponse.AccessToken.Split('.');
            //    var header = parts[0];
            //    var claims = parts[1];

            //    JObject claimsObject = JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(claims)));

            //    s_log.Debug(JObject.Parse(Encoding.UTF8.GetString(Base64Url.Decode(header))));
            //    s_log.Debug(claimsObject);

            //    var claimsParsed = claimsObject.ToClaims();

            //    var idClaim = claimsParsed.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == "sub");

            //    context.Data["UserId"] = idClaim.Value;

            //    List<string> roleList = new List<string>();
            //    var roleClaims = claimsParsed.Where(x => x.Type == ClaimTypes.Role || x.Type == "role");
            //    foreach (var claim in roleClaims)
            //    {
            //        roleList.Add(claim.Value);
            //    }
            //    context.Data.Add("RoleList", roleList.ToArray());
            //}
        }

        //protected virtual async Task<TokenResponse> RequestToken()
        //{
        //    var client = new TokenClient(
        //        $"{Settings.Default.IdentityServerBaseAddress}/connect/token",
        //        Properties.Settings.Default.ServiceName,
        //        Properties.Settings.Default.ServiceSecretKey);

        //    return await client.RequestResourceOwnerPasswordAsync(Properties.Settings.Default.ServiceUsername, Properties.Settings.Default.ServicePassword, Properties.Settings.Default.ServiceScopes); //read write 
        //    //return await client.RequestClientCredentialsAsync("read write roles");
        //}

    }
}
