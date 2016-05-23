using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Validation
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(ValidationResult result) :
            base("Validation exception")
        {
            ValidationResult = result;
        }
        public ValidationResult ValidationResult { get; set; }
    }
}
