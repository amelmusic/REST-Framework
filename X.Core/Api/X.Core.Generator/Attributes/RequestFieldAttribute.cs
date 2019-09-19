using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Property, Inherited = true,
        AllowMultiple = true)]
    public class RequestFieldAttribute : Attribute
    {
        /// <summary>
        /// Name of the request where this property should be generated
        /// </summary>
        public string RequestName { get; set; }
    }
}
