using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    [Table("ClientIdPRestrictions")]
    public partial class ClientIdPrestrictions
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Provider { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Clients.ClientIdPrestrictions))]
        public virtual Clients Client { get; set; }
    }
}
