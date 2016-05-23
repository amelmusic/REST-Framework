using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true,
    AllowMultiple = true)]
    public class RequestFieldAttribute : Attribute
    {
        public RequestFieldAttribute(string requestName)
        {
            RequestName = requestName;
        }
        public RequestFieldAttribute(string requestName, string attributes)
        {
            RequestName = requestName;
            Attributes = attributes;
        }
        protected string RequestName { get; set; }
        protected string Attributes { get; set; }
    }
}
