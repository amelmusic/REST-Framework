using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interface
{
    public interface IServicesRegistration
    {
        /// <summary>
        /// Lower the priority, sooner services registration kicks in
        /// </summary>
        int Priority { get; set; }

        /// <summary>
        /// Contains registration for services
        /// </summary>
        void Register(ref ContainerBuilder container);
    }
}
