using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A.Core.Interface
{
    /// <summary>
    /// Holds data for current request
    /// </summary>
    public interface IActionContext
    {
        IUnityContainer CurrentContainer { get; set; }
        Dictionary<string,object> Data { get; set; }
    }
}
