using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    public partial class AspNetRoles
    {
        [Required(AllowEmptyStrings = false)]
        [Key]
        public string Id { get; set; }
        [StringLength(256)]
        public string Name { get; set; }
    }
}
