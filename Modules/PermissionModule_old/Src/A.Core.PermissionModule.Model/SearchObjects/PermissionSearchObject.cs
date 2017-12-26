using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model.SearchObjects
{
    public partial class PermissionSearchObject
    {
        /// <summary>
        /// Retreives complete hierarchy. For example Account.Get, Account.*, *
        /// </summary>
        public string NameWithHierarchy { get; set; }
    }
}
