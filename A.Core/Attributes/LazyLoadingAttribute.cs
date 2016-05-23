using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    /// <summary>
    /// Determines is property subresource on given entity. If it is, then it can be lazy loaded
    /// </summary>
    public class LazyLoadingAttribute : Attribute
    {
        public LazyLoadingAttribute(bool isLoadedByDefault = false)
        {
            IsLoadedByDefault = isLoadedByDefault;
        }
        /// <summary>
        /// If true, subresource will be loaded by default. That state can be overriden in searchobject
        /// </summary>
        public bool IsLoadedByDefault { get; set; }
    }
}
