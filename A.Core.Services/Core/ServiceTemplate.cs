














using System.Linq;
using Microsoft.Practices.Unity;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System;
using System.Collections.Generic;

//A.Core.Interfaces
using A.Core.Model;
using A.Core.Model.Requests;


namespace A.Core.Services 
	{ 
	public partial class AddressService : A.Core.Services.Core.BaseEFBasedReadService<A.Core.Model.Address,A.Core.Model.SearchObjects.AddressSearchObject,A.Core.Model.SearchObjects.AddressAdditionalSearchRequestData, Context>, A.Core.Interfaces.IAddressService
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
								if(search.WeightGTE.HasValue)
								{
									query = query.Where(x => x.Weight >= search.WeightGTE);
	
								}
								if(search.WeightLTE.HasValue)
								{
									query = query.Where(x => x.Weight <= search.WeightLTE);
	
								}
								
						}
					
	
						protected override void AddInclude(A.Core.Model.SearchObjects.ProductSearchObject search, ref System.Linq.IQueryable<A.Core.Model.Product> query)
						{
							if(search.AdditionalData.IsProductGroupLoadingEnabled.HasValue && search.AdditionalData.IsProductGroupLoadingEnabled == true)
									{
										search.AdditionalData.IncludeList.Add("ProductGroup");
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
		public void Register(ref Microsoft.Practices.Unity.UnityContainer container)
		{
		container.RegisterType<A.Core.Interfaces.IAddressService, AddressService>(new HierarchicalLifetimeManager());
	container.RegisterType<A.Core.Interfaces.ICurrencyService, CurrencyService>(new HierarchicalLifetimeManager());
	container.RegisterType<A.Core.Interfaces.IProductService, ProductService>(new HierarchicalLifetimeManager());
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
	public System.Data.Entity.DbSet<A.Core.Model.Address> Address { get; set; }
	public System.Data.Entity.DbSet<A.Core.Model.Currency> Currency { get; set; }
	
		protected override void OnModelCreating(System.Data.Entity.DbModelBuilder modelBuilder)
	        {
			modelBuilder.Configurations.Add(new A.Core.Services.Mapping.CurrencyMap());
	modelBuilder.Configurations.Add(new A.Core.Services.Mapping.AddressMap());
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

	//file:C:\Projects\Other\REST-Framework\A.Core.Model\A.Core.Model.csproj_C:\Projects\Other\REST-Framework\A.Core.Model
	//Creating state machine from: C:\Projects\Other\REST-Framework\A.Core.Model\AddressStateMachine.tastate
					//StateMachine: IAddressService, name: AddressStateMachine, graphCount 3, enum A.Core.Model.AddressStateMachineEnum
					public partial class AddressStateMachineInitState : A.Core.Services.Core.StateBase 
					{
						protected A.Core.Model.AddressStateMachineEnum mStateId = A.Core.Model.AddressStateMachineEnum.Init;
						public AddressStateMachineInitState(A.Core.Services.Core.StateMachineBase machine) : base(machine) 
						{
							StateId = (int)mStateId;
						}
					}
					public partial class AddressStateMachineEnteredState : A.Core.Services.Core.StateBase 
					{
						protected A.Core.Model.AddressStateMachineEnum mStateId = A.Core.Model.AddressStateMachineEnum.Entered;
						public AddressStateMachineEnteredState(A.Core.Services.Core.StateMachineBase machine) : base(machine)
						{
							StateId = (int)mStateId;
						}
					}
					public partial class AddressStateMachineVerifiedState : A.Core.Services.Core.StateBase 
					{
						protected A.Core.Model.AddressStateMachineEnum mStateId = A.Core.Model.AddressStateMachineEnum.Verified;
						public AddressStateMachineVerifiedState(A.Core.Services.Core.StateMachineBase machine) : base(machine)
						{
							StateId = (int)mStateId;
						}
					}
					public partial class AddressStateMachineInvalidState : A.Core.Services.Core.StateBase 
					{
						protected A.Core.Model.AddressStateMachineEnum mStateId = A.Core.Model.AddressStateMachineEnum.Invalid;
						public AddressStateMachineInvalidState(A.Core.Services.Core.StateMachineBase machine) : base(machine)
						{
							StateId = (int)mStateId;
						}
					}
					public partial class AddressStateMachineVerifyTrigger : A.Core.Services.Core.TriggerBase 
					{
						protected AddressTriggerEnum mTriggerId = AddressTriggerEnum.AddressVerifyRequest;
						static AddressStateMachineVerifyTrigger()
	                    {
	                        AutoMapper.Mapper.CreateMap<AddressVerifyRequest, A.Core.Model.Address>().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
	                    }
						public AddressStateMachineVerifyTrigger()
						{
							TriggerId = (int)mTriggerId;
						}
						public AddressVerifyRequest Request {get; set;}
						public override void UpdateEntity(object entity)
						{
							AutoMapper.Mapper.Map(Request, entity);
						}
					}
	
					public partial class AddressStateMachineMarkAsInvalidTrigger : A.Core.Services.Core.TriggerBase 
					{
						protected AddressTriggerEnum mTriggerId = AddressTriggerEnum.AddressMarkAsInvalidRequest;
						static AddressStateMachineMarkAsInvalidTrigger()
	                    {
	                        AutoMapper.Mapper.CreateMap<AddressMarkAsInvalidRequest, A.Core.Model.Address>().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
	                    }
						public AddressStateMachineMarkAsInvalidTrigger()
						{
							TriggerId = (int)mTriggerId;
						}
						public AddressMarkAsInvalidRequest Request {get; set;}
						public override void UpdateEntity(object entity)
						{
							AutoMapper.Mapper.Map(Request, entity);
						}
					}
	
						public partial class AddressStateMachineStartTrigger : A.Core.Services.Core.TriggerBase 
					{
						protected AddressTriggerEnum mTriggerId = AddressTriggerEnum.AddressStartRequest;
						static AddressStateMachineStartTrigger()
	                    {
	                        AutoMapper.Mapper.CreateMap<AddressStartRequest, A.Core.Model.Address>().ForAllMembers(opt => opt.Condition(srs => !srs.IsSourceValueNull));
	                    }
						public AddressStateMachineStartTrigger()
						{
							TriggerId = (int)mTriggerId;
						}
						public AddressStartRequest Request {get; set;}
						public override void UpdateEntity(object entity)
						{
							AutoMapper.Mapper.Map(Request, entity);
						}
					}
					/// <summary>
					/// This class is the actual state machine designed in the State-Diagarm.
					/// </summary>
					public partial class AddressStateMachine : A.Core.Services.Core.StateMachineBase
					{
						/// <summary>
						/// Makes the state machine react to a trigger.
						/// </summary>
						public override void ProcessTrigger(A.Core.Services.Core.TriggerBase trigger)
						{
							if (this.CurrentState == null) return;
							if (trigger == null) throw new ArgumentException("Trigger must not be null");
	
							// determine what action to take based on the current state
							// and the given trigger.
							// iterate all states in the diagram
							if (this.CurrentState is AddressStateMachineInitState)
							{
								if(!GetAllowedTriggerList().Contains((AddressTriggerEnum)trigger.TriggerId))
	                                    {
	                                        throw new ApplicationException("Invalid trigger!");
	                                    }
									if (trigger is AddressStateMachineStartTrigger)
									{
										this.TransitionToNewState(new AddressStateMachineEnteredState(this), trigger);
									}
							}
	
								else if (this.CurrentState is AddressStateMachineEnteredState)
								{
									if(!GetAllowedTriggerList().Contains((AddressTriggerEnum)trigger.TriggerId))
	                                        {
	                                            throw new ApplicationException("Invalid trigger!");
	                                        }
										if (trigger is AddressStateMachineVerifyTrigger)
										{
											this.TransitionToNewState(new AddressStateMachineVerifiedState(this), trigger);
											
										}
										if (trigger is AddressStateMachineMarkAsInvalidTrigger)
										{
											this.TransitionToNewState(new AddressStateMachineInvalidState(this), trigger);
											
										}
								}
								else if (this.CurrentState is AddressStateMachineVerifiedState)
								{
									if(!GetAllowedTriggerList().Contains((AddressTriggerEnum)trigger.TriggerId))
	                                        {
	                                            throw new ApplicationException("Invalid trigger!");
	                                        }
								}
								else if (this.CurrentState is AddressStateMachineInvalidState)
								{
									if(!GetAllowedTriggerList().Contains((AddressTriggerEnum)trigger.TriggerId))
	                                        {
	                                            throw new ApplicationException("Invalid trigger!");
	                                        }
								}
	
						}
	
						 public System.Collections.Generic.IList<AddressTriggerEnum> GetAllowedTriggerList()
	                        {
	                            IList<AddressTriggerEnum> triggerList = new List<AddressTriggerEnum>();
								if (this.CurrentState is AddressStateMachineInitState)
								{
										triggerList.Add(AddressTriggerEnum.AddressStartRequest);
								}
								if (this.CurrentState is AddressStateMachineEnteredState)
								{
										triggerList.Add(AddressTriggerEnum.AddressVerifyRequest);
										triggerList.Add(AddressTriggerEnum.AddressMarkAsInvalidRequest);
								}
								if (this.CurrentState is AddressStateMachineVerifiedState)
								{
								}
								if (this.CurrentState is AddressStateMachineInvalidState)
								{
								}
	
	
	                            return triggerList;
	                        }
	
							
							public A.Core.Model.Address Entity { get; set; }
							public AddressStateMachine()
							:base()
							{
								
							}
							public override void UpdateEntityState()
	                        {
								//Entity: A.Core.Model.Address
								Entity.StateId = (A.Core.Model.AddressStateMachineEnum)CurrentState.StateId;
	                            base.UpdateEntityState();
	                        }
	
							public void Initialize(A.Core.Model.Address entity)
							{
								//bind entity that we are operating ON
								
								Entity = entity;
								CurrentEntity = entity;
								CurrentState = GetState(entity.StateId);
							}
	
							public A.Core.Services.Core.StateBase GetState(A.Core.Model.AddressStateMachineEnum stateId)
							{
								switch(stateId)
								{
									case A.Core.Model.AddressStateMachineEnum.Init:
									{
										return new AddressStateMachineInitState(this);
									}
									case A.Core.Model.AddressStateMachineEnum.Entered:
									{
										return new AddressStateMachineEnteredState(this);
									}
									case A.Core.Model.AddressStateMachineEnum.Verified:
									{
										return new AddressStateMachineVerifiedState(this);
									}
									case A.Core.Model.AddressStateMachineEnum.Invalid:
									{
										return new AddressStateMachineInvalidState(this);
									}
									default:
									{
										throw new ApplicationException("Invalid stateId:" + stateId);
									}
								}
	
								throw new ApplicationException("States undefined");
							}
	
					}
	
					public partial class AddressService
					{
						[Dependency]
						public AddressStateMachine AddressStateMachineInstance { get; set; }
						public virtual Address Start(A.Core.Model.Requests.AddressStartRequest request)
						{
							var entity = CreateNewInstance();
	
							AddressStateMachineInstance.Initialize(entity);
	
							AddressStateMachineInstance.ProcessTrigger(new AddressStateMachineStartTrigger() { Request = request });
							Entity.Attach(entity);
	                        Context.Entry(entity).State = EntityState.Added;
							Save(entity);
							return entity;
						}
						public virtual Address Verify(System.Object id,A.Core.Model.Requests.AddressVerifyRequest request)
						{
							var entity = Get(id);
							if(entity != null)
							{
								AddressStateMachineInstance.Initialize(entity);
	
								AddressStateMachineInstance.ProcessTrigger(new AddressStateMachineVerifyTrigger() { Request = request });
								
								Entity.Attach(entity);
								Context.Entry(entity).State = EntityState.Modified;
							
								Save(entity);
							}
							else
							{
								throw new ApplicationException("Entity not found!");
							}
							return entity;
						}
						public virtual Address MarkAsInvalid(System.Object id,A.Core.Model.Requests.AddressMarkAsInvalidRequest request)
						{
							var entity = Get(id);
							if(entity != null)
							{
								AddressStateMachineInstance.Initialize(entity);
	
								AddressStateMachineInstance.ProcessTrigger(new AddressStateMachineMarkAsInvalidTrigger() { Request = request });
								
								Entity.Attach(entity);
								Context.Entry(entity).State = EntityState.Modified;
							
								Save(entity);
							}
							else
							{
								throw new ApplicationException("Entity not found!");
							}
							return entity;
						}
						
					}

}
