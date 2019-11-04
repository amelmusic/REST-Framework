using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionModule.Services.Database
{
    [Table("RoleRelations", Schema = "permission")]
    public partial class RoleRelations
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int? ParentRoleId { get; set; }
        public bool IsDeleted { get; set; }
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("ParentRoleId")]
        [InverseProperty("RoleRelationsParentRole")]
        public virtual Role ParentRole { get; set; }
        [ForeignKey("RoleId")]
        [InverseProperty("RoleRelationsRole")]
        public virtual Role Role { get; set; }
    }
}
