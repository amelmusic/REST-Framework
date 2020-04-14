using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    /// <summary>
    /// Extending search object and query
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public class QueryAttribute : Attribute
    {
        public string Property { get; set; }
        public string PropertyType { get; set; }

        /// <summary>
        /// If this is populated, it will be used instead of standard check is value null
        /// </summary>
        public string PropertyValueCheck { get; set; }
        /// <summary>
        /// query will be generated in format filteredQuery = filteredQuery.Where(x => x.Property == search.FTS)
        /// Calling property must start with an x and search object is accessible from search
        /// </summary>
        public string Query { get; set; }
    }
}
