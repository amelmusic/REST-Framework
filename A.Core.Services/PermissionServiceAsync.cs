using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A.Core.Model;
using A.Core.Model.SearchObjects;
using A.Core.Interceptors;

namespace A.Core.Services
{
    public partial class PermissionService
    {


        [Cache(ExpirationType.ExpiresIn, "00:00:15", isUserContextAware: true)]
        public virtual int GetCached()
        {
            return 255;
        }
    }
}
