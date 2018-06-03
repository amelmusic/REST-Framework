using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using System.Reflection;
using Foundatio.Caching;
using A.Core.Interface;
using System.Linq.Expressions;
using System.Collections.Concurrent;
using System.Reflection.Emit;

namespace A.Core.Interceptors
{
    public class CacheInterceptorProxy : BaseAttributeBasedInterceptorProxy<CacheAttribute>
    {
        private static object _lock = new object();
        protected static Dictionary<MethodInfo, CacheInterceptorContext> MethodList 
            = new Dictionary<MethodInfo, CacheInterceptorContext>();

        static JsonSerializerSettings jsonSerializerSettings;

        public ICacheClient CacheClient { get; set; }
        public IActionContext ActionContext { get; set; }
        static Dictionary<MethodInfo, bool> _cachedDictAccess = new Dictionary<MethodInfo, bool>();
        protected override Dictionary<MethodInfo, bool> CachedDictAccess => _cachedDictAccess;

        static CacheInterceptorProxy()
        {
            jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
        }

        public CacheInterceptorProxy(ICacheClient cacheClient, IActionContext actionContext)
        {
            CacheClient = cacheClient;
            ActionContext = actionContext;
        }

        protected override void InterceptSync(IInvocation invocation)
        {
            CacheInterceptorContext context = null;
            context = GetCacheInterceptorContext(invocation);

            var key = BuildCacheKey(invocation, context);

            if(context.CacheAttr.Invalidate)
            {
                var prefix = context.CacheAttr.Prefix;
                if (string.IsNullOrWhiteSpace(prefix))
                {
                    prefix = context.ClassName;
                }
                CacheClient.RemoveByPrefixAsync(prefix).GetAwaiter().GetResult();

                base.InterceptSync(invocation);
            }
            else
            {
                var cacheResultTask = (dynamic)context.GetFromCacheMethodInfo.Invoke(CacheClient, new string[] { key });
                var cacheResult = cacheResultTask.GetAwaiter().GetResult();
                if (cacheResult.HasValue)
                {
                    invocation.ReturnValue = cacheResult.Value;
                }
                else
                {
                    base.InterceptSync(invocation);
                    //ADD to cache
                    AddToCache(context, key, invocation.ReturnValue);
                }
            }
           
        }

        protected override void InterceptAsync(IInvocation invocation, object context = null)
        {
            var tmpContext = GetCacheInterceptorContext(invocation);

            var key = BuildCacheKey(invocation, tmpContext);

            context = new CacheInterceptorMethodContext()
            {
                Context = tmpContext,
                CacheKey = key
            };

            if(tmpContext.CacheAttr.Invalidate)
            {
                base.InterceptAsync(invocation, context);
            }
            else
            {
                var cacheResultTask = (dynamic)tmpContext.GetFromCacheMethodInfo.Invoke(CacheClient, new string[] { key });
                var cacheResult = cacheResultTask.GetAwaiter().GetResult();
                if (cacheResult.HasValue)
                {
                    if (cacheResult.IsNull)
                    {
                        var result = tmpContext.CreateNewInstanceMethodInfo.Invoke(null, new object[] { null }); //Call the convert method and return the generic Task, e.g. Task<int>

                        invocation.ReturnValue = result;
                    }
                    else
                    {
                        invocation.ReturnValue = Task.FromResult(cacheResult.Value);
                    }
                }
                else
                {
                    base.InterceptAsync(invocation, context);
                }
            }
        }

        protected override Task InterceptInternalAsyncAfter(Task task, IInvocation invocation, object context = null)
        {
            throw new ApplicationException("Can't cache Task method. Use Task<T> instead");
        }

        protected override async Task<T> InterceptInternalAsyncAfter<T>(Task<T> task, IInvocation invocation, object context = null)
        {
            var result = await base.InterceptInternalAsyncAfter(task, invocation, context);
            var tmpContext = context as CacheInterceptorMethodContext;
            if(tmpContext.Context.CacheAttr.Invalidate)
            {
                var prefix = tmpContext.Context.CacheAttr.Prefix;
                if(string.IsNullOrWhiteSpace(prefix))
                {
                    prefix = tmpContext.Context.ClassName;
                }
                await CacheClient.RemoveByPrefixAsync(prefix);
            }
            else
            {
                var key = tmpContext.CacheKey;
                AddToCache(tmpContext.Context, key, result);
            }
            
            return result;
        }

        private void AddToCache(CacheInterceptorContext context, string key, object result)
        {
            if (context.CacheAttr.ExpirationType == ExpirationType.Default)
            {
                CacheClient.AddAsync(key, result).GetAwaiter().GetResult();
            }
            else if (context.CacheAttr.ExpirationType == ExpirationType.ExpiresIn)
            {
                TimeSpan span = TimeSpan.Parse(context.CacheAttr.ExpirationPattern);
                CacheClient.AddAsync(key, result, span).GetAwaiter().GetResult();
            }
        }

        public static Task<T> Convert<T>(T value) { return Task.FromResult<T>(default(T)); }
        private CacheInterceptorContext GetCacheInterceptorContext(IInvocation invocation)
        {
            CacheInterceptorContext context;
            if (!MethodList.TryGetValue(invocation.Method, out context))
            {
                lock (_lock)
                {
                    context = new CacheInterceptorContext();
                    context.MethodName = invocation.Method.Name;
                    context.ClassName = invocation.TargetType.FullName;
                    context.CacheAttr = invocation.Method.GetCustomAttribute<CacheAttribute>();

                    var methodType = invocation.Method.ReturnType;
                    if (methodType.IsGenericType)
                    {
                        context.GetFromCacheMethodInfo = typeof(ICacheClient).GetMethod("GetAsync")
                            .MakeGenericMethod(new Type[] { methodType.GetGenericArguments()[0] });

                        var task = Task.FromResult<object>(null);

                        Type taskReturnType = (context.GetFromCacheMethodInfo).ReturnType; //e.g. Task<int>

                        var type = taskReturnType.GetGenericArguments()[0].GetGenericArguments()[0]; //get the result type, e.g. int

                        var convert_method = this.GetType().GetMethod("Convert").MakeGenericMethod(type); //Get the closed version of the Convert method, e.g. Convert<int>
                        context.CreateNewInstanceMethodInfo = convert_method;

                    }
                    else
                    {
                        context.GetFromCacheMethodInfo = typeof(ICacheClient).GetMethod("GetAsync")
                            .MakeGenericMethod(new Type[] { methodType });
                    }

                    if (MethodList.ContainsKey(invocation.Method))
                    {
                        MethodList[invocation.Method] = context;
                    }
                    else
                    {
                        MethodList.Add(invocation.Method, context);
                    }
                    
                }
                
            }

            return context;
        }

        protected virtual string BuildCacheKey(IInvocation invocation, CacheInterceptorContext context)
        {
            var sb = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(context.CacheAttr.Prefix))
            {
                sb.Append(context.CacheAttr.Prefix);
            }
            else
            {
                sb.Append(context.ClassName);
                sb.Append(".");
                sb.Append(context.MethodName);
            }
            if (context.CacheAttr.IsUserContextAware && ActionContext.Data.ContainsKey("UserId"))
            {
                var userId = ActionContext.Data["UserId"].ToString();
                sb.Append(".u." + userId);
            }
            sb.Append(":");
            string output = JsonConvert.SerializeObject(invocation.Arguments, jsonSerializerSettings);

            sb.Append(output);
            return sb.ToString();
        }
    }

    public class CacheInterceptorContext
    {
        public string MethodName { get; set; }
        public string ClassName { get; set; }
        public MethodInfo GetFromCacheMethodInfo { get; set; }
        public CacheAttribute CacheAttr { get; set; }
        public MethodInfo CreateNewInstanceMethodInfo { get; set; }
    }

    public class CacheInterceptorMethodContext
    {
        public string CacheKey { get; set; }
        public CacheInterceptorContext Context { get; set; }
    }

    public enum ExpirationType
    {
        Default = 0,
        ExpiresIn = 1
    }
}
