using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.Services.Core
{
    public partial class BaseEFBasedCRUDService<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate, TDBContext, TDbEntity> : BaseEFBasedReadService<TEntity, TSearchObject, TSearchAdditionalData, TDBContext, TDbEntity>, ICRUDService<TEntity, TSearchObject, TSearchAdditionalData, TInsert, TUpdate>
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
        public static IMapper RequestMapper { get; set; }
        // ReSharper disable once StaticMemberInGenericType
        public static IMapper AllFieldsMapper { get; set; }
        static BaseEFBasedCRUDService()
        {

            var config = new MapperConfiguration(cfg =>
            {
                //cfg.ForAllPropertyMaps(pm => !pm.HasSource(cfg),
                //    (pm, opt) => opt.UseDestinationValue());
                cfg.CreateMap<long?, long>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<short?, short>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<byte?, byte>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<bool?, bool>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<decimal?, decimal>().ConvertUsing((src, dest) => src ?? dest);
                cfg.CreateMap<DateTime?, DateTime>().ConvertUsing((src, dest) => src ?? dest);

                cfg.CreateMap<TInsert, TDbEntity>().ForAllMembers(opt => opt.Condition(
                    (src, dest, srcVal) => { return srcVal != null; }));
                cfg.CreateMap<TUpdate, TDbEntity>().ForAllMembers(opt => opt.Condition(
                    (src, dest, srcVal, dstVal, ctx) => { return srcVal != null; }));
            });


            RequestMapper = config.CreateMapper();

            var configAllFields = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TInsert, TDbEntity>();
                cfg.CreateMap<TUpdate, TDbEntity>();
            });

            AllFieldsMapper = configAllFields.CreateMapper();
        }

        //[Transaction]
        public virtual async Task<TEntity> InsertAsync(TInsert request, bool saveChanges = true)
        {
            TDbEntity entity = CreateNewInstance();
            if (entity != null)
            {
                var validationResult = await ValidateInsertAsync(request, entity);
                MapInsert(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new X.Core.Validation.ValidationException(validationResult);
                }
                await BeforeInsertInternal(request, entity);

                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Added;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return Mapper.Map<TEntity>(entity);
        }

        protected virtual void MapInsert(TInsert request, TDbEntity entity)
        {
            RequestMapper.Map<TInsert, TDbEntity>(request, entity);
        }

#pragma warning disable 1998
        public virtual async Task BeforeInsertInternal(TInsert request, TDbEntity internalEntity)
#pragma warning restore 1998
        {

        }

        //[Transaction]
        public virtual async Task<TEntity> UpdateAsync(object id, TUpdate request, bool saveChanges = true)
        {
            var entity = await GetByIdInternalAsync(id);
            if (entity != null)
            {
                var validationResult = await ValidateUpdateAsync(request, entity);
                MapUpdate(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new X.Core.Validation.ValidationException(validationResult);
                }
                await BeforeUpdateInternal(request, entity);
                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return Mapper.Map<TEntity>(entity);
        }

        protected virtual void MapUpdate(TUpdate request, TDbEntity entity)
        {
            AllFieldsMapper.Map<TUpdate, TDbEntity>(request, entity);
        }

        public virtual async Task<TEntity> PatchAsync(object id, TUpdate request, bool saveChanges = true)
        {
            var entity = await GetByIdInternalAsync(id);
            if (entity != null)
            {
                var validationResult = await ValidateUpdateAsync(request, entity);
                MapPatch(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new X.Core.Validation.ValidationException(validationResult);
                }
                await BeforeUpdateInternal(request, entity);
                Entity.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;
                if (saveChanges)
                {
                    await SaveAsync(entity);
                }
            }
            return Mapper.Map<TEntity>(entity);
        }

        protected virtual void MapPatch(TUpdate request, TDbEntity entity)
        {
            RequestMapper.Map<TUpdate, TDbEntity>(request, entity);
        }

        public virtual async Task BeforeUpdateInternal(TUpdate request, TDbEntity internalEntity)
        {

            return;
        }

        public virtual async Task<X.Core.Validation.ValidationResult> ValidateInsertAsync(TInsert request, TDbEntity entity)
        {
            X.Core.Validation.ValidationResult result = new X.Core.Validation.ValidationResult();

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);
            if (!isValid)
            {
                validationResults.ForEach(x => { result.ResultList.Add(new X.Core.Validation.ValidationResultItem() { Key = x.MemberNames.FirstOrDefault(), Description = x.ErrorMessage }); });
            }
            return await Task.FromResult(result);
        }

        public virtual async Task<X.Core.Validation.ValidationResult> ValidateUpdateAsync(TUpdate request, TDbEntity entity)
        {
            X.Core.Validation.ValidationResult result = new X.Core.Validation.ValidationResult();

            var context = new System.ComponentModel.DataAnnotations.ValidationContext(request, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(request, context, validationResults, true);
            if (!isValid)
            {
                validationResults.ForEach(x => { result.ResultList.Add(new X.Core.Validation.ValidationResultItem() { Key = x.MemberNames.FirstOrDefault(), Description = x.ErrorMessage }); });
            }
            return await Task.FromResult(result);
        }
    }
}
