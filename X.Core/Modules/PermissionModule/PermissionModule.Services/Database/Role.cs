using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionModule.Services.Database
{
    [Table("Role", Schema = "permission")]
    public partial class Role
    {
        public Role()
        {
            RolePermission = new HashSet<RolePermission>();
            RoleRelationsParentRole = new HashSet<RoleRelations>();
            RoleRelationsRole = new HashSet<RoleRelations>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string OwnerPermission { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public short RoleTypeId { get; set; }
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("RoleTypeId")]
        [InverseProperty("Role")]
        public virtual RoleType RoleType { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RolePermission> RolePermission { get; set; }
        [InverseProperty("ParentRole")]
        public virtual ICollection<RoleRelations> RoleRelationsParentRole { get; set; }
        [InverseProperty("Role")]
        public virtual ICollection<RoleRelations> RoleRelationsRole { get; set; }
    }
}
