using System;
using System.Collections.Generic;
using System.Text;

namespace X.Core.Generator.Attributes
{
    public class EntityAttribute : Attribute
    {
        /// <summary>
        /// Name of the class that is used for database
        /// </summary>
        public string MapTo { get; set; }
        /// <summary>
        /// Behaviour of generated class.
        /// </summary>
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
        /// <summary>
        /// Used for readonly operations and also as a base for state machine
        /// </summary>
        Read = 1,
        CRUD = 2,
        /// <summary>
        /// Same as CRUD but, with only one object for insert and update
        /// </summary>
        CRUDAsUpsert = 3,
        /// <summary>
        /// Used when we need to provide upload of the file functionality
        /// </summary>
        CRUDAsUpload = 4
    }

    public enum ServiceTypeEnum
    {
        EntityFramework = 1
    }
}
