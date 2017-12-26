using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using A.Core.Interface;
using System.Reflection;

namespace A.Core.Interceptors
{
    public class TransactionInterceptorProxy : BaseAttributeBasedInterceptorProxy<TransactionAttribute>
    {
        static Dictionary<MethodInfo, bool> _cachedDictAccess = new Dictionary<MethodInfo, bool>();
        protected override Dictionary<MethodInfo, bool> CachedDictAccess => _cachedDictAccess;

        protected override void InterceptSync(IInvocation invocation)
        {
            IService instance = (IService)invocation.InvocationTarget;
            instance = instance ??
                throw new ApplicationException("You have to implement IService interface in order to use TransactionInterceptorProxy");
            bool isStarted = false;
            try
            {
                isStarted = instance.BeginTransaction();
                base.InterceptSync(invocation);
                if (isStarted)
                {
                    instance.CommitTransaction();
                }
            }
            catch(Exception ex)
            {
                if (isStarted)
                {
                    instance.RollbackTransaction();
                }
                
                var logger = GetLogger(invocation);
                logger.Error(ex);
                throw;
            }
            finally
            {
                if (isStarted)
                {
                    instance.DisposeTransaction();
                }
            }
        }

        protected override void InterceptAsync(IInvocation invocation, object context = null)
        {
            context = new TransactionContextData();
            var tmpContext = context as TransactionContextData;

            IService instance = (IService)invocation.InvocationTarget;
            tmpContext.Service = instance ??
                throw new ApplicationException("You have to implement IService interface in order to use TransactionInterceptorProxy");
            tmpContext.IsTransactionStarted = instance.BeginTransaction();

            base.InterceptAsync(invocation, context);
        }

        protected override async Task InterceptInternalAsyncAfter(Task task, IInvocation invocation, object context = null)
        {
            var tmpContext = context as TransactionContextData;
            try
            {
                await base.InterceptInternalAsyncAfter(task, invocation, context);
                if (tmpContext.IsTransactionStarted)
                {
                    tmpContext.Service.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                if (tmpContext.IsTransactionStarted)
                {
                    tmpContext.Service.RollbackTransaction();
                }
                var logger = GetLogger(invocation);
                logger.Error(ex);
                throw;
            }
            finally
            {
                if (tmpContext.IsTransactionStarted)
                {
                    tmpContext.Service.DisposeTransaction();
                }
            }
        }

        protected override async Task<T> InterceptInternalAsyncAfter<T>(Task<T> task, IInvocation invocation, object context = null)
        {
            var tmpContext = context as TransactionContextData;
            try
            {
                var result = await base.InterceptInternalAsyncAfter(task, invocation, context);
                if (tmpContext.IsTransactionStarted)
                {
                    tmpContext.Service.CommitTransaction();
                }
                return result;
            }
            catch (Exception ex)
            {
                if (tmpContext.IsTransactionStarted)
                {
                    tmpContext.Service.RollbackTransaction();
                }
                var logger = GetLogger(invocation);
                logger.Error(ex);
                throw;
            }
            finally
            {
                if (tmpContext.IsTransactionStarted)
                {
                    tmpContext.Service.DisposeTransaction();
                }
            }
        }
    }

    class TransactionContextData
    {
        public IService Service { get; set; }
        public bool IsTransactionStarted { get; set; }
    }
}
