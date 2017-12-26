using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interfaces
{
    public partial interface IPermissionService
    {
        [DefaultMethodBehaviour(BehaviourEnum.Get)]
        int GetCached();
    }
}
