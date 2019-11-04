using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PermissionModule.Services.Database
{
    [Table("RoleType", Schema = "permission")]
    public partial class RoleType
    {
        public RoleType()
        {
            Role = new HashSet<Role>();
        }

        public short Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsMultipleAllowed { get; set; }
        public short? PermissionHierarchyLevel { get; set; }
        public bool IsAllowedAssigningToUsers { get; set; }
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime ModifiedOn { get; set; }
        [Required]
        public bool? IsAllowedAssigningPermissionToRole { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public bool IsAllowedMatchingOnSameLevel { get; set; }

        [InverseProperty("RoleType")]
        public virtual ICollection<Role> Role { get; set; }
    }
}
