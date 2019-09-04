using System;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace X.Core.Interface
{
    /// <summary>
    /// Holds data for current request
    /// </summary>
    public interface IActionContext
    {
        ILifetimeScope CurrentContainer { get; set; }
        Dictionary<string, object> Data { get; set; }
    }
}
