using Common.Model;
using Common.Model.Requests;
using Common.Services.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using X.Core.Model;

namespace Common.Services.DocumentGenerators
{
    public interface IDocumentGenerator
    {
        Task<DownloadRequest> Generate(TemplateGenerateRequest request, Model.Template template);
    }
}
