using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PermissionModule.Interfaces
{
    partial interface IRoleService
    {
        Task<IList<string>> ChildrenRoles(string role);
        Task<IList<string>> ParentRoles(string role);
    }
}
