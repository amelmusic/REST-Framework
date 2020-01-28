using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using X.Core.Interface;

namespace X.Core
{
    public class ActionContext : IActionContext
    {
        public ILifetimeScope CurrentContainer { get; set; }
        public ActionContext()
        {
            Data = new Dictionary<string, object>();
        }
        public Dictionary<string, object> Data { get; set; }
    }

    public static class ActionContextExt
    {
        public static string UserId(this IActionContext context)
        {
            if (context.Data.TryGetValue("UserId", out object id))
            {
                return id.ToString();
            }
            return null;
        }
    }

}
