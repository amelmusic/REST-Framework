




#region A_Core generated code

using System.Collections.Generic;
using A.Core.Interface;
using A.Core.Attributes;
using A.Core.PermissionModule.Model;
//Requests section
namespace A.Core.PermissionModule.Interfaces
{
	//Creating interfaces from model...3
		[DefaultServiceBehaviour(DefaultImplementationEnum.EntityFramework, "permissions")]
		public partial interface IPermissionService : IReadService<A.Core.PermissionModule.Model.Permission, A.Core.PermissionModule.Model.SearchObjects.PermissionSearchObject, A.Core.PermissionModule.Model.SearchObjects.PermissionAdditionalSearchRequestData>
		{
		
		}
		[DefaultServiceBehaviour(DefaultImplementationEnum.EntityFramework, "roles")]
		public partial interface IRoleService : IReadService<A.Core.PermissionModule.Model.Role, A.Core.PermissionModule.Model.SearchObjects.RoleSearchObject, A.Core.PermissionModule.Model.SearchObjects.RoleAdditionalSearchRequestData>
		{
		
		}
		[DefaultServiceBehaviour(DefaultImplementationEnum.EntityFramework, "rolesPermissions")]
		public partial interface IRolePermissionService : IReadService<A.Core.PermissionModule.Model.RolePermission, A.Core.PermissionModule.Model.SearchObjects.RolePermissionSearchObject, A.Core.PermissionModule.Model.SearchObjects.RolePermissionAdditionalSearchRequestData>
		{
		
		}

}

#endregion

