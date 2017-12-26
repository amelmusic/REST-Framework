using A.Core.Attributes;
using A.Core.PermissionModule.Model;
using A.Core.PermissionModule.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Interfaces
{
    [DefaultServiceBehaviour(DefaultImplementationEnum.WithoutServiceImplementation, "permissionChecker")]
    public partial interface IPermissionChecker
    {
        [DefaultMethodBehaviour(BehaviourEnum.Get)]
        PermissionCheckResult IsAllowed(PermissionCheckRequest request);
    }
}
