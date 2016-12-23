using A.Core.Interceptors;
using A.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core
{
    public class DummyPermissionChecker : IPermissionChecker
    {
        public bool IsAllowed(string permission)
        {
            return true;
        }

        public void ThrowExceptionIfNotAllowed(string permission)
        {
            
        }
    }
}
