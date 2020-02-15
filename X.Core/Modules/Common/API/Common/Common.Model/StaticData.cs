using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.CRUDAsUpsert)]
    public class StaticData
    {
        [Key]
        public int Id { get; set; }
        
        [RequestField(RequestName = "Upsert")]
        [StringLength(200)]
        [Required(AllowEmptyStrings = false)]
        [Filter(Filter = FilterEnum.Equal | FilterEnum.List)]
        public string Code { get; set; }
        
        [RequestField(RequestName = "Upsert")]
        public string ParentCode { get; set; }
        
        [RequestField(RequestName = "Upsert")]
        [StringLength(200)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Filter(Filter = FilterEnum.Equal | FilterEnum.List)]
        [RequestField(RequestName = "Upsert")]
        public string Language { get; set; }

        [RequestField(RequestName = "Upsert")]
        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}
