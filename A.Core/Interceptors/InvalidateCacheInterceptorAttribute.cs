using Foundatio.Caching;
using PostSharp.Aspects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using System.Reflection;

namespace A.Core.Interceptors
{
    [Serializable]
    public class InvalidateCacheInterceptorAttribute : OnMethodBoundaryAspect
    {
        private string _prefix;

        public InvalidateCacheInterceptorAttribute(string prefix = null)
        {
            _prefix = prefix;
        }

        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            if(string.IsNullOrWhiteSpace(_prefix))
            {
                _prefix = method.DeclaringType.FullName;
            }
            base.CompileTimeInitialize(method, aspectInfo);
        }

        public override async void OnSuccess(MethodExecutionArgs args)
        {
            A.Core.Interface.IService instance = (A.Core.Interface.IService)args.Instance;
            var cacheClient = instance.ActionContext.Value.CurrentContainer.Resolve<ICacheClient>();
            await cacheClient.RemoveByPrefixAsync(_prefix);
            base.OnSuccess(args);
        }
    }
}
