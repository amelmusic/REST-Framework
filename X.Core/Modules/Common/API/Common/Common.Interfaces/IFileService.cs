using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;
using X.Core.Model;

namespace Common.Interfaces
{
    public partial interface IFileService
    {
        [MethodBehaviour(Behaviour = BehaviourEnum.Download)]
        Task<DownloadRequest> Download(long id);
    }
}
