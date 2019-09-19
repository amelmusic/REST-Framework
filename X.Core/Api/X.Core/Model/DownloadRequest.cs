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
        public string FileName { get; set; }
        public string ContentType { get; set; }
        /// <summary>
        /// Default disposition type is attachment
        /// </summary>
        public string DispositionType { get; set; } = "attachment";
        public Func<Stream, Task> PushStreamFunction { get; set; }
    }
}
