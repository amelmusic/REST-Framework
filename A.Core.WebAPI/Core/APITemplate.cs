









using System.Net;
using System.Net.Http;
using System.Web.Http;
namespace A.Core.WebAPI.Controllers 
{ 
[RoutePrefix("products")]
public partial class productsController : System.Web.Http.ApiController
{
	[Microsoft.Practices.Unity.Dependency]
	public A.Core.Interfaces.IProductService Service { get; set; }
	
	//Generating template for Insert for: Insert

	[Route("")]
	[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Product))]
	[System.Web.Http.HttpPost]
	public HttpResponseMessage  Insert([FromBody]A.Core.Model.Requests.ProductInsertRequest request)
	{
		var result = Service.Insert(request);					 
		var response = Request.CreateResponse<A.Core.Model.Product>(HttpStatusCode.Created, result);
		return response;
	}
//Generating template for Update for: Update

	[Route("{id}")]
	[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Product))]
	[System.Web.Http.HttpPut]
	public HttpResponseMessage  Update([FromUri]System.Int32 id,[FromBody]A.Core.Model.Requests.ProductUpdateRequest request)
	{
        //can return "Not Found" if Update throws NotFoundException
		var result = Service.Update(id,request);					 
		var response = Request.CreateResponse<A.Core.Model.Product>(HttpStatusCode.OK, result);
		return response;
	}
//Generating template for GetById for: Get

				[Route("{id}")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Product))]
				[System.Web.Http.HttpGet]
				public System.Web.Http.IHttpActionResult  Get(System.Int32 id, [FromUri]A.Core.Model.SearchObjects.ProductAdditionalSearchRequestData additionalData)
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
//Generating template for Get for: GetPage

	[Route("")]
	[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.Model.Product>))]
	[System.Web.Http.HttpGet]
	public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.Model.SearchObjects.ProductSearchObject search)
	{
        //call to partiall method
		var result = Service.GetPage(search);
		return Ok(result);
	}
}

[RoutePrefix("addresses")]
public partial class addressesController : System.Web.Http.ApiController
{
	[Microsoft.Practices.Unity.Dependency]
	public A.Core.Interfaces.IAddressService Service { get; set; }
	
	//Generating template for Insert for: Insert

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Address))]
				[System.Web.Http.HttpPost]
				public HttpResponseMessage  Insert([FromBody]A.Core.Model.Requests.AddressInsertRequest request)
				{
					var result = Service.Insert(request);					 
					var response = Request.CreateResponse<A.Core.Model.Address>(HttpStatusCode.Created, result);
					return response;
				}
//Generating template for Update for: Update

				[Route("{id}")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Address))]
				[System.Web.Http.HttpPut]
				public HttpResponseMessage  Update([FromUri]System.Int32 id,[FromBody]A.Core.Model.Requests.AddressUpdateRequest request)
				{
					var result = Service.Update(id,request);					 
					var response = Request.CreateResponse<A.Core.Model.Address>(HttpStatusCode.OK, result);
					return response;
				}
//Generating template for GetById for: Get

				[Route("{id}")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Address))]
				[System.Web.Http.HttpGet]
				public System.Web.Http.IHttpActionResult  Get(System.Int32 id, [FromUri]A.Core.Model.SearchObjects.AddressAdditionalSearchRequestData additionalData)
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
//Generating template for Get for: GetPage

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.Model.Address>))]
				[System.Web.Http.HttpGet]
				public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.Model.SearchObjects.AddressSearchObject search)
				{
					var result = Service.GetPage(search);
					return Ok(result);
				}



	}

[RoutePrefix("currencies")]
public partial class currenciesController : System.Web.Http.ApiController
{
	[Microsoft.Practices.Unity.Dependency]
	public A.Core.Interfaces.ICurrencyService Service { get; set; }
	
	//Generating template for GetById for: Get

				[Route("{id}")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.Currency))]
				[System.Web.Http.HttpGet]
				public System.Web.Http.IHttpActionResult  Get(System.String id, [FromUri]A.Core.Model.SearchObjects.CurrencyAdditionalSearchRequestData additionalData)
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
//Generating template for Get for: GetPage

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.Model.Currency>))]
				[System.Web.Http.HttpGet]
				public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.Model.SearchObjects.CurrencySearchObject search)
				{
					var result = Service.GetPage(search);
					return Ok(result);
				}



	}

}
