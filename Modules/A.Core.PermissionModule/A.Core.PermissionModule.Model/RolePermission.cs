using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model
{
    [Entity(true, "RolePermission")]
    [DefaultInterfaceBehaviour(DefaultInterfaceBehaviourEnum.ReadService, DefaultImplementationEnum.EntityFramework, "rolesPermissions")]
    public partial class RolePermission
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }
        public bool IsAllowed { get; set; }
        [LazyLoading]
        public virtual Permission Permission { get; set; }
        [LazyLoading]
        public virtual Role Role { get; set; }
    }
}
