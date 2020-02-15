using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.CRUD)]
    public class Template
    {

        [Filter(Filter = FilterEnum.Equal)]
        [Key]
        public int Id { get; set; }
        [RequestField(RequestName = "Generate")]
        [RequestField(RequestName = "Insert")]
        [StringLength(250)]
        [Filter(Filter = FilterEnum.Equal)]
        public string Code { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        [StringLength(250)]
        [Filter(Filter = FilterEnum.GreatherThanOrEqual)]
        public string Description { get; set; }
        public TemplateType TemplateType { get; set; }

        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        [Filter(Filter = FilterEnum.Equal)]
        public int TemplateTypeId { get; set; }
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Update")]
        public string Content { get; set; }
    }
}
