using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using X.Core.Generator;
using X.Core.Generator.Attributes;
using X.Core.Services.Core.StateMachine;
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

        [MethodBehaviour(Behaviour = BehaviourEnum.GetById)]
        Task<List<Channels>> GetActive(int id);


        [MethodBehaviour(Behaviour = BehaviourEnum.Delete)]
        Task<List<Channels>> Delete(int id);

        [MethodBehaviour(Behaviour = BehaviourEnum.Update)]
        Task<List<Channels>> UpdateAll(int id);
    }

    [ModelGenerator(Behaviour = EntityBehaviourEnum.Read)]
    [StateMachineModelGenerator(StateMachineDefinitionPath = "AccountStateMachine.txt", PropertyName = "AccountId")]
    public partial class Channels
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [RequestField(RequestName = "Update")]
        [Filter(Filter = FilterEnum.GreatherThanOrEqual)] 
        public string Name { get; set; }

        [Filter(Filter =FilterEnum.Equal)]
        public int AccountId { get; set; }

        //private ChannelsSearchObject c;
    }

    //[StateMachineModelGenerator(StateMachineDefinitionPath = "AccountStateMachine.txt", PropertyName = "AccountId")]
    //public partial class AccountStateMachineDefinition
    //{
       
    //}

    partial class ChannelsService
    {
        public Task<List<Channels>> Active(ChannelsSearchObject searchAmel)
        {
            throw new NotImplementedException();
        }

        public Task<List<Channels>> GetActive(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Channels>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Channels>> UpdateAll(int id)
        {
            throw new NotImplementedException();
        }

        private Task OnInsertToBacklog(TriggerRequest<ChannelsStateMachineTriggerEnum> arg)
        {
            throw new NotImplementedException();
        }
    }
}
