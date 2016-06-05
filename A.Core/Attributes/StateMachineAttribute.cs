using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    /// <summary>
    /// Represents attribute that marks entity suitable for generating state machine from .tastate file
    /// </summary>
    public class StateMachineAttribute : Attribute
    {
        public string StateMachineName { get; set; }
        public string StateMachineEnumName { get; set; }
        public string PropertyNameOnModel { get; set; }

        public StateMachineAttribute(string stateMachineName, string stateMachineEnumName, string propertyNameOnModel)
        {
            StateMachineName = stateMachineName;
            StateMachineEnumName = stateMachineEnumName;
            PropertyNameOnModel = propertyNameOnModel;
        }
    }
}
