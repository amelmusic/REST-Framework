using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionModule.Model.Requests
{
    public class PermissionCheckRequest
    {
        public string Permission { get; set; }
        /// <summary>
        /// Custom operation flag like Add, Edit, Delete, etc...
        /// </summary>
        public string OperationType { get; set; }

        public bool IsExactMatchRequired { get; set; }

        /// <summary>
        /// If we couldn't resolve permission by assigned roles, resolve by permission default value
        /// </summary>
        public bool IsDefaultResolveModeDisabled { get; set; }
    }
}
