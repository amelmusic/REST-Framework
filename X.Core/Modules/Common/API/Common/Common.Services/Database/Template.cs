using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("Template", Schema = "common")]
    public class Template
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        public string Code { get; set; }
        [StringLength(250)]
        public string Description { get; set; }

        [ForeignKey("TemplateTypeId")]
        public TemplateType TemplateType { get; set; }
        public int TemplateTypeId { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Content { get; set; }
    }
}
