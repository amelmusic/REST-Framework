using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class ClientScopes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Scope { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Clients.ClientScopes))]
        public virtual Clients Client { get; set; }
    }
}
