using System;
using System.IO;
using System.Threading.Tasks;

namespace X.Core.Model
{
    /// <summary>
    /// Contains necessary download data
    /// </summary>
    public class DownloadRequest
    {
        public string Name { get; set; }
        public long Length { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        /// <summary>
        /// Default disposition type is attachment
        /// </summary>
        public string ContentDisposition { get; set; } = "attachment";
        public Stream Stream { get; set; }
    }
}
