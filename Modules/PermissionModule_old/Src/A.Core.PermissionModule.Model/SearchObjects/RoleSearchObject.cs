using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model.SearchObjects
{
    public partial class RoleSearchObject
    {
        public string PermissionName { get; set; }
        public bool IsRolePermissionsLoadingEnabled { get; set; }
    }
}
