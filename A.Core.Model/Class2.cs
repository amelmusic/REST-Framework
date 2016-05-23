//using A.Core.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace A.Core.Model
//{
//    [Entity]
//    public class Contract
//    {
//        //[Key]
//        public int Id { get; set; }

//        //[Filter(EQ|NEQ)]
//        [Filter(FilterEnum.Equal | FilterEnum.NotEqual | FilterEnum.GreatherThanOrEqual | FilterEnum.List)]
//        public string ContractNumber { get; set; }
//        [RequestField("Insert")]
//        [RequestField("UpdateState")]
//        public string Description { get; set; }
//        [Filter(FilterEnum.Equal | FilterEnum.NotEqual | FilterEnum.GreatherThanOrEqual | FilterEnum.GreatherThan | FilterEnum.LowerThan | FilterEnum.List)]
//        [RequestField("UpdateState")]
//        public int StatusId { get; set; }

//        //[LazyLoading(false)]
//        //public ContractType ContractType { get; set; }
//    }
//}
