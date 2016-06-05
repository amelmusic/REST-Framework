using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    [StateMachine("AddressStateMachine", "A.Core.Model.AddressStateMachineEnum", "StateId")]
    [Entity]
    public partial class Address
    {
        public Address()
        {
            rowguid = Guid.NewGuid();
            ModifiedDate = DateTime.Now;
        }
        [Key]
        [Filter(FilterEnum.Equal | FilterEnum.List)]
        public int AddressID { get; set; }
        [RequestField("Insert", "[Required]")]
        [RequestField("Update")]
        [RequestField("Start")]
        
        public string AddressLine1 { get; set; }
        [RequestField("Insert")]
        [RequestField("Update")]
        public string AddressLine2 { get; set; }

        [RequestField("Insert")]
        [RequestField("Update")]
        [Filter(FilterEnum.Equal | FilterEnum.NotEqual | FilterEnum.GreatherThanOrEqual | FilterEnum.List)]
        public string City { get; set; }

        [RequestField("Insert")]
        [Filter(FilterEnum.Equal | FilterEnum.NotEqual | FilterEnum.GreatherThanOrEqual | FilterEnum.List)]
        public int StateProvinceID { get; set; }
        
        [RequestField("Insert")]
        public string PostalCode { get; set; }
        //public System.Data.Entity.Spatial.DbGeography SpatialLocation { get; set; }
        public System.Guid rowguid { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        public AddressStateMachineEnum StateId { get; set; }
    }

    public enum AddressStateMachineEnum
    {
        Init = 0,
        Verified = 1,
        Invalid = 2,
        Entered = 3
    }
}
