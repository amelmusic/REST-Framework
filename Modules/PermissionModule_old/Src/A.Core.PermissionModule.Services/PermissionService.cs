using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.PermissionModule.Model;
using A.Core.PermissionModule.Model.SearchObjects;

namespace A.Core.PermissionModule.Services
{
    public partial class PermissionService
    {
        protected override void AddFilter(PermissionSearchObject search, ref IQueryable<Permission> query)
        {
            if (!string.IsNullOrWhiteSpace(search.NameWithHierarchy))
            {
                List<string> permissionList = new List<string>();
                search.NameList.Add(search.NameWithHierarchy);
                string[] permissionParts = search.NameWithHierarchy.Split('.');
                StringBuilder previousPermissionPart = new StringBuilder();
                for (int i = 0; i < permissionParts.Length - 1; i++)
                {
                    string permissionPart = permissionParts[i];
                    previousPermissionPart.Append(permissionPart + ".");
                    string permission = previousPermissionPart.ToString() + "*";
                    search.NameList.Add(permission);
                }
                //add root permission to list
                search.NameList.Add("*");
            }

            base.AddFilter(search, ref query);
        }
    }
}
