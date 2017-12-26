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
using A.Core;
using A.Core.Interceptors;
using Autofac.Extras.DynamicProxy;

namespace A.Core.Services.Core
{
    public partial class BaseEFBasedCRUDServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate, TDBContext, TDbEntity> : BaseEFBasedReadServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TDBContext, TDbEntity>, ICRUDServiceAsync<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate>
        where TEntity : class, new()
        where TDbEntity : class, new()
        where TSearchAdditionalData : BaseAdditionalSearchRequestData, new()
        where TDBContext : DbContext, new()
        where TSearchObject : BaseSearchObject<TSearchAdditionalData>, new()
    {
        class IgnoreNullSourceValues : IMemberValueResolver<TUpdate, TDbEntity, object, object>
        {
            public object Resolve(object source, object destination, object sourceMember, object destinationMember, ResolutionContext context)
            {
                return sourceMember ?? destinationMember;
            }

            public object Resolve(TUpdate source, TDbEntity destination, object sourceMember, object destMember, ResolutionContext context)
            {
                return sourceMember ?? destMember;
            }
        }

        // ReSharper disable once StaticMemberInGenericType
        public static IMapper Mapper { get; set; }
        static BaseEFBasedCRUDServiceAsync()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.ForAllPropertyMaps(pm => !pm.HasSource(),
                        (pm, opt) => opt.UseDestinationValue());
                cfg.CreateMap<long?, long>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<short?, short>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<byte?, byte>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);


                cfg.CreateMap<TInsert, TDbEntity>().ForAllMembers(opt => opt.Condition(
                    (src, dest, srcVal) => { return srcVal != null; }));
                cfg.CreateMap<TUpdate, TDbEntity>().ForAllMembers(opt => opt.Condition(
                     (src, dest, srcVal, dstVal, ctx) => { return srcVal != null; }));
            });


            Mapper = config.CreateMapper();
        }

        [Transaction]

        public virtual async Task<TEntity> InsertAsync(TInsert request, bool saveChanges = true)
        {
            TDbEntity entity = CreateNewInstance();
            if (entity != null)
            {
                Mapper.Map<TInsert, TDbEntity>(request, entity);
                var validationResult = await ValidateInsertAsync(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new A.Core.Validation.ValidationException(validationResult);
                }
                await BeforeInsertInternal(request, entity);

                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Added;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return GlobalMapper.Mapper.Map<TEntity>(entity);
        }

        public virtual async Task BeforeInsertInternal(TInsert request, TDbEntity internalEntity)
        {

            return;
        }

        [Transaction]
        public virtual async Task<TEntity> UpdateAsync(object id, TUpdate request, bool saveChanges = true)
        {
            var entity = await GetByIdInternalAsync(id);
            if (entity != null)
            {
                Mapper.Map<TUpdate, TDbEntity>(request, entity);
                var validationResult = await ValidateUpdateAsync(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new A.Core.Validation.ValidationException(validationResult);
                }
                await BeforeUpdateInternal(request, entity);
                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return GlobalMapper.Mapper.Map<TEntity>(entity);
        }

        public virtual async Task BeforeUpdateInternal(TUpdate request, TDbEntity internalEntity)
        {

            return;
        }

        public virtual async Task<A.Core.Validation.ValidationResult> ValidateInsertAsync(TInsert request, TDbEntity entity)
        {
            A.Core.Validation.ValidationResult result = new A.Core.Validation.ValidationResult();

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);
            if (!isValid)
            {
                validationResults.ForEach(x => { result.ResultList.Add(new A.Core.Validation.ValidationResultItem() { Key = x.MemberNames.FirstOrDefault(), Description = x.ErrorMessage }); });
            }
            return await Task.FromResult(result);
        }

        public virtual async Task<A.Core.Validation.ValidationResult> ValidateUpdateAsync(TUpdate request, TDbEntity entity)
        {
            A.Core.Validation.ValidationResult result = new A.Core.Validation.ValidationResult();

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
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
