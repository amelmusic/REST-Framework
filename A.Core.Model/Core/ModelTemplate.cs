















#region A_Core generated code
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//Requests section
namespace A.Core.Model.Requests
{
	public partial class __IGNORE_CLASS { }
														//A.Core.Model.Permission
				public partial class PermissionInsertRequest
{

	
	public System.String Description { get; set; }
}

											//A.Core.Model.Permission
				public partial class PermissionUpdateRequest
{

	
	public System.String Description { get; set; }
}

							
	
		//Creating requests for state machines

	
}

namespace A.Core.Model.SearchObjects
{
	public partial class __IGNORE_CLASS { }
							//A.Core.Model.Permission
				
	public partial class PermissionSearchObject : A.Core.Model.BaseSearchObject<PermissionAdditionalSearchRequestData>
{
	[Key]//nofilter
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Int32? Id { get; set; }
	

}
public partial class PermissionAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
}

		
}

#endregion

