using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Interface;
using PostSharp.Aspects;

namespace A.Core.Interceptors
{
    [Serializable]
    public sealed class TransactionInterceptorAsyncAttribute : OnAsyncMethodBoundaryAspect
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

        public override void OnTaskFinished(Task precedingTask, MethodExecutionArgs args)
        {
           
        }

        public override void OnTaskFaulted(Task precedingTask, MethodExecutionArgs args)
        {
            var instance = (TransactionInterceptorData)args.MethodExecutionTag;
            if (instance.IsTransactionStarted)
            {
                instance.Service.RollbackTransaction();
            }

          
            if (instance.IsTransactionStarted)
            {
                instance.Service.DisposeTransaction();
            }
        }

       
        public override void OnTaskCompletion(Task precedingTask, MethodExecutionArgs args)
        {
            var instance = (TransactionInterceptorData)args.MethodExecutionTag;
            if (instance.IsTransactionStarted)
            {
                instance.Service.CommitTransaction();
            }

         
            if (instance.IsTransactionStarted)
            {
                instance.Service.DisposeTransaction();
            }
        }
    }
}
