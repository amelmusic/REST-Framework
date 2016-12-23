using A.Core.Interface;
using A.Core.Model;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using A.Core.Interceptors;

namespace A.Core.Services.Core
{
    /// <summary>
    /// Base implementation for IReadService and ICRUDService
    /// </summary>
    public partial class BaseEFBasedReadServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TDBContext> : IReadServiceAsync<TEntity, TSearchObject, TSearchAdditionalData>
        where TEntity : class, new()
        where TSearchAdditionalData : BaseAdditionalSearchRequestData, new()
        where TDBContext : DbContext, new()
        where TSearchObject : BaseSearchObject<TSearchAdditionalData>, new()
    {
        [Dependency]
        public Lazy<IActionContext> ActionContext { get; set; }

        public virtual int DefaultPageSize { get; set; }
        DbSet<TEntity> mEntity = null;
        protected virtual DbSet<TEntity> Entity
        {
            get
            {
                if (mEntity == null)
                {
                    mEntity = Context.Set<TEntity>();
                }
                return mEntity;
            }
        }
        protected virtual TEntity CreateNewInstance()
        {
            TEntity entity = new TEntity();
            return entity;
        }

        [Dependency]
        public TDBContext Context { get; set; }

        public BaseEFBasedReadServiceAsync()
        {
            DefaultPageSize = 100;
        }

        public virtual async Task SaveChangesAsync()
        {
            if (Context != null)
            {
                await this.Context.SaveChangesAsync();
            }
        }

        public virtual async Task SaveAsync(TEntity entity)
        {
            if (entity is BaseEntityWithDateTokens)
            {
                var tmpEntity = entity as BaseEntityWithDateTokens;
                if (tmpEntity.CreatedDate == DateTime.MinValue)
                {
                    tmpEntity.CreatedDate = DateTime.UtcNow;
                }

                tmpEntity.ModifiedDate = DateTime.UtcNow;
            }

            var validationResult = await ValidateAsync(entity);
            if (validationResult.HasErrors)
            {
                throw new A.Core.Validation.ValidationException(validationResult);
            }
            await this.SaveChangesAsync();
        }
        public virtual async Task<A.Core.Validation.ValidationResult> ValidateAsync(object entity)
        {
            A.Core.Validation.ValidationResult result = new A.Core.Validation.ValidationResult();

            var context = new ValidationContext(entity, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(entity, context, validationResults, true);
            if (!isValid)
            {
                validationResults.ForEach(x => { result.ResultList.Add(new A.Core.Validation.ValidationResultItem() { Key = x.MemberNames.First(), Description = x.ErrorMessage }); });
            }
            return await Task.FromResult(result);
        }

        [TransactionInterceptor]
        public virtual async Task<TEntity> GetAsync(object id, TSearchAdditionalData additionalData = null)
        {
            if (additionalData != null)
            {

            }
            if (additionalData != null && additionalData.IncludeList.Count > 0)
            {
                //TODO: Implement lazy loading
                throw new ApplicationException("Not yet supported :(");
            }
            else
            {
                return await GetByIdInternalAsync(id);
            }
        }

        /// <summary>
        /// Simple getbyid, suitable for override when you want to include caching
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdInternalAsync(object id)
        {
            return await Entity.FindAsync(id);
        }

        public virtual async Task<IQueryable<TEntity>> GetAsync(TSearchObject search)
        {
            var query = Entity.AsQueryable();
            query = await AddFilterAsync(search, query);
            AddInclude(search.AdditionalData, ref query);
            AddOrder(search, ref query);
            return await Task.FromResult(query);
        }

        /// <summary>
        /// Adds include based on IncludeList from search object
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        protected virtual void AddInclude(TSearchAdditionalData searchAdditionalData, ref IQueryable<TEntity> query)
        {
            var include = GetIncludeList(searchAdditionalData);
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
        }

        protected virtual IList<string> GetIncludeList(TSearchAdditionalData searchAdditionalData)
        {
            var include = searchAdditionalData.IncludeList.ToArray();
            return include;
        }

        protected virtual async Task<IQueryable<TEntity>> AddFilterAsync(TSearchObject search, IQueryable<TEntity> query)
        {
            AddFilterFromGeneratedCode(search, ref query);

            return await Task.FromResult(query);
        }
        protected virtual void AddFilterFromGeneratedCode(TSearchObject search, ref IQueryable<TEntity> query)
        {

        }

        protected virtual void AddOrder(TSearchObject search, ref IQueryable<TEntity> query)
        {
            if (!string.IsNullOrWhiteSpace(search.OrderBy))
            {
                var items = search.OrderBy.Split(' ');
                if (items.Length > 2 || items.Length == 0)
                {
                    throw new ApplicationException("You can only sort by one field");
                }
                if (items.Length == 1)
                {
                    query = query.OrderBy("@0", search.OrderBy);
                }
                else
                {
                    query = query.OrderBy(string.Format("{0} {1}", items[0], items[1]));
                }
            }
            else
            {
                //If client didn't specify order, we wil add default one
                var propertyList =
                    typeof(TEntity).GetProperties().Where(x => x.GetCustomAttributes(typeof(KeyAttribute), true).Count() > 0);
                if (propertyList.Count() != 1)
                {
                    throw new System.Exception(
                        string.Format(
                            "KeyAttribute not found, or found multiple times on: {0}. Please override this implementation",
                            typeof(TEntity)));
                }

                search.OrderBy = propertyList.First().Name;
                query = query.OrderBy(search.OrderBy);
            }
        }

        public virtual async Task<PagedResult<TEntity>> GetPageAsync(TSearchObject search)
        {
            if (search == null)
            {
                search = new TSearchObject(); //if we don't get search object, instantiate default
            }
            PagedResult<TEntity> result = new PagedResult<TEntity>();
            var query = await GetAsync(search);
            if (search.IncludeCount.GetValueOrDefault(false) == true)
            {
                result.Count = await GetCountAsync(query);
            }
            AddPaging(search, ref query);
            result.ResultList = await query.ToListAsync();
            result.HasMore = result.ResultList.Count >= search.PageSize.GetValueOrDefault(DefaultPageSize) && result.ResultList.Count > 0;

            return result;
        }


        protected virtual async Task<long> GetCountAsync(IQueryable<TEntity> query)
        {
            return await query.LongCountAsync();
        }

        public virtual void AddPaging(TSearchObject search, ref IQueryable<TEntity> query)
        {
            if (!search.RetrieveAll.GetValueOrDefault(false) == true)
            {
                query = query.Skip(search.Page.GetValueOrDefault(0)
                    * search.PageSize.GetValueOrDefault(DefaultPageSize))
                    .Take(search.PageSize.GetValueOrDefault(DefaultPageSize));
            }
        }

        public bool BeginTransaction()
        {
            object transactionStarted = false;
            bool exists = this.ActionContext.Value.Data.TryGetValue("CORE_TRANSACTION_STARTED", out transactionStarted);
            if (exists && (bool)transactionStarted)
            {
                return false;
            }
            else
            {
                this.Context.Database.BeginTransaction();
                this.ActionContext.Value.Data["CORE_TRANSACTION_STARTED"] = true;
                return true;
            }

        }

        public void CommitTransaction()
        {
            if (this.Context.Database.CurrentTransaction != null)
            {
                this.Context.Database.CurrentTransaction.Commit();
                this.ActionContext.Value.Data["CORE_TRANSACTION_STARTED"] = false;
            }
        }

        public void RollbackTransaction()
        {
            if (this.Context.Database.CurrentTransaction != null)
            {
                this.Context.Database.CurrentTransaction.Rollback();
                this.ActionContext.Value.Data["CORE_TRANSACTION_STARTED"] = false;
            }
        }

        public void DisposeTransaction()
        {
            if (this.Context.Database.CurrentTransaction != null)
            {
                this.Context.Database.CurrentTransaction.Dispose();

            }
        }
    }
}
