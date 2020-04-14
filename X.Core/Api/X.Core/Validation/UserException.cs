using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X.Core.Validation
{
    [Serializable]
    public class UserException : ApplicationException
    {
        public string Code { get; set; }
        public UserException(string message, string code = null)
            : base(message)
        {
            Code = code;
        }
    }
}
