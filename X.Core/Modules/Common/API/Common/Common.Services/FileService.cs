using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Model;
using Common.Model.Requests;
using Common.Services.Database;
using Common.Services.StorageProviders;
using X.Core.Model;

namespace Common.Services
{
    partial class FileService
    {
        public Lazy<ILifetimeScope> LifetimeScope { get; set; }

        public override async Task BeforeInsertInternal(FileInsertRequest request, Database.File internalEntity)
        {
            //we copy metadata from file here
            internalEntity.MimeType = request.File.ContentType;
            internalEntity.Title = request.File.FileName;
            internalEntity.StorageType = internalEntity.StorageType?.ToUpper() ?? "DB";
            internalEntity.Size = request.File.Length;

            var provider = CreateNewStorageProvider(internalEntity);
            var content = await provider.Save(request.File);
            internalEntity.FileContentId = content.Id;
            await base.BeforeInsertInternal(request, internalEntity);
        }

        public async Task<DownloadRequest> Download(long id)
        {
            var file = await GetByIdInternalAsync(id);
            var provider = CreateNewStorageProvider(file);

            return await provider.DownloadFile(file);
        }

        protected virtual IStorageProvider CreateNewStorageProvider(Database.File template)
        {
            var documentGeneratorName = $"Common.Services.StorageProviders.{template.StorageType}";
            IStorageProvider generator = LifetimeScope.Value.ResolveNamed<IStorageProvider>(documentGeneratorName);
            return generator;
        }
    }
}
