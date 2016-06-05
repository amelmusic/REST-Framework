using A.Core.Attributes;
using A.Core.Interceptors;
using A.Core.Interface;
using A.Core.Model;
using A.Core.Model.Requests;
using A.Core.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interfaces
{
    /// <summary>
    /// Handles address logic
    /// </summary>
    [DefaultServiceBehaviour(DefaultImplementationEnum.EntityFramework, "addresses")]
    [StateMachine("AddressStateMachine", "A.Core.Model.AddressStateMachineEnum", "StateId")]
    public interface IAddressService : IReadService<Address, AddressSearchObject, AddressAdditionalSearchRequestData>
    {        
        [DefaultMethodBehaviour(BehaviourEnum.StateMachineInsert)]
        Address Start(AddressStartRequest request);

        [DefaultMethodBehaviour(BehaviourEnum.StateMachineUpdate)]
        Address Verify(object id, AddressVerifyRequest request);

        [DefaultMethodBehaviour(BehaviourEnum.StateMachineUpdate)]
        Address MarkAsInvalid(object id, AddressMarkAsInvalidRequest request);  
    }

}
