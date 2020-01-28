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
        public string ResourceName { get; set; } //key name for permissions and rest controller
        
        /// <summary>
        /// If this is true, no controller will be generated.
        /// </summary>
        public bool Internal { get; set; }
    }

    public enum EntityBehaviourEnum
    {
        Empty = 0,
        Read = 1,
        CRUD = 2,
        CRUDAsUpsert = 3,
        CRUDAsUpload = 4
    }

    public enum ServiceTypeEnum
    {
        EntityFramework = 1
    }
}
