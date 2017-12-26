









#region A_Core generated code

using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace A.Core.WebAPI.Controllers
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


namespace A.Core.WebAPI.Controllers 
{ 
[RoutePrefix("permissions")]
public partial class permissionsController : System.Web.Http.ApiController
{
	public A.Core.Interfaces.IPermissionService Service { get; set; }
	
	
				[Route("getCached")]
				[System.Web.Http.Description.ResponseType(typeof(System.Int32))]
				[System.Web.Http.HttpGet]
				[A.Core.WebAPI.Core.Permission("A.Core.Interfaces.IPermissionService.GetCached", "View")]
				public System.Web.Http.IHttpActionResult  GetCached()
				{
					var result = Service.GetCached();
					return Ok(result);
				}

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Permission))]
				[System.Web.Http.HttpPost]
				[A.Core.WebAPI.Core.Permission("A.Core.Interfaces.IPermissionService.Insert", "Edit")]
				public HttpResponseMessage  Insert([FromBody]A.Core.Model.Requests.PermissionInsertRequest request)
				{
					var result = Service.Insert(request);					 
					var response = Request.CreateResponse<A.Core.Model.Permission>(HttpStatusCode.Created, result);
					return response;
				}

				[Route("{id}")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Permission))]
				[System.Web.Http.HttpPut]
				[A.Core.WebAPI.Core.Permission("A.Core.Interfaces.IPermissionService.Update", "Edit")]
				public HttpResponseMessage  Update([FromUri]System.Int32 id,[FromBody]A.Core.Model.Requests.PermissionUpdateRequest request)
				{
					var result = Service.Update(id,request);					 
					var response = Request.CreateResponse<A.Core.Model.Permission>(HttpStatusCode.OK, result);
					return response;
				}

					[Route("{id}")]
					[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Permission))]
					[System.Web.Http.HttpGet]
					[A.Core.WebAPI.Core.Permission("A.Core.Interfaces.IPermissionService.Get", "View")]
					public System.Web.Http.IHttpActionResult  Get(System.Int32 id, [FromUri]A.Core.Model.SearchObjects.PermissionAdditionalSearchRequestData additionalData)
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
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.Model.Permission>))]
				[System.Web.Http.HttpGet]
				[A.Core.WebAPI.Core.Permission("A.Core.Interfaces.IPermissionService.GetPage", "View")]
				public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.Model.SearchObjects.PermissionSearchObject search)
				{
					var result = Service.GetPage(search);
					return Ok(result);
				}



	}

}


#endregion
