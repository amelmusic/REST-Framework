using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.Model
{
    public class TemplateDownload
    {
        public string FileName { get; set; }
        public string ContentType { get; set; }
        //
        // Summary:
        //     Default disposition type is attachment
        public string DispositionType { get; set; }
        public Stream Stream { get; set; }
    }
}
