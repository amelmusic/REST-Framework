using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace PermissionModule.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class PermissionGroup
    {
        public PermissionGroup()
        {
            Permission = new HashSet<Permission>();
        }

        [Key]
        public short Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [InverseProperty("PermissionGroup")]
        public virtual ICollection<Permission> Permission { get; set; }
    }
}
