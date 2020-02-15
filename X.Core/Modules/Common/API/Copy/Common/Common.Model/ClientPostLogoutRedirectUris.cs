using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Model
{
    public partial class ClientPostLogoutRedirectUris
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(2000)]
        public string PostLogoutRedirectUri { get; set; }
        public int ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        [InverseProperty(nameof(Clients.ClientPostLogoutRedirectUris))]
        public virtual Clients Client { get; set; }
    }
}
