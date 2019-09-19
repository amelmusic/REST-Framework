using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    public class MetadataAttribute : Attribute
    {
        public string Key { get; set; }
        public string KeyType { get; set; }
    }
}
