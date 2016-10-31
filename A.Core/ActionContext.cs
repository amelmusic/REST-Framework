using A.Core.Interface;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core
{
    public class ActionContext : IActionContext
    {
        public IUnityContainer CurrentContainer { get; set; }
        public ActionContext()
        {
            Data = new Dictionary<string, object>();
        }
        public Dictionary<string, object> Data { get; set; }
    }
}
