using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Method, Inherited = true,
 AllowMultiple = false)]
    public class DefaultMethodBehaviourAttribute : Attribute
    {
        public DefaultMethodBehaviourAttribute(BehaviourEnum behaviour)
        {
            Behaviour = behaviour;
        }

        public BehaviourEnum Behaviour { get; set; }
    }

    public enum BehaviourEnum
    {
        GetById = 1,
        Get = 2,
        Insert = 10,
        Update = 11,
        StateMachineInsert = 20,
        StateMachineUpdate = 21,
        StateMachineInsertWithoutServiceImpl = 22,
        StateMachineUpdateWithoutServiceImpl = 23
    }
}
