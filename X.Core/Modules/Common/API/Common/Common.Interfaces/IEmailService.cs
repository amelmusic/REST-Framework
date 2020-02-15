using Common.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;

namespace Common.Interfaces
{
    public partial interface IEmailService
    {
        [MethodBehaviour(Behaviour = BehaviourEnum.Update)]
        Task<int> Send(int id);
    }
}
