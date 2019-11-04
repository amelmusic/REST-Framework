using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionModule.Model.SearchObjects
{
    partial class RoleSearchObject
    {
        public string PermissionName { get; set; }
        public bool IsRolePermissionsLoadingEnabled { get; set; }
        public string FTS { get; set; }
        public bool? IsAllowedAssigningToUsers { get; set; }
    }
}
