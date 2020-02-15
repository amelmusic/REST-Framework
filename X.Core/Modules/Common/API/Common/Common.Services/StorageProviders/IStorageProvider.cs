using Common.Services.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;
using X.Core.Model;
using File = Common.Services.Database.File;

namespace Common.Services.StorageProviders
{
    public interface IStorageProvider
    {
        Task<FileContent> Save(IFormFile stream);
        Task<DownloadRequest> DownloadFile(File id); //download by fileId
    }
}
