using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.PermissionModule.Model
{
    [Entity(true,"Role")]
    [DefaultInterfaceBehaviour(DefaultInterfaceBehaviourEnum.ReadService, DefaultImplementationEnum.EntityFramework, "roles")]
    public partial class Role
    {
        [Key]
        public int Id { get; set; }

        [Filter(FilterEnum.Equal | FilterEnum.List)]
        public string Name { get; set; }

        [LazyLoading]
        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }
}
