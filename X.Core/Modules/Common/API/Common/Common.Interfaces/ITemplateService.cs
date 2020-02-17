using Common.Model;
using Common.Model.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;
using X.Core.Model;

namespace Common.Interfaces
{
    partial interface ITemplateService
    {
        [MethodBehaviour(Behaviour = BehaviourEnum.DownloadAsPost)]
        Task<DownloadRequest> Generate(TemplateGenerateRequest request);

        [MethodBehaviour(Behaviour = BehaviourEnum.Get)]
        Task<TemplateGenerateAsString> GenerateAsString(TemplateGenerateRequest request);

        [MethodBehaviour(Behaviour = BehaviourEnum.Insert)]
        Task<TemplateGenerateAsString> GenerateAsPost(TemplateGenerateRequest request);
    }
}
