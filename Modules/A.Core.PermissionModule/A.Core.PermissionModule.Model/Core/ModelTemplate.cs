















#region A_Core generated code
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//Requests section
namespace A.Core.PermissionModule.Model.Requests
{
	public partial class __IGNORE_CLASS { }
					
					
					
	
		//Creating requests for state machines

	
}

namespace A.Core.PermissionModule.Model.SearchObjects
{
	public partial class __IGNORE_CLASS { }
							//A.Core.PermissionModule.Model.Permission
				
	public partial class PermissionSearchObject : A.Core.Model.BaseSearchObject<PermissionAdditionalSearchRequestData>
{
	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.String Name { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThanOrEqual, false)]
	public virtual System.String NameGTE { get; set; }
	

	protected System.Collections.Generic.IList<System.String> mNameList = new System.Collections.Generic.List<System.String>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.String> NameList { get {return mNameList;} set { mNameList = value; }}
	

	//PROP:boolean
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Boolean? IsAllowed { get; set; }
	

	[Key]//nofilter
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Int32? Id { get; set; }
	

}
public partial class PermissionAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
}

							//A.Core.PermissionModule.Model.Role
				
	public partial class RoleSearchObject : A.Core.Model.BaseSearchObject<RoleAdditionalSearchRequestData>
{
	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.String Name { get; set; }
	

	protected System.Collections.Generic.IList<System.String> mNameList = new System.Collections.Generic.List<System.String>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.String> NameList { get {return mNameList;} set { mNameList = value; }}
	

	[Key]//nofilter
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Int32? Id { get; set; }
	

}
public partial class RoleAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
	//A.Core.Attributes.LazyLoadingAttribute
	protected bool? mIsRolePermissionsLoadingEnabled = false;
	[A.Core.Attributes.LazyLoading(false)]
	public virtual bool? IsRolePermissionsLoadingEnabled { get { return  mIsRolePermissionsLoadingEnabled; } set { mIsRolePermissionsLoadingEnabled = value; } }
	

}

							//A.Core.PermissionModule.Model.RolePermission
				
	public partial class RolePermissionSearchObject : A.Core.Model.BaseSearchObject<RolePermissionAdditionalSearchRequestData>
{
	[Key]//nofilter
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Int32? Id { get; set; }
	

}
public partial class RolePermissionAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
	//A.Core.Attributes.LazyLoadingAttribute
	protected bool? mIsPermissionLoadingEnabled = false;
	[A.Core.Attributes.LazyLoading(false)]
	public virtual bool? IsPermissionLoadingEnabled { get { return  mIsPermissionLoadingEnabled; } set { mIsPermissionLoadingEnabled = value; } }
	

	//A.Core.Attributes.LazyLoadingAttribute
	protected bool? mIsRoleLoadingEnabled = false;
	[A.Core.Attributes.LazyLoading(false)]
	public virtual bool? IsRoleLoadingEnabled { get { return  mIsRoleLoadingEnabled; } set { mIsRoleLoadingEnabled = value; } }
	

}

		
}

#endregion

