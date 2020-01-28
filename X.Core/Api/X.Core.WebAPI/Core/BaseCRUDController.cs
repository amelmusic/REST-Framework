using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Autofac.Core;
using Microsoft.AspNetCore.Mvc;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.WebAPI.Core
{
    public abstract class BaseCRUDController<TKey, TModel, TSearch, TAdditionalData, TInsert, TUpdate> : BaseReadController<TKey, TModel, TSearch, TAdditionalData> where TModel : class, new() where TSearch : BaseSearchObject<TAdditionalData>, new() where TAdditionalData : BaseAdditionalSearchRequestData, new()
    {
        protected readonly ICRUDService<TModel, TSearch, TAdditionalData, TInsert, TUpdate> _crudService;
        protected BaseCRUDController(ICRUDService<TModel, TSearch, TAdditionalData, TInsert, TUpdate> service) : base(service)
        {
            _crudService = service;
        }

        [Route("")]
        [HttpPost]
        public System.Threading.Tasks.Task<TModel> InsertAsync([FromBody]TInsert request)
        {
            var result = _crudService.InsertAsync(request);
            return result;
        }

        [Route("{id}")]
        [HttpPut]
        public System.Threading.Tasks.Task<TModel> UpdateAsync([FromRoute]TKey id, [FromBody]TUpdate request)
        {
            var result = _crudService.UpdateAsync(id, request);
            return result;
        }

        [Route("{id}")]
        [HttpPatch]
        public System.Threading.Tasks.Task<TModel> PatchAsync([FromRoute]TKey id, [FromBody]TUpdate request)
        {
            var result = _crudService.PatchAsync(id, request);
            return result;
        }

        [Route("{id}")]
        [HttpDelete]
        public System.Threading.Tasks.Task<bool> DeleteAsync([FromRoute]TKey id)
        {
            var result = _crudService.DeleteAsync(id);
            return result;
        }
    }
}
