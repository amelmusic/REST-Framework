using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Core.Generator.Attributes;

namespace Common.Model
{
    [ModelGenerator(Behaviour = EntityBehaviourEnum.CRUD)]
    public class Email
    {
        [Key]
        public int Id { get; set; }

        [RequestField(RequestName = "Insert")]
        [StringLength(250)]
        public string From { get; set; }
        [StringLength(250)]
        [RequestField(RequestName = "Insert")]
        public string To { get; set; }
        [RequestField(RequestName = "Insert")]
        [StringLength(250)]
        public string Cc { get; set; }
        [RequestField(RequestName = "Insert")]
        [StringLength(250)]
        public string Bcc { get; set; }
        public bool Sent { get; set; }
        [RequestField(RequestName = "Update")]
        public bool FailedDelivery { get; set; }
        [RequestField(RequestName = "Insert")]
        public string Content { get; set; }
        [RequestField(RequestName = "Insert")]
        public string Subject { get; set; }
    }
}
