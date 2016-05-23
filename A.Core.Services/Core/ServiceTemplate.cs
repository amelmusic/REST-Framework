











using System.Linq;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

//A.Core.Interfaces



namespace A.Core.Services 
	{ 
	public partial class ProductService : A.Core.Services.Core.BaseEFBasedCRUDService<A.Core.Model.Product,A.Core.Model.SearchObjects.ProductSearchObject,A.Core.Model.SearchObjects.ProductAdditionalSearchRequestData,A.Core.Model.Requests.ProductInsertRequest,A.Core.Model.Requests.ProductUpdateRequest, Context>, A.Core.Interfaces.IProductService
	{
	
		
						protected override void AddFilterFromGeneratedCode(A.Core.Model.SearchObjects.ProductSearchObject search, ref System.Linq.IQueryable<A.Core.Model.Product> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(!string.IsNullOrWhiteSpace(search.NameGTE))
								{
									query = query.Where(x => x.Name.StartsWith(search.NameGTE));
	
								}
								if(!string.IsNullOrWhiteSpace(search.Number))
								{
									query = query.Where(x => x.Number == search.Number);
	
								}
								if(search.NumberList != null && search.NumberList.Count > 0)
								{
									query = query.Where(x => search.NumberList.Contains(x.Number));
	
								}
								if(search.ListPriceGTE.HasValue)
								{
									query = query.Where(x => x.ListPrice >= search.ListPriceGTE);
	
								}
								if(search.ListPriceLTE.HasValue)
								{
									query = query.Where(x => x.ListPrice <= search.ListPriceLTE);
	
								}
								
						}
					
	
						protected override void AddInclude(A.Core.Model.SearchObjects.ProductSearchObject search, ref System.Linq.IQueryable<A.Core.Model.Product> query)
						{
							
							base.AddInclude(search, ref query);
						}
					
	}

	public partial class AddressService : A.Core.Services.Core.BaseEFBasedCRUDService<A.Core.Model.Address,A.Core.Model.SearchObjects.AddressSearchObject,A.Core.Model.SearchObjects.AddressAdditionalSearchRequestData,A.Core.Model.Requests.AddressInsertRequest,A.Core.Model.Requests.AddressUpdateRequest, Context>, A.Core.Interfaces.IAddressService
	{
	
		
						protected override void AddFilterFromGeneratedCode(A.Core.Model.SearchObjects.AddressSearchObject search, ref System.Linq.IQueryable<A.Core.Model.Address> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(search.AddressID.HasValue)
								{
										query = query.Where(x => x.AddressID == search.AddressID);
								}
								if(search.AddressIDList != null && search.AddressIDList.Count > 0)
								{
									query = query.Where(x => search.AddressIDList.Contains(x.AddressID));
	
								}
								if(!string.IsNullOrWhiteSpace(search.City))
								{
									query = query.Where(x => x.City == search.City);
	
								}
								if(!string.IsNullOrWhiteSpace(search.CityNE))
								{
									query = query.Where(x => x.City != search.CityNE);
	
								}
								if(!string.IsNullOrWhiteSpace(search.CityGTE))
								{
									query = query.Where(x => x.City.StartsWith(search.CityGTE));
	
								}
								if(search.CityList != null && search.CityList.Count > 0)
								{
									query = query.Where(x => search.CityList.Contains(x.City));
	
								}
								if(search.StateProvinceID.HasValue)
								{
										query = query.Where(x => x.StateProvinceID == search.StateProvinceID);
								}
								if(search.StateProvinceIDNE.HasValue)
								{
									query = query.Where(x => x.StateProvinceID != search.StateProvinceIDNE);
	
								}
								if(search.StateProvinceIDGTE.HasValue)
								{
									query = query.Where(x => x.StateProvinceID >= search.StateProvinceIDGTE);
	
								}
								if(search.StateProvinceIDList != null && search.StateProvinceIDList.Count > 0)
								{
									query = query.Where(x => search.StateProvinceIDList.Contains(x.StateProvinceID));
	
								}
								
						}
					
	
						protected override void AddInclude(A.Core.Model.SearchObjects.AddressSearchObject search, ref System.Linq.IQueryable<A.Core.Model.Address> query)
						{
							
							base.AddInclude(search, ref query);
						}
					
	}

	public partial class CurrencyService : A.Core.Services.Core.BaseEFBasedReadService<A.Core.Model.Currency,A.Core.Model.SearchObjects.CurrencySearchObject,A.Core.Model.SearchObjects.CurrencyAdditionalSearchRequestData, Context>, A.Core.Interfaces.ICurrencyService
	{
	
		
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
					
	
						protected override void AddInclude(A.Core.Model.SearchObjects.CurrencySearchObject search, ref System.Linq.IQueryable<A.Core.Model.Currency> query)
						{
							if(search.AdditionalData.IsAddrLoadingEnabled.HasValue && search.AdditionalData.IsAddrLoadingEnabled == true)
									{
										search.AdditionalData.IncludeList.Add("Addr");
									}
							base.AddInclude(search, ref query);
						}
					
	}

	public partial class ServicesRegistration : A.Core.Interface.IServicesRegistration
	{
		public int Priority {get; set; }
		public ServicesRegistration()
		{
			Priority = 0; //This is root, If you want to override this. Add new class with higher priority
		}
		public void Register(Microsoft.Practices.Unity.UnityContainer container)
		{
		container.RegisterType<A.Core.Interfaces.IProductService, ProductService>(new HierarchicalLifetimeManager());
	container.RegisterType<A.Core.Interfaces.IAddressService, AddressService>(new HierarchicalLifetimeManager());
	container.RegisterType<A.Core.Interfaces.ICurrencyService, CurrencyService>(new HierarchicalLifetimeManager());
		}
	}

	public partial class Context : System.Data.Entity.DbContext
	{
			public Context()
	            : base("Context")
	        {
	            this.Configuration.LazyLoadingEnabled = false;
	        }
	
		public System.Data.Entity.DbSet<A.Core.Model.Product> Product { get; set; }
	public System.Data.Entity.DbSet<A.Core.Model.Currency> Currency { get; set; }
	public System.Data.Entity.DbSet<A.Core.Model.Address> Address { get; set; }
	
		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
	        {
			modelBuilder.Configurations.Add(new A.Core.Services.Mapping.AddressMap());
	modelBuilder.Configurations.Add(new A.Core.Services.Mapping.CurrencyMap());
			}
	
			public partial class ContextInitializer : IDatabaseInitializer<Context>
			{
				public void InitializeDatabase(Context context)
				{
					if (!context.Database.Exists())
					{
						context.Database.Create();
					}
				}
			}
	
	}

}
