using A.Core.Interceptors;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace $rootnamespace$.Core //DD
{
    /// <summary>
    /// Root service, serves for declaration of all interceptors
    /// </summary>
    //[Intercept(typeof(LogInterceptorProxy))]
    //[Intercept(typeof(CacheInterceptorProxy))]
    //[Intercept(typeof(TransactionInterceptorProxy))]
    public abstract class BaseService
    {
        public T Clone<T>(T source)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            
            var serialized = JsonConvert.SerializeObject(source, settings);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

    }
}
