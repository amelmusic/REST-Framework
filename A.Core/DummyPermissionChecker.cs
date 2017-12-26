using A.Core.Interceptors;
using A.Core.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Model;

namespace A.Core
{
    public class DummyPermissionChecker : IPermissionChecker
    {
        public bool IsAllowed(string permission)
        {
            return true;
        }

        public bool IsAllowed(PermissionCheckRequest permission)
        {
            return true;
        }

        public void ThrowExceptionIfNotAllowed(string permission)
        {
            
        }

        public void ThrowExceptionIfNotAllowed(PermissionCheckRequest permission)
        {
            
        }
    }
}
