






















#region A_Core generated code
using System.Linq;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Collections.Generic;
using A.Core.Interface;
using AutoMapper.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;
using A.Core.Validation;

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
		public void Register(ref Microsoft.Practices.Unity.UnityContainer container)
		{
		container.RegisterType<A.Core.PermissionModule.Interfaces.IPermissionChecker, PermissionChecker>(new HierarchicalLifetimeManager());
	container.RegisterType<A.Core.PermissionModule.Interfaces.IPermissionService, PermissionService>(new HierarchicalLifetimeManager());
	container.RegisterType<A.Core.PermissionModule.Interfaces.IRoleService, RoleService>(new HierarchicalLifetimeManager());
		}
	}

	public partial class Context : System.Data.Entity.DbContext
	{
			
			public Context()
	            : base("Context")
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
	
		public System.Data.Entity.DbSet<A.Core.PermissionModule.Model.Permission> Permission { get; set; }
	public System.Data.Entity.DbSet<A.Core.PermissionModule.Model.Role> Role { get; set; }
	
		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
	        {
				base.OnModelCreating(modelBuilder);
			modelBuilder.Configurations.Add(new A.Core.PermissionModule.Services.Mapping.RoleMap());
	modelBuilder.Configurations.Add(new A.Core.PermissionModule.Services.Mapping.PermissionMap());
	modelBuilder.Configurations.Add(new A.Core.PermissionModule.Services.Mapping.RolePermissionMap());
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
			public void Register(ref Microsoft.Practices.Unity.UnityContainer container)
			{
				container.RegisterType<Context, Context>(new HierarchicalLifetimeManager());
			}
		}
	

	public partial class PermissionService : A.Core.PermissionModule.Services.Core.BaseEFBasedReadService<A.Core.PermissionModule.Model.Permission, A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject, A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData, Context>, A.Core.PermissionModule.Interfaces.IPermissionService
	{
	
		
						protected override void AddFilterFromGeneratedCode( A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject search, ref System.Linq.IQueryable<A.Core.PermissionModule.Model.Permission> query)
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
					
	//PropertyName: NameWithHierarchy
	//PropertyName: Name
	//PropertyName: NameGTE
	//PropertyName: NameList
	//PropertyName: IsAllowed
	//PropertyName: Id
	
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
	public partial class RoleService : A.Core.PermissionModule.Services.Core.BaseEFBasedReadService<A.Core.PermissionModule.Model.Role, A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject, A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData, Context>, A.Core.PermissionModule.Interfaces.IRoleService
	{
	
		
						protected override void AddFilterFromGeneratedCode( A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject search, ref System.Linq.IQueryable<A.Core.PermissionModule.Model.Role> query)
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
					
	//PropertyName: PermissionName
	//PropertyName: IsRolePermissionsLoadingEnabled
	//PropertyName: Name
	//PropertyName: NameList
	//PropertyName: Id
	
						partial void OnGetIncludeList( A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData search);
						protected override IList<string> GetIncludeList( A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData search)
						{
							
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



}
#endregion
