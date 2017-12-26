using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace A.Core.PermissionModule.Model
{
    [Entity]
    [DefaultInterfaceBehaviour(DefaultInterfaceBehaviourEnum.ReadService, DefaultImplementationEnum.EntityFramework, "permissions")]
    public partial class Permission
    {
        [Key]
        public int Id { get; set; }
        [Filter(FilterEnum.Equal | FilterEnum.GreatherThanOrEqual | FilterEnum.List)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Filter(FilterEnum.Equal)]
        public bool IsAllowed { get; set; }

        public string OperationType { get; set; }
    }
}
