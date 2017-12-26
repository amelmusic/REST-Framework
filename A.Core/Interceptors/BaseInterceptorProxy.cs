using Castle.DynamicProxy;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    public abstract class BaseInterceptorProxy : IInterceptor
    {
        protected static Dictionary<string, ILog> mLoggerList = new Dictionary<string, ILog>();
        protected abstract Dictionary<MethodInfo, bool> CachedDictAccess { get; }

        protected ILog GetLogger(IInvocation invocation)
        {
            ILog log = null;
            string loggerName = invocation.TargetType.FullName;
            if (mLoggerList.ContainsKey(loggerName))
            {
                log = mLoggerList[loggerName];
            }
            else
            {
                log = log4net.LogManager.GetLogger(loggerName);
                mLoggerList.Add(loggerName, log);
            }

            return log;
        }

        public virtual void Intercept(IInvocation invocation)
        {
            if(ShouldIntercept(invocation))
            {
                InterceptInternal(invocation);
            }
            else
            {
                invocation.Proceed();
            }
        }

        protected virtual bool ShouldIntercept(IInvocation invocation)
        {
            bool interceptTmp = false;
            bool intercept = interceptTmp;
            if (CachedDictAccess.TryGetValue(invocation.Method, out interceptTmp))
            {
                intercept = interceptTmp;
            }
            else
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

            return intercept;
        }

        protected virtual bool OnException(IInvocation invocation, Exception ex)
        {
            //TODO: Impl this
            return true;
        }

        protected virtual bool HasInvalidArguments(IInvocation invocation)
        {
            bool hasInvalid = false;
            foreach (var arg in invocation.Arguments)
            {
                if (arg is IQueryable)
                {
                    hasInvalid = true;
                }
            }
            return hasInvalid;
        }

        protected virtual void InterceptInternal(IInvocation invocation)
        {
            try
            {
                var method = invocation.MethodInvocationTarget;
                var isAsync = method.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null;
                if (!isAsync)
                {
                    isAsync = typeof(Task).IsAssignableFrom(method.ReturnType);
                }
                if (isAsync)
                {
                    InterceptAsync(invocation);
                }
                else
                {
                    InterceptSync(invocation);
                }
            }
            catch (Exception ex)
            {
                if (OnException(invocation, ex))
                {
                    throw;
                }
            }
        }

        protected virtual void InterceptAsync(IInvocation invocation, object context = null)
        {
            invocation.Proceed();
            invocation.ReturnValue = InterceptInternalAsyncAfter((dynamic)invocation.ReturnValue, invocation, context);
        }

        protected virtual void InterceptSync(IInvocation invocation)
        {
            invocation.Proceed();
        }

        protected virtual async Task InterceptInternalAsyncAfter(Task task, IInvocation invocation, object context = null)
        {
            await task.ConfigureAwait(false);
            // do the logging here, as continuation work for Task...
        }

        protected virtual async Task<T> InterceptInternalAsyncAfter<T>(Task<T> task, IInvocation invocation, object context = null)
        {

            try
            {
                T result = await task.ConfigureAwait(false);
                // do the logging here, as continuation work for Task<T>...
                return result;
            }
            catch (AggregateException aex)
            {
                OnException(invocation, aex.InnerException);
                throw aex.InnerException;
            }
        }

    }

}
