# REST-Framework with T4 and WebAPI
Creating REST services based on existing model and interfaces with T4 and WebAPI - <b>proof of concept</b>

TODO:
- Create nuget packages which will contain scripts
- Remove dummy classes from platform
- Create example project

# Project structure

Solution is divided in 4 projects
 - Model
 - Interfaces
 - Services
 - API

Every project has "Core" folder that contains T4 scripts and Base classes needed to run T4 generators. Please take a look at those scripts and base classes. Since they aren't wrapped in some dll you can easily extend those scripts to suit your needs.

# Model

Inside model project we will put all classes that serves as a representation of our model. Also inside this project we put classes that updates our model called "requests" and classes called "search objects" that serves as simple container for filtering data that is retreived by API.

Every entity inside Model project must have [Entity] attribute in order to be scanned by T4 scripts. If we want for example to have some field filtered by Equal and GTE operators we will simply put [Filter(FilterEnum.Equal | FilterEnum.GreatherThanOrEqual)] and T4 will create new class called [EntityName]SearchObject with two new properties. One for filtering that field by Equal operator and one for GTE operator.

    [Entity]
    public partial class Currency
    {
        [Key]
        [Filter(FilterEnum.Equal | FilterEnum.List | FilterEnum.GreatherThan)]
        public string CurrencyCode { get; set; }

        [Filter(FilterEnum.GreatherThan)]
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }

This will create following SearchObject class

    public partial class CurrencySearchObject : A.Core.Model.BaseSearchObject<CurrencyAdditionalSearchRequestData>
    {
    public virtual System.String CurrencyCode { get; set; }
    public virtual System.String CurrencyCodeGT { get; set; }
    protected System.Collections.Generic.IList<System.String> mCurrencyCodeList = new    System.Collections.Generic.List<System.String>();
    public virtual System.Collections.Generic.IList<System.String> CurrencyCodeList { get {return mCurrencyCodeList;} set {    mCurrencyCodeList = value; }}
    public virtual System.String NameGT { get; set; }
    }

This is basically same class that we would write by hand and this also helps us to consistently name properties and therefore client will know by postfix how will model be filtered on serverside.


If we for example want to update some entity, instead of sending complete entity with all properties from the API, we will create new class that will only hold those properties that are needed for update. This way we can use AutoMapper and control more easily what gets mapped and therefore reduce chance of accidentaly mapping properties that we shouldn't map from some method. In order to ease creation of those "requests" classes, we can use [RequestField("Update or any other name")] attribute. This way we will automatically get  new class called [Entity]"Update or any other name"Request holding properties that we need. This applies for Insert/Update or any other request method that we need. 

    [Entity]
    public partial class Currency
    {
        [Key]
        [Filter(FilterEnum.Equal | FilterEnum.List | FilterEnum.GreatherThan)]
        [RequestField("Insert")]
        public string CurrencyCode { get; set; }

        [Filter(FilterEnum.GreatherThan)]
        [RequestField("Insert", "[Required][MinLength(10)]]")]
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    }

This will create new class with following properties:

				public partial class CurrencyInsertRequest
				{
								public System.String CurrencyCode { get; set; }
								[Required][MinLength(10)]]
								public System.String Name { get; set; }
				}

Creating classes this way will help us to use property name in the request same as in entity and therefore it will ease clients for understanding which property gets updated from request class.



# Interfaces

After we create our model we will start creating interfaces. If we follow convention that method should accept one class with multiple properties instead of multiple parameters we can easily create REST wrappers with T4 scripts which will in the end help us to easily maintain our code since we will only add new properties in class instead of rewriting interface signature and therefore need to fix code in every place where we used old signature of interface.
By using attributes for all methods that follows some conventions we can with help of T4 automatically create code that will be by that convention.
For example:

        [DefaultMethodBehaviour(BehaviourEnum.GetById)]
        TEntity Get(object id, TSearchAdditionalData additionalData = null);

        [DefaultMethodBehaviour(BehaviourEnum.Get)]
        PagedResult<TEntity> GetPage(TSearchObject search);

By following best REST practices on the API side we will implement Get and GetPage methods accordingly. When using this approach we don't have to worry did we get REST status codes and method naming conventions get right because we will implement it once in T4 script and T4 will do the rest. This is how it looks for Get method.

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

This way we will never forget to write NotFound when its needed. We can of course always change T4 scripts to suit our needs.

# Services


Same as with model, by using convention we can create all CRUD methods with T4 scripts. When we added [Filter] attribute on model itself we created new properties. Now because we know which property needs to be filtered by which operator, with T4 we can implement filtering logic also.

						protected override void AddFilterFromGeneratedCode(A.Core.Model.SearchObjects.CurrencySearchObject search, ref System.Linq.IQueryable<A.Core.Model.Currency> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(!string.IsNullOrWhiteSpace(search.CurrencyCode))
								{
									query = query.Where(x => x.CurrencyCode == search.CurrencyCode);
	
								}
								if(search.CurrencyCodeList != null && search.CurrencyCodeList.Count > 0)
								{
									query = query.Where(x => search.CurrencyCodeList.Contains(x.CurrencyCode));
	
								}
								
						}

Please inspect T4 scripts and se how all CRUD methods are implemented.
We can even implement something as lazy loading from the client side. For example in model itself we can annotate property that should be lazily loaded and on the services side include data for that property also. This will save us lots of roundtrips or won't send data when it isn't needed and therefore improve performance.

Model: 

    [Entity]
    public partial class Currency
    {
        public string CurrencyCode { get; set; }

        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        [LazyLoading(true)]
        public Address Addr { get; set; }
    }

And on the services side, with T4 we can automatically implement it as:

						protected override void AddInclude(A.Core.Model.SearchObjects.CurrencySearchObject search, ref System.Linq.IQueryable<A.Core.Model.Currency> query)
						{
							if(search.AdditionalData.IsAddrLoadingEnabled.HasValue && search.AdditionalData.IsAddrLoadingEnabled == true)
									{
										search.AdditionalData.IncludeList.Add("Addr");
									}
							base.AddInclude(search, ref query);
							
             //in base method: query = include.Aggregate(query, (current, inc) => current.Include(inc));
						}

# API

Since we decorated interfaces with attributes that tells us what method should do and REST API should simply be wrapper around our implementation, we can fully implement API side.
This is how it looks like for currency service:

    [RoutePrefix("currencies")]
    public partial class currenciesController : System.Web.Http.ApiController
    {
				[Microsoft.Practices.Unity.Dependency]
				public A.Core.Interfaces.ICurrencyService Service { get; set; }
	
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

				[Route("")]
				[System.Web.Http.Description.ResponseType(typeof(A.Core.Model.PagedResult<A.Core.Model.Currency>))]
				[System.Web.Http.HttpGet]
				public System.Web.Http.IHttpActionResult  GetPage([FromUri]A.Core.Model.SearchObjects.CurrencySearchObject search)
				{
					var result = Service.GetPage(search);
					return Ok(result);
				}
	}
	

Did I mention that you will get Swagger to? :)

	
# Media

InfoQ.com article - https://www.infoq.com/articles/T4-Rest-Code-Generation
