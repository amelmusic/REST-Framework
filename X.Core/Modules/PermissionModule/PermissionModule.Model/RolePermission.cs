using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace PermissionModule.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("RolePermission")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RolePermission")]
        public virtual Role Role { get; set; }
    }
}
