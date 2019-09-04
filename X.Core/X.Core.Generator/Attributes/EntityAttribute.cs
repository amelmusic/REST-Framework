using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    public class EntityAttribute : Attribute
    {
        public string MapTo { get; set; }
        public EntityBehaviourEnum Behaviour { get; set; }
        public ServiceTypeEnum ServiceType { get; set; }
    }

    public enum EntityBehaviourEnum
    {
        Empty = 0,
        Read = 1,
        CRUD = 2,
        CRUDAsUpsert = 3
    }

    public enum ServiceTypeEnum
    {
        EntityFramework = 1
    }
}
