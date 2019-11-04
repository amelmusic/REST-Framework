using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PermissionModule.Model;
using PermissionModule.Model.Requests;
using X.Core.Generator.Attributes;
using X.Core.Interface;

namespace PermissionModule.Interfaces
{
    //[Service(Behaviour = EntityBehaviourEnum.Empty, ResourceName = "PermissionChecker")]
    public partial interface IPermissionChecker : IService, X.Core.Interface.IPermissionChecker
    {
        [MethodBehaviour(Behaviour = BehaviourEnum.Get)]
        Task<PermissionCheckResult> IsAllowed(Model.Requests.PermissionCheckRequest request);
    }
}
