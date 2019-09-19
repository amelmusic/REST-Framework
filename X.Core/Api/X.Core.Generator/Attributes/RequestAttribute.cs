using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    public class RequestAttribute : Attribute
    {
        public string Name { get; set; }
        /// <summary>
        /// Required scope in order to execute this action. Can be comma separated for "ANY" style
        /// </summary>
        public string Scope { get; set; }
    }
}
