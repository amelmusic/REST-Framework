using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class ClientClaims
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(250)]
        public string Type { get; set; }
        [Required]
        [StringLength(250)]
        public string Value { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Clients.ClientClaims))]
        public virtual Clients Client { get; set; }
    }
}
