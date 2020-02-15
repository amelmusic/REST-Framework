using Common.Model.SearchObjects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;

namespace Common.Interfaces
{
    partial interface IStaticDataService
    {
        [MethodBehaviour(Behaviour = BehaviourEnum.Get)]
        Task<dynamic> Content(Common.Model.SearchObjects.StaticDataSearchObject search);
    }
}
