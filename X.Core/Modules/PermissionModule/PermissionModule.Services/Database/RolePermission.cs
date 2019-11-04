using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionModule.Services.Database
{
    [Table("RolePermission", Schema = "permission")]
    public partial class RolePermission
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("PermissionId")]
        [InverseProperty("RolePermission")]
        public virtual Permission Permission { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RolePermission")]
        public virtual Role Role { get; set; }
    }
}
