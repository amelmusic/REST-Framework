using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PermissionModule.Interfaces
{
    partial interface IRoleRelationsService
    {
        Task<IList<string>> ChildrenRoles(int roleId, short? level);

        Task<IList<string>> ParentRoles(int roleId, short? level);
    }
}
