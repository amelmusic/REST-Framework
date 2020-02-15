using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class ApiScopeClaims
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Type { get; set; }
        public int ApiScopeId { get; set; }

        [ForeignKey(nameof(ApiScopeId))]
        [InverseProperty(nameof(ApiScopes.ApiScopeClaims))]
        public virtual ApiScopes ApiScope { get; set; }
    }
}
