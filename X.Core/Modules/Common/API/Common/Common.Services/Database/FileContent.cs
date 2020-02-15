using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("TemplateItem", Schema = "common")]
    public class FileContent
    {
        [Key]
        public long Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string ContentStr { get; set; } //if used local
        public byte[] Content { get; set; } //if used local
    }
}
