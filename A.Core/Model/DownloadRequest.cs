using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
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
