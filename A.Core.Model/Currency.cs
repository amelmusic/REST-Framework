using A.Core.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    [Entity]
    public partial class Currency
    {
        public Currency()
        {

        }

        [Key]
        [Filter(FilterEnum.Equal | FilterEnum.List | FilterEnum.GreatherThan)]
        [RequestField("Insert")]
        public string CurrencyCode { get; set; }
        [RequestField("Insert", "[Required][MinLength(10)][Range(10,100,ErrorMessageResourceName=\"DD\")]")]
        [Filter(FilterEnum.GreatherThan)]
        public string Name { get; set; }
        public System.DateTime ModifiedDate { get; set; }

        [LazyLoading(true)]
        public Address Addr { get; set; }
    }
}
