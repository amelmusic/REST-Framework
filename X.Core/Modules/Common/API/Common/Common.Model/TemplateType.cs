using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public class TemplateType
    {
        [Key]
        public int Id { get; set; }
        [StringLength(250)]
        [Filter(Filter = FilterEnum.GreatherThanOrEqual)]
        public string Description { get; set; }
        [StringLength(1250)]
        public string StorageType { get; set; }
    }
}
