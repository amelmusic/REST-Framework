






















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

//A.Core.PermissionModule.Interfaces
using A.Core.PermissionModule.Model;
using A.Core.PermissionModule.Model.Requests;


namespace A.Core.PermissionModule.Services 
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
	                .As<A.Core.PermissionModule.Interfaces.IPermissionService>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new A.Core.Interceptors.ForceVirtualMethodsHook()})
					.InterceptedBy(typeof(A.Core.Interceptors.LogInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.CacheInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.TransactionInterceptorProxy));
	container.RegisterType<RoleService>()
	                .As<A.Core.PermissionModule.Interfaces.IRoleService>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new A.Core.Interceptors.ForceVirtualMethodsHook()})
					.InterceptedBy(typeof(A.Core.Interceptors.LogInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.CacheInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.TransactionInterceptorProxy));
	container.RegisterType<RolePermissionService>()
	                .As<A.Core.PermissionModule.Interfaces.IRolePermissionService>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new A.Core.Interceptors.ForceVirtualMethodsHook()})
					.InterceptedBy(typeof(A.Core.Interceptors.LogInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.CacheInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.TransactionInterceptorProxy));
	container.RegisterType<PermissionChecker>()
	                .As<A.Core.PermissionModule.Interfaces.IPermissionChecker>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors(new Castle.DynamicProxy.ProxyGenerationOptions() { Hook = new A.Core.Interceptors.ForceVirtualMethodsHook()})
					.InterceptedBy(typeof(A.Core.Interceptors.LogInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.CacheInterceptorProxy))
	                .InterceptedBy(typeof(A.Core.Interceptors.TransactionInterceptorProxy));
		}
	}

	public partial class PermissionContext : Database.PermissionContext
	{
			
					public PermissionContext()
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
	
		
		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
	        {
				base.OnModelCreating(modelBuilder);
					}
			
			public partial class PermissionContextInitializer : IDatabaseInitializer<PermissionContext>
			{
				partial void OnInitializeDatabase(ref PermissionContext context, ref bool isHandled);
				public void InitializeDatabase(PermissionContext context)
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
	
	public partial class PermissionContextRegistration : A.Core.Interface.IServicesRegistration
		{
			public int Priority {get; set; }
			public PermissionContextRegistration()
			{
				Priority = 0; //This is root. Add new class with higher priority
			}
			public void Register(ref Autofac.ContainerBuilder container)
			{
				container.RegisterType<PermissionContext>()
	                .As<PermissionContext>()
	                .InstancePerLifetimeScope()
	                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
	                .EnableClassInterceptors();
			}
		}
	

	public partial class PermissionService : A.Core.PermissionModule.Services.Core.BaseEFBasedReadService<A.Core.PermissionModule.Model.Permission, A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject, A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData, PermissionContext, Database.Permission>, A.Core.PermissionModule.Interfaces.IPermissionService
	{
	
		
						protected override void AddFilterFromGeneratedCode( A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject search, ref System.Linq.IQueryable<Database.Permission> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(!string.IsNullOrWhiteSpace(search.Name))
								{
									query = query.Where(x => x.Name == search.Name);
	
								}
								if(!string.IsNullOrWhiteSpace(search.NameGTE))
								{
									query = query.Where(x => x.Name.StartsWith(search.NameGTE));
	
								}
								if(search.NameList != null && search.NameList.Count > 0)
								{
									query = query.Where(x => search.NameList.Contains(x.Name));
	
								}
								if(search.IsAllowed.HasValue)
								{
										query = query.Where(x => x.IsAllowed == search.IsAllowed);
								}
								if(search.Id.HasValue)
								{
										query = query.Where(x => x.Id == search.Id);
								}
								
						}
					
	//PropertyName: Name
	//PropertyName: NameGTE
	//PropertyName: NameList
	//PropertyName: IsAllowed
	//PropertyName: Id
	//PropertyName: NameWithHierarchy
	
						partial void OnGetIncludeList( A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData search);
						protected override IList<string> GetIncludeList( A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData search)
						{
							
							OnGetIncludeList(search);
							return base.GetIncludeList(search);
						}
					
	
						public override A.Core.PermissionModule.Model.Permission Get(object id,  A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData additionalData = null)
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
									 A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject search = new  A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject();
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
	public partial class RoleService : A.Core.PermissionModule.Services.Core.BaseEFBasedReadService<A.Core.PermissionModule.Model.Role, A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject, A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData, PermissionContext, Database.Role>, A.Core.PermissionModule.Interfaces.IRoleService
	{
	
		
						protected override void AddFilterFromGeneratedCode( A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject search, ref System.Linq.IQueryable<Database.Role> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(!string.IsNullOrWhiteSpace(search.Name))
								{
									query = query.Where(x => x.Name == search.Name);
	
								}
								if(search.NameList != null && search.NameList.Count > 0)
								{
									query = query.Where(x => search.NameList.Contains(x.Name));
	
								}
								if(search.Id.HasValue)
								{
										query = query.Where(x => x.Id == search.Id);
								}
								
						}
					
	//PropertyName: Name
	//PropertyName: NameList
	//PropertyName: Id
	//PropertyName: PermissionName
	//PropertyName: IsRolePermissionsLoadingEnabled
	
						partial void OnGetIncludeList( A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData search);
						protected override IList<string> GetIncludeList( A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData search)
						{
							if(search.IsRolePermissionsLoadingEnabled.HasValue && search.IsRolePermissionsLoadingEnabled == true)
										{
											search.IncludeList.Add("RolePermissions");
										}
							OnGetIncludeList(search);
							return base.GetIncludeList(search);
						}
					
	
						public override A.Core.PermissionModule.Model.Role Get(object id,  A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData additionalData = null)
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
									 A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject search = new  A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject();
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
	public partial class RolePermissionService : A.Core.PermissionModule.Services.Core.BaseEFBasedReadService<A.Core.PermissionModule.Model.RolePermission, A.Core.PermissionModule.Model.SearchObjects.RolePermissionSearchObject, A.Core.PermissionModule.Model.SearchObjects.RolePermissionAdditionalSearchRequestData, PermissionContext, Database.RolePermission>, A.Core.PermissionModule.Interfaces.IRolePermissionService
	{
	
		
						protected override void AddFilterFromGeneratedCode( A.Core.PermissionModule.Model.SearchObjects.RolePermissionSearchObject search, ref System.Linq.IQueryable<Database.RolePermission> query)
						{
							base.AddFilterFromGeneratedCode(search, ref query);
							if(search.Id.HasValue)
								{
										query = query.Where(x => x.Id == search.Id);
								}
								
						}
					
	//PropertyName: Id
	
						partial void OnGetIncludeList( A.Core.PermissionModule.Model.SearchObjects.RolePermissionAdditionalSearchRequestData search);
						protected override IList<string> GetIncludeList( A.Core.PermissionModule.Model.SearchObjects.RolePermissionAdditionalSearchRequestData search)
						{
							if(search.IsPermissionLoadingEnabled.HasValue && search.IsPermissionLoadingEnabled == true)
										{
											search.IncludeList.Add("Permission");
										}if(search.IsRoleLoadingEnabled.HasValue && search.IsRoleLoadingEnabled == true)
										{
											search.IncludeList.Add("Role");
										}
							OnGetIncludeList(search);
							return base.GetIncludeList(search);
						}
					
	
						public override A.Core.PermissionModule.Model.RolePermission Get(object id,  A.Core.PermissionModule.Model.SearchObjects.RolePermissionAdditionalSearchRequestData additionalData = null)
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
									 A.Core.PermissionModule.Model.SearchObjects.RolePermissionSearchObject search = new  A.Core.PermissionModule.Model.SearchObjects.RolePermissionSearchObject();
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


				public partial class PermissionToDatabase_PermissionMapProfile : AutoMapper.Profile
					{
					        public PermissionToDatabase_PermissionMapProfile()
							{
								var profile = CreateMap<Permission, Database.Permission>();
								profile.ForAllMembers(opt =>
									opt.Condition((src, dest, srcVal) => { return srcVal != null; }));
								profile.ReverseMap();
							}
				}
				public partial class RoleToDatabase_RoleMapProfile : AutoMapper.Profile
					{
					        public RoleToDatabase_RoleMapProfile()
							{
								var profile = CreateMap<Role, Database.Role>();
								profile.ForAllMembers(opt =>
									opt.Condition((src, dest, srcVal) => { return srcVal != null; }));
								profile.ReverseMap();
							}
				}
				public partial class RolePermissionToDatabase_RolePermissionMapProfile : AutoMapper.Profile
					{
					        public RolePermissionToDatabase_RolePermissionMapProfile()
							{
								var profile = CreateMap<RolePermission, Database.RolePermission>();
								profile.ForAllMembers(opt =>
									opt.Condition((src, dest, srcVal) => { return srcVal != null; }));
								profile.ReverseMap();
							}
				}

}
#endregion
