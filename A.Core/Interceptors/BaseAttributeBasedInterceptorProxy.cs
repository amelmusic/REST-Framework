using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    public abstract class BaseAttributeBasedInterceptorProxy<T> : BaseInterceptorProxy where T : BaseInterceptorAttribute
    {

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
                        //CachedAccess.Add(invocation.Method);
                        CachedDictAccess.Add(invocation.Method, true);
                        intercept = true;
                    }
                    else
                    {
                        throw new ApplicationException("Arguments for intercepting are invalid (IQueryable or not serializable)");
                    }
                }
                else
                {
                    CachedDictAccess.Add(invocation.Method, false);
                    intercept = false;
                }
               
                
            }

            return intercept;
        }
    }
}
