using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interceptors
{
    [global::System.AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true,
        AllowMultiple = false)]
    public class TransactionAttribute : LogAttribute
    {

    }
}
