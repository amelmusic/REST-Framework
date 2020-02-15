using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common.Model;
using Common.Model.Requests;
using X.Core.Interface;
using RazorLight;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Linq;
using Common.Services.Database;
using X.Core.Model;

namespace Common.Services.DocumentGenerators
{
    public class HTMLGenerator : IDocumentGenerator
    {
        public Lazy<IActionContext> ActionContext { get; set; }

        public async Task<DownloadRequest> Generate(TemplateGenerateRequest request, Model.Template template)
        {
            MemoryStream data = null;
            var engine = new RazorLightEngineBuilder()
              // required to have a default RazorLightProject type, but not required to create a template from string.
              .UseEmbeddedResourcesProject(typeof(HTMLGenerator))
              .UseMemoryCachingProvider()
              .Build();

            dynamic model = PrepareContent(request);

            string content = null;
            var cacheResult = engine.Handler.Cache.RetrieveTemplate(template.Code);
            if (cacheResult.Success)
            {
                content = await engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
            }
            else
            {
                content = await engine.CompileRenderStringAsync(template.Code, template.Content, model);
            }
            //engine.ge
            byte[] byteArray = Encoding.UTF8.GetBytes(content);
            data = new MemoryStream(byteArray);

            var invalidChars = System.IO.Path.GetInvalidFileNameChars().ToList();
            invalidChars.Add('č');
            invalidChars.Add('ć');
            invalidChars.Add('ž');
            invalidChars.Add('đ');
            var fileName = template.Description + " " + Guid.NewGuid() + ".html";
            fileName = fileName.ToLower();
            var cleanFileName = new string(fileName.Where(m => !invalidChars.Contains(m)).ToArray<char>());

            var downloadResponse = new DownloadRequest();
            downloadResponse.FileName = $"{cleanFileName}";
            downloadResponse.ContentType = "application/octet-stream";
            downloadResponse.Stream = data;

            return downloadResponse;
        }

        protected virtual dynamic PrepareContent(TemplateGenerateRequest request)
        {
            dynamic data = new { Data = request.Data, User = PrepareUserInfo() };

            return data;
        }

        protected dynamic PrepareUserInfo()
        {
            if (ActionContext.Value.Data.ContainsKey("SYS_CLAIMS"))
            {
                IEnumerable<Claim> claims = ActionContext.Value.Data["SYS_CLAIMS"] as IEnumerable<Claim>;
                var givenName = claims.FirstOrDefault(x => x.Type == "given_name")?.Value;
                var lastName = claims.FirstOrDefault(x => x.Type == "family_name")?.Value;
                var username = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier || x.Type == "sub")?.Value;

                var reportDate = DateTime.Now;

                dynamic userData = new
                {
                    FirstName = givenName,
                    LastName = lastName,
                    Username = username,
                    ReportDate = reportDate,
                    Name = $"{givenName} {lastName}"
                };

                return userData;
            }

            return null;
        }
    }
}
