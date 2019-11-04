using System;
using System.Collections.Generic;
using System.Text;

namespace PermissionModule.Model.SearchObjects
{
    partial class PermissionSearchObject
    {
        /// <summary>
        /// Retreives complete hierarchy. For example Account.Get, Account.*, *
        /// </summary>
        public string NameWithHierarchy { get; set; }

        /// <summary>
        /// Fulltext search functionality
        /// </summary>
        public string FTS { get; set; }
    }
}
