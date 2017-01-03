









#region A_Core generated code

using System.Net;
using System.Net.Http;
using System.Web.Http;

using Microsoft.Practices.Unity;
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
}


#endregion
