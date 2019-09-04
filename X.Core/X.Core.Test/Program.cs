using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using X.Core.Generator.Attributes;
using X.Core.Test.SearchObjects;

[assembly: WebAPIGenerator(InterfacesPath = "../X.Core.Test"
    , InterfacesNamespace = "X.Core.Test"
    , ModelNamespace = "X.Core.Test"
    , WebAPINamespace = "X.Core.Test")]

[assembly: InterfacesGenerator(ModelPath = "../X.Core.Test"
    , InterfacesNamespace = "X.Core.Test"
    , ModelNamespace = "X.Core.Test")]

[assembly: ServicesGenerator(ModelPath = "../X.Core.Test"
    , ModelNamespace = "X.Core.Test"
    , ServicesNamespace = "X.Core.Test"
    , EntityFrameworkContextName = "XCoreDB"
    , EntityFrameworkContextNamespace = "X.Core.Test.Database")]
namespace X.Core.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Bug b = new Bug("mr");
            var res = b.ToJson();
            Console.WriteLine("Hello World!");
        }
    }

    partial interface IChannelsService
    {
        [MethodBehaviour(Behaviour = BehaviourEnum.Get)]
        Task<List<Channels>> Active(ChannelsSearchObject searchAmel);
    }

    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    public partial class Channels
    {
        [Key]
        public int Id { get; set; }
        [Filter(Filter = FilterEnum.GreatherThanOrEqual)]
        public string Name { get; set; }

        //private ChannelsSearchObject c;
    }

    [StateMachineModelGenerator(StateMachineDefinitionPath = "AccountStateMachine.txt", PropertyName = "AccountId")]
    public partial class AccountStateMachineDefinition
    {
       
    }

    partial class ChannelsService
    {
        public Task<List<Channels>> Active(ChannelsSearchObject searchAmel)
        {
            throw new NotImplementedException();
        }
    }
}
