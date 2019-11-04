using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.RESTClient.Core
{
    //TODO: Implement this
    public class RESTClient<TKey, TModel, TSearch, TAdditionalSearchData> where TModel : class, new() where TSearch : BaseSearchObject<TAdditionalSearchData>, new() where TAdditionalSearchData : BaseAdditionalSearchRequestData, new()
    {
        public string ResourceName { get; set; }

        public IActionContext ActionContext { get; set; }

        public RESTClient(string resourceName, IActionContext actionContext)
        {
            ResourceName = resourceName;
            ActionContext = actionContext;
        }

        public virtual Task<TModel> GetById(TKey id, TAdditionalSearchData additionalSearchData)
        {
            return FlurlExtension.GetAsync<TModel>($"{ResourceName}/{id}", ActionContext, additionalSearchData);
        }
    }
}
