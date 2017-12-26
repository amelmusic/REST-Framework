using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    /// <summary>
    /// Base interceptor attribute. Proxy hook will skip all methods that doesn't have this attribute
    /// </summary>
    public abstract class BaseInterceptorAttribute : Attribute
    {
    }
}
