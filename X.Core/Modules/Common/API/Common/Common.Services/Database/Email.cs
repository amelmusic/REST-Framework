using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("Email", Schema = "common")]
    public class Email
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string From { get; set; }
        [StringLength(250)]
        public string To { get; set; }
        [StringLength(250)]
        public string Cc { get; set; }
        [StringLength(250)]
        public string Bcc { get; set; }

        public bool Sent { get; set; }
        public bool FailedDelivery { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Content { get; set; }

        [StringLength(550)]
        public string Subject { get; set; }
    }
}
