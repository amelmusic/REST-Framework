using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class IdentityClaims
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Type { get; set; }
        public int IdentityResourceId { get; set; }

        [ForeignKey(nameof(IdentityResourceId))]
        [InverseProperty(nameof(IdentityResources.IdentityClaims))]
        public virtual IdentityResources IdentityResource { get; set; }
    }
}
