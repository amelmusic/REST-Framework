using System.Collections.Generic;

namespace X.Core.Model
{
    public partial class BaseAdditionalSearchRequestData
    {
        public BaseAdditionalSearchRequestData()
        {
            IncludeList = new List<string>();
        }
        /// <summary>
        /// Subresources list
        /// </summary>
        public IList<string> IncludeList { get; set; }
    }
}
