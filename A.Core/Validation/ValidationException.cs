using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Validation
{
    [Serializable]
    public class ValidationException : ApplicationException
    {
        public ValidationException(ValidationResult result) :
            base("Validation exception")
        {
            ValidationResult = result;
        }
        public ValidationResult ValidationResult { get; set; }
        public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
