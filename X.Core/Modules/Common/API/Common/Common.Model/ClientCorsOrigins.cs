using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class ClientCorsOrigins
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(150)]
        public string Origin { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Clients.ClientCorsOrigins))]
        public virtual Clients Client { get; set; }
    }
}
