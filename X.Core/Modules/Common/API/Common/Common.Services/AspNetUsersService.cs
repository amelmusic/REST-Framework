using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Model;
using Common.Model.Requests;
using Common.Model.SearchObjects;
using Common.Services.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PermissionModule.Services.Database;
using X.Core.Model;
using X.Core.Validation;

namespace Common.Services
{
    partial class AspNetUsersService
    {
        public UserManager<Database.AspNetUsers> UserManager { get; set; }
        public RoleManager<IdentityRole> RoleManager { get; set; }
        public IEmailService EmailService { get; set; }
        public IConfiguration Configuration { get; set; }
        public ITemplateService TemplateService { get; set; }
        public PermissionModuleContext PermissionModuleContext { get; set; }
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

                //we will reuse asp.net core user manager for persistence
                var result = await this.UserManager.CreateAsync(entity, request.Password);
                
                foreach (var error in result.Errors)
                {
                    validationResult.Error(true, error.Description, error.Code);
                }
                validationResult.StopIfHasErrors();
                
                if (request.Roles != null)
                {
                    await AddRolesIfNeeded(request.Roles);
                    if (request.Roles.Count == 0)
                    {
                        await this.UserManager.AddToRoleAsync(entity, "User");
                    }
                    else
                    {
                        await this.UserManager.AddToRolesAsync(entity, request.Roles.Select(x => x.Id));
                    }
                }

                if (!entity.EmailConfirmed)
                {
                    var token = await this.UserManager.GenerateEmailConfirmationTokenAsync(entity);

                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

                    var url = $"{Configuration["Application:BaseUrl"]}api/AspNetUsers/{entity.Id}/ConfirmEmail?Token={token}";
                    url = HtmlEncoder.Default.Encode(url);

                    var data = new
                    {
                        Url = url,
                        User = entity
                    };

                    TemplateGenerateRequest template = new TemplateGenerateRequest();
                    template.Data = data;
                    template.Code = "REG_EMAIL";
                    var emailContent = await TemplateService.GenerateAsString(template);

                    EmailInsertRequest email = new EmailInsertRequest();
                    email.To = entity.Email;
                    email.Subject = "Register";
                    email.Content = emailContent.Content;
                    var e = await EmailService.InsertAsync(email);
                    await EmailService.Send(e.Id);
                }

                await Context.SaveChangesAsync();
            }
            return Mapper.Map<Model.AspNetUsers>(entity);
        }

        public override async Task<Model.AspNetUsers> UpdateAsync(object id, AspNetUsersUpdateRequest request, bool saveChanges = true)
        {
            var user = await base.UpdateAsync(id, request, saveChanges);
            var internalUser = await GetByIdInternalAsync(id);

            if (request.Roles != null)
            {
                await AddRolesIfNeeded(request.Roles);
                //if role does not exist in new request, remove it
                var requestRoles = request.Roles.Select(x => x.Id).ToList();
                var existingRoles = await this.Context.UserRoles.Where(x => x.UserId == user.Id).ToListAsync();
                foreach(var role in existingRoles)
                {
                    await UserManager.RemoveFromRoleAsync(internalUser, role.RoleId);
                }
                if(request.Roles.Count == 0)
                {
                    await this.UserManager.AddToRoleAsync(internalUser, "User");
                }
                else
                {
                    await this.UserManager.AddToRolesAsync(internalUser, request.Roles.Select(x => x.Id));
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                var hash = UserManager.PasswordHasher.HashPassword(internalUser, request.Password);
                internalUser.PasswordHash = hash;
                await SaveAsync(internalUser);
            }
            return user;
        }

        public async Task<string> ConfirmEmail(string id, string code)
        {
            var user = await this.UserManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new ApplicationException("Invalid user id");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await UserManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                //redirect url
                return Configuration["Application:EmailRegisterConfirmedUrl"];
            }

            throw new UserException("Invalid code");
        }

        public override async Task<Model.AspNetUsers> GetByIdInternalMappedAsync(Database.AspNetUsers item, AspNetUsersAdditionalSearchRequestData additionalData = null)
        {
            var entity = await base.GetByIdInternalMappedAsync(item, additionalData);
            var claims = Context.UserClaims.Where(x => x.UserId == entity.Id);
            entity.AspNetUserClaims = await claims.ToListAsync();

            var userRoles = await Context.UserRoles.Where(x => x.UserId == entity.Id).ToListAsync();
            entity.AspNetUserRoles = userRoles;
            return entity;
        }

        public override async Task<PagedResult<Model.AspNetUsers>> GetPageAsync(AspNetUsersSearchObject search)
        {
            //Thread.Sleep(3000);
            var list = await base.GetPageAsync(search);
            if (list.ResultList.Count > 0)
            {
                var idList = list.ResultList.Select(x => x.Id).ToList();
                var claims = Context.UserClaims.Where(x => idList.Contains(x.UserId));
                var roles = Context.UserRoles.Where(x => idList.Contains(x.UserId));

                list.ResultList.ToList().ForEach(x =>
                {
                    x.AspNetUserClaims = claims.Where(y => y.UserId == x.Id).ToList();
                    x.AspNetUserRoles = roles.Where(y => y.UserId == x.Id).ToList();
                });
            }

            return list;
        }

        protected virtual async Task AddRolesIfNeeded(List<AspNetRolesUpsertRequest> roles)
        {
            foreach(var role in roles)
            {
                var existing = await PermissionModuleContext.Role.Where(x=>x.Name == role.Id).FirstOrDefaultAsync();
                if (existing != null)
                {
                    var moved = await RoleManager.RoleExistsAsync(role.Id);
                    if (!moved)
                    {
                        await RoleManager.CreateAsync(new IdentityRole()
                        {
                            Id = role.Id,
                            Name = role.Id,
                            NormalizedName = role.Id.ToUpper()
                        });
                    }
                }
            }
        }

        protected override async Task<IQueryable<Database.AspNetUsers>> AddFilterAsync(AspNetUsersSearchObject search, IQueryable<Database.AspNetUsers> query)
        {
            var filteredQuery = await base.AddFilterAsync(search, query);

            if (!string.IsNullOrWhiteSpace(search.FTS))
            {
                filteredQuery = filteredQuery.Where(x => x.FirstName.Contains(search.FTS)
                        || x.LastName.Contains(search.FTS)
                        || x.Email == search.FTS
                        || x.UserName == search.FTS);
            }

            return filteredQuery;
        }
    }
}
