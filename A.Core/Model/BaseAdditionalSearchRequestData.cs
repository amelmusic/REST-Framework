using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
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
