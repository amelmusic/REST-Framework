using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model.Requests
{
    public class PermissionCheckRequest
    {
        public string Permission { get; set; }
        public bool IsExactMatchRequired { get; set; }

        /// <summary>
        /// If we couldn't resolve permission by assigned roles, resolve by permission default value
        /// </summary>
        public bool IsDefaultResolveModeDisabled { get; set; }
    }
}
