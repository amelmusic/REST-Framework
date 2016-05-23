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
    public partial class Product
    {
        [Key]
        public int Id { get; set; }
        [Filter(FilterEnum.GreatherThanOrEqual)]
        [RequestField("Insert")]
        [RequestField("Update")]
        public string Name { get; set; }
        [Filter(FilterEnum.Equal | FilterEnum.List)]
        public string Number { get; set; }
        [RequestField("Insert")]
        public int ProductGroupId { get; set; }
        [RequestField("Insert")]
        [Filter(FilterEnum.GreatherThanOrEqual | FilterEnum.LowerThanOrEqual)]
        public decimal? ListPrice { get; set; }
        [RequestField("Insert")]
        public decimal? Size { get; set; }
        [RequestField("Insert")]
        [Filter(FilterEnum.GreatherThanOrEqual | FilterEnum.LowerThanOrEqual)]
        public decimal Weight { get; set; }

        [LazyLoading]
        public ProductGroup ProductGroup { get; set; }
    }

    public partial class ProductGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
