using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("TemplateType", Schema = "common")]
    public class TemplateType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(1250)]
        public string StorageType { get; set; }
    }
}
