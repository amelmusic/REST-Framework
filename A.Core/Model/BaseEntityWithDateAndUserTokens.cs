using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    public class BaseEntityWithDateAndUserTokens : BaseEntityWithDateTokens
    {
        [StringLength(50)]
        public string CreatedById { get; set; }
        [StringLength(50)]
        public string ModifiedById { get; set; }
    }
}
