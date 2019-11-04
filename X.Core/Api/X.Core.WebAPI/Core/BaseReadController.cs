using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.WebAPI.Core
{
    public abstract class BaseReadController<TKey, TModel, TSearch, TAdditionalSearchData> : BaseController where TModel : class, new() where TSearch : BaseSearchObject<TAdditionalSearchData>, new() where TAdditionalSearchData : BaseAdditionalSearchRequestData, new()
    {
        protected readonly IReadService<TModel, TSearch, TAdditionalSearchData> _service;

        protected BaseReadController(IReadService<TModel, TSearch, TAdditionalSearchData> service)
        {
            _service = service;
        }

        [Route("{id}")]
        [HttpGet]
        public virtual Task<TModel> GetAsync(TKey id, [FromQuery]TAdditionalSearchData additionalSearchData)
        {
            return _service.GetAsync(id, additionalSearchData);
        }

        [Route("")]
        [HttpGet]
        public virtual async Task<PagedResult<TModel>> GetPageAsync([FromQuery]TSearch search)
        {
            var result = await _service.GetPageAsync(search);
            return result;
        }

    }
}
