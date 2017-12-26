using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    [Entity(true)]
    [DefaultInterfaceBehaviour(DefaultInterfaceBehaviourEnum.CRUDService, DefaultImplementationEnum.EntityFramework, "permissions")]
    public class Permission
    {
        [Key]
        public int Id { get; set; }
        [RequestField("Insert"), RequestField("Update")]
        public string Description { get; set; }
    }
}
