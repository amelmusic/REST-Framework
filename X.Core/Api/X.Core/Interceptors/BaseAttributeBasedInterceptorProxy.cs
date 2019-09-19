using System;
using System.Reflection;
using Castle.DynamicProxy;

namespace X.Core.Interceptors
{
    public abstract class BaseAttributeBasedInterceptorProxy<T> : BaseInterceptorProxy where T : BaseInterceptorAttribute
    {
        private static object _lock = new object();

        protected override bool ShouldIntercept(IInvocation invocation)
        {
            bool interceptTmp = false;
            bool intercept = interceptTmp;
            if (CachedDictAccess.TryGetValue(invocation.Method, out interceptTmp))
            {
                intercept = interceptTmp;
            }
            else
            {
                if (invocation.Method.GetCustomAttribute<T>() != null)
                {
                    if (!HasInvalidArguments(invocation))
                    {
                        lock (_lock)
                        {
                            if (!CachedDictAccess.ContainsKey(invocation.Method))
                            {
                                CachedDictAccess.Add(invocation.Method, true);
                                intercept = true;
                            }
                            //CachedAccess.Add(invocation.Method);
                            
                        }
                    }
                    else
                    {
                        throw new ApplicationException("Arguments for intercepting are invalid (IQueryable or not serializable)");
                    }
                }
                else
                {
                    lock (_lock)
                    {
                        if (!CachedDictAccess.ContainsKey(invocation.Method))
                        {
                            CachedDictAccess.Add(invocation.Method, false);
                            intercept = false;
                        }
                        //CachedAccess.Add(invocation.Method);
                    }
                }
            }

            return intercept;
        }
    }
}
