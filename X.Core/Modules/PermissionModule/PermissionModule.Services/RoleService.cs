using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PermissionModule.Interfaces;
using PermissionModule.Model.SearchObjects;
using PermissionModule.Services.Database;

namespace PermissionModule.Services
{
    partial class RoleService
    {
        public Lazy<IRoleRelationsService> RoleRelationService { get; set; }
        public override async Task<IQueryable<Role>> GetAsync(RoleSearchObject search)
        {
            var query = await base.GetAsync(search);

            if (!string.IsNullOrWhiteSpace(search.PermissionName))
            {
                List<string> permissionList = new List<string>();
                permissionList.Add(search.PermissionName);
                string[] permissionParts = search.PermissionName.Split('.');
                StringBuilder previousPermissionPart = new StringBuilder();
                for (int i = 0; i < permissionParts.Length - 1; i++)
                {
                    string permissionPart = permissionParts[i];
                    previousPermissionPart.Append(permissionPart + ".");
                    string permission = previousPermissionPart.ToString() + "*";
                    permissionList.Add(permission);
                }
                //add root permission to list
                permissionList.Add("*");
                //TODO: FIX
                //query = query.IncludeFilter(x => x.RolePermissions.Where(y => permissionList.Contains(y.Permission.Name)))
                //    .IncludeFilter(x => x.RolePermissions.Where(y => permissionList.Contains(y.Permission.Name)).Select(y => y.Permission));
            }

            if (!string.IsNullOrWhiteSpace(search.FTS))
            {
                query = query.Where(x => x.Name.Contains(search.FTS));
            }

            if (search.IsAllowedAssigningToUsers.HasValue)
            {
                query = query.Where(x => x.RoleType.IsAllowedAssigningToUsers == search.IsAllowedAssigningToUsers);
            }

            return query;
        }



        public virtual async Task<IList<string>> ChildrenRoles(string role)
        {
            IList<string> list = new List<string>();
            var currentRole = await this.GetFirstOrDefaultForSearchObject(new RoleSearchObject()
            {
                Name = role,
                AdditionalData = new RoleAdditionalSearchRequestData()
                {
                    IncludeList = { "RoleType" }
                }
            });

            if (currentRole == null)
            {
                throw new ApplicationException($"{role} not found!");
            }

            list = await this.RoleRelationService.Value.ChildrenRoles(currentRole.Id,
                currentRole.RoleType.PermissionHierarchyLevel);
            return list;
        }

        public async Task<IList<string>> ParentRoles(string role)
        {
            IList<string> list = new List<string>();
            var currentRole = await this.GetFirstOrDefaultForSearchObject(new RoleSearchObject()
            {
                Name = role,
                AdditionalData = new RoleAdditionalSearchRequestData()
                {
                    IncludeList = { "RoleType" }
                }
            });

            if (currentRole == null)
            {
                throw new ApplicationException($"{role} not found!");
            }

            list = await this.RoleRelationService.Value.ParentRoles(currentRole.Id,
                currentRole.RoleType.PermissionHierarchyLevel);
            return list;
        }

        protected virtual async Task<Model.Role> GetFirstOrDefaultForSearchObject(RoleSearchObject search)
        {
            var query = await GetAsync(search);
            var result = query.FirstOrDefault();
            var resultMapped = Mapper.Map<Model.Role>(result);
            return resultMapped;
        }
    }
}
