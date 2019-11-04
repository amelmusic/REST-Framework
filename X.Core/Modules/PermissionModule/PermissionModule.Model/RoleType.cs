using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace PermissionModule.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class RoleType
    {
        public RoleType()
        {
            Role = new HashSet<Role>();
        }

        [Key]
        public short Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool IsMultipleAllowed { get; set; }
        public short? PermissionHierarchyLevel { get; set; }
        public bool IsAllowedAssigningToUsers { get; set; }
       
        [Required]
        public bool? IsAllowedAssigningPermissionToRole { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public bool IsAllowedMatchingOnSameLevel { get; set; }

        [InverseProperty("RoleType")]
        public virtual ICollection<Role> Role { get; set; }
    }
}
