using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Interface, Inherited = true,
     AllowMultiple = false)]
    public class DefaultServiceBehaviourAttribute : Attribute
    {
        public DefaultServiceBehaviourAttribute(DefaultImplementationEnum defaultImplementation, string endpointName)
        { 
            EndpointName = endpointName;
        }

        public string EndpointName { get; set; }

    }

    public enum DefaultImplementationEnum
    {
        EntityFramework = 1,
        MSSQLStoredProcedures = 2,
        MongoDB = 3
    }
}
