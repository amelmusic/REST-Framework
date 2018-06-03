using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Messaging
{

    /// <summary>
    /// Main interface for publishing messages. Currently only fanout is supported.
    /// </summary>
    public partial interface IBus
    {
        /// <summary>
        /// Main publish method.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task Publish(dynamic message);
    }
}
