using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Common.Services.Database;
using Microsoft.AspNetCore.Http;
using X.Core.Model;

namespace Common.Services.StorageProviders
{
    public class DBStorageProvider : IStorageProvider
    {
        public CommonContext Context { get; set; }
        public async Task<DownloadRequest> DownloadFile(Database.File file)
        {
            DownloadRequest download = new DownloadRequest();
            download.ContentType = file.MimeType;
            download.Length = file.Size;
            download.FileName = file.Title;
            download.Name = file.Title;

            var fileContent = await Context.FileContents.FindAsync(file.FileContentId);
            var stream = new MemoryStream(fileContent.Content);
            download.Stream = stream;
            return download;
        }

        public async Task<FileContent> Save(IFormFile file)
        {
            byte[] content = null;
            using(var stream = file.OpenReadStream())
            {
                using(var buffer = new MemoryStream())
                {
                    await stream.CopyToAsync(buffer);
                    content = buffer.ToArray();
                }
            }

            FileContent fileContent = new FileContent();
            fileContent.Content = content;

            Context.Add(fileContent);
            await Context.SaveChangesAsync();
            return fileContent;
        }
    }
}
