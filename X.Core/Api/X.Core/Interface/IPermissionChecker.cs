using X.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Interface
{
    /// <summary>
    /// Checks is requested operation allowed
    /// </summary>
    public interface IPermissionChecker
    {
        /// <summary>
        /// Returns true if allowed
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool IsAllowed(string permission);
        /// <summary>
        /// Throws exception if permission not allowed
        /// </summary>
        /// <param name="permission"></param>
        void ThrowExceptionIfNotAllowed(string permission);

        /// <summary>
        /// Returns true if allowed
        /// </summary>
        /// <param name="permission"></param>
        /// <returns></returns>
        bool IsAllowed(PermissionCheckRequest permission);

        /// <summary>
        /// Throws exception if permission not allowed
        /// </summary>
        /// <param name="permission"></param>
        void ThrowExceptionIfNotAllowed(PermissionCheckRequest permission);
    }
}
