using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    /// <summary>
    /// Represents result of search request
    /// </summary>
    [Serializable]
    public class PagedResult<TEntity>
    {
        /// <summary>
        /// If enabled, this field will contain total number of rows available for specified search object
        /// </summary>
        public long? Count { get; set; }
        /// <summary>
        /// Result
        /// </summary>
        public IList<TEntity> ResultList { get; set; }
        /// <summary>
        /// If true, page+=1 will return more rows
        /// </summary>
        public bool? HasMore { get; set; }
    }
}
