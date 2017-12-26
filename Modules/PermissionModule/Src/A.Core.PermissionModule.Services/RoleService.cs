using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.PermissionModule.Model;
using A.Core.PermissionModule.Model.SearchObjects;
using Z.EntityFramework.Plus;

namespace A.Core.PermissionModule.Services
{
    public partial class RoleService
    {
        public override IQueryable<Role> Get(RoleSearchObject search)
        {
            var query = base.Get(search);

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

                query = query.IncludeFilter(x => x.RolePermissions.Where(y => permissionList.Contains(y.Permission.Name)))
                            .IncludeFilter(x=> x.RolePermissions.Where(y => permissionList.Contains(y.Permission.Name)).Select( y=>y.Permission));
            }

            return query;
        }

    }
}
