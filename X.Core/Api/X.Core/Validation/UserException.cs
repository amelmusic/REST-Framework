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
        public UserException(string message)
            : base(message)
        {

        }
    }
}
