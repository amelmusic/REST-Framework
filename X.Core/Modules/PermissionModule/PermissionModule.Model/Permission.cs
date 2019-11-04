using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace PermissionModule.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class Permission
    {
        public Permission()
        {
            RolePermission = new HashSet<RolePermission>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(500)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(100)]
        public string OperationType { get; set; }
        [Required]
        [StringLength(500)]
        public string OwnerPermission { get; set; }
        public short PermissionGroupId { get; set; }

        public PermissionGroup PermissionGroup { get; set; }
        public ICollection<RolePermission> RolePermission { get; set; }
    }
}
