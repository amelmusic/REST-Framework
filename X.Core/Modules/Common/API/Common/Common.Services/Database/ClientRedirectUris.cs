using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Services.Database
{
    public partial class ClientRedirectUris
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(2000)]
        public string RedirectUri { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Clients.ClientRedirectUris))]
        public virtual Clients Client { get; set; }
    }
}
