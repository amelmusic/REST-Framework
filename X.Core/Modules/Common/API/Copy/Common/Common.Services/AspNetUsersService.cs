using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Requests;
using Common.Model.SearchObjects;
using Common.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using X.Core.Model;
using X.Core.Validation;

namespace Common.Services
{
    partial class AspNetUsersService
    {
        public UserManager<Database.AspNetUsers> UserManager { get; set; }
        public override async Task<Model.AspNetUsers> InsertAsync(AspNetUsersInsertRequest request, bool saveChanges = true)
        {
            var entity = CreateNewInstance();
            if (entity != null)
            {
                var validationResult = await ValidateInsertAsync(request, entity);
                MapInsert(request, entity);
                if (validationResult.HasErrors)
                {
                    throw new X.Core.Validation.ValidationException(validationResult);
                }
                await BeforeInsertInternal(request, entity);

                entity.EmailConfirmed = true; //TODO: Check this
                //we will reuse asp.net core user manager for persistence
                var result = await this.UserManager.CreateAsync(entity, request.Password);
                
                foreach (var error in result.Errors)
                {
                    validationResult.Error(true, error.Description, error.Code);
                }
                validationResult.StopIfHasErrors();

                await this.UserManager.AddClaimAsync(entity, new System.Security.Claims.Claim("name", $"{request.GivenName} {request.FamilyName}"));
                await this.UserManager.AddClaimAsync(entity, new System.Security.Claims.Claim("given_name", $"{request.GivenName}"));
                await this.UserManager.AddClaimAsync(entity, new System.Security.Claims.Claim("family_name", $"{request.FamilyName}"));

                if (request.Roles != null)
                {
                    foreach(var role in request.Roles)
                    {
                        await this.UserManager.AddClaimAsync(entity, new System.Security.Claims.Claim("roles", $"{role}"));
                    }
                }
            }
            return Mapper.Map<Model.AspNetUsers>(entity);
        }

        public override async Task<Model.AspNetUsers> GetByIdInternalMappedAsync(Database.AspNetUsers item, AspNetUsersAdditionalSearchRequestData additionalData = null)
        {
            var entity = await base.GetByIdInternalMappedAsync(item, additionalData);
            var claims = Context.UserClaims.Where(x => x.UserId == entity.Id);
            entity.AspNetUserClaims = await claims.ToListAsync();
            return entity;
        }

        public override async Task<PagedResult<Model.AspNetUsers>> GetPageAsync(AspNetUsersSearchObject search)
        {
            var list = await base.GetPageAsync(search);
            if (list.ResultList.Count > 0)
            {
                var idList = list.ResultList.Select(x => x.Id).ToList();
                var claims = Context.UserClaims.Where(x => idList.Contains(x.UserId));

                list.ResultList.ToList().ForEach(x =>
                {
                    x.AspNetUserClaims = claims.Where(y => y.UserId == x.Id).ToList();
                });
            }

            return list;
        }
    }
}
