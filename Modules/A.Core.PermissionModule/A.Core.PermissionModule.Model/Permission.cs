using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model
{
    [Entity(true, "Permission")]
    [DefaultInterfaceBehaviour(DefaultInterfaceBehaviourEnum.ReadService, DefaultImplementationEnum.EntityFramework, "permissions")]
    public class Permission
    {

        ///<summary>
        /// Id (Primary key)
        ///</summary>
        [Key]
        public int Id { get; set; }

        ///<summary>
        /// Name (length: 250)
        ///</summary>
        [Filter(FilterEnum.Equal | FilterEnum.GreatherThanOrEqual | FilterEnum.List)]
        public string Name { get; set; }

        ///<summary>
        /// Description (length: 250)
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// IsAllowed
        ///</summary>
        [Filter(FilterEnum.Equal)]
        public bool IsAllowed { get; set; }

        ///<summary>
        /// OperationType (length: 100)
        ///</summary>
        public string OperationType { get; set; }
    }
}
