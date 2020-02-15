using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core.RESTClient.Core
{
    //TODO: Implement this
    public abstract class RESTClient
    {
        protected string APIUrl { get; set; }
        public string ResourceName { get; set; }

        public IActionContext ActionContext { get; set; }

        public RESTClient(string moduleName, string resourceName, IActionContext actionContext, IConfiguration configuration)
        {
            var key = "RESTModules:" + moduleName;
            var absolutePath = configuration[key];

            if (string.IsNullOrWhiteSpace(absolutePath))
            {
                throw new ApplicationException($"You are missing {key} inside config file. This has to point to base path of rest service");
            }
            if (!absolutePath.EndsWith("/", StringComparison.Ordinal))
            {
                absolutePath += "/";
            }

            ResourceName = absolutePath + resourceName;
            ActionContext = actionContext;
        }

        public virtual Task<TModel> GetById<TModel>(object id, object additionalSearchData)
        {
            return FlurlExtension.GetAsync<TModel>($"{ResourceName}/{id}", ActionContext, additionalSearchData);
        }

        public virtual Task<TModel> Get<TModel>(object search, string actionName = "")
        {
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                if (!actionName.StartsWith("/"))
                {
                    actionName = "/" + actionName;
                }
            }
            return $"{ResourceName}{actionName}".GetAsync<TModel>(ActionContext, search);
        }

        public virtual Task<TModel> Post<TModel>(object request = null, object queryString = null, string actionName = "")
        {
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                if (!actionName.StartsWith("/"))
                {
                    actionName = "/" + actionName;
                }
            }
            return $"{ResourceName}{actionName}".PostAsync<TModel>(ActionContext, queryString, request);
        }

        public virtual Task<TModel> Put<TModel>(object id, object request = null, object queryString = null, string actionName = "")
        {
            if(!string.IsNullOrWhiteSpace(actionName))
            {
                if (!actionName.StartsWith("/"))
                {
                    actionName = "/" + actionName;
                }
            }
            return $"{ResourceName}/{id}{actionName}".PutAsync<TModel>(ActionContext, queryString, request);
        }

        public virtual Task<TModel> Delete<TModel>(object id, object request = null, object queryString = null, string actionName = "")
        {
            if (!string.IsNullOrWhiteSpace(actionName))
            {
                if (!actionName.StartsWith("/"))
                {
                    actionName = "/" + actionName;
                }
            }
            return $"{ResourceName}/{id}{actionName}".DeleteAsync<TModel>(ActionContext, queryString, request);
        }
    }
}