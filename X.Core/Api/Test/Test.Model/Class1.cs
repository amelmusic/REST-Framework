using System;
using System.ComponentModel.DataAnnotations;
using X.Core.Generator.Attributes;

namespace Test.Model
{
   
    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class Channels
    {
        [Key]
        public int Id { get; set; }
        [Filter(Filter = FilterEnum.GreatherThanOrEqual)]
        public string Name { get; set; }

        //private ChannelsSearchObject c;
    }
}
