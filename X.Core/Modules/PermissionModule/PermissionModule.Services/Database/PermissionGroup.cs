using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionModule.Services.Database
{
    [Table("PermissionGroup", Schema = "permission")]
    public partial class PermissionGroup
    {
        public PermissionGroup()
        {
            Permission = new HashSet<Permission>();
        }

        public short Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }

        [InverseProperty("PermissionGroup")]
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
