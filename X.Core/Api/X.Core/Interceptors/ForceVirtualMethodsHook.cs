using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;

namespace X.Core.Interceptors
{
    public class ForceVirtualMethodsHook : IProxyGenerationHook
    {
        public void MethodsInspected()
        {
            
        }

        public void NonProxyableMemberNotification(Type type, MemberInfo memberInfo)
        {
            var intercept = memberInfo.GetCustomAttributes(typeof(BaseInterceptorAttribute)).Count() != 0;
            if (intercept)
            {
                throw new SystemException($"Method: {memberInfo.Name} on type: {type.FullName} must be virtual!");
            }
        }

        public bool ShouldInterceptMethod(Type type, MethodInfo methodInfo)
        {
            var intercept = methodInfo.GetCustomAttributes(typeof(BaseInterceptorAttribute)).Count() != 0;

            return intercept;
        }
    }
}
