using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PostSharp.Aspects;

namespace A.Core.Interceptors
{
    [Serializable]
    public abstract class OnAsyncMethodBoundaryAspect : OnMethodBoundaryAspect
    {
        public override bool CompileTimeValidate(MethodBase method)
        {
            // make sure we have access to the method info so we can check the return type
            var methodInfo = method as MethodInfo;
            if (methodInfo == null)
            {
                throw new Exception("method is not MethodInfo");
            }

            // make sure the method returns Task or Task<T>
            if (!typeof(Task).IsAssignableFrom(methodInfo.ReturnType))
            {
                var message = string.Format(
                    "[{0}] can only be applied to a method which returns Task or Task<T>",
                    GetType().Name);
                throw new Exception(message);
            }

            return base.CompileTimeValidate(method);
        }

        public override  void OnExit(MethodExecutionArgs args)
        {
            var returnedTask = (Task)args.ReturnValue;
            returnedTask.ContinueWith(t => OnTaskFinished(t, args), TaskContinuationOptions.ExecuteSynchronously);
            returnedTask.ContinueWith(t => OnTaskFaulted(t, args), TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously);
            returnedTask.ContinueWith(t => OnTaskCompletion(t, args), TaskContinuationOptions.OnlyOnRanToCompletion | TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        /// Handler for when the preceding task returned by the method has finished, regardless whether or
        /// not the task faulted or ran to completion.
        /// </summary>
        public abstract void OnTaskFinished(Task precedingTask, MethodExecutionArgs args);

        /// <summary>
        /// Handler for when the preceding task returned by the method has faulted.
        /// </summary>
        public abstract void OnTaskFaulted(Task precedingTask, MethodExecutionArgs args);

        /// <summary>
        /// Handler for when the preceding task returned by the method has run to completion.
        /// </summary>
        public abstract void OnTaskCompletion(Task precedingTask, MethodExecutionArgs args);
    }
}
