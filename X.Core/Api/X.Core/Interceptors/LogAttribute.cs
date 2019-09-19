using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Interceptors
{
     [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true,
 AllowMultiple = false)]
    public class LogAttribute : BaseInterceptorAttribute
    {
        
    }
}
