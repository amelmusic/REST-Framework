









#region A_Core generated code

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace A.Core.PermissionModule.WebAPI.Controllers
{
    public partial class EndpointModel
	{
		public EndpointModel()
		{
			Links = new Dictionary<string, string>();
		}
		public Dictionary<string, string> Links {get; set;}
	}
	[RoutePrefix("")]
    public class endpointsController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            EndpointModel model = new EndpointModel();
            
            var apiDescriptions = Configuration.Services.GetApiExplorer().ApiDescriptions;
            foreach (var description in apiDescriptions.ToList())
            {
                var route = Url.Request.RequestUri.AbsoluteUri;
                var path = route + description.RelativePath.Split('?')[0];
                var controller = description.ActionDescriptor.ControllerDescriptor.ControllerName;
                string key = string.Format("{1}", "", description.RelativePath.Split('?')[0].Replace('/', '_').Replace("{id}","id"), description.HttpMethod.Method.ToLower());

                    if(key == controller + "_get")
                    {
                        key = controller;
                    }
                    if (!model.Links.ContainsKey(key))
                    {
                        model.Links[key] = path;
                    }
            }

            
            var resp = Request.CreateResponse<EndpointModel>(HttpStatusCode.OK, model);
           
            return resp;
        }
    }
}


namespace A.Core.PermissionModule.WebAPI.Controllers 
{ 
[RoutePrefix("permissionChecker")]
public partial class permissionCheckerController : System.Web.Http.ApiController
{
	[Microsoft.Practices.Unity.Dependency]
	public A.Core.PermissionModule.Interfaces.IPermissionChecker Service { get; set; }
	
	
				[Route("isAllowed")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.PermissionModule.Model.PermissionCheckResult))]
				[System.Web.Http.HttpGet]
				[A.Core.PermissionModule.WebAPI.Core.Permission("A.Core.PermissionModule.Interfaces.IPermissionChecker.IsAllowed")]
				public System.Web.Http.IHttpActionResult  IsAllowed([FromUri]A.Core.PermissionModule.Model.Requests.PermissionCheckRequest request)
				{
					var result = Service.IsAllowed(request);
					return Ok(result);
				}



	}

[RoutePrefix("permissions")]
public partial class permissionsController : System.Web.Http.ApiController
{
	[Microsoft.Practices.Unity.Dependency]
	public A.Core.PermissionModule.Interfaces.IPermissionService Service { get; set; }
	
	
					[Route("{id}")]
					[System.Web.Http.Description.ResponseType(typeof(A.Core.PermissionModule.Model.Permission))]
					[System.Web.Http.HttpGet]
					[A.Core.PermissionModule.WebAPI.Core.Permission("A.Core.PermissionModule.Interfaces.IPermissionService.Get")]
					public System.Web.Http.IHttpActionResult  Get(System.Int32 id, [FromUri]A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData additionalData)
					{
						var result = Service.Get(id, additionalData);
						if(result == null)
						{
							return NotFound();
						}
						else
						{
							return Ok(result);
						}
					}

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.PermissionModule.Model.Permission>))]
				[System.Web.Http.HttpGet]
				[A.Core.PermissionModule.WebAPI.Core.Permission("A.Core.PermissionModule.Interfaces.IPermissionService.GetPage")]
				public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject search)
				{
					var result = Service.GetPage(search);
					return Ok(result);
				}



	}

[RoutePrefix("roles")]
public partial class rolesController : System.Web.Http.ApiController
{
	[Microsoft.Practices.Unity.Dependency]
	public A.Core.PermissionModule.Interfaces.IRoleService Service { get; set; }
	
	
					[Route("{id}")]
					[System.Web.Http.Description.ResponseType(typeof(A.Core.PermissionModule.Model.Role))]
					[System.Web.Http.HttpGet]
					[A.Core.PermissionModule.WebAPI.Core.Permission("A.Core.PermissionModule.Interfaces.IRoleService.Get")]
					public System.Web.Http.IHttpActionResult  Get(System.Int32 id, [FromUri]A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData additionalData)
					{
						var result = Service.Get(id, additionalData);
						if(result == null)
						{
							return NotFound();
						}
						else
						{
							return Ok(result);
						}
					}

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.PermissionModule.Model.Role>))]
				[System.Web.Http.HttpGet]
				[A.Core.PermissionModule.WebAPI.Core.Permission("A.Core.PermissionModule.Interfaces.IRoleService.GetPage")]
				public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject search)
				{
					var result = Service.GetPage(search);
					return Ok(result);
				}



	}

}


#endregion
