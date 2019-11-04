using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionModule.Services.Database
{
    [Table("Permission", Schema = "permission")]
    public partial class Permission
    {
        public Permission()
        {
            RolePermission = new HashSet<RolePermission>();
        }

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
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("PermissionGroupId")]
        [InverseProperty("Permission")]
        public virtual PermissionGroup PermissionGroup { get; set; }
        [InverseProperty("Permission")]
        public virtual ICollection<RolePermission> RolePermission { get; set; }
    }
}
