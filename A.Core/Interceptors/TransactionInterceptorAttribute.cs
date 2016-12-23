using A.Core.Interface;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace A.Core.Interceptors
{
    [Serializable]
    public class TransactionInterceptorAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            IService instance = (IService)args.Instance;
            if (instance == null)
            {
                throw new ApplicationException("You have to implement IService interface in order to use TransactionInterceptor");
            }
            TransactionInterceptorData data = new TransactionInterceptorData();
            data.Service = instance;
            data.IsTransactionStarted = instance.BeginTransaction();
            args.MethodExecutionTag = data;
        }
 
        public override void OnSuccess(MethodExecutionArgs args)
        {
            var instance = (TransactionInterceptorData)args.MethodExecutionTag;
            if(instance.IsTransactionStarted)
            {
                instance.Service.CommitTransaction();
            }
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var instance = (TransactionInterceptorData)args.MethodExecutionTag;
            if (instance.IsTransactionStarted)
            {
                instance.Service.DisposeTransaction();
            }
        }

        public override void OnException(MethodExecutionArgs args)
        {
            var instance = (TransactionInterceptorData)args.MethodExecutionTag;
            if (instance.IsTransactionStarted)
            {
                instance.Service.RollbackTransaction();
            }
        }
    }

    class TransactionInterceptorData
    {
        public IService Service { get; set; }
        public bool IsTransactionStarted { get; set; }
    }
}
