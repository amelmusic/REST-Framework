using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Validation
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

        public void StopIfHasErrors()
        {
            if (HasErrors)
            {
                throw new ValidationException(this);
            }
        }

        public ValidationResultItem When(bool expr)
        {
            ValidationResultItem item = new ValidationResultItem();
            item.Level = ValidationResultLevelEnum.Info;
            item.AllowChaining = expr;
            if (expr)
            {
                this.ResultList.Add(item);
            }
            return item;
        }

        public ValidationResultItem When(Func<bool> expr)
        {
            return When(expr());
        }
    }
}
