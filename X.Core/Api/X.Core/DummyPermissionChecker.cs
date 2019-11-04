using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core
{
    public class DummyPermissionChecker : IPermissionChecker
    {
        public Task<bool> IsAllowed(PermissionCheckRequest permission)
        {
            return Task.FromResult(true);
        }

        public void ThrowExceptionIfNotAllowed(PermissionCheckRequest permission)
        {

        }
    }
}
