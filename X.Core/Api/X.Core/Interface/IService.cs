using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Interface
{
    /// <summary>
    /// Root interface. Main purpose is to enable getting action context so that we can handle transactions and stuff through Postsharp
    /// </summary>
    public interface IService
    {
        /// <summary>
        /// Current request context. Same behaviour as WebAPI's context
        /// </summary>
        Lazy<IActionContext> ActionContext { get; set; }

        /// <summary>
        /// Returns true if it successfully started transaction
        /// </summary>
        /// <returns></returns>
        bool BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        void DisposeTransaction();
    }
}
