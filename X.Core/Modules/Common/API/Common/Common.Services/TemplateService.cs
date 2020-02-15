using Autofac;
using Common.Model;
using Common.Model.Requests;
using Common.Model.SearchObjects;
using Common.Services.DocumentGenerators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using X.Core.Model;
using X.Core.Validation;

namespace Common.Services
{
    partial class TemplateService
    {
        public Lazy<ILifetimeScope> LifetimeScope { get; set; }

        public async Task<DownloadRequest> Generate(TemplateGenerateRequest request)
        {
            TemplateSearchObject searchObject = new TemplateSearchObject();
            searchObject.Code = request.Code;

            var template = await GetFirstOrDefaultForSearchObjectAsync(searchObject);

            if (template != null)
            {
                var generator = CreateNewGenerator(template);
                var result = await generator.Generate(request, template);

                return result;
            }
            throw new UserException($"Template {request.Code} not found");
        }

        public async Task<TemplateGenerateAsString> GenerateAsString(TemplateGenerateRequest request)
        {
            TemplateSearchObject searchObject = new TemplateSearchObject();
            searchObject.Code = request.Code;

            var result = await Generate(request);
            StreamReader reader = new StreamReader(result.Stream);
            string text = reader.ReadToEnd();
            return new TemplateGenerateAsString() { Content = text };
        }


        protected virtual IDocumentGenerator CreateNewGenerator(Template template)
        {
            IDocumentGenerator generator = null;

            var documentGeneratorName = $"Common.Services.DocumentGenerators.TemplateType.{template.TemplateTypeId}";

            generator = LifetimeScope.Value.ResolveNamed<IDocumentGenerator>(documentGeneratorName);

            return generator;
        }
    }
}
