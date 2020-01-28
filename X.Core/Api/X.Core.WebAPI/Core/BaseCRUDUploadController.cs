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
    public abstract class BaseCRUDUploadController<TKey, TModel, TSearch, TAdditionalData, TInsert, TUpdate> : BaseReadController<TKey, TModel, TSearch, TAdditionalData> where TModel : class, new() where TSearch : BaseSearchObject<TAdditionalData>, new() where TAdditionalData : BaseAdditionalSearchRequestData, new()
    {
        protected readonly ICRUDService<TModel, TSearch, TAdditionalData, TInsert, TUpdate> _crudService;
        protected BaseCRUDUploadController(ICRUDService<TModel, TSearch, TAdditionalData, TInsert, TUpdate> service) : base(service)
        {
            _crudService = service;
        }

        [Route("")]
        [HttpPost]
        public virtual System.Threading.Tasks.Task<TModel> InsertAsync([FromForm]TInsert request)
        {
            var result = _crudService.InsertAsync(request);
            return result;
        }

        [Route("{id}")]
        [HttpPut]
        public virtual System.Threading.Tasks.Task<TModel> UpdateAsync([FromRoute]TKey id, [FromForm]TUpdate request)
        {
            var result = _crudService.UpdateAsync(id, request);
            return result;
        }

        [Route("{id}")]
        [HttpPatch]
        public virtual System.Threading.Tasks.Task<TModel> PatchAsync([FromRoute]TKey id, [FromForm]TUpdate request)
        {
            var result = _crudService.PatchAsync(id, request);
            return result;
        }

        [Route("{id}")]
        [HttpDelete]
        public virtual System.Threading.Tasks.Task<bool> DeleteAsync([FromRoute]TKey id)
        {
            var result = _crudService.DeleteAsync(id);
            return result;
        }
    }
}
