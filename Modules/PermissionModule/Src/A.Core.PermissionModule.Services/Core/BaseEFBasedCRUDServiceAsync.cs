using A.Core.Interface;
using A.Core.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using AutoMapper.Internal;


namespace A.Core.PermissionModule.Services.Core
{
    public partial class BaseEFBasedCRUDServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate, TDBContext> : BaseEFBasedReadServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TDBContext>, ICRUDServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate>
        where TEntity : class, new()
        where TSearchAdditionalData : BaseAdditionalSearchRequestData, new()
        where TDBContext : DbContext, new()
        where TSearchObject : BaseSearchObject<TSearchAdditionalData>, new()
    {
        // ReSharper disable once StaticMemberInGenericType
        public static IMapper Mapper { get; set; }
        static BaseEFBasedCRUDServiceAsync()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInsert, TEntity>().ForAllMembers(opt => opt.Condition(
                    context => ((context.PropertyMap.DestinationPropertyType.IsNullableType() && !context.IsSourceValueNull)
                                || context.SourceType.IsClass && !context.IsSourceValueNull)
                                || (context.SourceType.IsValueType
                                   && !context.IsSourceValueNull && !context.SourceValue.Equals(Activator.CreateInstance(context.SourceType))
                               )));
                cfg.CreateMap<TUpdate, TEntity>().ForAllMembers(opt => opt.Condition(
                    context => ((context.PropertyMap.DestinationPropertyType.IsNullableType() && !context.IsSourceValueNull)
                               || context.SourceType.IsClass && !context.IsSourceValueNull)
                               || (context.SourceType.IsValueType
                                  && !context.IsSourceValueNull && !context.SourceValue.Equals(Activator.CreateInstance(context.SourceType))
                               )));
            });

            Mapper = config.CreateMapper();
        }

        [A.Core.Interceptors.LogInterceptor(AspectPriority = 0)]
        [A.Core.Interceptors.TransactionInterceptorAsync(AspectPriority = 1)]

        public virtual async Task<TEntity> InsertAsync(TInsert request, bool saveChanges = true)
        {
            TEntity entity = CreateNewInstance();
            if (entity != null)
            {
                Mapper.Map<TInsert, TEntity>(request, entity);
                var validationResult = await ValidateInsertAsync(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new A.Core.Validation.ValidationException(validationResult);
                }
                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Added;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return entity;
        }

        [A.Core.Interceptors.LogInterceptor(AspectPriority = 0)]
        [A.Core.Interceptors.TransactionInterceptorAsync(AspectPriority = 1)]

        public virtual async Task<TEntity> UpdateAsync(object id, TUpdate request, bool saveChanges = true)
        {
            var entity = await GetAsync(id);
            if (entity != null)
            {
                Mapper.Map<TUpdate, TEntity>(request, entity);
                var validationResult = await ValidateUpdateAsync(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new A.Core.Validation.ValidationException(validationResult);
                }
                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return entity;
        }

        public virtual async Task<A.Core.Validation.ValidationResult> ValidateInsertAsync(TInsert request, TEntity entity)
        {
            A.Core.Validation.ValidationResult result = new A.Core.Validation.ValidationResult();

            var context = new ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);
            if (!isValid)
            {
                validationResults.ForEach(x => { result.ResultList.Add(new A.Core.Validation.ValidationResultItem() { Key = x.MemberNames.FirstOrDefault(), Description = x.ErrorMessage }); });
            }
            return await Task.FromResult(result);
        }

        public virtual async Task<A.Core.Validation.ValidationResult> ValidateUpdateAsync(TUpdate request, TEntity entity)
        {
            A.Core.Validation.ValidationResult result = new A.Core.Validation.ValidationResult();

            var context = new ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);
            if (!isValid)
            {
                validationResults.ForEach(x => { result.ResultList.Add(new A.Core.Validation.ValidationResultItem() { Key = x.MemberNames.FirstOrDefault(), Description = x.ErrorMessage }); });
            }
            return await Task.FromResult(result);
        }
    }
}
