using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using X.Core.Generator.Attributes;

namespace ApiTemplate.Model
{
    [StateMachineModelGenerator(StateMachineDefinitionPath = "XCoreHelloStateMachine.txt", PropertyName = "StateId")]
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    //[ModelGenerator(Behaviour = EntityBehaviourEnum.CRUD)]
    public partial class XCoreHello
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Finish")]
        [Filter(Filter = FilterEnum.GreatherThanOrEqual)]
        public string Name { get; set; }

        [RequestField(RequestName = "Insert")]
        [RequestField(RequestName = "Finish")]
        public string Code { get; set; }

        public int StateId { get; set; }
    }
}
