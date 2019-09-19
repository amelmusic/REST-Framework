using System;
using System.Collections.Generic;
using System.Text;
using X.Core.Interface;
using X.Core.Model;

namespace X.Core
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
