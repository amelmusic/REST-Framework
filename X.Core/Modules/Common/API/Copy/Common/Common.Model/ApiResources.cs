using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class ApiResources
    {
        public ApiResources()
        {
            ApiClaims = new HashSet<ApiClaims>();
            ApiProperties = new HashSet<ApiProperties>();
            ApiScopes = new HashSet<ApiScopes>();
            ApiSecrets = new HashSet<ApiSecrets>();
        }

        [Key]
        public int Id { get; set; }
        public bool Enabled { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string DisplayName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }

        [InverseProperty("ApiResource")]
        public virtual ICollection<ApiClaims> ApiClaims { get; set; }
        [InverseProperty("ApiResource")]
        public virtual ICollection<ApiProperties> ApiProperties { get; set; }
        [InverseProperty("ApiResource")]
        public virtual ICollection<ApiScopes> ApiScopes { get; set; }
        [InverseProperty("ApiResource")]
        public virtual ICollection<ApiSecrets> ApiSecrets { get; set; }
    }
}
