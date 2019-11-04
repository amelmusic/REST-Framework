using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace PermissionModule.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class RoleRelations
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? ParentRoleId { get; set; }
        public bool IsDeleted { get; set; } 

        [ForeignKey("ParentRoleId")]
        [InverseProperty("RoleRelationsParentRole")]
        public virtual Role ParentRole { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RoleRelationsRole")]
        public virtual Role Role { get; set; }
    }
}
