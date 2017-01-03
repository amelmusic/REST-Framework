using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Class, Inherited = true,
 AllowMultiple = false)]
    public class DefaultInterfaceBehaviourAttribute : Attribute
    {
        public DefaultInterfaceBehaviourAttribute(DefaultInterfaceBehaviourEnum defaultInterface, DefaultImplementationEnum defaultImplementation, string endpointName)
        {
            EndpointName = endpointName;
            DefaultImplementation = defaultImplementation;
        }
        public DefaultImplementationEnum DefaultImplementation { get; private set; }
        public string EndpointName { get; private set; }

    }

    public enum DefaultInterfaceBehaviourEnum
    {
        ReadService = 1
        ,CRUDService = 2
        ,ReadServiceAsync = 3
        ,CRUDServiceAsync = 4
    }
}
