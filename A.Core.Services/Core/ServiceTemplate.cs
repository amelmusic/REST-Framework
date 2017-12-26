






















#region A_Core generated code
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Collections.Generic;
using A.Core.Interface;
using AutoMapper.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using A.Core.Validation;
using Autofac;
using Autofac.Extras.DynamicProxy;

//A.Core.Interfaces
using A.Core.Model;
using A.Core.Model.Requests;


namespace A.Core.Services 
	{ 
	
	public partial class ServicesRegistration : A.Core.Interface.IServicesRegistration
	{
		public int Priority {get; set; }
		public ServicesRegistration()
		{
			Priority = 0; //This is root, If you want to override this. Add new class with higher priority
		}
		public void Register(ref Autofac.ContainerBuilder container)
		{
		container.RegisterType<PermissionService>()
	                .As<A.Core.Interfaces.IPermissionService>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new A.Core.Interceptors.ForceVirtualMethodsHook()})
					.InterceptedBy(typeof(A.Core.Interceptors.LogInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.CacheInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.TransactionInterceptorProxy));
		}
	}

	public partial class Context : System.Data.Entity.DbContext
	{
			
					public Context()
	            : base()
	        {
	            this.Configuration.LazyLoadingEnabled = false;
	        }
			
			public virtual bool ExistsInContext<TEntity>(TEntity entity)
				where TEntity : class
			{
				return this.Set<TEntity>().Local.Any(e => e == entity);
			}
	
			public override int SaveChanges()
		    {
		        try
		        {
	                return base.SaveChanges();
	            }
		        catch (System.Data.Entity.Validation.DbEntityValidationException e)
		        {
	                A.Core.Validation.ValidationResult validationResult = new A.Core.Validation.ValidationResult();
		            foreach (var dbEntityValidationResult in e.EntityValidationErrors)
		            {
		                if (!dbEntityValidationResult.IsValid)
		                {
		                    foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
		                    {
	                            validationResult.ResultList.Add(new ValidationResultItem { Description = dbValidationError.ErrorMessage
	                                , Level = ValidationResultLevelEnum.Error
	                                , Key = dbValidationError.PropertyName});
	                        }
		                }
		            }
		            throw new ValidationException(validationResult);
		        }
		    }
	
		    public override async Task<int> SaveChangesAsync()
		    {
	            try
	            {
	                return await base.SaveChangesAsync();
	            }
	            catch (System.Data.Entity.Validation.DbEntityValidationException e)
	            {
	                A.Core.Validation.ValidationResult validationResult = new A.Core.Validation.ValidationResult();
	                foreach (var dbEntityValidationResult in e.EntityValidationErrors)
	                {
	                    if (!dbEntityValidationResult.IsValid)
	                    {
	                        foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
	                        {
	                            validationResult.ResultList.Add(new ValidationResultItem
	                            {
	                                Description = dbValidationError.ErrorMessage
	                                ,
	                                Level = ValidationResultLevelEnum.Error
	                                ,
	                                Key = dbValidationError.PropertyName
	                            });
	                        }
	                    }
	                }
	                throw new ValidationException(validationResult);
	            }
		    }
	
		public System.Data.Entity.DbSet<A.Core.Model.Permission> Permission { get; set; }
	
		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
	        {
				base.OnModelCreating(modelBuilder);
					}
			
			public partial class ContextInitializer : IDatabaseInitializer<Context>
			{
				partial void OnInitializeDatabase(ref Context context, ref bool isHandled);
				public void InitializeDatabase(Context context)
				{
					bool isHandled = false;
					OnInitializeDatabase(ref context, ref isHandled);
					if(!isHandled)
					{
						if (!context.Database.Exists())
						{
							context.Database.Create();
						}
					}
				}
			}
	
	}
	
	public partial class ContextRegistration : A.Core.Interface.IServicesRegistration
		{
			public int Priority {get; set; }
			public ContextRegistration()
			{
				Priority = 0; //This is root. Add new class with higher priority
			}
			public void Register(ref Autofac.ContainerBuilder container)
			{
				container.RegisterType<Context>()
	                .As<Context>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors();
			}
		}
	

	public partial class PermissionService : A.Core.Services.Core.BaseEFBasedCRUDService<A.Core.Model.Permission, A.Core.Model.SearchObjects.PermissionSearchObject, A.Core.Model.SearchObjects.PermissionAdditionalSearchRequestData, A.Core.Model.Requests.PermissionInsertRequest, A.Core.Model.Requests.PermissionUpdateRequest, Context>, A.Core.Interfaces.IPermissionService
	{
	
		
						protected override void AddFilterFromGeneratedCode( A.Core.Model.SearchObjects.PermissionSearchObject search, ref System.Linq.IQueryable<A.Core.Model.Permission> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(search.Id.HasValue)
								{
										query = query.Where(x => x.Id == search.Id);
								}
								
						}
					
	//PropertyName: Id
	
						partial void OnGetIncludeList( A.Core.Model.SearchObjects.PermissionAdditionalSearchRequestData search);
						protected override IList<string> GetIncludeList( A.Core.Model.SearchObjects.PermissionAdditionalSearchRequestData search)
						{
							
							OnGetIncludeList(search);
							return base.GetIncludeList(search);
						}
					
	
						public override A.Core.Model.Permission Get(object id,  A.Core.Model.SearchObjects.PermissionAdditionalSearchRequestData additionalData = null)
						{
								bool redirectGet = IsMultitenantAwareService;
								if (additionalData != null)
	                            {
	                                var count = GetIncludeList(additionalData).Count;
	                                if (count > 0)
	                                {
	                                    redirectGet = true;
	                                }
	                            }
								if (redirectGet) 
								{
									 A.Core.Model.SearchObjects.PermissionSearchObject search = new  A.Core.Model.SearchObjects.PermissionSearchObject();
									search.Id = (System.Nullable<System.Int32>)id;
									if (additionalData != null)
									{
										search.AdditionalData = additionalData;
									}
									var result = GetFirstOrDefaultForSearchObject(search);
		                            return result;
								}
							return base.Get(id, additionalData);
						}
					
	}



}
#endregion
