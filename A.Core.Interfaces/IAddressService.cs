using A.Core.Attributes;
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
    public interface IAddressService : ICRUDService<Address, AddressSearchObject, AddressAdditionalSearchRequestData, AddressInsertRequest, AddressUpdateRequest>
    {

    }
}
