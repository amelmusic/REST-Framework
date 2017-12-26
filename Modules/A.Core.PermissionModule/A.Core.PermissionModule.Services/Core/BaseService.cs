using A.Core.Interceptors;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Services.Core //DD
{
    /// <summary>
    /// Root service, serves for declaration of all interceptors
    /// </summary>
    //[Intercept(typeof(LogInterceptorProxy))]
    //[Intercept(typeof(CacheInterceptorProxy))]
    //[Intercept(typeof(TransactionInterceptorProxy))]
    public abstract class BaseService
    {

    }
}
