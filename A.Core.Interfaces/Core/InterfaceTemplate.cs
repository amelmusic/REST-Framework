




#region A_Core generated code

using System.Collections.Generic;
using A.Core.Interface;
using A.Core.Attributes;
using A.Core.Model;
//Requests section
namespace A.Core.Interfaces
{
	//Creating interfaces from model...1
		[DefaultServiceBehaviour(DefaultImplementationEnum.EntityFramework, "permissions")]
		public partial interface IPermissionService : ICRUDService<A.Core.Model.Permission, A.Core.Model.SearchObjects.PermissionSearchObject, A.Core.Model.SearchObjects.PermissionAdditionalSearchRequestData, A.Core.Model.Requests.PermissionInsertRequest, A.Core.Model.Requests.PermissionUpdateRequest>
		{
		
		}

}

#endregion

