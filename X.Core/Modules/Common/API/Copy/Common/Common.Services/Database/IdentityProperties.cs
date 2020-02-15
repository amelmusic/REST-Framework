using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Services.Database
{
    public partial class IdentityProperties
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Key { get; set; }
        [Required]
        [StringLength(2000)]
        public string Value { get; set; }
        public int IdentityResourceId { get; set; }

        [ForeignKey(nameof(IdentityResourceId))]
        [InverseProperty(nameof(IdentityResources.IdentityProperties))]
        public virtual IdentityResources IdentityResource { get; set; }
    }
}
