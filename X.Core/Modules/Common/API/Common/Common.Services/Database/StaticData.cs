using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Services.Database
{
    [Table("StaticData", Schema = "common")]
    public class StaticData
    {
        [Key]
        public int Id { get; set; }
        [StringLength(200)]
        [Required(AllowEmptyStrings = false)]
        public string Code { get; set; }
        public string ParentCode { get; set; }
        [StringLength(200)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public string Language { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Content { get; set; }
    }
}
