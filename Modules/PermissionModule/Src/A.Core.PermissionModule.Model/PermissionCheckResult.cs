using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model
{
    public class PermissionCheckResult
    {
        public bool IsAllowed { get; set; }
        public string RequestedPermission { get; set; }
        public string ResolvedByPermission { get; set; }
        public PermissionResolveMode PermissionResolveMode { get; set; }
    }

    public enum PermissionResolveMode
    {
        Role = 0,
        Default = 1
    }
}
