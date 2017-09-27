using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace A.Core.PermissionModule.Model
{
    [Entity]
    [DefaultInterfaceBehaviour(DefaultInterfaceBehaviourEnum.ReadService, DefaultImplementationEnum.EntityFramework, "roles")]
    public partial class Role
    {
        [Key]
        public int Id { get; set; }

        [Filter(FilterEnum.Equal | FilterEnum.List)]
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
