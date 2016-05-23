using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Validation
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ResultList = new List<ValidationResultItem>();
        }

        public ICollection<ValidationResultItem> ResultList { get; set; }
        public bool HasErrors { get { return ResultList.Any(x => x.Level == ValidationResultLevelEnum.Error); } }
        public bool HasWarnings { get { return ResultList.Any(x => x.Level == ValidationResultLevelEnum.Warning); } }
    }
}
