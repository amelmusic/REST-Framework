using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    /// <summary>
    /// Base class for search request
    /// </summary>
    public class BaseSearchObject<TAdditionalData>
        where TAdditionalData : BaseAdditionalSearchRequestData, new()
    {
        public BaseSearchObject()
        {
            AdditionalData = new TAdditionalData();
        }

        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public bool? RetrieveAll { get; set; }

        public bool? IncludeCount { get; set; }
        public string OrderBy { get; set; }

        public TAdditionalData AdditionalData { get; set; }
        public bool ShouldSerializeAdditionalData()
        {
            bool shouldSerialize = true;
            if(AdditionalData != null && (AdditionalData.IncludeList == null || AdditionalData.IncludeList.Count == 0))
            {
                shouldSerialize = false;
            }
            return shouldSerialize;
        }
    }
}
