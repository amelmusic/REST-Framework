using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Model
{
    public class PermissionCheckRequest
    {
        public string Permission { get; set; }
        public string OperationType { get; set; }
    }
}
