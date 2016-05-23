













using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
//Requests section
namespace A.Core.Model.Requests
{
														//A.Core.Model.Product
				public partial class ProductInsertRequest
{

	
	public System.String Name { get; set; }

	public System.Int32 ProductGroupId { get; set; }

	public System.Nullable<System.Decimal> ListPrice { get; set; }

	public System.Nullable<System.Decimal> Size { get; set; }

	public System.Decimal Weight { get; set; }
}

											//A.Core.Model.Product
				public partial class ProductUpdateRequest
{

	
	public System.String Name { get; set; }
}

							
														//A.Core.Model.Currency
				public partial class CurrencyInsertRequest
{

	
	public System.String CurrencyCode { get; set; }
	[Required][MinLength(10)][Range(10,100,ErrorMessageResourceName="DD")]
	public System.String Name { get; set; }
}

							
														//A.Core.Model.Address
				public partial class AddressInsertRequest
{

	[Required]
	public System.String AddressLine1 { get; set; }

	public System.String AddressLine2 { get; set; }

	public System.String City { get; set; }

	public System.Int32 StateProvinceID { get; set; }

	public System.String PostalCode { get; set; }
}

											//A.Core.Model.Address
				public partial class AddressUpdateRequest
{

	
	public System.String AddressLine1 { get; set; }

	public System.String AddressLine2 { get; set; }

	public System.String City { get; set; }
}

							
	
}

namespace A.Core.Model.SearchObjects
{
							//A.Core.Model.Product
				
	public partial class ProductSearchObject : A.Core.Model.BaseSearchObject<ProductAdditionalSearchRequestData>
{
	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThanOrEqual, false)]
	public virtual System.String NameGTE { get; set; }
	

	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.String Number { get; set; }
	

	protected System.Collections.Generic.IList<System.String> mNumberList = new System.Collections.Generic.List<System.String>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.String> NumberList { get {return mNumberList;} set { mNumberList = value; }}
	

	//PROP:nullable
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThanOrEqual, false)]
	public virtual System.Nullable<System.Decimal> ListPriceGTE { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.LowerThanOrEqual, false)]
	public virtual System.Nullable<System.Decimal> ListPriceLTE { get; set; }
	

	//PROP:decimal
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThanOrEqual, false)]
	public virtual System.Decimal? WeightGTE { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.LowerThanOrEqual, false)]
	public virtual System.Decimal? WeightLTE { get; set; }
	

}
public partial class ProductAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
	//A.Core.Attributes.LazyLoadingAttribute
	protected bool? mIsProductGroupLoadingEnabled = false;
	[A.Core.Attributes.LazyLoading(false)]
	public virtual bool? IsProductGroupLoadingEnabled { get { return  mIsProductGroupLoadingEnabled; } set { mIsProductGroupLoadingEnabled = value; } }
	

}

							//A.Core.Model.Currency
				
	public partial class CurrencySearchObject : A.Core.Model.BaseSearchObject<CurrencyAdditionalSearchRequestData>
{
	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.String CurrencyCode { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThan, false)]
	public virtual System.String CurrencyCodeGT { get; set; }
	

	protected System.Collections.Generic.IList<System.String> mCurrencyCodeList = new System.Collections.Generic.List<System.String>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.String> CurrencyCodeList { get {return mCurrencyCodeList;} set { mCurrencyCodeList = value; }}
	

	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThan, false)]
	public virtual System.String NameGT { get; set; }
	

}
public partial class CurrencyAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
	//A.Core.Attributes.LazyLoadingAttribute
	protected bool? mIsAddrLoadingEnabled = true;
	[A.Core.Attributes.LazyLoading(true)]
	public virtual bool? IsAddrLoadingEnabled { get { return  mIsAddrLoadingEnabled; } set { mIsAddrLoadingEnabled = value; } }
	

}

							//A.Core.Model.Address
				
	public partial class AddressSearchObject : A.Core.Model.BaseSearchObject<AddressAdditionalSearchRequestData>
{
	//PROP:int32
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Int32? AddressID { get; set; }
	

	protected System.Collections.Generic.IList<System.Int32> mAddressIDList = new System.Collections.Generic.List<System.Int32>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.Int32> AddressIDList { get {return mAddressIDList;} set { mAddressIDList = value; }}
	

	//PROP:string
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.String City { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.NotEqual, false)]
	public virtual System.String CityNE { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThanOrEqual, false)]
	public virtual System.String CityGTE { get; set; }
	

	protected System.Collections.Generic.IList<System.String> mCityList = new System.Collections.Generic.List<System.String>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.String> CityList { get {return mCityList;} set { mCityList = value; }}
	

	//PROP:int32
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.Equal, false)]
	public virtual System.Int32? StateProvinceID { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.NotEqual, false)]
	public virtual System.Int32? StateProvinceIDNE { get; set; }
	

	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.GreatherThanOrEqual, false)]
	public virtual System.Int32? StateProvinceIDGTE { get; set; }
	

	protected System.Collections.Generic.IList<System.Int32> mStateProvinceIDList = new System.Collections.Generic.List<System.Int32>();
	[A.Core.Attributes.Filter(A.Core.Attributes.FilterEnum.List, false)]
	public virtual System.Collections.Generic.IList<System.Int32> StateProvinceIDList { get {return mStateProvinceIDList;} set { mStateProvinceIDList = value; }}
	

}
public partial class AddressAdditionalSearchRequestData :  A.Core.Model.BaseAdditionalSearchRequestData
{
}

		
}


