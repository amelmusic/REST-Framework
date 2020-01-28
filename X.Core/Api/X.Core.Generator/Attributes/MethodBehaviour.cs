using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    [global::System.AttributeUsage(AttributeTargets.Method, Inherited = true,
        AllowMultiple = false)]
    public class MethodBehaviourAttribute : Attribute
    {
        public BehaviourEnum Behaviour { get; set; }
    }

    public enum BehaviourEnum
    {
        GetById = 1,
        Get = 2,
        Insert = 10,
        Update = 11,
        Patch = 12,
        StateMachineInsert = 20,
        StateMachineUpdate = 21,
        StateMachineInsertWithoutServiceImpl = 22,
        StateMachineUpdateWithoutServiceImpl = 23,
        DeleteWithoutServiceImpl = 30,
        Delete = 31,
        Download = 50,
        DownloadAsPost = 51,
        Custom1 = 100,
        Custom2 = 101,
        Custom3 = 102,
        Custom4 = 103,
        Custom5 = 104,
        Custom6 = 105,
        Custom7 = 106,
        Custom8 = 107,
        Custom9 = 108,
        Custom10 = 109
    }
}
