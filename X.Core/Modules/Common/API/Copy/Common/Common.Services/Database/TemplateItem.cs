using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("TemplateItem", Schema = "common")]
    public class TemplateItem
    {
        [Key]
        public int Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }

        [ForeignKey("FileId")]
        public File File { get; set; }
        public int? FileId { get; set; }
    }
}
